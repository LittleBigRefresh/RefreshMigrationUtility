using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Realms;
using Refresh.Database;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility;

[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature, ImplicitUseTargetFlags.WithInheritors)]
public abstract class Migrator<TOld, TNew> : MigrationTask
    where TOld : IRealmObject
    where TNew : class
{
    protected internal override int Total { get; set; }
    protected override int Progress { get; set; }

    protected Migrator(RealmDatabaseContext realm, GameDatabaseContext ef)
    {
        this.Total = realm.All<TOld>().Count();
        if (ef.Set<TNew>().Any())
        {
            throw new InvalidOperationException($"Cannot start migration for {MigrationType} because the new database already has objects");
        }
    }

    public abstract TNew Map(GameDatabaseContext ef, TOld old);

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
            TNew mapped = Map(ef, old);
            set.Add(mapped);
            Progress++;
        }

        ef.SaveChanges();
    }

    public override string MigrationType => $"{typeof(TOld).Name}->{typeof(TNew).Name}";
    
    public override Type SourceType => typeof(TOld);
    public override Type ProvidesType => typeof(TNew);
}