using Microsoft.EntityFrameworkCore;
using Realms;
using Refresh.Database;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility;

public abstract class MigrationTask<TOld, TNew>
    where TOld : IRealmObject
    where TNew : class
{
    private readonly RealmDatabaseContext _realm;
    private readonly GameDatabaseContext _ef;

    private readonly int _total;
    private int _progress;

    public MigrationTask(RealmDatabaseContext realm, GameDatabaseContext ef)
    {
        this._realm = realm;
        this._ef = ef;

        this._total = realm.All<TOld>().Count();
        if (ef.Set<TNew>().Any())
        {
            throw new InvalidOperationException($"Cannot start migration for {MigrationType} because the new database already has objects");
        }
    }

    public abstract TNew Map(TOld old);

    public void MigrateChunk()
    {
        IEnumerable<TOld> chunk = this._realm.All<TOld>()
            .AsEnumerable()
            .Skip(_progress)
            .Take(100)
            .ToArray();

        DbSet<TNew> set = this._ef.Set<TNew>();

        foreach (TOld old in chunk)
        {
            TNew mapped = Map(old);
            set.Add(mapped);
            _progress++;
        }

        this._ef.SaveChanges();
    }

    private static string MigrationType => $"{typeof(TOld).Name}->{typeof(TNew).Name}";

    public override string ToString()
    {
        return $"{MigrationType} ({this._progress}/{this._total})";
    }
}