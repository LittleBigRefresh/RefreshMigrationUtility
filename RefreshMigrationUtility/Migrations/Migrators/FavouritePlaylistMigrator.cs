using Refresh.Database;
using Refresh.Database.Models.Playlists;
using Refresh.Database.Models.Relations;
using Refresh.Database.Models.Users;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility.Migrations.Migrators;

public class FavouritePlaylistMigrator : Migrator<RealmFavouritePlaylistRelation, FavouritePlaylistRelation>
{
    public FavouritePlaylistMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override FavouritePlaylistRelation Map(GameDatabaseContext ef, RealmFavouritePlaylistRelation old)
    {
        return new FavouritePlaylistRelation
        {
            Timestamp = old.Timestamp,
            UserId = old.User.UserId,
            PlaylistId = old.Playlist.PlaylistId,
        };
    }

    public override IEnumerable<Type> NeedsTypes { get; } = [typeof(GameUser), typeof(GamePlaylist)];
}