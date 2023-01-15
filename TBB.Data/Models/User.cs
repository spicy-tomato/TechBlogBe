using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;

namespace TBB.Data.Models;

public class User : IdentityUser
{
    [UsedImplicitly]
    public string FullName { get; set; } = null!;

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