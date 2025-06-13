using Refresh.Database;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations.Migrators;

public class FavouriteLevelMigrator : UserAndLevelDependentMigrator<RealmFavouriteLevelRelation, FavouriteLevelRelation>
{
    public FavouriteLevelMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override FavouriteLevelRelation Map(GameDatabaseContext ef, RealmFavouriteLevelRelation old)
    {
        return new FavouriteLevelRelation()
        {
            LevelId = old.Level.LevelId,
            Timestamp = old.Timestamp,
            UserId = old.User.UserId,
        };
    }
}