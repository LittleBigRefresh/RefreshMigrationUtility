using Refresh.Database;
using Refresh.Database.Models.Levels;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations.Migrators;

public class LevelMigrator : UserDependentMigrator<RealmGameLevel, GameLevel>, INeedsSequenceRecalculation
{
    public LevelMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override GameLevel Map(GameDatabaseContext ef, RealmGameLevel old)
    {
        return new GameLevel
        {
            Description = old.Description,
            IconHash = old.IconHash,
            LocationX = old.LocationX,
            LocationY = old.LocationY,
            Publisher = ef.GameUsers.Find(old.Publisher?.UserId),
            Title = old.Title,
            _GameVersion = old._GameVersion,
            _LevelType = old._LevelType,
            BackgroundGuid = old.BackgroundGuid,
            DateTeamPicked = old.DateTeamPicked,
            EnforceMinMaxPlayers = old.EnforceMinMaxPlayers,
            GameVersion = old.GameVersion,
            IsAdventure = old.IsAdventure,
            IsCopyable = old.IsCopyable,
            IsLocked = old.IsLocked,
            IsModded = old.IsModded,
            IsReUpload = old.IsReUpload,
            IsSubLevel = old.IsSubLevel,
            LevelId = old.LevelId,
            LevelType = old.LevelType,
            MaxPlayers = old.MaxPlayers,
            MinPlayers = old.MinPlayers,
            OriginalPublisher = old.OriginalPublisher,
            PublishDate = old.PublishDate,
            RequiresMoveController = old.RequiresMoveController,
            // Reviews = {},
            RootResource = old.RootResource,
            SameScreenGame = old.SameScreenGame,
            Score = old.Score,
            // SkillRewards = {},
            StoryId = old.StoryId,
            UpdateDate = old.UpdateDate
        };
    }
    
    public string SequenceName => "GameLevels_LevelId_seq";
}