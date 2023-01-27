using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TBB.API.Core.Controllers;
using TBB.Data.Core.Response;
using TechBlogBe.Services;

namespace TechBlogBe.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : BaseController
{
    #region Properties

    private readonly ImageService _imageService;

    #endregion

    #region Constructor

    public ImageController(IMapper mapper, ImageService imageService) : base(mapper)
    {
        _imageService = imageService;
    }

    #endregion

    [HttpPost("upload/{folder}")]
    [Authorize]
    public Result<Result> UploadImage(string folder)
    {
        var file = Request.Form.Files[0];
        var result = _imageService.Upload(file, folder);

        return Result<Result>.Get(result);
    }
}