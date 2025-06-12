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
    
    public RealmDbSet<GameUser> GameUsers => new(this._realm);
    public RealmDbSet<Token> Tokens => new(this._realm);
    public RealmDbSet<GameLevel> GameLevels => new(this._realm);
    public RealmDbSet<GameProfileComment> GameProfileComments => new(this._realm);
    public RealmDbSet<GameLevelComment> GameLevelComments => new(this._realm);
    public RealmDbSet<ProfileCommentRelation> ProfileCommentRelations => new(this._realm);
    public RealmDbSet<LevelCommentRelation> LevelCommentRelations => new(this._realm);
    public RealmDbSet<FavouriteLevelRelation> FavouriteLevelRelations => new(this._realm);
    public RealmDbSet<QueueLevelRelation> QueueLevelRelations => new(this._realm);
    public RealmDbSet<FavouriteUserRelation> FavouriteUserRelations => new(this._realm);
    public RealmDbSet<PlayLevelRelation> PlayLevelRelations => new(this._realm);
    public RealmDbSet<UniquePlayLevelRelation> UniquePlayLevelRelations => new(this._realm);
    public RealmDbSet<RateLevelRelation> RateLevelRelations => new(this._realm);
    public RealmDbSet<Event> Events => new(this._realm);
    public RealmDbSet<GameSubmittedScore> GameSubmittedScores => new(this._realm);
    public RealmDbSet<GameAsset> GameAssets => new(this._realm);
    public RealmDbSet<GameNotification> GameNotifications => new(this._realm);
    public RealmDbSet<GamePhoto> GamePhotos => new(this._realm);
    public RealmDbSet<GameIpVerificationRequest> GameIpVerificationRequests => new(this._realm);
    public RealmDbSet<GameAnnouncement> GameAnnouncements => new(this._realm);
    public RealmDbSet<QueuedRegistration> QueuedRegistrations => new(this._realm);
    public RealmDbSet<EmailVerificationCode> EmailVerificationCodes => new(this._realm);
    public RealmDbSet<RequestStatistics> RequestStatistics => new(this._realm);
    public RealmDbSet<SequentialIdStorage> SequentialIdStorage => new(this._realm);
    public RealmDbSet<GameContest> GameContests => new(this._realm);
    public RealmDbSet<AssetDependencyRelation> AssetDependencyRelations => new(this._realm);
    public RealmDbSet<GameReview> GameReviews => new(this._realm);
    public RealmDbSet<DisallowedUser> DisallowedUsers => new(this._realm);
    public RealmDbSet<RateReviewRelation> RateReviewRelations => new(this._realm);
    public RealmDbSet<TagLevelRelation> TagLevelRelations => new(this._realm);
    public RealmDbSet<GamePlaylist> GamePlaylists => new(this._realm);
    public RealmDbSet<LevelPlaylistRelation> LevelPlaylistRelations => new(this._realm);
    public RealmDbSet<SubPlaylistRelation> SubPlaylistRelations => new(this._realm);
    public RealmDbSet<FavouritePlaylistRelation> FavouritePlaylistRelations => new(this._realm);
    public RealmDbSet<GameUserVerifiedIpRelation> GameUserVerifiedIpRelations => new(this._realm);
    public RealmDbSet<GameChallenge> GameChallenges => new(this._realm);
    public RealmDbSet<GameChallengeScore> GameChallengeScores => new(this._realm);
    public RealmDbSet<PinProgressRelation> PinProgressRelations => new(this._realm);
    public RealmDbSet<ProfilePinRelation> ProfilePinRelations => new(this._realm);
    
    private List<Type> SchemaTypes { get; } =
    [
        typeof(RequestStatistics),
        typeof(SequentialIdStorage),

        typeof(Event),
        typeof(GameAnnouncement),
        typeof(GameContest),
        typeof(GamePhoto),

        // levels
        typeof(GameLevel),
        typeof(GameSkillReward),
        typeof(TagLevelRelation),
        typeof(GameLevelComment),
        typeof(LevelCommentRelation),
        typeof(RateLevelRelation),
        typeof(FavouriteLevelRelation),
        typeof(PlayLevelRelation),
        typeof(UniquePlayLevelRelation),
        typeof(QueueLevelRelation),
        typeof(GameSubmittedScore),

        // reviews
        typeof(GameReview),
        typeof(RateReviewRelation),

        // users
        typeof(GameUser),
        typeof(Token),
        typeof(GameProfileComment),
        typeof(FavouriteUserRelation),
        typeof(DisallowedUser),
        typeof(GameNotification),
        typeof(ProfileCommentRelation),
        typeof(EmailVerificationCode),
        typeof(QueuedRegistration),
        typeof(GameIpVerificationRequest),
        typeof(GameUserVerifiedIpRelation),

        // pins
        typeof(PinProgressRelation),
        typeof(ProfilePinRelation),

        // assets
        typeof(GameAsset),
        typeof(AssetDependencyRelation),
        
        // playlists
        typeof(GamePlaylist),
        typeof(LevelPlaylistRelation),
        typeof(SubPlaylistRelation),
        typeof(FavouritePlaylistRelation),

        // challenges
        typeof(GameChallenge),
        typeof(GameChallengeScore),
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