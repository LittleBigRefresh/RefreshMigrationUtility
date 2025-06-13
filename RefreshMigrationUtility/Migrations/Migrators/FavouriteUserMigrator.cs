using Refresh.Database;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations.Migrators;

public class FavouriteUserMigrator : UserDependentMigrator<RealmFavouriteUserRelation, FavouriteUserRelation>
{
    public FavouriteUserMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override bool IsOldValid(GameDatabaseContext ef, RealmFavouriteUserRelation old)
    {
        return ef.GameUsers.Select(u => u.UserId).Contains(old.UserToFavourite.UserId);
    }

    protected override FavouriteUserRelation Map(GameDatabaseContext ef, RealmFavouriteUserRelation old)
    {
        return new FavouriteUserRelation
        {
            UserFavouritingId = old.UserFavouriting.UserId,
            UserToFavouriteId = old.UserToFavourite.UserId,
        };
    }
}