using System.ComponentModel.DataAnnotations.Schema;

namespace TechBlogBe.Models;

public class Comment
{
    public Comment()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }

    [Column(TypeName = "nvarchar(1000)")]
    public string Body { get; set; }

    public int LikeCount { get; set; }

    public int ShareCount { get; set; }

    public Post ReplyTo { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; }

    public DateTime DeletedAt { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }
}