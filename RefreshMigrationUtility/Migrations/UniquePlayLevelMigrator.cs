using Microsoft.EntityFrameworkCore;
using Refresh.Database;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations;

public class UniquePlayLevelMigrator : UserAndLevelDependentMigrator<RealmUniquePlayLevelRelation, UniquePlayLevelRelation>
{
    public UniquePlayLevelMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}
    
    public override void MigrateChunk(RealmDatabaseContext realm, GameDatabaseContext ef)
    {
        IEnumerable<RealmUniquePlayLevelRelation> chunk = realm.All<RealmUniquePlayLevelRelation>()
            .AsEnumerable()
            .Skip(Progress)
            .Take(TakeSize);

        DbSet<UniquePlayLevelRelation> set = ef.Set<UniquePlayLevelRelation>();

        foreach (RealmUniquePlayLevelRelation old in chunk)
        {
            // some of these are apparently null in realm so we have to check here
            if (old.Level != null && ef.GameLevels.Select(u => u.LevelId).Contains(old.Level.LevelId) &&
                old.User != null && ef.GameUsers.Select(u => u.UserId).Contains(old.User.UserId))
            {
                UniquePlayLevelRelation mapped = Map(ef, old);
                set.Add(mapped);
            }

            Progress++;
        }

        ef.SaveChanges();
    }

    protected override UniquePlayLevelRelation Map(GameDatabaseContext ef, RealmUniquePlayLevelRelation old)
    {
        return new UniquePlayLevelRelation
        {
            UserId = old.User.UserId,
            LevelId = old.Level.LevelId,
            Timestamp = old.Timestamp
        };
    }
}