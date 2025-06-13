namespace RefreshMigrationUtility;

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
}