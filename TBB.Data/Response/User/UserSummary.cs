
using JetBrains.Annotations;

namespace TBB.Data.Response.User;

[UsedImplicitly(ImplicitUseTargetFlags.Members)]
public class UserSummary
{
    public string FullName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime JoinedDate { get; set; }
}