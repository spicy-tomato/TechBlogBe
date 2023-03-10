using System.Net;

namespace TBB.Common.Core.Exceptions;

public class NotFoundException : InnerException
{
    private const HttpStatusCode CODE = HttpStatusCode.NotFound;

    public NotFoundException(string? message, Exception? innerException = null) : base(message, innerException) { }

    public override HttpException WrapException()
    {
        var errorResponse = new List<Error> { new(CODE.GetHashCode(), Message) };
        return new HttpException(CODE, errorResponse, Message, this);
    }
}