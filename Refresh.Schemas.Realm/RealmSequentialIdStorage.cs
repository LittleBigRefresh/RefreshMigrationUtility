namespace Refresh.Database.Models;

#nullable disable

[MapTo("SequentialIdStorage")]
public partial class RealmSequentialIdStorage : IRealmObject
{
    public string TypeName { get; set; }
    public int SequentialId { get; set; }
}