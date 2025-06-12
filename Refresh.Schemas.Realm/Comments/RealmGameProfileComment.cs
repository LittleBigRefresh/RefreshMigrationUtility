using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Comments;

#nullable disable

[MapTo("GameProfileComment")]
public partial class RealmGameProfileComment : IRealmObject
{
    [PrimaryKey] public int SequentialId { get; set; }

    /// <inheritdoc/>
    public RealmGameUser Author { get; set; } = null!;

    /// <summary>
    /// The destination profile this comment was posted to.
    /// </summary>
    public RealmGameUser Profile { get; set; } = null!;
    
    /// <inheritdoc/>
    public string Content { get; set; } = string.Empty;
    
    /// <inheritdoc/>
    public DateTimeOffset Timestamp { get; set; } 
}