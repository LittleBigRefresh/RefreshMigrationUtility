using Refresh.Database;
using Refresh.Database.Models.Notifications;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility.Migrations;

public class AnnouncementMigrationTask : MigrationTask<RealmGameAnnouncement, GameAnnouncement>
{
    public AnnouncementMigrationTask(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    public override GameAnnouncement Map(RealmGameAnnouncement old)
    {
        return new GameAnnouncement
        {
            AnnouncementId = old.AnnouncementId,
            CreatedAt = old.CreatedAt,
            Text = old.Text,
            Title = old.Title
        };
    }
}