using Refresh.Database;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility;

public abstract class MigrationTask
{
    protected const int TakeSize = 4096;
    
    public abstract void MigrateChunk(RealmDatabaseContext realm, GameDatabaseContext ef);
    public abstract string MigrationType { get; }

    public abstract int Progress { get; set; }
    protected internal abstract int Total { get; set; }
    
    public abstract Type SourceType { get; }
    public abstract Type ProvidesType { get; }

    public virtual IEnumerable<Type> NeedsTypes => [];

    public bool Complete => this.Progress >= this.Total;

    internal static int MaxMigrationTypeLength = 0;
    
    public override string ToString()
    {
        string progress = Complete ? $"{this.Total:N0} migrated" : $"{this.Total - this.Progress:N0} migrating...";
        return $"{MigrationType.PadRight(MaxMigrationTypeLength + 1)}({progress})";
    }
}