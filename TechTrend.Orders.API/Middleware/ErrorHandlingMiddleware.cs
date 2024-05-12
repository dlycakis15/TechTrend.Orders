using System.Net;
using System.Text.Json;
using TechTrend.Orders.Application.Exceptions;
using TechTrend.Orders.Application.Models;

namespace TechTrend.Orders.API.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var message = "An unexpected error occurred.";
        
        if (exception is HttpException httpException)
        {
            code = httpException.GetHttpStatusCode();
            message = httpException.Message;
        }
        
        var result = JsonSerializer.Serialize(new ErrorResponse() { Errors = [message] });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}