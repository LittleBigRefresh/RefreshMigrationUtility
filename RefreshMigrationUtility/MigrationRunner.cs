using System.Collections.Concurrent;
using System.Diagnostics;
using Realms;
using Refresh.Database;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations;

namespace RefreshMigrationUtility;

public class MigrationRunner
{
    private readonly ConcurrentQueue<Migrator> _taskQueue = [];
    private readonly List<Migrator> _tasks = [];
    private readonly MigrationConfig _config;

    public MigrationRunner(MigrationConfig config)
    {
        this._config = config;
    }

    private void MigrateLoop()
    {
        using RealmDatabaseContext realm = new(this._config.RealmFilePath);
        using GameDatabaseContext ef = new(this._config.PostgresConnectionString);
        
        while (_taskQueue.TryDequeue(out Migrator? task) && !task.Complete)
        {
            Debug.Assert(task != null);
            task.MigrateChunk(realm, ef);
            Console.WriteLine(task);
        }
    }

    public void RunAllTasks()
    {
        int nproc = Environment.ProcessorCount;

        Thread[] threads = new Thread[nproc];
        for (int i = 0; i < nproc; i++)
        {
            Thread thread = new(MigrateLoop)
            {
                Name = $"Migration Thread {i}"
            };

            threads[i] = thread;
            thread.Start();
        }
        
        foreach (Thread thread in threads)
        {
            thread.Join();
        }
    }

    private void AddTask(Migrator task)
    {
        this._taskQueue.Enqueue(task);
        this._tasks.Add(task);
    }

    public void AddTask<TMigrationTask>() where TMigrationTask : Migrator
    {
        using RealmDatabaseContext realm = new(this._config.RealmFilePath);
        using GameDatabaseContext ef = new(this._config.PostgresConnectionString);

        TMigrationTask? task = (TMigrationTask?)Activator.CreateInstance(typeof(TMigrationTask), realm, ef);
        Debug.Assert(task != null);
        
        AddTask(task);
    }

    public void AddSimpleTask<TOld, TNew>() where TOld : IRealmObject where TNew : class, new()
        => AddTask<SimpleMigrator<TOld, TNew>>();
}