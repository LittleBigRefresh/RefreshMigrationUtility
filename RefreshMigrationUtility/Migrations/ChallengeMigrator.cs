using Refresh.Database;
using Refresh.Database.Models.Levels.Challenges;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations;

public class ChallengeMigrator : UserAndLevelDependentMigrator<RealmGameChallenge, GameChallenge>
{
    public ChallengeMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    public override GameChallenge Map(GameDatabaseContext ef, RealmGameChallenge old)
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
}