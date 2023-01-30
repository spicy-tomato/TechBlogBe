using AutoMapper;
using FluentValidation;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TBB.API.Core.Controllers;
using TBB.Common.Core.Exceptions;
using TBB.Common.Core.Helpers;
using TBB.Data.Core.Response;
using TBB.Data.Requests.Post;
using TBB.Data.Response.Post;
using TBB.Data.Response.User;
using TBB.Data.Validations;
using TechBlogBe.Repositories.Implementations;

namespace TechBlogBe.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : BaseController
{
    #region Properties

    private readonly PostRepository _postRepository;
    private readonly TagRepository _tagRepository;

    #endregion

    #region Constructor

    public PostController(IMapper mapper, PostRepository postRepository, TagRepository tagRepository) : base(mapper)
    {
        _postRepository = postRepository;
        _tagRepository = tagRepository;
    }

    #endregion

    #region Public Methods

    [HttpGet]
    public Result<GetAllPostResponse> GetAll()
    {
        var userName = Request.Query["userName"].ToString();
        var hasOffset = DateTime.TryParse(Request.Query["offset"].ToString(), out var offset);
        var hasSize = int.TryParse(Request.Query["size"].ToString(), out var size);
        return _postRepository.GetAll(userName, hasOffset ? offset : null, hasSize ? size : null);
    }

    [HttpPost]
    [Authorize]
    public async Task<Result<string>> Create(CreatePostRequest request)
    {
        await new CreatePostValidator().ValidateAndThrowAsync(request);
        var userId = GetUserId();

        _tagRepository.CreateRange(request.Tags);
        var tags = await _tagRepository.FindAsync(tag => request.Tags.Contains(tag.Name));
        var timeToRead = CalculateTimeToRead(request.Body);

        var createdPost = _postRepository.Create(request, userId, tags, timeToRead);
        var userName = GetUserName();
        var postUrl = $"/u/{userName}/{createdPost.Id}";

        return Result<string>.Get(postUrl);
    }

    [HttpGet("{postId}")]
    public Result<GetPostResponse> GetPostById(string postId)
    {
        if (postId.IsNullOrEmpty())
        {
            throw new BadRequestException("Post ID cannot be empty");
        }

        var result = _postRepository.GetById(postId);
        if (result == null)
        {
            throw new NotFoundException("Post ID does not exist");
        }

        var post = Mapper.Map<GetPostResponse>(result);
        return Result<GetPostResponse>.Get(post);
    }

    #endregion

    #region Private Methods

    private static int CalculateTimeToRead(string postBody)
    {
        var document = new HtmlDocument();
        document.LoadHtml(postBody);
        return CountWords(document.DocumentNode) / 250 + 2;
    }

    private static int CountWords(HtmlNode node)
    {
        var timeToRead = 0;
        foreach (var n in node.ChildNodes)
        {
            switch (n.NodeType)
            {
                case HtmlNodeType.Element:
                    timeToRead += CountWords(n);
                    break;
                case HtmlNodeType.Text:
                    timeToRead += StringHelper.Trim(n.InnerText).Split(' ').Length;
                    break;
                case HtmlNodeType.Document:
                case HtmlNodeType.Comment:
                default:
                    break;
            }
        }

        return timeToRead;
    }

    #endregion
}