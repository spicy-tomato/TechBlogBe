using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace TBB.Data.Models;

public class Tag
{
    public Tag()
    {
        Id = Guid.NewGuid().ToString();
    }
    
    [UsedImplicitly]
    public string Id { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; } = null!;

    public int TrendingPoint { get; set; }

    public ICollection<Post> Posts { get; set; } = null!;
}