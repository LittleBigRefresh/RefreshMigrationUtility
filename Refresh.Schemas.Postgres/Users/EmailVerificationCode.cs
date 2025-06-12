using MongoDB.Bson;

namespace Refresh.Database.Models.Users;

#nullable disable
[PrimaryKey(nameof(UserId), nameof(Code))]
public partial class EmailVerificationCode
{
    [ForeignKey(nameof(UserId))]
    public GameUser User { get; set; }
    public string Code { get; set; }
    
    public ObjectId UserId { get; set; }
    
    public DateTimeOffset ExpiryDate { get; set; }
}