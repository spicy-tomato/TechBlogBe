using JetBrains.Annotations;
using TBB.Data.Models;

namespace TBB.Data.Requests.Post;

public class CreatePostRequest
{
    [UsedImplicitly]
    public string Title { get; set; } = null!;

    [UsedImplicitly]
    public string Body { get; set; } = null!;

    [UsedImplicitly]
    public PostMode Mode { get; set; }

    [UsedImplicitly]
    public string Image { get; set; } = null!;

    [UsedImplicitly]
    public ICollection<string> Tags { get; set; } = null!;
}