using JetBrains.Annotations;

namespace TBB.Data.Requests.User;

[UsedImplicitly(ImplicitUseTargetFlags.Members)]
public class SignUpRequest
{
    public string UserName { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}