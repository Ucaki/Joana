using Joana.Application.DTOs.KupacDTOS;
using Joana.Application.Interfaces;
using Joana.Application.Security;
using Joana.Domain;

namespace Joana.Application.Services;

public class KupacService : IKupacService
{
    private readonly IKupacRepository _repo;

    public KupacService(IKupacRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<KupacDto>> GetByNameAsync(string ime, int page = 1, int pageSize = 10)
    {
        var kupci = await _repo.GetByNameAsync(ime, page, pageSize);
        return kupci.Select(MapToDto).ToList();
    }

    public async Task<KupacDto?> GetByEmailAsync(string email)
    {
        var kupac = await _repo.GetByEmailAsync(email);
        return kupac == null ? null : MapToDto(kupac);
    }

    public async Task<KupacDto> AddAsync(CreateKupacDto dto)
    {
        var lozinkaHash = PasswordHasher.Hash(dto.Lozinka);
        var kupac = new Kupac(dto.Ime, dto.Prezime, dto.Email, lozinkaHash, dto.Telefon, dto.Grad, dto.Adresa);

        await _repo.AddAsync(kupac);
        return MapToDto(kupac);
    }

    public async Task<KupacDto?> UpdateAsync(int kupacId, CreateKupacDto dto)
    {
        var kupac = await _repo.GetByIdAsync(kupacId);
        if (kupac == null) return null;

        kupac.PromeniEmail(dto.Email);
        kupac.PromeniLozinku(PasswordHasher.Hash(dto.Lozinka));
        kupac.PromeniTelefon(dto.Telefon);
        kupac.PromeniAdresu(dto.Adresa);
        kupac.PromeniGrad(dto.Grad);

        await _repo.UpdateAsync(kupac);
        return MapToDto(kupac);
    }

    public async Task DeleteAsync(int id)
    {
        var kupac = await _repo.GetByIdAsync(id);
        if (kupac == null) return;
        await _repo.DeleteAsync(kupac);
    }

    private static KupacDto MapToDto(Kupac kupac) => new KupacDto
    {
        Id = kupac.IdKorisnik,
        Ime = kupac.Ime,
        Prezime = kupac.Prezime,
        Email = kupac.Email,
        Telefon = kupac.Telefon,
        Adresa = kupac.Adresa,
        Grad = kupac.Grad
    };
}