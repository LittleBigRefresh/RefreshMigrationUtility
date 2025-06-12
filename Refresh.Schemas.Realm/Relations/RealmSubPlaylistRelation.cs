using Refresh.Database.Models.Playlists;

namespace Refresh.Database.Models.Relations;

#nullable disable

/// <summary>
/// A mapping of playlist -> sub-playlist
/// </summary>
[MapTo("SubPlaylistRelation")]
public partial class RealmSubPlaylistRelation : IRealmObject
{
    /// <summary>
    /// The playlist the level is contained in
    /// </summary>
    public RealmGamePlaylist Playlist { get; set; }
    /// <summary>
    /// The sub-playlist contained within the playlist
    /// </summary>
    public RealmGamePlaylist SubPlaylist { get; set; }
    
    public DateTimeOffset Timestamp { get; set; }
}