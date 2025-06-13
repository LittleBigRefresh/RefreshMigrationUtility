using Microsoft.EntityFrameworkCore;
using Refresh.Database;
using Refresh.Database.Models.Comments;
using Refresh.Database.Models.Levels;
using Refresh.Database.Models.Relations;
using Refresh.Database.Models.Users;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations;

public class RateReviewMigrator : Migrator<RealmRateReviewRelation, RateReviewRelation>
{
    public RateReviewMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}
    
    public override void MigrateChunk(RealmDatabaseContext realm, GameDatabaseContext ef)
    {
        IEnumerable<RealmRateReviewRelation> chunk = realm.All<RealmRateReviewRelation>()
            .AsEnumerable()
            .Skip(Progress)
            .Take(TakeSize);

        DbSet<RateReviewRelation> set = ef.Set<RateReviewRelation>();

        foreach (RealmRateReviewRelation old in chunk)
        {
            // some of these are apparently null in realm so we have to check here
            if (old.Review != null && ef.GameReviews.Select(u => u.ReviewId).Contains(old.Review.ReviewId))
            {
                RateReviewRelation mapped = Map(ef, old);
                set.Add(mapped);
            }

            Progress++;
        }

        ef.SaveChanges();
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