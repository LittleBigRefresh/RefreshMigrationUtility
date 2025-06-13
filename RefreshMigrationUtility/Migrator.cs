using Microsoft.EntityFrameworkCore;
using Realms;
using Refresh.Database;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility;

public abstract class Migrator<TOld, TNew> : Migrator
    where TOld : IRealmObject
    where TNew : class
{
    protected override int Total { get; set; }
    protected override int Progress { get; set; }

    protected Migrator(RealmDatabaseContext realm, GameDatabaseContext ef)
    {
        this.Total = realm.All<TOld>().Count();
        if (ef.Set<TNew>().Any())
        {
            throw new InvalidOperationException($"Cannot start migration for {MigrationType} because the new database already has objects");
        }
    }

    public abstract TNew Map(TOld old);

    public override void MigrateChunk(RealmDatabaseContext realm, GameDatabaseContext ef)
    {
        IEnumerable<TOld> chunk = realm.All<TOld>()
            .AsEnumerable()
            .Skip(Progress)
            .Take(100)
            .ToArray();

        DbSet<TNew> set = ef.Set<TNew>();

        foreach (TOld old in chunk)
        {
            TNew mapped = Map(old);
            set.Add(mapped);
            Progress++;
        }

        ef.SaveChanges();
    }

    public override string MigrationType => $"{typeof(TOld).Name}->{typeof(TNew).Name}";
}

public abstract class Migrator
{
    public abstract void MigrateChunk(RealmDatabaseContext realm, GameDatabaseContext ef);
    public abstract string MigrationType { get; }

    protected abstract int Progress { get; set; }
    protected abstract int Total { get; set; }

    public bool Complete => this.Progress >= this.Total;
    
    public override string ToString()
    {
        return $"{MigrationType} ({this.Progress}/{this.Total})";
    }
}