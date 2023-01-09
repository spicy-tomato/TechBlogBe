using TechBlogBe.Contexts;
using TechBlogBe.Models;

namespace TechBlogBe.Repositories;

public class PostRepository : RepositoryBase<Post>, IPostRepository
{
    public PostRepository(ApplicationContext context) : base(context) { }

    public new Post Create(Post entity)
    {
        var id = GenerateId(entity);
        while (true)
        {
            var duplicatedId = Context.Posts.SingleOrDefault(e => e.Id == id);
            if (duplicatedId == null)
            {
                break;
            }

            id = GenerateIdWithRandomString(entity);
        }

        entity.Id = id;
        var result = Context.Posts.Add(entity);
        Context.SaveChanges();
        return result.Entity;
    }

    private static string GenerateId(Post post) => string.Join("-", post.Title.ToLower().Split(" "));

    private static string GenerateIdWithRandomString(Post post)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var randomString = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());

        var idFromTitle = GenerateId(post);

        return idFromTitle + "-" + randomString;
    }
}