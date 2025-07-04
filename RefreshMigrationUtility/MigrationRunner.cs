﻿using System.Collections.Concurrent;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Realms;
using Refresh.Database;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Interface;
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
        
        while (!this.Complete)
        {
            if (!_taskQueue.TryDequeue(out MigrationTask? task))
            {
                Thread.Sleep(20);
                continue;
            }
            
            ef.ChangeTracker.Clear();

            Debug.Assert(task != null);
            if (!IsTaskReady(task))
            {
                _taskQueue.Enqueue(task);
                Thread.Sleep(20);
                continue;
            }
            
            // remove from queue if complete
            if (task.Complete)
            {
                // we just completed a task, relieve some GC pressure
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Aggressive, true);
                continue;
            }
            

            MigrationChunk chunk = task.GetChunk(realm);
            _taskQueue.Enqueue(task); // allow other threads to pick up more chunks

            Interlocked.Increment(ref task.ThreadsAccessing);
            using (IDbContextTransaction transaction = ef.Database.BeginTransaction())
            {
                task.MigrateChunk(chunk, ef);
                transaction.Commit();
            }
            Interlocked.Decrement(ref task.ThreadsAccessing);
            
            // relieve a small amount of pressure since we completed a chunk
            GC.Collect(1, GCCollectionMode.Forced, false);

            // Debug.Assert(!ef.ChangeTracker.HasChanges());
        }
    }

    public void StartAllTasks()
    {
        MigrationTask.MaxMigrationTypeLength = _tasks.Select(t => t.MigrationType.Length).Max();
        
        int nproc = Environment.ProcessorCount;
        // nproc = 1;

        for (int i = 0; i < nproc; i++)
        {
            Thread thread = new(MigrateLoop)
            {
                Name = $"Migration Thread {i}"
            };

            thread.Start();
        }
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

        return task.NeedsTypes.All(neededType =>
            _tasks.Any(otherTask =>
                otherTask != task &&
                otherTask.Complete &&
                otherTask.ProvidesType == neededType
            )
        );
    }

#pragma warning disable EF1002 // warning for sql injection. all strings are results of things at compile-time so its fine
    public void RecalculateSequences()
    {
        using GameDatabaseContext ef = new(this._config.PostgresConnectionString);
        
        foreach (MigrationTask task in this._tasks)
        {
            if (task is not INeedsSequenceRecalculation seq)
                continue;

            string[] split = seq.SequenceName.Split('_');

            string table = split[0];
            string key = split[1];

            int highestId = ef.Database.SqlQueryRaw<int>($"SELECT MAX(\"{key}\") as \"Value\" FROM \"{table}\"").First();
            highestId++;

            ef.Database.ExecuteSqlRaw($"ALTER SEQUENCE \"{seq.SequenceName}\" RESTART WITH {highestId};");
            
            Console.WriteLine($"Set {seq.SequenceName} to {highestId}");
        }
#pragma warning restore EF1002
    }
}