using JetBrains.Annotations;
using TBB.Data.Dtos.Post;

namespace TBB.Data.Response.Post;

[UsedImplicitly(ImplicitUseTargetFlags.Members)]
public class GetAllPostResponse
{
    public ICollection<PostSummary> Posts = new List<PostSummary>();
}