using Joana.Application.DTOs.AuthDTOS;

namespace Joana.Application.Interfaces;

public interface IAuthService
{
    Task<string> LoginAsync(LoginDto dto);
}
