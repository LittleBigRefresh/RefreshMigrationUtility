using Refresh.Database.Models.Comments;
using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Relations;

#nullable disable

public partial class RateReviewRelation : IRealmObject
{
    public GameReview Review { get; set; }
    public GameUser User { get; set; }

    // we could just reuse RatingType from GameLevel rating logic
    [Ignored]
    public RatingType RatingType
    {
        get => (RatingType)this._ReviewRatingType;
        set => this._ReviewRatingType = (int)value;
    }
    
    // ReSharper disable once InconsistentNaming
    public int _ReviewRatingType { get; set; }
    public DateTimeOffset Timestamp { get; set; }
}