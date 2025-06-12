using Refresh.Database.Models.Levels;
using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Relations;

#nullable disable
[MapTo("UniquePlayLevelRelation")]
public partial class RealmUniquePlayLevelRelation : IRealmObject
{
    public RealmGameLevel Level { get; set; }
    public RealmGameUser User { get; set; }

    public DateTimeOffset Timestamp { get; set; }
}