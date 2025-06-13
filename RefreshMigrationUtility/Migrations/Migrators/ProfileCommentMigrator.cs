using Refresh.Database;
using Refresh.Database.Models.Comments;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations.Migrators;

public class ProfileCommentMigrator : UserDependentMigrator<RealmGameProfileComment, GameProfileComment>
{
    public ProfileCommentMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override GameProfileComment Map(GameDatabaseContext ef, RealmGameProfileComment old)
    {
        return new GameProfileComment
        {
            Profile = ef.GameUsers.Find(old.Profile.UserId),
            Timestamp = old.Timestamp,
            SequentialId = old.SequentialId,
            Author = ef.GameUsers.Find(old.Author.UserId),
            Content = old.Content,
        };
    }
}