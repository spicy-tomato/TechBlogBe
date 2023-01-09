using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechBlogBe.Models;

public class Post
{
    [Key]
    public string Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(1000)")]
    public string Title { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(1000)")]
    public string Body { get; set; }

    public PostMode Mode { get; set; }
    
    public int ViewCount { get; set; }
    
    public int LikeCount { get; set; }
    
    public int ShareCount { get; set; }
    
    public int TrendingPoint { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; }

    public DateTime DeletedAt { get; set; }
    
    public string UserId { get; set; }
    public virtual User User { get; set; }

    public ICollection<Tag> Tags { get; set; }
}