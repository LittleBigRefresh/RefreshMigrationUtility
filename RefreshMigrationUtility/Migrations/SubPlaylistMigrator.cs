using Microsoft.EntityFrameworkCore;
using Refresh.Database;
using Refresh.Database.Models.Playlists;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility.Migrations;

public class SubPlaylistMigrator : Migrator<RealmSubPlaylistRelation, SubPlaylistRelation>
{
    public SubPlaylistMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}
    
    public override void MigrateChunk(RealmDatabaseContext realm, GameDatabaseContext ef)
    {
        IEnumerable<RealmSubPlaylistRelation> chunk = realm.All<RealmSubPlaylistRelation>()
            .AsEnumerable()
            .Skip(Progress)
            .Take(TakeSize);

        DbSet<SubPlaylistRelation> set = ef.Set<SubPlaylistRelation>();

        foreach (RealmSubPlaylistRelation old in chunk)
        {
            // some of these are apparently null in realm so we have to check here
            if (ef.GamePlaylists.Select(p => p.PlaylistId).Contains(old.Playlist.PlaylistId) &&
                ef.GamePlaylists.Select(p => p.PlaylistId).Contains(old.SubPlaylist.PlaylistId))
            {
                SubPlaylistRelation mapped = Map(ef, old);
                set.Add(mapped);
            }

            Progress++;
        }

        ef.SaveChanges();
    }

    public override SubPlaylistRelation Map(GameDatabaseContext ef, RealmSubPlaylistRelation old)
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