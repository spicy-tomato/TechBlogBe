using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechBlogBe.Models;

namespace TechBlogBe.Contexts;

public class ApplicationContext : IdentityUserContext<User>
{
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public override DbSet<User> Users { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}