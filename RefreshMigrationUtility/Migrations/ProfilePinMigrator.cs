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
    
    public override void MigrateChunk(RealmDatabaseContext realm, GameDatabaseContext ef)
    {
        DbSet<ProfilePinRelation> set = ef.Set<ProfilePinRelation>();

        IEnumerable<ProfilePinRelation> chunk = realm.All<RealmProfilePinRelation>()
            .AsEnumerable()
            .Skip(Progress)
            .Take(TakeSize)
            .Select(old =>
            {
                Progress++;
                return Map(ef, old);
            })
            .DistinctBy(x => (x.PinId, x.PublisherId)); // Deduplicate here, after Map()

        foreach (ProfilePinRelation mapped in chunk)
        {
            set.Add(mapped);
        }

        ef.SaveChanges();
    }

    public override ProfilePinRelation Map(GameDatabaseContext ef, RealmProfilePinRelation old)
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