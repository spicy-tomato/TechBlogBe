using System.ComponentModel.DataAnnotations;

namespace TechBlogBe.Models;

public class CreatePostModel
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Body { get; set; }

    [Required]
    public PostMode Mode { get; set; }

    public Post ToPost() =>
        new()
        {
            Title = Title,
            Body = Body,
            Mode = Mode
        };
}