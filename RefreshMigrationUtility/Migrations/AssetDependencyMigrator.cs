using Microsoft.EntityFrameworkCore;
using Refresh.Database;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility.Migrations;

public class AssetDependencyMigrator : Migrator<RealmAssetDependencyRelation, AssetDependencyRelation>
{
    public AssetDependencyMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    public override void MigrateChunk(MigrationChunk chunk, GameDatabaseContext ef)
    {
        DbSet<AssetDependencyRelation> set = ef.Set<AssetDependencyRelation>();

        IEnumerable<AssetDependencyRelation> oldItems = ((MigrationChunk<RealmAssetDependencyRelation>)chunk)
            .Old
            .Select(old =>
            {
                OnMigrate();
                return Map(ef, old);
            })
            .DistinctBy(x => (x.Dependent, x.Dependency)); // Deduplicate here, after Map()

        foreach (AssetDependencyRelation mapped in oldItems)
        {
            set.Add(mapped);
        }

        ef.SaveChanges();
    }

    protected override AssetDependencyRelation Map(GameDatabaseContext ef, RealmAssetDependencyRelation old)
    {
        return new AssetDependencyRelation
        {
            Dependency = old.Dependency,
            Dependent = old.Dependent,
        };
    }
}