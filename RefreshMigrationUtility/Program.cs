using Refresh.Database.Models.Notifications;
using RefreshMigrationUtility;

MigrationConfig config = new()
{
    RealmFilePath = @"X:\Refresh\Refresh.GameServer\bin\Debug\net9.0\refreshGameServer.realm",
    // PostgresConnectionString = 
};

ProgressReporter.PromptForMigrationConsent(config);

Console.WriteLine();
Console.WriteLine("Preparing migration...");

ProgressReporter.TestDatabases(config);

Console.WriteLine("Both databases connected successfully!");
Console.WriteLine("Setting up migration runner");

MigrationRunner runner = new(config);
runner.AddSimpleTask<RealmGameAnnouncement, GameAnnouncement>();

ProgressReporter.Wall("Beginning migration of data! Do not interrupt this process.");

runner.RunAllTasks();

ProgressReporter.Wall("Migration has successfully completed!");
