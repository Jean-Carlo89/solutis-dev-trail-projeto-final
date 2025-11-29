
using System.Net;

public class NotFoundException : DevError
{
    public NotFoundException(string message)
        : base(HttpStatusCode.NotFound, message)
    {
    }
}

