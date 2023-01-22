using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TBB.Common.Core.Exceptions;

namespace TBB.API.Core.Controllers;

public class BaseController : ControllerBase
{
    protected readonly IMapper Mapper;
    
    public BaseController(IMapper mapper)
    {
        Mapper = mapper;
    }

    public string GetUserId()
    {
        try
        {
            return User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
        }
        catch
        {
            throw new UnauthorizedException();
        }
    }

    public string GetUserName()
    {
        try
        {
            return User.Claims.First(i => i.Type == ClaimTypes.Name).Value;
        }
        catch
        {
            throw new UnauthorizedException();
        }
    }
}