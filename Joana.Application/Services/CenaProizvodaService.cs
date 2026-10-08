using Joana.Application.DTOs.CenaProizvodaDTOS;
using Joana.Application.Interfaces;
using Joana.Domain;

namespace Joana.Application.Services;

public class CenaProizvodaService : ICenaProizvodaService
{
    private readonly ICenaProizvodaRepository _cenaRepo;
    private readonly IProizvodRepository _proizvodRepo;

    public CenaProizvodaService(ICenaProizvodaRepository cenaRepo, IProizvodRepository proizvodRepo)
    {
        _cenaRepo = cenaRepo;
        _proizvodRepo = proizvodRepo;
    }

    public async Task<List<CenaProizvodaDto>> GetAsync(int proizvodId)
    {
        var cene = await _cenaRepo.GetByProizvodIdAsync(proizvodId);
        return cene.Select(MapToDto).ToList();
    }
    public async Task<CenaProizvodaDto> CreateAsync(CreateCenaProizvodaDto dto)
    {
        var proizvod = await _proizvodRepo.GetByIdAsync(dto.IdProizvod);
        if (proizvod == null) throw new ArgumentException($"Proizvod sa ID {dto.IdProizvod} ne postoji.");
    
        await _cenaRepo.DeactivateAllAsync(dto.IdProizvod);
        
        var cena = new CenaProizvoda(dto.IdProizvod, dto.Cena);
        await _cenaRepo.AddAsync(cena);

        return new CenaProizvodaDto
        {
            ProizvodNaziv = proizvod.Naziv,
            CreatedAt = cena.CreatedAt,
            Cena = cena.Cena
        };
    }

    private static CenaProizvodaDto MapToDto(CenaProizvoda cena) => new CenaProizvodaDto
    {
        ProizvodNaziv = cena.Proizvod.Naziv,
        CreatedAt = cena.CreatedAt,
        Cena = cena.Cena
    };
}