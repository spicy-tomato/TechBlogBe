using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace TechBlogBe.Models;

public class User : IdentityUser
{
    [Required]
    public string Name { get; set; }

    //
    // public DateTime Birth { get; set; }
    //
    // public bool? IsMale { get; set; }
    //
    // public string Bio { get; set; } = string.Empty;
    //
    // public string Work { get; set; } = string.Empty;
    //
    // public string Education { get; set; } = string.Empty;
    //
    // public Role Role { get; set; }
    //
    // public int FollowerCount { get; set; }
    //
    // public int TrendingPoint { get; set; }
    //
    // public IdentityUser user { get; set; }
    //
    // public ICollection<Post> Posts { get; set; } = new List<Post>();
    //
    // public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}