namespace Refresh.Database.Models;

[MapTo("RequestStatistics")]
public partial class RealmRequestStatistics : IRealmObject
{
    public long TotalRequests { get; set; }
    public long ApiRequests { get; set; }
    public long GameRequests { get; set; }
}