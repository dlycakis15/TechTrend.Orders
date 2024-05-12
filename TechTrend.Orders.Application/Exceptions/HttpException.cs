using System.Net;

namespace TechTrend.Orders.Application.Exceptions;

public class HttpException : Exception
{
    private readonly HttpStatusCode _httpStatusCode;
    
    public HttpException(HttpStatusCode httpStatusCode)
    {
        _httpStatusCode = httpStatusCode;
    }

    public HttpException(string? message, HttpStatusCode httpStatusCode) : base(message)
    {
        _httpStatusCode = httpStatusCode;
    }

    public HttpException(string? message, Exception? innerException, HttpStatusCode httpStatusCode) : base(message, innerException)
    {
        _httpStatusCode = httpStatusCode;
    }

    public HttpStatusCode GetHttpStatusCode()
    {
        return _httpStatusCode;
    }
}