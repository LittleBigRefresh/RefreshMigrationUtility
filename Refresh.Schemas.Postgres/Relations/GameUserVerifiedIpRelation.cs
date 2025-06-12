using MongoDB.Bson;
using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Relations;

#nullable disable

#if POSTGRES
[PrimaryKey(nameof(UserId), nameof(IpAddress))]
#endif
public partial class GameUserVerifiedIpRelation
{
    [ForeignKey(nameof(UserId))]
    public GameUser User { get; set; }
    public string IpAddress { get; set; }
    
    public ObjectId UserId { get; set; }
    
    public DateTimeOffset VerifiedAt { get; set; }
}