using Imagekit.Sdk;

namespace TechBlogBe.Services;

public class ImageService
{
    private readonly IConfiguration _configuration;

    public ImageService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Result Upload(IFormFile file, string folder)
    {
        var imagekit = new ImagekitClient(
            _configuration["ImageKit:PublicKey"],
            _configuration["ImageKit:PrivateKey"],
            _configuration["ImageKit:UrlEndPoint"]
        );

        byte[] fileBytes;
        using var ms = new MemoryStream();
        {
            file.CopyTo(ms);
            fileBytes = ms.ToArray();
        }

        var base64String = Convert.ToBase64String(fileBytes);
        var ob = new FileCreateRequest
        {
            file = base64String,
            fileName = file.FileName,
            folder = folder,
            overwriteFile = false,
            useUniqueFileName = true
        };
        var result = imagekit.Upload(ob);
        return result;
    }
}