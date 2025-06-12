namespace Refresh.Database.Models.Relations;

#nullable disable

#if POSTGRES
[PrimaryKey(nameof(Dependent), nameof(Dependency))]
#endif
public partial class AssetDependencyRelation
{
    public string Dependent { get; set; }
    public string Dependency { get; set; }
}