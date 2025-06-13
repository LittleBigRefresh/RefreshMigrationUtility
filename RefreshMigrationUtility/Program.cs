using Microsoft.EntityFrameworkCore;
using Refresh.Database;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations;

GameDatabaseContext postgres = new();
postgres.Database.EnsureDeleted();
postgres.Database.Migrate();

RealmDatabaseContext realm = new(@"X:\Refresh\Refresh.GameServer\bin\Debug\net9.0\refreshGameServer.realm");

Console.WriteLine("Both databases opened successfully");

AnnouncementMigrationTask migration = new(realm, postgres);
migration.MigrateChunk();