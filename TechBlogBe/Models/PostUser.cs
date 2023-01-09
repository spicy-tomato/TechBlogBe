namespace TechBlogBe.Models;

public class PostUser
{
    public string UserId { get; set; }
    public User User { get; set; }

    public string PostId { get; set; }
    public Post Post { get; set; }

    public bool IsLiked { get; set; }
    public bool IsShared { get; set; }
    public bool IsSaved { get; set; }
    public int SearchCount { get; set; }
}