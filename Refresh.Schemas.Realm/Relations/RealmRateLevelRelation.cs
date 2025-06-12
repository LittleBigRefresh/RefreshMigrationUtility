using MongoDB.Bson;
using Refresh.Database.Models.Comments;
using Refresh.Database.Models.Levels;
using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Relations;

#nullable disable

[MapTo("RateLevelRelation")]
public partial class RealmRateLevelRelation : IRealmObject
{
    public ObjectId RateLevelRelationId { get; set; } = ObjectId.GenerateNewId();
    
    [Ignored]
    public RatingType RatingType
    {
        get => (RatingType)this._RatingType;
        set => this._RatingType = (int)value;
    }
    
    // ReSharper disable once InconsistentNaming
    public int _RatingType { get; set; }
    
    public RealmGameLevel Level { get; set; }
    public RealmGameUser User { get; set; }
    public DateTimeOffset Timestamp { get; set; }
}