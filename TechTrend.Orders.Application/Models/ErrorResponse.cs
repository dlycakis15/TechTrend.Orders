namespace TechTrend.Orders.Application.Models;

public class ErrorResponse
{
    public bool Success { get; set; } = false;
    public List<string> Errors { get; set; } = [];
    public string Message { get; set; }

    public ErrorResponse(string message)
    {
        Message = message;
    }

    public ErrorResponse()
    {
        
    }

    public void AddError(string error)
    {
        Errors.Add(error);
    }
}
