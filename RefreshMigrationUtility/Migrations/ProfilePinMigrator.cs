using Refresh.Database;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations;

public class ProfilePinMigrator : UserDependentMigrator<RealmProfilePinRelation, ProfilePinRelation>
{
    public ProfilePinMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override bool IsOldValid(GameDatabaseContext ef, RealmProfilePinRelation old)
    {
        return ef.ProfilePinRelations.Any(r => r.PinId == old.PinId && r.PublisherId == old.Publisher.UserId && r.Index == old.Index);
    }

    protected override ProfilePinRelation Map(GameDatabaseContext ef, RealmProfilePinRelation old)
    {
        return new ProfilePinRelation
        {
            PublisherId = old.Publisher.UserId,
            Timestamp = old.Timestamp,
            Game = old.Game,
            _Game = old._Game,
            Index = old.Index,
            PinId = old.PinId
        };
    }
}