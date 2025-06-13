using Refresh.Database;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility;

public abstract class MigrationTask
{
    public abstract void MigrateChunk(RealmDatabaseContext realm, GameDatabaseContext ef);
    public abstract string MigrationType { get; }

    protected abstract int Progress { get; set; }
    protected internal abstract int Total { get; set; }
    
    public abstract Type SourceType { get; }
    public abstract Type ProvidesType { get; }

    public virtual IEnumerable<Type> NeedsTypes => [];

    public bool Complete => this.Progress >= this.Total;
    
    public override string ToString()
    {
        return $"{MigrationType} ({this.Progress}/{this.Total})";
    }
}