using Refresh.Database.Models.Users;

namespace Refresh.Database.Models.Relations;
#nullable disable

[MapTo("FavouriteUserRelation")]
public partial class RealmFavouriteUserRelation : IRealmObject
{
    public RealmGameUser UserToFavourite { get; set; }
    public RealmGameUser UserFavouriting { get; set; }
    
    public DateTimeOffset Timestamp { get; set; }
}