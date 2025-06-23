using Refresh.Database;
using Refresh.Database.Models;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility.Migrations.Migrators;

public class RequestStatisticsMigrator : Migrator<RealmRequestStatistics, RequestStatistics>, INeedsSequenceRecalculation
{
    public RequestStatisticsMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    protected override RequestStatistics Map(GameDatabaseContext ef, RealmRequestStatistics old)
    {
        return new RequestStatistics
        {
            ApiRequests = old.ApiRequests,
            GameRequests = old.GameRequests,
            TotalRequests = old.TotalRequests,
        };
    }

    public string SequenceName => "RequestStatistics_Id_seq";
}