using Refresh.Database;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations.Migrators;

public class VerifiedIpMigrator : UserDependentMigrator<RealmGameUserVerifiedIpRelation, GameUserVerifiedIpRelation>
{
    public VerifiedIpMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override bool IsOldValid(GameDatabaseContext ef, RealmGameUserVerifiedIpRelation old)
    {
        return ef.GameUsers.Select(u => u.UserId).Contains(old.User.UserId);
    }

    protected override GameUserVerifiedIpRelation Map(GameDatabaseContext ef, RealmGameUserVerifiedIpRelation old)
    {
        return new GameUserVerifiedIpRelation
        {
            UserId = old.User.UserId,
            IpAddress = old.IpAddress,
            VerifiedAt = old.VerifiedAt
        };
    }
}