using Realms;
using Refresh.Database;
using Refresh.Database.Models.Levels;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility.Migrations.Dependent;

public abstract class LevelDependentMigrator<TOld, TNew> : Migrator<TOld, TNew>
    where TOld : IRealmObject
    where TNew : class
{
    public LevelDependentMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}
    
    public override IEnumerable<Type> NeedsTypes { get; } = [typeof(GameLevel)];
}