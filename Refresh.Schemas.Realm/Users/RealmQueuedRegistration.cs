using MongoDB.Bson;

namespace Refresh.Database.Models.Users;

#nullable disable

/// <summary>
/// A registration request waiting to be activated.
/// </summary>
[MapTo("QueuedRegistration")]
public partial class RealmQueuedRegistration : IRealmObject
{
    [PrimaryKey] public ObjectId RegistrationId { get; set; } = ObjectId.GenerateNewId();
    [Indexed] public string Username { get; set; } = string.Empty;
    [Indexed] public string EmailAddress { get; set; } = string.Empty;
    public string PasswordBcrypt { get; set; }
    
    public DateTimeOffset ExpiryDate { get; set; }
}