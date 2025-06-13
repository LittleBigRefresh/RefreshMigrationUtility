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
    public override int Total { get; set; }
    public override int Progress { get; set; }

    public override int Skipped { get; set; }
    public override int Migrated { get; set; }

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

    public override void MigrateChunk(RealmDatabaseContext realm, GameDatabaseContext ef)
    {
        IEnumerable<TOld> chunk = realm.All<TOld>()
            .AsEnumerable()
            .Skip(Progress)
            .Take(TakeSize);

        DbSet<TNew> set = ef.Set<TNew>();

        foreach (TOld old in chunk)
        {
            if (IsOldValid(ef, old))
            {
                TNew mapped = Map(ef, old);
                set.Add(mapped);
                Migrated++;
            }
            else
            {
                Skipped++;
            }

            Progress++;
        }

        ef.SaveChanges();
    }

    public override string MigrationType { get; } = $"Migrate {typeof(TNew).Name}{(typeof(TNew).Name.EndsWith('s') ? "" : "s")} from Realm";
    
    public override Type SourceType => typeof(TOld);
    public override Type ProvidesType => typeof(TNew);
}