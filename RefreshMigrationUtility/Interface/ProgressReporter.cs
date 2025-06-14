using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Refresh.Database;
using Refresh.Database.Models.Levels;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations;

namespace RefreshMigrationUtility.Interface;

public static class ProgressReporter
{
    public static void PromptForMigrationConsent(MigrationConfig config)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("You are about to wipe the configured Postgres database.");
        Console.WriteLine("If there is any data, IT WILL BE LOST.");
        Console.WriteLine("Your Realm will remain untouched.");
        Console.WriteLine();
        Console.WriteLine($"\tYour connection string is: '{config.PostgresConnectionString}'");
        Console.WriteLine();
        Console.Write("IF YOU ARE SURE YOU WANT TO DESTROY THE ABOVE POSTGRES DATABASE, PRESS ENTER: ");
        Console.ResetColor();

        if (Console.ReadKey(true).Key != ConsoleKey.Enter)
        {
            Console.WriteLine("User didn't press enter, bailing");
            Environment.Exit(1);
        }
    }

    public static void Wall(string str)
    {
        string dashes = new string('-', str.Length + 2);
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine();
        Console.WriteLine(dashes);
        Console.Write(' ');
        Console.WriteLine(str);
        Console.WriteLine(dashes);
        Console.WriteLine();
        Console.ResetColor();
    }

    public static void TestDatabases(MigrationConfig config)
    {
        Console.WriteLine("Connecting to Realm...");
        try
        {
            using RealmDatabaseContext realm = new(config.RealmFilePath);
            Console.WriteLine("\tTesting Realm...");
            _ = realm.All<RealmGameLevel>().Count();
        }
        catch (Exception e)
        {
            ReportDbTestError(e, "Realm");
        }
        Console.WriteLine("Realm OK");
        
        Console.WriteLine("Connecting to Postgres...");
        try
        {
            using GameDatabaseContext postgres = new(config.PostgresConnectionString);
            Console.WriteLine("\tWiping database...");
            postgres.Database.EnsureDeleted();
            Console.WriteLine("\tMigrating database...");
            postgres.Database.Migrate();
            Console.WriteLine("\tTesting Postgres...");
            _ = postgres.Set<GameLevel>().Count();
        }
        catch (Exception e)
        {
            ReportDbTestError(e, "Postgres");
        }

        Console.WriteLine("Postgres OK");
    }

    [DoesNotReturn]
    private static void ReportDbTestError(Exception e, string dbName)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Could not connect to {dbName}! Migration cannot continue.");
        Console.WriteLine();
        Console.WriteLine("This is likely for one of the following reasons:");
        Console.WriteLine("  1. The path or connection string is wrong");
        Console.WriteLine("  2. Refresh was not upgraded to the latest version of v2 to complete all migrations");
        Console.WriteLine("  3. The migrator is bugged");
        Console.WriteLine();
        Console.WriteLine("The exception details are printed below for debugging.");
        Console.WriteLine();
        Console.ResetColor();
        Console.WriteLine(e);

        Environment.Exit(2);
    }

    public static void ReportProgress(MigrationRunner runner)
    {
        Stopwatch sw = Stopwatch.StartNew();
        while (!runner.Complete)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Migrating Refresh from Realm to Postgres - ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("do not interrupt!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Running for " + sw.Elapsed);
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Complete tasks:");
            foreach (MigrationTask task in runner.Tasks.Where(t => t.Complete))
            {
                Console.Write('\t');
                Console.WriteLine(task.ToString());
            }
        
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("In progress:");
            foreach (MigrationTask task in runner.Tasks.Where(t => !t.Complete && t.Progress > 0))
            {
                Console.Write('\t');
                Console.WriteLine(task.ToString());
            }
            
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Not started:");
            foreach (MigrationTask task in runner.Tasks.Where(t => !t.Complete && t.Progress <= 0))
            {
                Console.Write('\t');
                Console.WriteLine(task.ToString());
            }
            
            Console.ResetColor();
            Thread.Sleep(500);
        }
        
        Console.WriteLine();
        Console.WriteLine($"Migration took {sw.Elapsed}!");
    }
}