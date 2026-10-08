using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Joana.Web.Models;
using Microsoft.JSInterop;

namespace Joana.Web.Services;

public class AuthService
{
    private const string TokenStorageKey = "authToken";
    private readonly HttpClient _http;
    private readonly IJSRuntime _js;
    private string? _cachedToken;

    public event Action? OnAuthStateChanged;

    public AuthService(HttpClient http, IJSRuntime js)
    {
        _http = http;
        _js = js;
    }

    public async Task<ApiResult<TokenResponseModel>> LoginAsync(string email, string lozinka)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", new LoginModel { Email = email, Lozinka = lozinka });

            if (!response.IsSuccessStatusCode)
            {
                var error = await ApiErrorParser.ExtractErrorMessageAsync(response);
                return ApiResult<TokenResponseModel>.Fail(error, (int)response.StatusCode);
            }

            var result = await response.Content.ReadFromJsonAsync<TokenResponseModel>();
            if (result is null || string.IsNullOrWhiteSpace(result.Token))
                return ApiResult<TokenResponseModel>.Fail("Neuspešna prijava.", (int)response.StatusCode);

            await SetTokenAsync(result.Token);
            OnAuthStateChanged?.Invoke();
            return ApiResult<TokenResponseModel>.Ok(result, (int)response.StatusCode);
        }
        catch (HttpRequestException ex)
        {
            return ApiResult<TokenResponseModel>.Fail($"Ne mogu da se povežem sa serverom: {ex.Message}", 0);
        }
    }

    public async Task LogoutAsync()
    {
        _cachedToken = null;
        await _js.InvokeVoidAsync("localStorage.removeItem", TokenStorageKey);
        OnAuthStateChanged?.Invoke();
    }

    public async Task<bool> IsLoggedInAsync() => await GetTokenAsync() is not null;

    public async Task<string?> GetTokenAsync()
    {
        if (_cachedToken is not null) return _cachedToken;
        _cachedToken = await _js.InvokeAsync<string?>("localStorage.getItem", TokenStorageKey);
        return _cachedToken;
    }

    public async Task<int?> GetKupacIdAsync()
    {
        var claims = await GetClaimsAsync();
        return claims is not null && claims.TryGetValue("sub", out var sub) && int.TryParse(sub, out var id)
            ? id
            : null;
    }

    public async Task<string?> GetEmailAsync()
    {
        var claims = await GetClaimsAsync();
        return claims is not null && claims.TryGetValue("email", out var email) ? email : null;
    }

    public async Task<string?> GetRoleAsync()
    {
        var claims = await GetClaimsAsync();
        return claims is not null && claims.TryGetValue("role", out var role) ? role : null;
    }

    public async Task<bool> IsAdminAsync() => await GetRoleAsync() == "Administrator";

    private async Task SetTokenAsync(string token)
    {
        _cachedToken = token;
        await _js.InvokeVoidAsync("localStorage.setItem", TokenStorageKey, token);
    }

    private async Task<Dictionary<string, string>?> GetClaimsAsync()
    {
        var token = await GetTokenAsync();
        return token is null ? null : ParseClaims(token);
    }

    private static Dictionary<string, string> ParseClaims(string jwt)
    {
        var parts = jwt.Split('.');
        if (parts.Length < 2) return new Dictionary<string, string>();

        var payload = parts[1].Replace('-', '+').Replace('_', '/');
        payload = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');

        var bytes = Convert.FromBase64String(payload);
        var json = Encoding.UTF8.GetString(bytes);

        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.EnumerateObject()
            .ToDictionary(p => p.Name, p => p.Value.ToString());
    }
}
