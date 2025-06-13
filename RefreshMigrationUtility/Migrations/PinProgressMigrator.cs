using Refresh.Database;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations;

public class PinProgressMigrator : UserDependentMigrator<RealmPinProgressRelation, PinProgressRelation>
{
    public PinProgressMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    public override PinProgressRelation Map(GameDatabaseContext ef, RealmPinProgressRelation old)
    {
        return new PinProgressRelation()
        {
            PublisherId = old.Publisher.UserId,
            FirstPublished = old.FirstPublished,
            IsBeta = old.IsBeta,
            LastUpdated = old.LastUpdated,
            PinId = old.PinId,
            Progress = old.Progress,
        };
    }
}