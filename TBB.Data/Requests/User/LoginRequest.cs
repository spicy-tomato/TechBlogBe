using JetBrains.Annotations;

namespace TBB.Data.Requests.User;

public class LoginRequest
{
    [UsedImplicitly]
    public string UserName { get; set; } = default!;

    [UsedImplicitly]
    public string Password { get; set; } = default!;
}