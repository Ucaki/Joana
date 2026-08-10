using Joana.Application.DTOs.ProizvodDTOS;
using Joana.Application.Interfaces;
using Joana.Domain;

namespace Joana.Application.Services;
public class ProizvodService: IProizvodService
{
    private readonly IProizvodRepository _proizvodRepo;
    private readonly IKategorijaRepository _kategorijaRepo;
    public ProizvodService(IProizvodRepository proizvodRepo,  IKategorijaRepository kategorijaRepo)
    {
        _proizvodRepo = proizvodRepo;
        _kategorijaRepo = kategorijaRepo;
    }
    public async Task<List<ProizvodDto>> GetAllAsync(int  page = 1, int pageSize = 10)
    {
        var proizvodi = await _proizvodRepo.GetAsync(page, pageSize);
        return proizvodi.Select(p => new ProizvodDto
        {
            Id = p.IdProizvod,
            Naziv = p.Naziv,
            JedinicaMere = p.JedinicaMere,
            Opis = p.Opis,
            Lager = p.Lager,
            Kategorija = p.Kategorija.NazivKategorije,
            AktivnaCena = p.ListCenaProizvoda
                .OrderByDescending(c=>c.CreatedAt)
                .Select(c=> c.Cena)
                .FirstOrDefault()
        }).ToList();
    }
    public async Task<List<ProizvodDto>> GetAllByKategorijaAsync(int kategorijaId, int  page = 1, int pageSize = 10)
    {
        var proizvodi = await _proizvodRepo.GetByKategorijaAsync(kategorijaId, page, pageSize);
        return proizvodi.Select(p => new ProizvodDto
        {
            Id = p.IdProizvod,
            Naziv = p.Naziv,
            JedinicaMere = p.JedinicaMere,
            Opis = p.Opis,
            Lager = p.Lager,
            Kategorija = p.Kategorija.NazivKategorije,
            AktivnaCena = p.ListCenaProizvoda
                .OrderByDescending(c=>c.CreatedAt)
                .Select(c=> c.Cena)
                .FirstOrDefault()
        }).ToList();
    }
    public async Task<ProizvodDto?> GetByIdAsync(int proizvodId)
    {
        var proizvod= await _proizvodRepo.GetByIdAsync(proizvodId);
        if(proizvod==null) return null;
        return new ProizvodDto
        {
            Id = proizvod.IdProizvod,
            Naziv = proizvod.Naziv,
            JedinicaMere = proizvod.JedinicaMere,
            Opis = proizvod.Opis,
            Lager = proizvod.Lager,
            Kategorija = proizvod.Kategorija.NazivKategorije,
            AktivnaCena = proizvod.ListCenaProizvoda
                .OrderByDescending(c=>c.CreatedAt)
                .Select(c=> c.Cena)
                .FirstOrDefault()
        };
    }
    public async Task<ProizvodDto> AddAsync(CreateUpdateProizvodDto proizvodDto)
    {
        var kategorija = await _kategorijaRepo.GetByIdAsync(proizvodDto.KategorijaId);
        if (kategorija == null)  throw new ArgumentException($"Kategorija sa ID {proizvodDto.KategorijaId} ne postoji.");
        var proizvod = new Proizvod(proizvodDto.Naziv,proizvodDto.Opis, proizvodDto.JedinicaMere ,proizvodDto.Lager, proizvodDto.KategorijaId);

        await _proizvodRepo.AddAsync(proizvod);
        return new ProizvodDto
        {
            Id = proizvod.IdProizvod,
            Naziv = proizvod.Naziv,
            Opis = proizvod.Opis,
            JedinicaMere = proizvod.JedinicaMere,
            Lager = proizvod.Lager,
            Kategorija = kategorija.NazivKategorije,
            AktivnaCena = null, //novi proizvod nema cenu još. proizvod.ListCenaProizvoda.FirstOrDefault()?.Cena (ovo je bilo pre null)
                                //AktivnaCena = proizvod.ListCenaProizvoda.FirstOrDefault()!.Cena  Može i ovako ako garantujem da prilikom kreiranja proizvoda , 
                                //mora da postoji i jedan element u listi cenaProizvoda
        };
    }
    public async Task<ProizvodDto?> UpdateAsync(int id, CreateUpdateProizvodDto proizvodDto)
    {
        var proizvod = await _proizvodRepo.GetByIdAsync(id);
        if(proizvod==null) return null;
        var kategorija= await _kategorijaRepo.GetByIdAsync(proizvodDto.KategorijaId);
        if (kategorija == null) throw new ArgumentException($"Kategorija sa ID {proizvodDto.KategorijaId} ne postoji.");
        
        proizvod.AzurirajProizvod(proizvodDto.Naziv, proizvodDto.Opis, proizvodDto.JedinicaMere, proizvodDto.Lager, proizvodDto.KategorijaId);
        await _proizvodRepo.UpdateAsync(proizvod);
        
        return new ProizvodDto
        {
            Id = id,
            Naziv = proizvod.Naziv,
            Opis = proizvod.Opis,
            Lager = proizvod.Lager,
            JedinicaMere = proizvod.JedinicaMere,
            Kategorija = kategorija.NazivKategorije,
            
            AktivnaCena = proizvod.ListCenaProizvoda
                        .OrderByDescending(c=>c.CreatedAt)
                        .FirstOrDefault()?.Cena
        };
    }
    public async Task DeleteAsync(int id)
    {
        var proizvod = await _proizvodRepo.GetByIdAsync(id);
        if(proizvod==null) return;
        await _proizvodRepo.DeleteAsync(proizvod);
    }
}