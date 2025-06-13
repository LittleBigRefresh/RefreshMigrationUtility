using Refresh.Database.Models.Assets;
using Refresh.Database.Models.Levels;
using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Photos;

#nullable disable

[JsonObject(MemberSerialization.OptOut)]
public partial class GamePhoto
{
    [Key] public int PhotoId { get; set; }
    public DateTimeOffset TakenAt { get; set; }
    public DateTimeOffset PublishedAt { get; set; }
    
    [Required]
    public GameUser Publisher { get; set; }
    #nullable restore
    [ForeignKey(nameof(LevelIdKey))]
    public GameLevel? Level { get; set; }
    public int? LevelIdKey { get; set; }
    #nullable disable
    
    public string LevelName { get; set; }
    public string LevelType { get; set; }
    public int LevelId { get; set; }
    
    [Required]
    public GameAsset SmallAsset { get; set; }
    [Required]
    public GameAsset MediumAsset { get; set; }
    [Required]
    public GameAsset LargeAsset { get; set; }
    public string PlanHash { get; set; }

    #region Subjects

#nullable enable
    #pragma warning disable CS8618 // realm forces us to have a non-nullable IList<float> so we have to have these shenanigans
    
    public GameUser? Subject1User { get; set; }
    public string? Subject1DisplayName { get; set; }
    
    public GameUser? Subject2User { get; set; }
    public string? Subject2DisplayName { get; set; }
    
    public GameUser? Subject3User { get; set; }
    public string? Subject3DisplayName { get; set; }
    
    public GameUser? Subject4User { get; set; }
    public string? Subject4DisplayName { get; set; }
    
    public List<float> Subject1Bounds { get; set; } = [];
    public List<float> Subject2Bounds { get; set; } = [];
    public List<float> Subject3Bounds { get; set; } = [];
    public List<float> Subject4Bounds { get; set; } = [];
    
    #pragma warning restore CS8618
    #nullable disable
    
    #endregion
    
    [JsonIgnore] [NotMapped] public int SequentialId
    {
        get => this.PhotoId;
        set => this.PhotoId = value;
    }
}