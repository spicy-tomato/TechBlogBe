using JetBrains.Annotations;
using TBB.Common.Core;

namespace TBB.Data.Core.Response;

public class Result<T>
{
    [UsedImplicitly]
    public T Data { get; set; } = default!;

    [UsedImplicitly]
    public bool Success { get; set; }

    public IEnumerable<Error>? Errors { [UsedImplicitly] get; set; }

    public static Result<T1> Get<T1>(T1 data)
    {
        return new Result<T1>
        {
            Success = true,
            Data = data
        };
    }
}