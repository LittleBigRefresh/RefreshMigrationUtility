using MongoDB.Bson;
using Refresh.Database.Models.Comments;
using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Relations;

#nullable disable

public partial class ProfileCommentRelation
{
    [Key] public ObjectId CommentRelationId { get; set; } = ObjectId.GenerateNewId();
    [Required]
    public GameUser User { get; set; }
    [Required]
    public GameProfileComment Comment { get; set; }
    [NotMapped]
    public RatingType RatingType
    {
        get => (RatingType)this._RatingType;
        set => this._RatingType = (int)value;
    }
    
    // ReSharper disable once InconsistentNaming
    public int _RatingType { get; set; }
    public DateTimeOffset Timestamp { get; set; }
}