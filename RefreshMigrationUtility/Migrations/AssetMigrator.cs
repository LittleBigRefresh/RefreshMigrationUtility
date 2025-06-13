using Refresh.Database;
using Refresh.Database.Models.Assets;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility.Migrations;

public class AssetMigrator : UserDependentMigrator<RealmGameAsset, GameAsset>
{
    public AssetMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    public override GameAsset Map(GameDatabaseContext ef, RealmGameAsset old)
    {
        return new GameAsset()
        {
            _AssetSerializationMethod = old._AssetSerializationMethod,
            _AssetType = old._AssetType,
            AsMainlineIconHash = old.AsMainlineIconHash,
            AsMainlinePhotoHash = old.AsMainlinePhotoHash,
            AsMipIconHash = old.AsMipIconHash,
            AssetFormat = old.AssetFormat,
            AssetHash = old.AssetHash,
            AssetType = old.AssetType,
            IsPSP = old.IsPSP,
            OriginalUploader = ef.GameUsers.Find(old.OriginalUploader?.UserId),
            SizeInBytes = old.SizeInBytes,
            UploadDate = old.UploadDate,
            // Dependencies = {  }
        };
    }
}