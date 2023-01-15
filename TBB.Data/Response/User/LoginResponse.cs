using JetBrains.Annotations;

namespace TBB.Data.Response.User;

public class LoginResponse
{
    [UsedImplicitly]
    public string Token { get; set; } = default!;

    [UsedImplicitly]
    public DateTime Expiration { get; set; }
}