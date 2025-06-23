using Refresh.Database;
using Refresh.Database.Models.Levels.Challenges;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations.Migrators;

public class ChallengeMigrator : UserAndLevelDependentMigrator<RealmGameChallenge, GameChallenge>, INeedsSequenceRecalculation
{
    public ChallengeMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override GameChallenge Map(GameDatabaseContext ef, RealmGameChallenge old)
    {
        return new GameChallenge
        {
            Level = ef.GameLevels.Find(old.Level.LevelId),
            Name = old.Name,
            _Type = old._Type,
            ChallengeId = old.ChallengeId,
            ExpirationDate = old.ExpirationDate,
            FinishCheckpointUid = old.FinishCheckpointUid,
            LastUpdateDate = old.LastUpdateDate,
            PublishDate = old.PublishDate,
            Publisher = ef.GameUsers.Find(old.Publisher?.UserId),
            SequentialId = old.SequentialId,
            StartCheckpointUid = old.StartCheckpointUid,
            Type = old.Type
        };
    }

    public string SequenceName => "GameChallenges_ChallengeId_seq";
}