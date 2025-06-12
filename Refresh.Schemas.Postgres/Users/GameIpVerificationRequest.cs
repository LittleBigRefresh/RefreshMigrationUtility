using MongoDB.Bson;

namespace Refresh.Database.Models.Users;

#nullable disable
[PrimaryKey(nameof(UserId), nameof(IpAddress))]
public partial class GameIpVerificationRequest
{
    [ForeignKey(nameof(UserId))]
    public GameUser User { get; set; }
    
    public ObjectId UserId { get; set; }
    
    public string IpAddress { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}