using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace TBB.Data.Models;

public class Tag
{
    [Key]
    [UsedImplicitly]
    public string Name { get; set; } = null!;

    [UsedImplicitly]
    public string Description { get; set; } = "";

    [UsedImplicitly]
    public int TrendingPoint { get; set; }

    [UsedImplicitly]
    public ICollection<Post> Posts { get; set; } = null!;
}