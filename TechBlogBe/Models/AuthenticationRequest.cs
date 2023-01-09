using System.ComponentModel.DataAnnotations;

namespace TechBlogBe.Models;

public class AuthenticationRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}