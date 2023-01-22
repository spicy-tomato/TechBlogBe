using JetBrains.Annotations;
using TBB.Data.Dtos.User;

namespace TBB.Data.Response.User;

[UsedImplicitly(ImplicitUseTargetFlags.Members)]
public class GetPostResponse
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Body { get; set; } = null!;
    public string Image { get; set; } = null!;
    public int ViewCount { get; set; }
    public int LikeCount { get; set; }
    public int ShareCount { get; set; }
    public int TrendingPoint { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public SimpleUserDto User { get; set; } = null!;
    public ICollection<string> Tags { get; set; } = new List<string>();
}