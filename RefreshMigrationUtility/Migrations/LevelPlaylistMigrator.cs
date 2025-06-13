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
    
    public override void MigrateChunk(RealmDatabaseContext realm, GameDatabaseContext ef)
    {
        IEnumerable<RealmLevelPlaylistRelation> chunk = realm.All<RealmLevelPlaylistRelation>()
            .AsEnumerable()
            .Skip(Progress)
            .Take(TakeSize);

        DbSet<LevelPlaylistRelation> set = ef.Set<LevelPlaylistRelation>();

        foreach (RealmLevelPlaylistRelation old in chunk)
        {
            // some of these are apparently null in realm so we have to check here
            if (ef.GamePlaylists.Select(u => u.PlaylistId).Contains(old.Playlist.PlaylistId))
            {
                LevelPlaylistRelation mapped = Map(ef, old);
                set.Add(mapped);
            }

            Progress++;
        }

        ef.SaveChanges();
    }

    public override LevelPlaylistRelation Map(GameDatabaseContext ef, RealmLevelPlaylistRelation old)
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