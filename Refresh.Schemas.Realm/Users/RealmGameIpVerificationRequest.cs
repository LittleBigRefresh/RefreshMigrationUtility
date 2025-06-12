namespace Refresh.Database.Models.Users;

#nullable disable
[MapTo("GameIpVerificationRequest")]
public partial class RealmGameIpVerificationRequest : IRealmObject
{
    public RealmGameUser User { get; set; }
    

    public string IpAddress { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}