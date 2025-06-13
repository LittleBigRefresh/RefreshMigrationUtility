using Refresh.Database;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations;

public class TagMigrator : UserAndLevelDependentMigrator<RealmTagLevelRelation, TagLevelRelation>
{
    public TagMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override TagLevelRelation Map(GameDatabaseContext ef, RealmTagLevelRelation old)
    {
        return new TagLevelRelation
        {
            UserId = old.User.UserId,
            LevelId = old.Level.LevelId,
            Timestamp = old.Timestamp,
            _Tag = old._Tag,
            Tag = old.Tag,
        };
    }
}