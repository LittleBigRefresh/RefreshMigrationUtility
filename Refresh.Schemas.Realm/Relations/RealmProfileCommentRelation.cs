using MongoDB.Bson;
using Refresh.Database.Models.Comments;
using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Relations;

#nullable disable

[MapTo("ProfileCommentRelation")]
public partial class RealmProfileCommentRelation : IRealmObject
{
    public ObjectId CommentRelationId { get; set; } = ObjectId.GenerateNewId();
    public RealmGameUser User { get; set; }
    public RealmGameProfileComment Comment { get; set; }
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