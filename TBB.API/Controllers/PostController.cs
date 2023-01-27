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
using TechBlogBe.Services;

namespace TechBlogBe.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : BaseController
{
    private readonly PostRepository _postRepository;
    private readonly TagRepository _tagRepository;
    private readonly ImageService _imageService;

    public PostController(IMapper mapper,
        PostRepository postRepository,
        ImageService imageService,
        TagRepository tagRepository) : base(mapper)
    {
        _postRepository = postRepository;
        _imageService = imageService;
        _tagRepository = tagRepository;
    }

    [HttpGet]
    public Result<GetAllPostResponse> GetAll() => _postRepository.GetAll();

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
        var postUrl = $"/{userName}/{createdPost.Id}";

        return Result<string>.Get(postUrl);
    }

    [HttpPost("cover-image")]
    [Authorize]
    public Result<Result> UploadCoverImage()
    {
        var file = Request.Form.Files[0];
        var result = _imageService.Upload(file, "ci");

        return Result<Result>.Get(result);
    }

    [HttpGet("{postId}")]
    public Result<GetPostResponse> GetPost(string postId)
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
}