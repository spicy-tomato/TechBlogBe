
using JetBrains.Annotations;

namespace TBB.Data.Response.User;

public class GetUserResponse
{
    [UsedImplicitly]
    public string FullName { get; set; } = null!;

    [UsedImplicitly]
    public string UserName { get; set; } = null!;
}