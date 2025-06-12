using System.Xml.Serialization;
using Refresh.Database.Models.Authentication;
using Refresh.Database.Models.Comments;
using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Levels;

[JsonObject(MemberSerialization.OptIn)]
[Index(nameof(Title), nameof(Description), nameof(StoryId))]
public partial class GameLevel
{
    [Key] public int LevelId { get; set; }
    
    public bool IsAdventure { get; set; }
    
    public string Title { get; set; } = "";
    public string IconHash { get; set; } = "0";
    public string Description { get; set; } = "";

    public int LocationX { get; set; }
    public int LocationY { get; set; }

    public string RootResource { get; set; } = string.Empty;

    /// <summary>
    /// When the level was first published in unix milliseconds
    /// </summary>
    public DateTimeOffset PublishDate { get; set; }
    /// <summary>
    /// When the level was last updated in unix milliseconds
    /// </summary>
    public DateTimeOffset UpdateDate { get; set; }
    
    public int MinPlayers { get; set; }
    public int MaxPlayers { get; set; }
    public bool EnforceMinMaxPlayers { get; set; }
    
    public bool SameScreenGame { get; set; }
    [NotMapped]
    public bool TeamPicked => this.DateTeamPicked != null;
    public DateTimeOffset? DateTeamPicked { get; set; }
    
    /// <summary>
    /// Whether any asset in the dependency tree is considered "modded"
    /// </summary>
    public bool IsModded { get; set; }
    
    /// <summary>
    /// The GUID of the background, this seems to only be used by LBP PSP
    /// </summary>
    public string? BackgroundGuid { get; set; }
    
    public TokenGame GameVersion 
    {
        get => (TokenGame)this._GameVersion;
        set => this._GameVersion = (int)value;
    }
    
    // ReSharper disable once InconsistentNaming
    public int _GameVersion { get; set; }
    
    public GameLevelType LevelType
    {
        get => (GameLevelType)this._LevelType;
        set => this._LevelType = (int)value;
    }

    // ReSharper disable once InconsistentNaming
    public int _LevelType { get; set; }

    /// <summary>
    /// The associated ID for the developer level.
    /// Set to 0 for user generated levels, since slot IDs of zero are invalid ingame.
    /// </summary>
    public int StoryId { get; set; }

    public GameSlotType SlotType 
        => this.StoryId == 0 ? GameSlotType.User : GameSlotType.Story;
    
    public bool IsLocked { get; set; }
    public bool IsSubLevel { get; set; }
    public bool IsCopyable { get; set; }
    public bool RequiresMoveController { get; set; }
    
    /// <summary>
    /// The score, used for Cool Levels.
    /// </summary>
    /// <seealso cref="CoolLevelsWorker"/>
    public float Score { get; set; }

#nullable disable
    #if !POSTGRES
    // ILists can't be serialized to XML, and Lists/Arrays cannot be stored in realm,
    // hence _SkillRewards and SkillRewards both existing
    // ReSharper disable once InconsistentNaming
    public IList<GameSkillReward> _SkillRewards { get; }
    #endif
    
    public IList<GameReview> Reviews { get; }
    
#nullable restore
    
    // TODO: Port GameSkillRewards to not use IEmbeddedObject
    public GameSkillReward[] SkillRewards
    {
        get => [];
        set => _ = value;
    }
    
    [NotMapped] public int SequentialId
    {
        get => this.LevelId;
        set => this.LevelId = value;
    }

    public GameUser? Publisher { get; set; }
    /// <summary>
    /// The publisher who originally published the level, if it has been re-uploaded by someone else.
    /// Should only be set if the original publisher does not have an account.
    /// </summary>
    public string? OriginalPublisher { get; set; }

    public bool IsReUpload { get; set; }
}