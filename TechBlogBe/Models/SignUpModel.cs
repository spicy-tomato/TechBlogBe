using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TechBlogBe.Models;

public class SignUpModel
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string? Password { get; set; }

    public User ToUser() => new()
    {
        UserName = UserName,
        Name = Name,
        Email = Email,
    };
}