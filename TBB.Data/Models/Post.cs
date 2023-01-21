using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace TBB.Data.Models;

public sealed class Post
{
    [Key]
    [UsedImplicitly]
    public string Id { get; set; } = null!;

    [UsedImplicitly]
    public string Title { get; set; } = null!;

    [UsedImplicitly]
    public string Body { get; set; } = null!;

    [UsedImplicitly]
    public PostMode Mode { get; set; }
    
    [UsedImplicitly]
    public string Image { get; set; } = null!;

    public int ViewCount { get; set; }

    public int LikeCount { get; set; }

    public int ShareCount { get; set; }

    public int TrendingPoint { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; }

    public DateTime DeletedAt { get; set; }

    [UsedImplicitly]
    public string UserId { get; set; } = null!;

    [UsedImplicitly]
    public User User { get; set; } = null!;

    public ICollection<Tag> Tags { get; set; } = null!;
}