using Realms;

namespace RefreshMigrationUtility;

public abstract class MigrationChunk
{
    public static readonly EmptyMigrationChunk Empty = new();
}

public sealed class MigrationChunk<TOld> : MigrationChunk where TOld : IRealmObject
{
    public readonly IEnumerable<TOld> Old;

    public MigrationChunk(IEnumerable<TOld> old)
    {
        this.Old = old;
    }
}

public sealed class EmptyMigrationChunk : MigrationChunk;