using System.Net;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TBB.API.Core.Controllers;
using TBB.Common.Core;
using TBB.Common.Core.Exceptions;
using TBB.Data.Core.Response;
using TBB.Data.Models;
using TBB.Data.Requests.User;
using TBB.Data.Response.User;
using TBB.Data.Validations;
using TechBlogBe.Services;

namespace TechBlogBe.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : BaseController
{
    private readonly UserManager<User> _userManager;
    private readonly JwtService _jwtService;

    public UserController(IMapper mapper, UserManager<User> userManager, JwtService jwtService) : base(mapper)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    [HttpPost]
    public async Task<Result<LoginResponse>> SignUp(SignUpRequest request)
    {
        await new SignUpValidator().ValidateAndThrowAsync(request);
        var userData = Mapper.Map<User>(request);

        var result = await _userManager.CreateAsync(
            userData,
            request.Password
        );

        if (result.Succeeded)
        {
            return await Login(new LoginRequest { UserName = request.UserName, Password = request.Password });
        }

        const string duplicate = "Duplicate";
        var isDuplicated = result.Errors.FirstOrDefault(e => e.Code.Contains(duplicate)) != null;
        var errorResponse = result.Errors
           .Select(e =>
                e.Code.Contains(duplicate)
                    ? new Error(e.Code.Replace(duplicate, ""), e.Description)
                    : new Error(e.Description)
            );

        throw new HttpException(
            isDuplicated ? HttpStatusCode.Conflict : HttpStatusCode.InternalServerError,
            errorResponse);
    }

    [HttpGet("{userId}")]
    public async Task<Result<GetUserResponse>> Get(string userId)
    {
        var result = await _userManager.FindByIdAsync(userId);
        if (result == null)
        {
            throw new NotFoundException("User does not exist!");
        }

        var user = new GetUserResponse
        {
            UserName = result.UserName,
            FullName = result.FullName
        };
        return Result<GetUserResponse>.Get(user);
    }

    [HttpPost("login")]
    public async Task<Result<LoginResponse>> Login(LoginRequest request)
    {
        await new LoginValidator().ValidateAndThrowAsync(request);

        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new UnauthorizedException();
        }

        var token = _jwtService.CreateToken(user);
        return Result<LoginResponse>.Get(token);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<Result<GetUserResponse>> MyProfile()
    {
        var userId = GetUserId();
        return await Get(userId);
    }
}