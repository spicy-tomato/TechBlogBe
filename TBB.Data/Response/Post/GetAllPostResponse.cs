using TBB.Data.Dtos.Post;

namespace TBB.Data.Response.Post;

public class GetAllPostResponse
{
    public ICollection<PostSummary> Posts = new List<PostSummary>();
}