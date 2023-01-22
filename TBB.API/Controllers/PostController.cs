using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TBB.API.Core.Controllers;
using TBB.Common.Core.Exceptions;
using TBB.Data.Core.Response;
using TBB.Data.Models;
using TBB.Data.Requests.Post;
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
    public IEnumerable<Post> GetAll() => _postRepository.GetAll();

    [HttpPost]
    [Authorize]
    public Result<string> Create(CreatePostRequest request)
    {
        new CreatePostValidator().ValidateAndThrow(request);
        var userId = GetUserId();

        _tagRepository.CreateRange(request.Tags);
        var tags = _tagRepository.Find(tag => request.Tags.Contains(tag.Name));

        var createdPost = _postRepository.Create(request, tags, userId);
        var userName = GetUserName();
        var postUrl = $"/{userName}/{createdPost.Id}";

        return Result<string>.Get(postUrl);
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

    [HttpPost("cover-image")]
    [Authorize]
    public Result<Result> UploadCoverImage()
    {
        var file = Request.Form.Files[0];
        var result = _imageService.Upload(file, "ci");

        return Result<Result>.Get(result);
    }
}