using Microsoft.EntityFrameworkCore;
using Refresh.Database;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations;

public class FavouriteUserMigrator : UserDependentMigrator<RealmFavouriteUserRelation, FavouriteUserRelation>
{
    public FavouriteUserMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    public override void MigrateChunk(RealmDatabaseContext realm, GameDatabaseContext ef)
    {
        IEnumerable<RealmFavouriteUserRelation> chunk = realm.All<RealmFavouriteUserRelation>()
            .AsEnumerable()
            .Skip(Progress)
            .Take(TakeSize);

        DbSet<FavouriteUserRelation> set = ef.Set<FavouriteUserRelation>();

        foreach (RealmFavouriteUserRelation old in chunk)
        {
            // some of these are apparently null in realm so we have to check here
            if (ef.GameUsers.Select(u => u.UserId).Contains(old.UserToFavourite.UserId))
            {
                FavouriteUserRelation mapped = Map(ef, old);
                set.Add(mapped);
            }

            Progress++;
        }

        ef.SaveChanges();
    }

    public override FavouriteUserRelation Map(GameDatabaseContext ef, RealmFavouriteUserRelation old)
    {
        return new FavouriteUserRelation
        {
            UserFavouritingId = old.UserFavouriting.UserId,
            UserToFavouriteId = old.UserToFavourite.UserId,
        };
    }
}