namespace Joana.Web.Services;

public class ApiResult<T>
{
    public bool Success { get; init; }
    public T? Data { get; init; }
    public string? ErrorMessage { get; init; }
    public int StatusCode { get; init; }

    public static ApiResult<T> Ok(T? data, int statusCode) =>
        new() { Success = true, Data = data, StatusCode = statusCode };

    public static ApiResult<T> Fail(string errorMessage, int statusCode) =>
        new() { Success = false, ErrorMessage = errorMessage, StatusCode = statusCode };
}
