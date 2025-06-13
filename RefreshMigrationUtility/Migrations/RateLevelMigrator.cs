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
    
    public override void MigrateChunk(RealmDatabaseContext realm, GameDatabaseContext ef)
    {
        IEnumerable<RealmRateLevelRelation> chunk = realm.All<RealmRateLevelRelation>()
            .AsEnumerable()
            .Skip(Progress)
            .Take(TakeSize);

        DbSet<RateLevelRelation> set = ef.Set<RateLevelRelation>();

        foreach (RealmRateLevelRelation old in chunk)
        {
            // some of these are apparently null in realm so we have to check here
            if (old.User != null && old.Level != null && ef.GameLevels.Select(u => u.LevelId).Contains(old.Level.LevelId))
            {
                RateLevelRelation mapped = Map(ef, old);
                set.Add(mapped);
            }

            Progress++;
        }

        ef.SaveChanges();
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