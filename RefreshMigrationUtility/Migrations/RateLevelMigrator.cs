using Microsoft.EntityFrameworkCore;
using Refresh.Database;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations;

public class RateLevelMigrator : UserAndLevelDependentMigrator<RealmRateLevelRelation, RateLevelRelation>
{
    public RateLevelMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override bool IsOldValid(GameDatabaseContext ef, RealmRateLevelRelation old)
    {
        return old.User != null &&
               old.Level != null &&
               ef.GameLevels.Select(u => u.LevelId).Contains(old.Level.LevelId);
    }

    protected override RateLevelRelation Map(GameDatabaseContext ef, RealmRateLevelRelation old)
    {
        return new RateLevelRelation()
        {
            Level = ef.GameLevels.Find(old.Level.LevelId),
            Timestamp = old.Timestamp,
            User = ef.GameUsers.Find(old.User.UserId),
            _RatingType = old._RatingType,
            RateLevelRelationId = old.RateLevelRelationId,
            RatingType = old.RatingType
        };
    }
}