using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Relations;

#nullable disable

[MapTo("GameUserVerifiedIpRelation")]
public partial class RealmGameUserVerifiedIpRelation : IRealmObject
{
    public RealmGameUser User { get; set; }
    public string IpAddress { get; set; }
    

    public DateTimeOffset VerifiedAt { get; set; }
}