using Microsoft.EntityFrameworkCore;
using Refresh.Database;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations;

public class VerifiedIpMigrator : UserDependentMigrator<RealmGameUserVerifiedIpRelation, GameUserVerifiedIpRelation>
{
    public VerifiedIpMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}
    
    public override void MigrateChunk(RealmDatabaseContext realm, GameDatabaseContext ef)
    {
        IEnumerable<RealmGameUserVerifiedIpRelation> chunk = realm.All<RealmGameUserVerifiedIpRelation>()
            .AsEnumerable()
            .Skip(Progress)
            .Take(TakeSize);

        DbSet<GameUserVerifiedIpRelation> set = ef.Set<GameUserVerifiedIpRelation>();

        foreach (RealmGameUserVerifiedIpRelation old in chunk)
        {
            // some of these are apparently null in realm so we have to check here
            if (ef.GameUsers.Select(u => u.UserId).Contains(old.User.UserId))
            {
                GameUserVerifiedIpRelation mapped = Map(ef, old);
                set.Add(mapped);
            }

            Progress++;
        }

        ef.SaveChanges();
    }

    public override GameUserVerifiedIpRelation Map(GameDatabaseContext ef, RealmGameUserVerifiedIpRelation old)
    {
        return new GameUserVerifiedIpRelation
        {
            UserId = old.User.UserId,
            IpAddress = old.IpAddress,
            VerifiedAt = old.VerifiedAt
        };
    }
}