using Refresh.Database;
using Refresh.Database.Models.Levels.Challenges;
using Refresh.Database.Models.Users;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility.Migrations.Migrators;

public class ChallengeScoreMigrator : Migrator<RealmGameChallengeScore, GameChallengeScore>
{
    public ChallengeScoreMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override GameChallengeScore Map(GameDatabaseContext ef, RealmGameChallengeScore old)
    {
        return new GameChallengeScore
        {
            Publisher = ef.GameUsers.Find(old.Publisher.UserId),
            PublishDate = old.PublishDate,
            Challenge = ef.GameChallenges.Find(old.Challenge.ChallengeId),
            GhostHash = old.GhostHash,
            Score = old.Score,
            ScoreId = old.ScoreId,
            Time = old.Time
        };
    }

    public override IEnumerable<Type> NeedsTypes { get; } = [typeof(GameChallenge), typeof(GameUser)];
}