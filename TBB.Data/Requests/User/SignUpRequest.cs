using JetBrains.Annotations;

namespace TBB.Data.Requests.User;

public class SignUpRequest
{
    [UsedImplicitly]
    public string UserName { get; set; } = default!;

    [UsedImplicitly]
    public string FullName { get; set; } = default!;

    [UsedImplicitly]
    public string Email { get; set; } = default!;

    [UsedImplicitly]
    public string Password { get; set; } = default!;

    public Models.User ToUser() => new()
    {
        UserName = UserName,
        FullName = FullName,
        Email = Email
    };
}