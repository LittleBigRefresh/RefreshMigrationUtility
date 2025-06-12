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

public class RealmDatabaseContext
{
    private readonly Realms.Realm _realm;
    
    public RealmDbSet<RealmGameUser> GameUsers => new(this._realm);
    public RealmDbSet<RealmToken> Tokens => new(this._realm);
    public RealmDbSet<RealmGameLevel> GameLevels => new(this._realm);
    public RealmDbSet<RealmGameProfileComment> GameProfileComments => new(this._realm);
    public RealmDbSet<RealmGameLevelComment> GameLevelComments => new(this._realm);
    public RealmDbSet<RealmProfileCommentRelation> ProfileCommentRelations => new(this._realm);
    public RealmDbSet<RealmLevelCommentRelation> LevelCommentRelations => new(this._realm);
    public RealmDbSet<RealmFavouriteLevelRelation> FavouriteLevelRelations => new(this._realm);
    public RealmDbSet<RealmQueueLevelRelation> QueueLevelRelations => new(this._realm);
    public RealmDbSet<RealmFavouriteUserRelation> FavouriteUserRelations => new(this._realm);
    public RealmDbSet<RealmPlayLevelRelation> PlayLevelRelations => new(this._realm);
    public RealmDbSet<RealmUniquePlayLevelRelation> UniquePlayLevelRelations => new(this._realm);
    public RealmDbSet<RealmRateLevelRelation> RateLevelRelations => new(this._realm);
    public RealmDbSet<RealmEvent> Events => new(this._realm);
    public RealmDbSet<RealmGameSubmittedScore> GameSubmittedScores => new(this._realm);
    public RealmDbSet<RealmGameAsset> GameAssets => new(this._realm);
    public RealmDbSet<RealmGameNotification> GameNotifications => new(this._realm);
    public RealmDbSet<RealmGamePhoto> GamePhotos => new(this._realm);
    public RealmDbSet<RealmGameIpVerificationRequest> GameIpVerificationRequests => new(this._realm);
    public RealmDbSet<RealmGameAnnouncement> GameAnnouncements => new(this._realm);
    public RealmDbSet<RealmQueuedRegistration> QueuedRegistrations => new(this._realm);
    public RealmDbSet<RealmEmailVerificationCode> EmailVerificationCodes => new(this._realm);
    public RealmDbSet<RealmRequestStatistics> RequestStatistics => new(this._realm);
    public RealmDbSet<RealmSequentialIdStorage> SequentialIdStorage => new(this._realm);
    public RealmDbSet<RealmGameContest> GameContests => new(this._realm);
    public RealmDbSet<RealmAssetDependencyRelation> AssetDependencyRelations => new(this._realm);
    public RealmDbSet<RealmGameReview> GameReviews => new(this._realm);
    public RealmDbSet<RealmDisallowedUser> DisallowedUsers => new(this._realm);
    public RealmDbSet<RealmRateReviewRelation> RateReviewRelations => new(this._realm);
    public RealmDbSet<RealmTagLevelRelation> TagLevelRelations => new(this._realm);
    public RealmDbSet<RealmGamePlaylist> GamePlaylists => new(this._realm);
    public RealmDbSet<RealmLevelPlaylistRelation> LevelPlaylistRelations => new(this._realm);
    public RealmDbSet<RealmSubPlaylistRelation> SubPlaylistRelations => new(this._realm);
    public RealmDbSet<RealmFavouritePlaylistRelation> FavouritePlaylistRelations => new(this._realm);
    public RealmDbSet<RealmGameUserVerifiedIpRelation> GameUserVerifiedIpRelations => new(this._realm);
    public RealmDbSet<RealmGameChallenge> GameChallenges => new(this._realm);
    public RealmDbSet<RealmGameChallengeScore> GameChallengeScores => new(this._realm);
    public RealmDbSet<RealmPinProgressRelation> PinProgressRelations => new(this._realm);
    public RealmDbSet<RealmProfilePinRelation> ProfilePinRelations => new(this._realm);
    
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

    public const int LatestRealmSchemaVersion = 169; 

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

    // private static void FailMigration(Migration migration, ulong oldSchemaVersion)
    // {
    //     throw new InvalidOperationException($"The realm database must be version {LatestRealmSchemaVersion}, not {oldSchemaVersion}." +
    //                                         $"Please upgrade your instance of Refresh to the latest v2 version.");
    // }
}