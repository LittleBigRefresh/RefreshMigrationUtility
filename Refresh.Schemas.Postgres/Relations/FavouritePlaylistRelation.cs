using MongoDB.Bson;
using Refresh.Database.Models.Playlists;
using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Relations;
#nullable disable

[PrimaryKey(nameof(UserId), nameof(PlaylistId))]
public partial class FavouritePlaylistRelation
{
    [ForeignKey(nameof(PlaylistId))]
    public GamePlaylist Playlist { get; set; }
    [ForeignKey(nameof(UserId))]
    public GameUser User { get; set; }
    
    public int PlaylistId { get; set; }
    public ObjectId UserId { get; set; }
    
    public DateTimeOffset Timestamp { get; set; }
}