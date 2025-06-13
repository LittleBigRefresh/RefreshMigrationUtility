using Refresh.Database;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility;

public abstract class Backfiller<TSource, TProvides> : MigrationTask, IBackfiller where TSource : class
{
    public override void MigrateChunk(RealmDatabaseContext realm, GameDatabaseContext ef)
    {
        TSource[] set = ef.Set<TSource>()
            .Skip(Progress)
            .Take(TakeSize)
            .ToArray(); // need a ToArray here since that would normally cause a double query execution

        foreach (TSource src in set)
        {
            Backfill(ef, src);
            Progress++;
        }

        ef.SaveChanges();
    }

    protected abstract void Backfill(GameDatabaseContext ef, TSource src);

    public override string MigrationType => $"{typeof(TSource).Name} <- {typeof(TProvides).Name}";
    public override int Progress { get; set; }
    protected internal override int Total { get; set; }

    public override Type SourceType => typeof(TSource);
    public override Type ProvidesType => typeof(TProvides);

    public override IEnumerable<Type> NeedsTypes { get; } = [typeof(TSource)];
}