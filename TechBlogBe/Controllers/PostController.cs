using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechBlogBe.Models;
using TechBlogBe.Repositories;

namespace TechBlogBe.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly PostRepository _postRepository;

    public PostController(PostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    [HttpGet]
    public IEnumerable<Post> GetAll() => _postRepository.GetAll();

    [HttpPost]
    [Authorize]
    public ActionResult<string> Create(CreatePostModel createPostModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var post = createPostModel.ToPost();
        post.UserId = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;

        try
        {
            var createdPost = _postRepository.Create(post);
            return createdPost.Id;
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Cannot create new post " + e.Message);
        }
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
}