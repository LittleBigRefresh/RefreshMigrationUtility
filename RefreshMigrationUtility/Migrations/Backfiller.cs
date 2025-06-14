﻿using Refresh.Database;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility.Migrations;

public abstract class Backfiller<TSource, TProvides> : MigrationTask, IBackfiller where TSource : class
{
    public override MigrationChunk GetChunk(RealmDatabaseContext realm) => MigrationChunk.Empty;

    public override void MigrateChunk(MigrationChunk chunk, GameDatabaseContext ef)
    {
        _ = chunk;
        TSource[] set = ef.Set<TSource>()
            .Skip(Progress)
            .Take(ChunkSize)
            .ToArray(); // need a ToArray here since that would normally cause a double query execution

        foreach (TSource src in set)
        {
            if (Backfill(ef, src)) Migrated++;
            else Skipped++;
        }

        ef.SaveChanges();
    }

    protected abstract bool Backfill(GameDatabaseContext ef, TSource src);

    public override string MigrationType => $"Backfill {typeof(TProvides).Name} into {typeof(TSource).Name}";
    public override int Total { get; set; }
    public override int Skipped { get; set; }
    public override int Migrated { get; set; }

    public override Type SourceType => typeof(TSource);
    public override Type ProvidesType => typeof(TProvides);

    public override IEnumerable<Type> NeedsTypes { get; } = [typeof(TSource)];
}