using JetBrains.Annotations;

namespace TBB.Data.Response.User;

[UsedImplicitly(ImplicitUseTargetFlags.Members)]
public class SignUpResponse
{
    public string UserName { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
}