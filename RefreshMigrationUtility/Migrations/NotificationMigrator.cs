using Refresh.Database;
using Refresh.Database.Models.Notifications;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations;

public class NotificationMigrator : UserDependentMigrator<RealmGameNotification, GameNotification>
{
    public NotificationMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override GameNotification Map(GameDatabaseContext ef, RealmGameNotification old)
    {
        return new GameNotification
        {
            Title = old.Title,
            CreatedAt = old.CreatedAt,
            Text = old.Text,
            FontAwesomeIcon = old.FontAwesomeIcon,
            NotificationId = old.NotificationId,
            User = ef.GameUsers.Find(old.User.UserId)
        };
    }
}