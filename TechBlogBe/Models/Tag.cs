using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechBlogBe.Models;

public class Tag
{
    public Tag()
    {
        Id = Guid.NewGuid().ToString();
    }
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]
    public string name { get; set; }

    public int TrendingPoint { get; set; }

    public ICollection<Post> Posts { get; set; }
}