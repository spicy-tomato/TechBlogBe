using TBB.Data.Dtos.User;

namespace TBB.Data.Dtos.Post;

public class PostSummary
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Image { get; set; } = null!;
    public int LikeCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public SimpleUserDto Author { get; set; } = null!;
    public ICollection<string> Tags { get; set; } = new List<string>();
    public int TimeToRead { get; set; }
}