using System.Net;

namespace TBB.Common.Core.Exceptions;

public class ConflictException : InnerException
{
    private const HttpStatusCode CODE = HttpStatusCode.Conflict;

    public ConflictException(string? message, Exception? innerException = null) : base(message, innerException) { }

    public override HttpException WrapException()
    {
        var errorResponse = new List<Error> { new(CODE.GetHashCode(), Message) };
        return new HttpException(CODE, errorResponse, Message, this);
    }
}