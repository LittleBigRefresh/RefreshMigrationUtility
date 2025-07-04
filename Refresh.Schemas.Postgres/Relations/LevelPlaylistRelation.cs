using Refresh.Database.Models.Levels;
using Refresh.Database.Models.Playlists;

namespace Refresh.Database.Models.Relations;

#nullable disable

/// <summary>
/// A mapping of playlist -> sub-level
/// </summary>
[PrimaryKey(nameof(PlaylistId), nameof(LevelId))]
public partial class LevelPlaylistRelation
{
    /// <summary>
    /// The playlist the level is contained in
    /// </summary>
    [ForeignKey(nameof(PlaylistId))]
    [Required]
    public GamePlaylist Playlist { get; set; }
    /// <summary>
    /// The level contained within the playlist
    /// </summary>
    [ForeignKey(nameof(LevelId))]
    [Required]
    public GameLevel Level { get; set; }
    
    [Required]
    public int PlaylistId { get; set; }
    [Required]
    public int LevelId { get; set; }
    
    /// <summary>
    /// The place of this level in the playlist, starts from 0
    /// </summary>
    public int Index { get; set; } = 0;
    public DateTimeOffset Timestamp { get; set; }
}