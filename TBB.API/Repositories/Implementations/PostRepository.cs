using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TBB.Data.Core.Response;
using TBB.Data.Dtos.Post;
using TBB.Data.Models;
using TBB.Data.Requests.Post;
using TBB.Data.Response.Post;
using TechBlogBe.Contexts;
using TechBlogBe.Repositories.Interface;

namespace TechBlogBe.Repositories.Implementations;

public class PostRepository : RepositoryBase<Post>, IPostRepository
{
    #region Constructor

    public PostRepository(ApplicationContext context, IMapper mapper) : base(context, mapper) { }

    #endregion

    #region Public Methods

    public Post Create(CreatePostRequest entity, string userId, IEnumerable<Tag> tags, int timeToRead)
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
                des.TimeToRead = timeToRead;
            })
        );

        var result = Context.Set<Post>().Add(post);

        Context.SaveChanges();
        return result.Entity;
    }

    public new Result<GetAllPostResponse> GetAll()
    {
        var posts = Context.Set<Post>()
           .Include(p => p.Tags)
           .Include(p => p.User)
           .OrderByDescending(p => p.CreatedAt).ToList()
           .Select(p => Mapper.Map<PostSummary>(p)).ToList();
        return Result<GetAllPostResponse>.Get(new GetAllPostResponse { Posts = posts });
    }

    public new Post? GetById(string id)
    {
        var post = Context.Set<Post>()
           .Include(p => p.Tags)
           .Include(p => p.User).FirstOrDefault(p => p.Id == id);
        return post;
    }

    #endregion

    #region Private Methods

    private static string GenerateId(string postTitle) => string.Join("-", postTitle.ToLower().Split(" "));

    private static string GenerateIdWithRandomString(string postTitle)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var randomString = new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());

        var idFromTitle = GenerateId(postTitle);

        return idFromTitle + "-" + randomString;
    }

    #endregion
}