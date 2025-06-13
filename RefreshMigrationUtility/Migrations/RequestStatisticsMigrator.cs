using Refresh.Database;
using Refresh.Database.Models;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility.Migrations;

public class RequestStatisticsMigrator : Migrator<RealmRequestStatistics, RequestStatistics>
{
    public RequestStatisticsMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    public override RequestStatistics Map(RealmRequestStatistics old)
    {
        return new RequestStatistics
        {
            ApiRequests = old.ApiRequests,
            GameRequests = old.GameRequests,
            TotalRequests = old.TotalRequests,
        };
    }
}