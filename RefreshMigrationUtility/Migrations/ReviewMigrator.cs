using Refresh.Database;
using Refresh.Database.Models.Comments;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations;

public class ReviewMigrator : UserAndLevelDependentMigrator<RealmGameReview, GameReview>
{
    public ReviewMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override GameReview Map(GameDatabaseContext ef, RealmGameReview old)
    {
        return new GameReview
        {
            Publisher = ef.GameUsers.Find(old.Publisher.UserId),
            Level = ef.GameLevels.Find(old.Level.LevelId),
            SequentialId = old.SequentialId,
            Content = old.Content,
            Labels = old.Labels,
            PostedAt = old.PostedAt,
            ReviewId = old.ReviewId
        };
    }
}