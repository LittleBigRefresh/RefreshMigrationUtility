using MongoDB.Bson;
using Refresh.Database.Models.Levels;
using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Relations;

#nullable disable

#if POSTGRES
[PrimaryKey(nameof(LevelId), nameof(UserId))]
#endif
public partial class QueueLevelRelation
{
    [ForeignKey(nameof(LevelId))]
    public GameLevel Level { get; set; }
    [ForeignKey(nameof(UserId))]
    public GameUser User { get; set; }
    
    public int LevelId { get; set; }
    public ObjectId UserId { get; set; }
    public DateTimeOffset Timestamp { get; set; }
}