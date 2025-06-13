using Realms;
using Refresh.Database;
using Refresh.Database.Models.Users;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility;

public abstract class UserDependentMigrator<TOld, TNew> : Migrator<TOld, TNew>
    where TOld : IRealmObject
    where TNew : class
{
    protected UserDependentMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}
    
    public override IEnumerable<Type> NeedsTypes { get; } = [typeof(GameUser)];
}