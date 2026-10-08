namespace Joana.Web.Models;

public class LoginModel
{
    public string Email { get; set; } = string.Empty;
    public string Lozinka { get; set; } = string.Empty;
}

public class TokenResponseModel
{
    public string Token { get; set; } = string.Empty;
}
