using Refresh.Database;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations.Migrators;

public class UniquePlayLevelMigrator : UserAndLevelDependentMigrator<RealmUniquePlayLevelRelation, UniquePlayLevelRelation>
{
    public UniquePlayLevelMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override bool IsOldValid(GameDatabaseContext ef, RealmUniquePlayLevelRelation old)
    {
        return old.Level != null && old.User != null &&
                                 ef.GameLevels.Select(u => u.LevelId).Contains(old.Level.LevelId) &&
                                 ef.GameUsers.Select(u => u.UserId).Contains(old.User.UserId);
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