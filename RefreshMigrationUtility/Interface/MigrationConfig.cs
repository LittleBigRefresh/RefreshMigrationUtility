namespace RefreshMigrationUtility.Interface;

#nullable disable

public class MigrationConfig
{
    public string RealmFilePath { get; set; } = "/path/to/refreshGameServer.realm";

    public string PostgresConnectionString { get; set; } = "Database=refresh;Username=refresh;Password=refresh;Host=localhost;Port=5432;Include Error Detail=true";
}