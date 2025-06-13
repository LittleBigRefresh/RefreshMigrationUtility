using Refresh.Database;
using Refresh.Database.Models.Assets;
using Refresh.Database.Models.Levels;
using Refresh.Database.Models.Photos;
using Refresh.Database.Models.Users;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility.Migrations.Migrators;

public class PhotoMigrator : Migrator<RealmGamePhoto, GamePhoto>
{
    public PhotoMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override GamePhoto Map(GameDatabaseContext ef, RealmGamePhoto old)
    {
        return new GamePhoto()
        {
            Publisher = ef.GameUsers.Find(old.Publisher.UserId),
            LevelId = old.LevelId,
            LevelType = old.LevelType,
            SequentialId = old.SequentialId,
            // assets are backfilled
            LargeAsset = ef.GameAssets.Find(old.LargeAsset.AssetHash),
            MediumAsset = ef.GameAssets.Find(old.MediumAsset.AssetHash),
            SmallAsset = ef.GameAssets.Find(old.SmallAsset.AssetHash),
            Level = ef.GameLevels.Find(old.Level?.LevelId),
            LevelIdKey = old.Level?.LevelId,
            LevelName = old.LevelName,
            PhotoId = old.PhotoId,
            PlanHash = old.PlanHash,
            PublishedAt = old.PublishedAt,
            Subject1Bounds = old.Subject1Bounds.ToList(),
            Subject2Bounds = old.Subject2Bounds.ToList(),
            Subject3Bounds = old.Subject3Bounds.ToList(),
            Subject4Bounds = old.Subject4Bounds.ToList(),
            // users are backfilled
            Subject1User = ef.GameUsers.Find(old.Subject1User?.UserId),
            Subject2User = ef.GameUsers.Find(old.Subject2User?.UserId),
            Subject3User = ef.GameUsers.Find(old.Subject3User?.UserId),
            Subject4User = ef.GameUsers.Find(old.Subject4User?.UserId),
            Subject1DisplayName = old.Subject1DisplayName,
            Subject2DisplayName = old.Subject2DisplayName,
            Subject3DisplayName = old.Subject3DisplayName,
            Subject4DisplayName = old.Subject4DisplayName,
            TakenAt = old.TakenAt,
        };
    }

    public override IEnumerable<Type> NeedsTypes { get; } = [typeof(GameUser), typeof(GameAsset), typeof(GameLevel)];
}