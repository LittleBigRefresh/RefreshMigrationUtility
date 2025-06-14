using Realms;
using Realms.Schema;
using Refresh.Database;
using Refresh.Database.Models;
using Refresh.Database.Models.Activity;
using Refresh.Database.Models.Assets;
using Refresh.Database.Models.Authentication;
using Refresh.Database.Models.Comments;
using Refresh.Database.Models.Contests;
using Refresh.Database.Models.Levels;
using Refresh.Database.Models.Levels.Challenges;
using Refresh.Database.Models.Levels.Scores;
using Refresh.Database.Models.Notifications;
using Refresh.Database.Models.Photos;
using Refresh.Database.Models.Playlists;
using Refresh.Database.Models.Relations;
using Refresh.Database.Models.Users;

namespace Refresh.Schema.Realm.Impl;

public class RealmDatabaseContext : IDisposable
{
    private readonly Realms.Realm _realm;
    
    private List<Type> SchemaTypes { get; } =
    [
        typeof(RealmRequestStatistics),
        typeof(RealmSequentialIdStorage),

        typeof(RealmEvent),
        typeof(RealmGameAnnouncement),
        typeof(RealmGameContest),
        typeof(RealmGamePhoto),

        // levels
        typeof(RealmGameLevel),
        typeof(RealmGameSkillReward),
        typeof(RealmTagLevelRelation),
        typeof(RealmGameLevelComment),
        typeof(RealmLevelCommentRelation),
        typeof(RealmRateLevelRelation),
        typeof(RealmFavouriteLevelRelation),
        typeof(RealmPlayLevelRelation),
        typeof(RealmUniquePlayLevelRelation),
        typeof(RealmQueueLevelRelation),
        typeof(RealmGameSubmittedScore),

        // reviews
        typeof(RealmGameReview),
        typeof(RealmRateReviewRelation),

        // users
        typeof(RealmGameUser),
        typeof(RealmToken),
        typeof(RealmGameProfileComment),
        typeof(RealmFavouriteUserRelation),
        typeof(RealmDisallowedUser),
        typeof(RealmGameNotification),
        typeof(RealmProfileCommentRelation),
        typeof(RealmEmailVerificationCode),
        typeof(RealmQueuedRegistration),
        typeof(RealmGameIpVerificationRequest),
        typeof(RealmGameUserVerifiedIpRelation),

        // pins
        typeof(RealmPinProgressRelation),
        typeof(RealmProfilePinRelation),

        // assets
        typeof(RealmGameAsset),
        typeof(RealmAssetDependencyRelation),
        
        // playlists
        typeof(RealmGamePlaylist),
        typeof(RealmLevelPlaylistRelation),
        typeof(RealmSubPlaylistRelation),
        typeof(RealmFavouritePlaylistRelation),

        // challenges
        typeof(RealmGameChallenge),
        typeof(RealmGameChallengeScore),
    ];

    public const int LatestRealmSchemaVersion = 171; 

    public RealmDatabaseContext(string path)
    {
        _realm = Realms.Realm.GetInstance(new RealmConfiguration(path)
        {
            IsReadOnly = true,
            // MigrationCallback = FailMigration,
            SchemaVersion = LatestRealmSchemaVersion,
            ShouldDeleteIfMigrationNeeded = false,
            Schema = SchemaTypes,
        });
    }

    public IQueryable<T> All<T>() where T : IRealmObject
    {
        return _realm.All<T>();
    }

    // private static void FailMigration(Migration migration, ulong oldSchemaVersion)
    // {
    //     throw new InvalidOperationException($"The realm database must be version {LatestRealmSchemaVersion}, not {oldSchemaVersion}." +
    //                                         $"Please upgrade your instance of Refresh to the latest v2 version.");
    // }

    public void Dispose()
    {
        _realm.Dispose();
    }
}