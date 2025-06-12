using Refresh.Database.Models.Playlists;
using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Relations;
#nullable disable

[MapTo("FavouritePlaylistRelation")]
public partial class RealmFavouritePlaylistRelation : IRealmObject
{
    public RealmGamePlaylist Playlist { get; set; }
    public RealmGameUser User { get; set; }

    public DateTimeOffset Timestamp { get; set; }
}