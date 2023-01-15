namespace TBB.Data.Models;

public class PostUser
{
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    public string PostId { get; set; } = null!;
    public Post Post { get; set; } = null!;

    public bool IsLiked { get; set; }
    public bool IsShared { get; set; }
    public bool IsSaved { get; set; }
    public int SearchCount { get; set; }
}