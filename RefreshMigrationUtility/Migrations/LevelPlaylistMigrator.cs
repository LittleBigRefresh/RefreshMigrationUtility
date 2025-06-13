using Microsoft.EntityFrameworkCore;
using Refresh.Database;
using Refresh.Database.Models.Levels;
using Refresh.Database.Models.Playlists;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility.Migrations;

public class LevelPlaylistMigrator : Migrator<RealmLevelPlaylistRelation, LevelPlaylistRelation>
{
    public LevelPlaylistMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override bool IsOldValid(GameDatabaseContext ef, RealmLevelPlaylistRelation old)
    {
        return ef.GamePlaylists.Select(u => u.PlaylistId).Contains(old.Playlist.PlaylistId);
    }

    protected override LevelPlaylistRelation Map(GameDatabaseContext ef, RealmLevelPlaylistRelation old)
    {
        return new LevelPlaylistRelation()
        {
            LevelId = old.Level.LevelId,
            Timestamp = old.Timestamp,
            PlaylistId = old.Playlist.PlaylistId,
            Index = old.Index
        };
    }

    public override IEnumerable<Type> NeedsTypes { get; } = [typeof(GameLevel), typeof(GamePlaylist)];
}