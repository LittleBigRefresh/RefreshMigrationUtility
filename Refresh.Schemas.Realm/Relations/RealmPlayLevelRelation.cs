using Refresh.Database.Models.Levels;
using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Relations;

#nullable disable

[MapTo("PlayLevelRelation")]
public partial class RealmPlayLevelRelation : IRealmObject
{
    public RealmGameLevel Level { get; set; }
    public RealmGameUser User { get; set; }

    public DateTimeOffset Timestamp { get; set; }
    public int Count { get; set; }
}