using Refresh.Database;
using Refresh.Database.Models.Playlists;
using Refresh.Database.Models.Users;

namespace RefreshMigrationUtility.Migrations.Backfillers;

public class UserRootPlaylistBackfiller : Backfiller<GameUser, GamePlaylist>
{
    protected override void Backfill(GameDatabaseContext ef, GameUser src)
    {
        GamePlaylist? playlist = ef.GamePlaylists.FirstOrDefault(p => p.IsRoot && p.Publisher == src);
        src.RootPlaylist = playlist;
    }

    public override IEnumerable<Type> NeedsTypes { get; } = [typeof(GameUser), typeof(GamePlaylist)];
}