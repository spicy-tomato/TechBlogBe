
using JetBrains.Annotations;

namespace TBB.Data.Response.User;

[UsedImplicitly(ImplicitUseTargetFlags.Members)]
public class GetUserResponse
{
    public string FullName { get; set; } = null!;
    public string UserName { get; set; } = null!;
}