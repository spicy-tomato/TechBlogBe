using JetBrains.Annotations;

namespace TBB.Data.Response.User;

public class SignUpResponse
{
    [UsedImplicitly]
    public string UserName { get; set; } = default!;

    [UsedImplicitly]
    public string FullName { get; set; } = default!;

    [UsedImplicitly]
    public string Email { get; set; } = default!;
}