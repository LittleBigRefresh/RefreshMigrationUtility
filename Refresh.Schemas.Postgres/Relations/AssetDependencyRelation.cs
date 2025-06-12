namespace Refresh.Database.Models.Relations;

#nullable disable

[PrimaryKey(nameof(Dependent), nameof(Dependency))]
public partial class AssetDependencyRelation
{
    public string Dependent { get; set; }
    public string Dependency { get; set; }
}