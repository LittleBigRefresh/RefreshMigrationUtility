using Microsoft.EntityFrameworkCore;
using Refresh.Database;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility.Migrations;

public class AssetDependencyMigrator : Migrator<RealmAssetDependencyRelation, AssetDependencyRelation>
{
    public AssetDependencyMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override bool IsOldValid(GameDatabaseContext ef, RealmAssetDependencyRelation old)
    {
        return ef.AssetDependencyRelations.Any(r => r.Dependency == old.Dependency && r.Dependent == old.Dependent);
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