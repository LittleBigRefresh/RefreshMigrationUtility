using Microsoft.EntityFrameworkCore;
using Refresh.Database;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations;

public class PlayLevelMigrator : UserAndLevelDependentMigrator<RealmPlayLevelRelation, PlayLevelRelation>
{
    public PlayLevelMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}
    
    public override void MigrateChunk(MigrationChunk chunk, GameDatabaseContext ef)
    {
        DbSet<PlayLevelRelation> set = ef.Set<PlayLevelRelation>();

        IEnumerable<PlayLevelRelation> oldItems = ((MigrationChunk<RealmPlayLevelRelation>)chunk).Old
            .Select(old =>
            {
                if (old.Level == null || old.User == null)
                {
                    OnSkip();
                    return null;
                }
                
                OnMigrate();
                return Map(ef, old);
            })
            .Where(x => x != null)
            .DistinctBy(x => (x!.LevelId, x!.UserId))!;

        foreach (PlayLevelRelation mapped in oldItems)
        {
            set.Add(mapped);
        }

        ef.SaveChanges();
    }

    protected override PlayLevelRelation Map(GameDatabaseContext ef, RealmPlayLevelRelation old)
    {
        return new PlayLevelRelation
        {
            UserId = old.User.UserId,
            LevelId = old.Level.LevelId,
            Timestamp = old.Timestamp,
            Count = old.Count,
        };
    }
}