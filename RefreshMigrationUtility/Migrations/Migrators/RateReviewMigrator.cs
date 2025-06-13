using Refresh.Database;
using Refresh.Database.Models.Comments;
using Refresh.Database.Models.Levels;
using Refresh.Database.Models.Relations;
using Refresh.Database.Models.Users;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility.Migrations.Migrators;

public class RateReviewMigrator : Migrator<RealmRateReviewRelation, RateReviewRelation>
{
    public RateReviewMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override bool IsOldValid(GameDatabaseContext ef, RealmRateReviewRelation old)
    {
        return old.Review != null &&
               ef.GameReviews.Select(u => u.ReviewId).Contains(old.Review.ReviewId);
    }

    protected override RateReviewRelation Map(GameDatabaseContext ef, RealmRateReviewRelation old)
    {
        return new RateReviewRelation
        {
            ReviewId = old.Review.ReviewId,
            Timestamp = old.Timestamp,
            UserId = old.User.UserId,
            RatingType = old.RatingType,
            _ReviewRatingType = old._ReviewRatingType,
        };
    }

    public override IEnumerable<Type> NeedsTypes { get; } = [typeof(GameLevel), typeof(GameUser), typeof(GameReview)];
}