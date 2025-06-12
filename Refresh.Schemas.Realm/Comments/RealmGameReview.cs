using Refresh.Database.Models.Users;
using Refresh.Database.Models.Levels;

namespace Refresh.Database.Models.Comments;

#nullable disable

[MapTo("GameReview")]
public partial class RealmGameReview : IRealmObject
{
    public int ReviewId { get; set; }
    
    public RealmGameLevel Level { get; set;  }

    public RealmGameUser Publisher { get; set; }
    
    public DateTimeOffset PostedAt { get; set; }
    
    public string Labels { get; set; }
    
    public string Content { get; set; }
    
    public int SequentialId
    {
        get => this.ReviewId;
        set => this.ReviewId = value;
    }
}