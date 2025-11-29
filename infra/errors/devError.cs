using System.Net;

public class DevError : Exception
{
    public HttpStatusCode StatusCode { get; set; }

    public DevError(HttpStatusCode statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }

    public DevError(string? message) : base(message)
    {
    }
}