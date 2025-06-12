namespace Refresh.Database.Models.Users;

#nullable disable
[MapTo("EmailVerificationCode")]
public partial class RealmEmailVerificationCode : IRealmObject
{
    public RealmGameUser User { get; set; }
    public string Code { get; set; }

    public DateTimeOffset ExpiryDate { get; set; }
}