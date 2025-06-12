using Microsoft.EntityFrameworkCore;
using Refresh.Database;

GameDatabaseContext postgres = new();
postgres.Database.Migrate();