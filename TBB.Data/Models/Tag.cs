using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace TBB.Data.Models;

[UsedImplicitly(ImplicitUseTargetFlags.Members)]
public sealed class Tag
{
    [Key]
    public string Name { get; set; } = null!;

    public string Description { get; set; } = "";

    public int TrendingPoint { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();
}