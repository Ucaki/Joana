using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Joana.Web.Services;

public class ApiService
{
    private readonly HttpClient _http;
    private readonly AuthService _authService;

    public ApiService(HttpClient http, AuthService authService)
    {
        _http = http;
        _authService = authService;
    }

    public Task<ApiResult<T>> GetAsync<T>(string uri) =>
        SendAsync<T>(() => _http.GetAsync(uri));

    public Task<ApiResult<TResponse>> PostAsync<TRequest, TResponse>(string uri, TRequest body) =>
        SendAsync<TResponse>(() => _http.PostAsJsonAsync(uri, body));

    public Task<ApiResult<TResponse>> PutAsync<TRequest, TResponse>(string uri, TRequest body) =>
        SendAsync<TResponse>(() => _http.PutAsJsonAsync(uri, body));

    public async Task<ApiResult<bool>> DeleteAsync(string uri)
    {
        var result = await SendAsync<object?>(() => _http.DeleteAsync(uri));
        return result.Success
            ? ApiResult<bool>.Ok(true, result.StatusCode)
            : ApiResult<bool>.Fail(result.ErrorMessage ?? "Greška.", result.StatusCode);
    }

    private async Task<ApiResult<T>> SendAsync<T>(Func<Task<HttpResponseMessage>> sendRequest)
    {
        await AddAuthHeaderAsync();

        try
        {
            var response = await sendRequest();
            return await BuildResultAsync<T>(response);
        }
        catch (HttpRequestException ex)
        {
            return ApiResult<T>.Fail($"Ne mogu da se povežem sa serverom: {ex.Message}", 0);
        }
    }

    private async Task AddAuthHeaderAsync()
    {
        var token = await _authService.GetTokenAsync();
        _http.DefaultRequestHeaders.Authorization =
            token is null ? null : new AuthenticationHeaderValue("Bearer", token);
    }

    private static async Task<ApiResult<T>> BuildResultAsync<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
                return ApiResult<T>.Ok(default, (int)response.StatusCode);

            var data = await response.Content.ReadFromJsonAsync<T>();
            return ApiResult<T>.Ok(data, (int)response.StatusCode);
        }

        var errorMessage = await ApiErrorParser.ExtractErrorMessageAsync(response);
        return ApiResult<T>.Fail(errorMessage, (int)response.StatusCode);
    }
}
