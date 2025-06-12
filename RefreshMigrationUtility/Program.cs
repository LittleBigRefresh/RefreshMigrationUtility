using Microsoft.EntityFrameworkCore;
using Refresh.Database;
using Refresh.Schema.Realm.Impl;

GameDatabaseContext postgres = new();
postgres.Database.Migrate();

RealmDatabaseContext realm = new(@"X:\Refresh\Refresh.GameServer\bin\Debug\net9.0\refreshGameServer.realm");

Console.WriteLine("Both databases opened successfully");