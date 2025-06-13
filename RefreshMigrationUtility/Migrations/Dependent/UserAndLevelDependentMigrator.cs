using Realms;
using Refresh.Database;
using Refresh.Database.Models.Levels;
using Refresh.Database.Models.Users;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility.Migrations.Dependent;

public abstract class UserAndLevelDependentMigrator<TOld, TNew> : Migrator<TOld, TNew>
    where TOld : IRealmObject
    where TNew : class
{
    public UserAndLevelDependentMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}
    
    public override IEnumerable<Type> NeedsTypes { get; } = [typeof(GameUser), typeof(GameLevel)];
}