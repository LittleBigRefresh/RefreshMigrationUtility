namespace Refresh.Database.Models.Users;

#nullable disable

[MapTo("DisallowedUser")]
public partial class RealmDisallowedUser : IRealmObject
{
    [PrimaryKey]
    public string Username { get; set; }
}