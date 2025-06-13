using Refresh.Database.Models.Notifications;
using RefreshMigrationUtility;
using RefreshMigrationUtility.Migrations;

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
runner.AddSimpleTask<RealmGameAnnouncement, GameAnnouncement>();
runner.AddTask<RequestStatisticsMigrator>();
runner.AddTask<UserMigrator>();

ProgressReporter.Wall("Beginning migration of data! Do not interrupt this process.");

runner.RunAllTasks();

ProgressReporter.Wall("Migration has successfully completed!");
