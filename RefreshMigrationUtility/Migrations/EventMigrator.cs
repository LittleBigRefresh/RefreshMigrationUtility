using Refresh.Database;
using Refresh.Database.Models.Activity;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations;

public class EventMigrator : UserDependentMigrator<RealmEvent, Event>
{
    public EventMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    public override Event Map(GameDatabaseContext ef, RealmEvent old)
    {
        return new Event
        {
            User = ef.GameUsers.Find(old.User?.UserId),
            _EventType = old._EventType,
            _StoredDataType = old._StoredDataType,
            EventId = old.EventId,
            EventType = old.EventType,
            IsPrivate = old.IsPrivate,
            StoredDataType = old.StoredDataType,
            StoredObjectId = old.StoredObjectId,
            StoredSequentialId = old.StoredSequentialId,
            Timestamp = old.Timestamp
        };
    }
}