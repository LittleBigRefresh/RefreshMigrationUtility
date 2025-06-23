using Refresh.Database;
using Refresh.Database.Models.Comments;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations.Migrators;

public class LevelCommentMigrator : UserAndLevelDependentMigrator<RealmGameLevelComment, GameLevelComment>, INeedsSequenceRecalculation
{
    public LevelCommentMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override bool IsOldValid(GameDatabaseContext ef, RealmGameLevelComment old)
    {
        return old.Level != null;
    }

    protected override GameLevelComment Map(GameDatabaseContext ef, RealmGameLevelComment old)
    {
        return new GameLevelComment
        {
            Level = ef.GameLevels.Find(old.Level.LevelId),
            Timestamp = old.Timestamp,
            SequentialId = old.SequentialId,
            Author = ef.GameUsers.Find(old.Author.UserId),
            Content = old.Content,
        };
    }

    public string SequenceName => "GameLevelComments_SequentialId_seq";
}