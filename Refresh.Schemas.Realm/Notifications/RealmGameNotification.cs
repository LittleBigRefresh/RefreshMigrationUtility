using MongoDB.Bson;
using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Notifications;

#nullable disable

[JsonObject(MemberSerialization.OptOut)]
[Serializable]
[MapTo("GameNotification")]
public partial class RealmGameNotification : IRealmObject
{
    public ObjectId NotificationId { get; set; } = ObjectId.GenerateNewId();
    public string Title { get; set; }
    public string Text { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    [JsonIgnore] public RealmGameUser User { get; set; }
    
    public string FontAwesomeIcon { get; set; }
}