using Refresh.Database;
using Refresh.Database.Models.Levels;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations.Migrators;

public class SkillRewardMigrator : LevelDependentMigrator<RealmGameSkillReward, GameSkillReward>
{
    public SkillRewardMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override GameSkillReward Map(GameDatabaseContext ef, RealmGameSkillReward old)
    {
        return new GameSkillReward
        {
            Id = old.Id,
            _ConditionType = old._ConditionType,
            ConditionType = old.ConditionType,
            Enabled = old.Enabled,
            LevelId = old.LevelId,
            RequiredAmount = old.RequiredAmount,
            Title = old.Title
        };
    }
}