using Refresh.Database.Models.Users;
using Refresh.Database.Models.Levels;

namespace Refresh.Database.Models.Comments;

#nullable disable

[MapTo("GameLevelComment")]
public partial class RealmGameLevelComment : IRealmObject
{
    [PrimaryKey] public int SequentialId { get; set; }

    /// <inheritdoc/>
    public RealmGameUser Author { get; set; } = null!;

    /// <summary>
    /// The destination level this comment was posted to.
    /// </summary>
    public RealmGameLevel Level { get; set; } = null!;
    
    /// <inheritdoc/>
    public string Content { get; set; } = string.Empty;
    
    /// <inheritdoc/>
    public DateTimeOffset Timestamp { get; set; }
}