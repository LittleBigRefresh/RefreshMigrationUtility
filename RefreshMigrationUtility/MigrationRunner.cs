using System.Collections.Concurrent;
using System.Diagnostics;
using Realms;
using Refresh.Database;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations;

namespace RefreshMigrationUtility;

public class MigrationRunner
{
    private readonly ConcurrentQueue<MigrationTask> _taskQueue = [];
    private readonly List<MigrationTask> _tasks = [];
    private readonly MigrationConfig _config;

    public IReadOnlyList<MigrationTask> Tasks => this._tasks.AsReadOnly();
    public bool Complete => _tasks.All(t => t.Complete);

    public MigrationRunner(MigrationConfig config)
    {
        this._config = config;
    }

    private void MigrateLoop()
    {
        using RealmDatabaseContext realm = new(this._config.RealmFilePath);
        using GameDatabaseContext ef = new(this._config.PostgresConnectionString);
        
        while (_taskQueue.TryDequeue(out MigrationTask? task) && !task.Complete)
        {
            Debug.Assert(task != null);
            if (!IsTaskReady(task))
            {
                _taskQueue.Enqueue(task);
                continue;
            }
            
            task.MigrateChunk(realm, ef);
            
            if(!task.Complete)
                _taskQueue.Enqueue(task);
        }
    }

    public void StartAllTasks()
    {
        MigrationTask.MaxMigrationTypeLength = _tasks.Select(t => t.MigrationType.Length).Max();
        
        int nproc = Environment.ProcessorCount;
        // nproc = 1;

        // Thread[] threads = new Thread[nproc];
        for (int i = 0; i < nproc; i++)
        {
            Thread thread = new(MigrateLoop)
            {
                Name = $"Migration Thread {i}"
            };

            // threads[i] = thread;
            thread.Start();
        }
        
        // foreach (Thread thread in threads)
        // {
            // thread.Join();
        // }
    }

    private void AddTask(MigrationTask task)
    {
        this._taskQueue.Enqueue(task);
        this._tasks.Add(task);
    }

    public void AddMigrator<TMigrationTask>() where TMigrationTask : MigrationTask
    {
        using RealmDatabaseContext realm = new(this._config.RealmFilePath);
        using GameDatabaseContext ef = new(this._config.PostgresConnectionString);

        TMigrationTask? task = (TMigrationTask?)Activator.CreateInstance(typeof(TMigrationTask), realm, ef);
        Debug.Assert(task != null);
        
        AddTask(task);
    }

    public void AddSimpleMigrator<TOld, TNew>() where TOld : IRealmObject where TNew : class, new()
        => AddMigrator<SimpleMigrator<TOld, TNew>>();
    
    public void AddBackfiller<TMigrationTask>() where TMigrationTask : MigrationTask, IBackfiller
    {
        TMigrationTask? task = Activator.CreateInstance<TMigrationTask>();
        Debug.Assert(task != null);

        int total = this._tasks.First(t => t.ProvidesType == task.SourceType).Total;
        task.Total = total;
        
        AddTask(task);
    }

    private bool IsTaskReady(MigrationTask task)
    {
        if (!task.NeedsTypes.Any())
            return true;
        
        foreach (Type neededType in task.NeedsTypes)
        {
            foreach (MigrationTask otherTask in _tasks)
            {
                if(task == otherTask)
                    continue;
                
                if(!otherTask.Complete)
                    continue;

                if (otherTask.ProvidesType == neededType)
                    return true;
            }
        }

        return false;
    }
}