using Refresh.Database;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations.Migrators;

public class PlayLevelMigrator : UserAndLevelDependentMigrator<RealmPlayLevelRelation, PlayLevelRelation>
{
    public PlayLevelMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override bool IsOldValid(GameDatabaseContext ef, RealmPlayLevelRelation old)
    {
        return old.Level != null && old.User != null;
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