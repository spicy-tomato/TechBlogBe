using JetBrains.Annotations;

namespace TBB.Data.Requests.User;

[UsedImplicitly(ImplicitUseTargetFlags.Members)]
public class LoginRequest
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
}