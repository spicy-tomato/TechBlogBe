using JetBrains.Annotations;
using TBB.Data.Models;

namespace TBB.Data.Requests.Post;

[UsedImplicitly(ImplicitUseTargetFlags.Members)]
public class CreatePostRequest
{
    public string Title { get; set; } = null!;
    public string Body { get; set; } = null!;
    public PostMode Mode { get; set; }
    public string Image { get; set; } = null!;
    public ICollection<string> Tags { get; set; } = null!;
}