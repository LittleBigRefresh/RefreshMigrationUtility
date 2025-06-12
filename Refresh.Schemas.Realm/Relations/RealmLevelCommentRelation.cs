using MongoDB.Bson;
using Refresh.Database.Models.Comments;
using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Relations;

#nullable disable

[MapTo("LevelCommentRelation")]
public partial class RealmLevelCommentRelation : IRealmObject
{
    public ObjectId CommentRelationId { get; set; } = ObjectId.GenerateNewId();
    public RealmGameUser User { get; set; }
    public RealmGameLevelComment Comment { get; set; }
    [Ignored]
    public RatingType RatingType
    {
        get => (RatingType)this._RatingType;
        set => this._RatingType = (int)value;
    }
    
    // ReSharper disable once InconsistentNaming
    public int _RatingType { get; set; }
    public DateTimeOffset Timestamp { get; set; }
}