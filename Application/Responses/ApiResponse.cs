using System.Net;
namespace Application.Responses;

public class ApiResponse<T>
{
    public HttpStatusCode StatusCode { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public ApiResponse(HttpStatusCode statusCode, string? message, T? data)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }
}
