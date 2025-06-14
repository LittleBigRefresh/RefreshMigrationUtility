using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using Npgsql;
using Refresh.Database.Compatibility;
using Refresh.Database.Models.Authentication;
using Refresh.Database.Models.Activity;
using Refresh.Database.Models.Assets;
using Refresh.Database.Models.Comments;
using Refresh.Database.Models.Contests;
using Refresh.Database.Models.Users;
using Refresh.Database.Models.Levels.Challenges;
using Refresh.Database.Models.Levels.Scores;
using Refresh.Database.Models.Levels;
using Refresh.Database.Models.Notifications;
using Refresh.Database.Models.Photos;
using Refresh.Database.Models.Playlists;
using Refresh.Database.Models.Relations;
using Refresh.Database.Models;

namespace Refresh.Database;

[SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
public partial class GameDatabaseContext : DbContext
{
    public DbSet<GameUser> GameUsers { get; set; }
    public DbSet<Token> Tokens { get; set; }
    public DbSet<GameLevel> GameLevels { get; set; }
    public DbSet<GameProfileComment> GameProfileComments { get; set; }
    public DbSet<GameLevelComment> GameLevelComments { get; set; }
    public DbSet<ProfileCommentRelation> ProfileCommentRelations { get; set; }
    public DbSet<LevelCommentRelation> LevelCommentRelations { get; set; }
    public DbSet<FavouriteLevelRelation> FavouriteLevelRelations { get; set; }
    public DbSet<QueueLevelRelation> QueueLevelRelations { get; set; }
    public DbSet<FavouriteUserRelation> FavouriteUserRelations { get; set; }
    public DbSet<PlayLevelRelation> PlayLevelRelations { get; set; }
    public DbSet<UniquePlayLevelRelation> UniquePlayLevelRelations { get; set; }
    public DbSet<RateLevelRelation> RateLevelRelations { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<GameSubmittedScore> GameSubmittedScores { get; set; }
    public DbSet<GameAsset> GameAssets { get; set; }
    public DbSet<GameNotification> GameNotifications { get; set; }
    public DbSet<GamePhoto> GamePhotos { get; set; }
    public DbSet<GameIpVerificationRequest> GameIpVerificationRequests { get; set; }
    public DbSet<GameAnnouncement> GameAnnouncements { get; set; }
    public DbSet<QueuedRegistration> QueuedRegistrations { get; set; }
    public DbSet<EmailVerificationCode> EmailVerificationCodes { get; set; }
    public DbSet<RequestStatistics> RequestStatistics { get; set; }
    public DbSet<GameContest> GameContests { get; set; }
    public DbSet<AssetDependencyRelation> AssetDependencyRelations { get; set; }
    public DbSet<GameReview> GameReviews { get; set; }
    public DbSet<DisallowedUser> DisallowedUsers { get; set; }
    public DbSet<RateReviewRelation> RateReviewRelations { get; set; }
    public DbSet<TagLevelRelation> TagLevelRelations { get; set; }
    public DbSet<GamePlaylist> GamePlaylists { get; set; }
    public DbSet<LevelPlaylistRelation> LevelPlaylistRelations { get; set; }
    public DbSet<SubPlaylistRelation> SubPlaylistRelations { get; set; }
    public DbSet<FavouritePlaylistRelation> FavouritePlaylistRelations { get; set; }
    public DbSet<GameUserVerifiedIpRelation> GameUserVerifiedIpRelations { get; set; }
    public DbSet<GameChallenge> GameChallenges { get; set; }
    public DbSet<GameChallengeScore> GameChallengeScores { get; set; }
    public DbSet<PinProgressRelation> PinProgressRelations { get; set; }
    public DbSet<ProfilePinRelation> ProfilePinRelations { get; set; }
    public DbSet<GameSkillReward> GameSkillRewards { get; set; }

    private readonly string _connectionString;

    public GameDatabaseContext()
    {
        NpgsqlConnectionStringBuilder builder = new()
        {
            Database = "refresh",
            Username = "refresh",
            Password = "refresh",
            Host = "localhost",
            Port = 5432,
        };

        _connectionString = builder.ToString();
    }

    public GameDatabaseContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(_connectionString);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder config)
    {
        config
            .Properties<ObjectId>()
            .HaveConversion<ObjectIdConverter>();
    }
}