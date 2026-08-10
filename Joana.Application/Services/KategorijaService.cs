using Joana.Application.DTOs.KategorijaDTOS;
using Joana.Application.Interfaces;
using Joana.Domain;

namespace Joana.Application.Services;

public class KategorijaService:IKategorijaService
{
    private readonly IKategorijaRepository _repo;

    public KategorijaService(IKategorijaRepository repos)
    {
        _repo = repos;
    }
    public async Task<List<KategorijaDto>> GetAllAsync()
    {
        var kategorije = await _repo.GetAsync();
        return kategorije.Select(k => new KategorijaDto
        {
            Id = k.IdKategorija,
            NazivKategorije = k.NazivKategorije
        }).ToList();
    }
    public async Task<KategorijaDto?> GetByIdAsync(int id)
    {
        var kategorija = await _repo.GetByIdAsync(id);
        if (kategorija == null) return null;
        return new KategorijaDto
        {
            Id = kategorija.IdKategorija,
            NazivKategorije =  kategorija.NazivKategorije
        };
    }
    public async Task<KategorijaDto> CreateAsync(CreateKategorijaDto kategorijaDto)
    {
        var kategorija = new Kategorija(kategorijaDto.NazivKategorije);
        await _repo.AddAsync(kategorija);
        return new KategorijaDto
        {
            Id = kategorija.IdKategorija,
            NazivKategorije = kategorija.NazivKategorije
        };
    }
}