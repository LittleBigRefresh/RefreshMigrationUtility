using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Realms;
using Refresh.Database;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility.Migrations;

[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature, ImplicitUseTargetFlags.WithInheritors)]
public abstract class Migrator<TOld, TNew> : MigrationTask
    where TOld : IRealmObject
    where TNew : class
{
    public override int Total { get; set; }

    public int Index;

    public override int Skipped
    {
        get => _skipped;
        set => _skipped = value;
    }

    public override int Migrated
    {
        get => _migrated;
        set => _migrated = value;
    }

    private int _skipped;
    private int _migrated;

    protected void OnMigrate() => Interlocked.Increment(ref _migrated);
    protected void OnSkip() => Interlocked.Increment(ref _skipped);

    protected Migrator(RealmDatabaseContext realm, GameDatabaseContext ef)
    {
        this.Total = realm.All<TOld>().Count();
        if (ef.Set<TNew>().Any())
        {
            throw new InvalidOperationException($"Cannot start migration for {MigrationType} because the new database already has objects");
        }
    }

    protected abstract TNew Map(GameDatabaseContext ef, TOld old);
    protected virtual bool IsOldValid(GameDatabaseContext ef, TOld old) => true;

    private readonly Lock _chunkLock = new();

    public override MigrationChunk GetChunk(RealmDatabaseContext realm)
    {
        lock (_chunkLock)
        {
            TOld[] chunk = realm.All<TOld>()
                .AsEnumerable()
                .Skip(Index)
                .Take(ChunkSize)
                .ToArray();

            Interlocked.Add(ref Index, chunk.Length);
            return new MigrationChunk<TOld>(chunk);
        }
    }

    public override void MigrateChunk(MigrationChunk chunk, GameDatabaseContext ef)
    {
        IEnumerable<TOld> oldObjects = ((MigrationChunk<TOld>)chunk).Old;
        DbSet<TNew> set = ef.Set<TNew>();

        foreach (TOld old in oldObjects)
        {
            if (IsOldValid(ef, old))
            {
                TNew mapped = Map(ef, old);
                set.Add(mapped);
                OnMigrate();
            }
            else OnSkip();
        }

        ef.SaveChanges();
    }

    public override string MigrationType { get; } = $"Migrate {typeof(TNew).Name}{(typeof(TNew).Name.EndsWith('s') ? "" : "s")} from Realm";
    
    public override Type SourceType => typeof(TOld);
    public override Type ProvidesType => typeof(TNew);
}