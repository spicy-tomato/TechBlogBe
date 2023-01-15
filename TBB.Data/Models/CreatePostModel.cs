using JetBrains.Annotations;

namespace TBB.Data.Models;

public class CreatePostModel
{
    [UsedImplicitly]
    public string Title { get; set; } = null!;

    [UsedImplicitly]
    public string Body { get; set; } = null!;

    [UsedImplicitly]
    public PostMode Mode { get; set; }

    public Post ToPost() =>
        new()
        {
            Title = Title,
            Body = Body,
            Mode = Mode
        };
}