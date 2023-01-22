using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TBB.Data.Models;
using TBB.Data.Requests.Post;
using TechBlogBe.Contexts;
using TechBlogBe.Repositories.Interface;

namespace TechBlogBe.Repositories.Implementations;

public class PostRepository : RepositoryBase<Post>, IPostRepository
{
    public PostRepository(ApplicationContext context, IMapper mapper) : base(context, mapper) { }

    public Post Create(CreatePostRequest entity, IEnumerable<Tag> tags, string userId)
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

        var tagEnumerable = tags.ToList();
        Context.Set<Tag>().AttachRange(tagEnumerable);

        var post = Mapper.Map<Post>(
            entity,
            options => options.AfterMap((_, des) =>
            {
                des.Id = id;
                des.UserId = userId;
                des.Tags = tagEnumerable;
            })
        );

        var result = Context.Set<Post>().Add(post);

        Context.SaveChanges();
        return result.Entity;
    }

    public new Post? GetById(string id)
    {
        return Context.Set<Post>()
           .Include(p => p.Tags)
           .Include(p => p.User)
           .FirstOrDefault(p => p.Id == id);
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