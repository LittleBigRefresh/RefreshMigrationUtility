using Refresh.Database;
using Refresh.Database.Models.Contests;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations;

public class ContestMigrator : UserAndLevelDependentMigrator<RealmGameContest, GameContest>
{
    public ContestMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override GameContest Map(GameDatabaseContext ef, RealmGameContest old)
    {
        return new GameContest
        {
            AllowedGames = old.AllowedGames.ToList(),
            BannerUrl = old.BannerUrl,
            ContestDetails = old.ContestDetails,
            ContestId = old.ContestId,
            ContestSummary = old.ContestSummary,
            ContestTag = old.ContestTag,
            ContestTheme = old.ContestTheme,
            ContestTitle = old.ContestTitle,
            CreationDate = old.CreationDate,
            EndDate = old.EndDate,
            Organizer = ef.GameUsers.Find(old.Organizer.UserId),
            StartDate = old.StartDate,
            TemplateLevel = ef.GameLevels.Find(old.TemplateLevel?.LevelId),
        };
    }
}