using Refresh.Database.Models.Levels;
using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Relations;

#nullable disable

[MapTo("TagLevelRelation")]
public partial class RealmTagLevelRelation : IRealmObject
{
    public RealmGameLevel Level { get; set; }
    public RealmGameUser User { get; set; }

    [Ignored]
    public Tag Tag
    {
        get => (Tag)this._Tag;
        set => this._Tag = (byte)value;
    }
    
    // ReSharper disable once InconsistentNaming
    public byte _Tag { get; set; }

    public DateTimeOffset Timestamp { get; set; }
}