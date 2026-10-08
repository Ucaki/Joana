using Joana.Application.DTOs.AuthDTOS;
using Joana.Application.Interfaces;
using Joana.Application.Security;

namespace Joana.Application.Services;

public class AuthService : IAuthService
{
    private const string RolaKupac = "Kupac";
    private const string RolaAdministrator = "Administrator";

    private readonly IKupacRepository _kupacRepo;
    private readonly IAdministratorRepository _administratorRepo;
    private readonly IJwtService _jwtService;

    public AuthService(IKupacRepository kupacRepo, IAdministratorRepository administratorRepo, IJwtService jwtService)
    {
        _kupacRepo = kupacRepo;
        _administratorRepo = administratorRepo;
        _jwtService = jwtService;
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var kupac = await _kupacRepo.GetByEmailAsync(dto.Email);
        if (kupac != null)
        {
            if (!PasswordHasher.Verify(dto.Lozinka, kupac.LozinkaHash))
                throw new UnauthorizedAccessException("Pogrešan email ili lozinka.");

            return _jwtService.GenerateToken(kupac.IdKorisnik, kupac.Email, RolaKupac);
        }

        var administrator = await _administratorRepo.GetByEmailAsync(dto.Email);
        if (administrator != null)
        {
            if (!PasswordHasher.Verify(dto.Lozinka, administrator.LozinkaHash))
                throw new UnauthorizedAccessException("Pogrešan email ili lozinka.");

            return _jwtService.GenerateToken(administrator.IdKorisnik, administrator.Email, RolaAdministrator);
        }

        throw new UnauthorizedAccessException("Pogrešan email ili lozinka.");
    }
}
