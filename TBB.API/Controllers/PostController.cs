using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TBB.API.Core.Controllers;
using TBB.Data.Core.Response;
using TBB.Data.Models;
using TBB.Data.Requests.Post;
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

    public PostController(PostRepository postRepository, ImageService imageService, TagRepository tagRepository)
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

        var createdPost = _postRepository.Create(request, userId);
        var userName = GetUserName();
        var postUrl = $"/{userName}/{createdPost.Id}";
        
        return Result<string>.Get(postUrl);
    }

    [HttpGet("{postId}")]
    public ActionResult<Post> GetPost(string postId)
    {
        try
        {
            var result = _postRepository.GetById(postId);
            if (result == null)
            {
                return NotFound();
            }

            return result;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
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