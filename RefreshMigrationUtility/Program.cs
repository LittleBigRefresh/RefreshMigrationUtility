using Refresh.Database.Models.Activity;
using Refresh.Database.Models.Notifications;
using RefreshMigrationUtility;
using RefreshMigrationUtility.Migrations;
using RefreshMigrationUtility.Migrations.Backfillers;

MigrationConfig config = new()
{
    RealmFilePath = @"X:\Refresh\Refresh.GameServer\bin\Debug\net9.0\refreshGameServer.realm",
    // PostgresConnectionString = 
};

#if !DEBUG
ProgressReporter.PromptForMigrationConsent(config);
#endif

Console.WriteLine();
Console.WriteLine("Preparing migration...");

ProgressReporter.TestDatabases(config);

Console.WriteLine("Both databases connected successfully!");
Console.WriteLine("Setting up migration runner");

MigrationRunner runner = new(config);
runner.AddSimpleMigrator<RealmGameAnnouncement, GameAnnouncement>();
runner.AddMigrator<RequestStatisticsMigrator>();
runner.AddMigrator<UserMigrator>();
runner.AddMigrator<LevelMigrator>();
runner.AddMigrator<AssetMigrator>();
runner.AddMigrator<PlaylistMigrator>();

runner.AddBackfiller<UserRootPlaylistBackfiller>();

ProgressReporter.Wall("Beginning migration of data! Do not interrupt this process.");

runner.RunAllTasks();

ProgressReporter.Wall("Migration has successfully completed!");
