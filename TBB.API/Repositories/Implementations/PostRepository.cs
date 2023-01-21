using AutoMapper;
using TBB.Data.Models;
using TBB.Data.Requests.Post;
using TechBlogBe.Contexts;
using TechBlogBe.Repositories.Interface;

namespace TechBlogBe.Repositories.Implementations;

public class PostRepository : RepositoryBase<Post>, IPostRepository
{
    public PostRepository(ApplicationContext context, IMapper mapper) : base(context, mapper) { }

    public Post Create(CreatePostRequest entity, string userId)
    {
        var id = GenerateId(entity.Title);
        while (true)
        {
            var duplicatedId = Context.Posts.SingleOrDefault(e => e.Id == id);
            if (duplicatedId == null)
            {
                break;
            }

            id = GenerateIdWithRandomString(entity.Title);
        }

        var post = Mapper.Map<Post>(
            entity,
            options => options.AfterMap((_, des) =>
            {
                des.Id = id;
                des.UserId = userId;
            })
        );

        var result = Context.Posts.Add(post);

        Context.SaveChanges();
        return result.Entity;
    }

    private static string GenerateId(string postTitle) => string.Join("-", postTitle.ToLower().Split(" "));

    private static string GenerateIdWithRandomString(string postTitle)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var randomString = new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());

        var idFromTitle = GenerateId(postTitle);

        return idFromTitle + "-" + randomString;
    }
}