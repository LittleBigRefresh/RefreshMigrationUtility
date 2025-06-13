using Refresh.Database;
using Refresh.Database.Models.Playlists;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility.Migrations.Migrators;

public class SubPlaylistMigrator : Migrator<RealmSubPlaylistRelation, SubPlaylistRelation>
{
    public SubPlaylistMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override bool IsOldValid(GameDatabaseContext ef, RealmSubPlaylistRelation old)
    {
        return ef.GamePlaylists.Select(p => p.PlaylistId).Contains(old.Playlist.PlaylistId) &&
               ef.GamePlaylists.Select(p => p.PlaylistId).Contains(old.SubPlaylist.PlaylistId);
    }

    protected override SubPlaylistRelation Map(GameDatabaseContext ef, RealmSubPlaylistRelation old)
    {
        return new SubPlaylistRelation
        {
            Timestamp = old.Timestamp,
            PlaylistId = old.Playlist.PlaylistId,
            SubPlaylistId = old.SubPlaylist.PlaylistId
        };
    }

    public override IEnumerable<Type> NeedsTypes { get; } = [typeof(GamePlaylist)];
}