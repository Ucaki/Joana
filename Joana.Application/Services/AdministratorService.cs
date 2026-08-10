using Joana.Application.DTOs.AdministratorDTOS;
using Joana.Application.Interfaces;

namespace Joana.Application.Services;

public class AdministratorService : IAdministratorService
{
    private readonly IAdministratorRepository _repo;

    public AdministratorService(IAdministratorRepository repo)
    {
        _repo = repo;
    }

    public async Task<AdministratorDto?> GetByEmailAsync(string email)
    {
        var administrator = await _repo.GetByEmailAsync(email);
        if (administrator == null) return null;

        return new AdministratorDto
        {
            Id = administrator.IdKorisnik,
            Ime = administrator.Ime,
            Prezime = administrator.Prezime,
            Email = administrator.Email
        };
    }
}