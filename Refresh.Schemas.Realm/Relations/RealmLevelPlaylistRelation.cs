using Refresh.Database.Models.Levels;
using Refresh.Database.Models.Playlists;

namespace Refresh.Database.Models.Relations;

#nullable disable

/// <summary>
/// A mapping of playlist -> sub-level
/// </summary>
[MapTo("LevelPlaylistRelation")]
public partial class RealmLevelPlaylistRelation : IRealmObject
{
    /// <summary>
    /// The playlist the level is contained in
    /// </summary>
    public RealmGamePlaylist Playlist { get; set; }
    /// <summary>
    /// The level contained within the playlist
    /// </summary>
    public RealmGameLevel Level { get; set; }
    
    /// <summary>
    /// The place of this level in the playlist, starts from 0
    /// </summary>
    public int Index { get; set; } = 0;
    public DateTimeOffset Timestamp { get; set; }
}