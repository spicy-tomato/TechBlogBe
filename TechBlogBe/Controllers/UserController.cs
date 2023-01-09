using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechBlogBe.Models;
using TechBlogBe.Services;

namespace TechBlogBe.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly JwtService _jwtService;

    public UserController(UserManager<User> userManager, JwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    [HttpPost]
    public async Task<ActionResult<User>> Create(SignUpModel user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userManager.CreateAsync(
            user.ToUser(),
            user.Password
        );

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        user.Password = null;
        return CreatedAtAction("Get", new { username = user.UserName }, user);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<User>> Get(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return NotFound();
        }

        return new User
        {
            UserName = user.UserName,
            Email = user.Email
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponse>> CreateBearerToken(AuthenticationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Bad credentials");
        }

        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return Unauthorized();
        }

        var token = _jwtService.CreateToken(user);
        return Ok(token);
    }
}