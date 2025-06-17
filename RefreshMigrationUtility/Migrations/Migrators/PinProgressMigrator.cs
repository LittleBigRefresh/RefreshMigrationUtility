using Refresh.Database;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations.Migrators;

public class PinProgressMigrator : UserDependentMigrator<RealmPinProgressRelation, PinProgressRelation>
{
    public PinProgressMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override bool IsOldValid(GameDatabaseContext ef, RealmPinProgressRelation old)
    {
        return old.Realm!.All<RealmPinProgressRelation>()
            .Where(r => r.PinId == old.PinId)
            .Where(r => r.Publisher == old.Publisher)
            .Where(r => r.IsBeta == old.IsBeta)
            .AsEnumerable()
            .Max(r => r.LastUpdated) == old.LastUpdated;
    }

    protected override PinProgressRelation Map(GameDatabaseContext ef, RealmPinProgressRelation old)
    {
        return new PinProgressRelation
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