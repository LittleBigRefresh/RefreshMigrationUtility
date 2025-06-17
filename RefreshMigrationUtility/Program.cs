using System.Diagnostics;
using System.Runtime;
using Newtonsoft.Json;
using Refresh.Database.Models.Activity;
using Refresh.Database.Models.Notifications;
using Refresh.Database.Models.Users;
using RefreshMigrationUtility;
using RefreshMigrationUtility.Interface;
using RefreshMigrationUtility.Migrations;
using RefreshMigrationUtility.Migrations.Backfillers;
using RefreshMigrationUtility.Migrations.Migrators;

const string configPath = "migration.config.json";

MigrationConfig? config = null;

try
{
    if (!File.Exists(configPath))
    {
        MigrationConfig exampleConfig = new();
        File.WriteAllText(configPath, JsonConvert.SerializeObject(exampleConfig, Formatting.Indented));

        ProgressReporter.Wall($"A configuration file has been created at {Path.GetFullPath(configPath)}.");
        Console.WriteLine("This file contains the Realm DB path and the Postgres connection string.");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("You need to set these before the migrator will do anything.");
        Console.ResetColor();
        Environment.Exit(1);
    }

    config = JsonConvert.DeserializeObject<MigrationConfig>(File.ReadAllText(configPath))!;
    if (config == null)
        throw new Exception("The read JSON file was null.");
}
catch (Exception e)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Could not read/write the migration configuration! Migration cannot continue.");
    ProgressReporter.PrintExceptionDetails(e);
    throw new UnreachableException();
}

#if !DEBUG
ProgressReporter.PromptForMigrationConsent(config);
#endif

Console.WriteLine();
Console.WriteLine("Preparing migration...");

ProgressReporter.TestDatabases(config);

Console.WriteLine("Both databases connected successfully!");

if(!GCSettings.IsServerGC)
    ProgressReporter.Warn("The migrator isn't running with Server GC enabled! This will probably make things a lot slower.");

Console.WriteLine("Setting up migration runner");

MigrationRunner runner = new(config);
runner.AddSimpleMigrator<RealmGameAnnouncement, GameAnnouncement>();
runner.AddSimpleMigrator<RealmDisallowedUser, DisallowedUser>();
runner.AddMigrator<RequestStatisticsMigrator>();
runner.AddMigrator<UserMigrator>();
runner.AddMigrator<LevelMigrator>();
runner.AddMigrator<PlaylistMigrator>();
runner.AddMigrator<NotificationMigrator>();
runner.AddMigrator<AssetMigrator>();
runner.AddMigrator<EventMigrator>();
runner.AddMigrator<AssetDependencyMigrator>();
runner.AddMigrator<ContestMigrator>();
runner.AddMigrator<ChallengeMigrator>();
runner.AddMigrator<ChallengeScoreMigrator>();
runner.AddMigrator<LevelCommentMigrator>();
runner.AddMigrator<ProfileCommentMigrator>();
runner.AddMigrator<ReviewMigrator>();
runner.AddMigrator<VerifiedIpMigrator>();
runner.AddMigrator<ScoreMigrator>();
runner.AddMigrator<PhotoMigrator>();

runner.AddMigrator<FavouriteLevelMigrator>();
runner.AddMigrator<FavouritePlaylistMigrator>();
runner.AddMigrator<FavouriteUserMigrator>();
runner.AddMigrator<LevelPlaylistMigrator>();
runner.AddMigrator<PinProgressMigrator>();
runner.AddMigrator<ProfilePinMigrator>();
runner.AddMigrator<PlayLevelMigrator>();
runner.AddMigrator<QueueMigrator>();
runner.AddMigrator<RateLevelMigrator>();
runner.AddMigrator<RateReviewMigrator>();
runner.AddMigrator<SubPlaylistMigrator>();
runner.AddMigrator<TagMigrator>();
runner.AddMigrator<UniquePlayLevelMigrator>();
runner.AddMigrator<SkillRewardMigrator>();

runner.AddBackfiller<UserRootPlaylistBackfiller>();

ProgressReporter.Wall("Beginning migration of data! Do not interrupt this process.");

// force maximum throughput profile
GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.Default;
GCSettings.LatencyMode = GCLatencyMode.Batch;

runner.StartAllTasks();
ProgressReporter.ReportProgress(runner);

ProgressReporter.Wall("Migration has successfully completed!");
