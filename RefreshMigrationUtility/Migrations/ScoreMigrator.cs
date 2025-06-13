using Refresh.Database;
using Refresh.Database.Models.Levels.Scores;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations;

public class ScoreMigrator : UserAndLevelDependentMigrator<RealmGameSubmittedScore, GameSubmittedScore>
{
    public ScoreMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override GameSubmittedScore Map(GameDatabaseContext ef, RealmGameSubmittedScore old)
    {
        return new GameSubmittedScore
        {
            Level = ef.GameLevels.Find(old.Level.LevelId),
            Score = old.Score,
            ScoreId = old.ScoreId,
            _Game = old._Game,
            Game = old.Game,
            _Platform = old._Platform,
            Platform = old.Platform,
            PlayerIdsRaw = old.Players.Select(p => p.UserId.ToString()).ToList(),
            ScoreSubmitted = old.ScoreSubmitted,
            ScoreType = old.ScoreType,
        };
    }
}