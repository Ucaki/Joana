using System.Net;
using System.Text.Json;

namespace Joana.Web.Services;

public static class ApiErrorParser
{
    public static async Task<string> ExtractErrorMessageAsync(HttpResponseMessage response)
    {
        var raw = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(raw))
            return DefaultMessageFor(response.StatusCode);

        try
        {
            using var doc = JsonDocument.Parse(raw);
            var root = doc.RootElement;
            
            if (root.TryGetProperty("errors", out var errorsElement) && errorsElement.ValueKind == JsonValueKind.Object)
            {
                var messages = errorsElement.EnumerateObject()
                    .SelectMany(p => p.Value.EnumerateArray().Select(v => v.GetString()))
                    .Where(m => !string.IsNullOrWhiteSpace(m));
                var joined = string.Join(" ", messages);
                if (!string.IsNullOrWhiteSpace(joined)) return joined;
            }
            
            if (root.TryGetProperty("message", out var messageElement) && messageElement.ValueKind == JsonValueKind.String)
            {
                var msg = messageElement.GetString();
                if (!string.IsNullOrWhiteSpace(msg)) return msg;
            }

        }
        catch (JsonException)
        {
        }

        return DefaultMessageFor(response.StatusCode);
    }

    private static string DefaultMessageFor(HttpStatusCode statusCode) => statusCode switch
    {
        HttpStatusCode.Unauthorized => "Niste prijavljeni ili je sesija istekla.",
        HttpStatusCode.Forbidden => "Nemate dozvolu za ovu akciju.",
        HttpStatusCode.NotFound => "Traženi podatak nije pronađen.",
        _ => $"Greška prilikom komunikacije sa serverom ({(int)statusCode})."
    };
}
