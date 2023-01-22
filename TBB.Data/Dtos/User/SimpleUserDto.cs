using JetBrains.Annotations;

namespace TBB.Data.Dtos.User;

[UsedImplicitly(ImplicitUseTargetFlags.Members)]
public class SimpleUserDto
{
    public string FullName = null!;
    public string UserName = null!;
    public string Email = null!;
}