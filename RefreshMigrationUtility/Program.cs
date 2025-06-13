using Microsoft.EntityFrameworkCore;
using Refresh.Database;
using Refresh.Database.Models.Levels;
using Refresh.Database.Models.Notifications;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility;
using RefreshMigrationUtility.Migrations;

MigrationConfig config = new()
{
    RealmFilePath = @"X:\Refresh\Refresh.GameServer\bin\Debug\net9.0\refreshGameServer.realm",
    // PostgresConnectionString = 
};

ProgressReporter.PromptForMigrationConsent(config);

Console.WriteLine();
Console.WriteLine("Beginning migration! Do not interrupt.");

Console.WriteLine("Connecting to Postgres...");

GameDatabaseContext postgres = new();
Console.WriteLine("\tWiping database...");
postgres.Database.EnsureDeleted();
Console.WriteLine("\tMigrating database...");
postgres.Database.Migrate();
Console.WriteLine("\tTesting Postgres...");
_ = postgres.Set<GameLevel>().Count();
Console.WriteLine("Postgres OK");

Console.WriteLine("Connecting to Realm...");
RealmDatabaseContext realm = new(config.RealmFilePath);
Console.WriteLine("\tTesting Realm...");
_ = realm.All<RealmGameLevel>().Count();
Console.WriteLine("Realm OK");

ProgressReporter.Wall("Both databases opened successfully!");

Console.WriteLine("Setting up migration runner");

SimpleMigrationTask<RealmGameAnnouncement, GameAnnouncement> migration = new(realm, postgres);
migration.MigrateChunk();

ProgressReporter.Wall("Migration has successfully completed!");
