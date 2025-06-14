using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Assets;

public partial class GameAsset
{
    [Key] public string AssetHash { get; set; } = string.Empty;
    public GameUser? OriginalUploader { get; set; }
    public DateTimeOffset UploadDate { get; set; }
    public bool IsPSP { get; set; }
    public int SizeInBytes { get; set; }
    [NotMapped] public GameAssetType AssetType
    {
        get => (GameAssetType)this._AssetType;
        set => this._AssetType = (int)value;
    }

    // ReSharper disable once InconsistentNaming
    public int _AssetType { get; set; }

    [NotMapped] public GameAssetFormat AssetFormat
    {
        get => (GameAssetFormat)this._AssetSerializationMethod;
        set => this._AssetSerializationMethod = (int)value;
    }
    
    // ReSharper disable once InconsistentNaming
    public int _AssetSerializationMethod { get; set; }

    [NotMapped] 
    public AssetFlags AssetFlags
    {
        get
        {
            AssetFlags flags = AssetSafetyLevelExtensions.FromAssetType(this.AssetType, this.AssetFormat);
            
            return flags;
        }
    }

    public string? AsMainlineIconHash { get; set; }
    public string? AsMipIconHash { get; set; }
    
    //NOTE: there's no "as MIP photo hash" because theres no way to browse photos on LBP PSP.
    public string? AsMainlinePhotoHash { get; set; }
}