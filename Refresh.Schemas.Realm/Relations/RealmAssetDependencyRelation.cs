namespace Refresh.Database.Models.Relations;

#nullable disable

[MapTo("AssetDependencyRelation")]
public partial class RealmAssetDependencyRelation : IRealmObject
{
    public string Dependent { get; set; }
    public string Dependency { get; set; }
}