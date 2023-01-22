using JetBrains.Annotations;

namespace TBB.Data.Response.User;

[UsedImplicitly(ImplicitUseTargetFlags.Members)]
public class LoginResponse
{
    public string Token { get; set; } = default!;
    public DateTime Expiration { get; set; }
}