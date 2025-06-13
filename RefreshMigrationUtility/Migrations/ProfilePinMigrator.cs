using Microsoft.EntityFrameworkCore;
using Refresh.Database;
using Refresh.Database.Models.Relations;
using Refresh.Schema.Realm.Impl;
using RefreshMigrationUtility.Migrations.Dependent;

namespace RefreshMigrationUtility.Migrations;

public class ProfilePinMigrator : UserDependentMigrator<RealmProfilePinRelation, ProfilePinRelation>
{
    public ProfilePinMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}
    
    public override void MigrateChunk(MigrationChunk chunk, GameDatabaseContext ef)
    {
        DbSet<ProfilePinRelation> set = ef.Set<ProfilePinRelation>();

        IEnumerable<ProfilePinRelation> oldItems = ((MigrationChunk<RealmProfilePinRelation>)chunk).Old
            .Select(old =>
            {
                OnMigrate();
                return Map(ef, old);
            })
            .DistinctBy(x => (x.PinId, x.PublisherId)); // Deduplicate here, after Map()

        foreach (ProfilePinRelation mapped in oldItems)
        {
            set.Add(mapped);
        }

        ef.SaveChanges();
    }

    protected override ProfilePinRelation Map(GameDatabaseContext ef, RealmProfilePinRelation old)
    {
        return new ProfilePinRelation()
        {
            PublisherId = old.Publisher.UserId,
            Timestamp = old.Timestamp,
            Game = old.Game,
            _Game = old._Game,
            Index = old.Index,
            PinId = old.PinId
        };
    }
}