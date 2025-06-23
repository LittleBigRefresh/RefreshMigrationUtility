using Refresh.Database;
using Refresh.Database.Models.Playlists;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations.Migrators;

public class PlaylistMigrator : UserDependentMigrator<RealmGamePlaylist, GamePlaylist>, INeedsSequenceRecalculation
{
    public PlaylistMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override GamePlaylist Map(GameDatabaseContext ef, RealmGamePlaylist old)
    {
        return new GamePlaylist()
        {
            Name = old.Name,
            Description = old.Description,
            IconHash = old.IconHash,
            IsRoot = old.IsRoot,
            LocationX = old.LocationX,
            LocationY = old.LocationY,
            Publisher = ef.GameUsers.Find(old.Publisher.UserId),
            CreationDate = old.CreationDate,
            LastUpdateDate = old.LastUpdateDate,
            PlaylistId = old.PlaylistId,
            PublisherId = old.Publisher.UserId
        };
    }

    public string SequenceName => "GamePlaylists_PlaylistId_seq";
}