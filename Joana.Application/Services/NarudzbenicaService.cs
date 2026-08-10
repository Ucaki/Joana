using Joana.Application.DTOs.NarudzbenicaDTOS;
using Joana.Application.DTOs.ObradaNarudzbeniceDTOS;
using Joana.Application.DTOs.StavkaNarudzbeniceDTOS;
using Joana.Application.Interfaces;
using Joana.Domain;

namespace Joana.Application.Services;

public class NarudzbenicaService : INarudzbenicaService
{
    private const int IdStatusKreirano = 1;

    private readonly INarudzbenicaRepository _narudzbenicaRepo;
    private readonly IKupacRepository _kupacRepo;
    private readonly IProizvodRepository _proizvodRepo;
    private readonly IStavkaNarudzRepository _stavkaRepo;
    private readonly IObradaNarudzRepository _obradaRepo;
    private readonly IStatusNarudzbeniceRepository _statusRepo;

    public NarudzbenicaService(
        INarudzbenicaRepository narudzbenicaRepo,
        IKupacRepository kupacRepo,
        IProizvodRepository proizvodRepo,
        IStavkaNarudzRepository stavkaRepo,
        IObradaNarudzRepository obradaRepo,
        IStatusNarudzbeniceRepository statusRepo)
    {
        _narudzbenicaRepo = narudzbenicaRepo;
        _kupacRepo = kupacRepo;
        _proizvodRepo = proizvodRepo;
        _stavkaRepo = stavkaRepo;
        _obradaRepo = obradaRepo;
        _statusRepo = statusRepo;
    }

    public async Task<List<NarudzbenicaDto>> GetByDateAsync(DateTimeOffset date, int page = 1, int pageSize = 20)
    {
        var narudzbenice = await _narudzbenicaRepo.GetByDateAsync(date, page, pageSize);
        return narudzbenice.Select(MapToDto).ToList();
    }

    public async Task<List<NarudzbenicaDto>> GetByKupacIdAsync(int kupacId, int page = 1, int pageSize = 20)
    {
        var narudzbenice = await _narudzbenicaRepo.GetByKupacIdAsync(kupacId, page, pageSize);
        return narudzbenice.Select(MapToDto).ToList();
    }

    public async Task<List<NarudzbenicaDto>> GetByStatusAsync(int statusNarudzbeniceId, int page = 1, int pageSize = 20)
    {
        var narudzbenice = await _narudzbenicaRepo.GetByStatusAsync(statusNarudzbeniceId, page, pageSize);
        return narudzbenice.Select(MapToDto).ToList();
    }

    public async Task<NarudzbenicaDto?> GetByIdAsync(int narudzbenicaId)
    {
        var narudzbenica = await _narudzbenicaRepo.GetByIdAsync(narudzbenicaId);
        return narudzbenica == null ? null : MapToDto(narudzbenica);
    }

    public async Task<NarudzbenicaDto> AddAsync(CreateNarudzbenicaDto dto)
    {
        var kupac = await _kupacRepo.GetByIdAsync(dto.IdKupac);
        if (kupac == null) throw new ArgumentException($"Kupac sa ID {dto.IdKupac} ne postoji.");

        var status = await _statusRepo.GetByIdAsync(IdStatusKreirano);
        if (status == null) throw new ArgumentException($"Status sa ID {IdStatusKreirano} ne postoji.");

        var narudzbenica = new Narudzbenica(dto.NapomenaKupca, IdStatusKreirano, dto.IdKupac);
        await _narudzbenicaRepo.AddAsync(narudzbenica);

        var stavke = await KreirajStavkeAsync(narudzbenica.IdNarudzbenica, dto.Stavke);

        var obrada = new ObradaNarudžbenice(narudzbenica.IdNarudzbenica, IdStatusKreirano, null, null);
        await _obradaRepo.AddAsync(obrada);

        return new NarudzbenicaDto
        {
            Id = narudzbenica.IdNarudzbenica,
            DatumKreiranja = narudzbenica.DatumKreiranja,
            NapomenaKupca = narudzbenica.NapomenaKupca,
            StatusNaziv = status.NazivStatusa,
            KupacIme = kupac.Ime,
            KupacPrezime = kupac.Prezime,
            Stavke = stavke,
            Obrade = new List<ObradaNarudzbeniceDto>
            {
                new ObradaNarudzbeniceDto
                {
                    AdminIme = null,
                    StatusNaziv = status.NazivStatusa,
                    DatumObrade = obrada.DatumObrada,
                    Komentar = obrada.Komentar
                }
            }
        };
    }

    public async Task<NarudzbenicaDto?> UpdateAsync(int narudzbenicaId, CreateNarudzbenicaDto dto)
    {
        var narudzbenica = await _narudzbenicaRepo.GetByIdAsync(narudzbenicaId);
        if (narudzbenica == null) return null;

        var kupac = await _kupacRepo.GetByIdAsync(dto.IdKupac);
        if (kupac == null) throw new ArgumentException($"Kupac sa ID {dto.IdKupac} ne postoji.");

        narudzbenica.AzurirajNapomenu(dto.NapomenaKupca);

        var postojeceStavke = await _stavkaRepo.GetAsync(narudzbenicaId);
        foreach (var stavka in postojeceStavke)
        {
            await _stavkaRepo.DeleteAsync(stavka);
        }

        var stavke = await KreirajStavkeAsync(narudzbenicaId, dto.Stavke);

        await _narudzbenicaRepo.UpdateAsync(narudzbenica);

        var status = await _statusRepo.GetByIdAsync(narudzbenica.IdStatusNarudzbenice) 
                     ?? throw new ArgumentException($"Status sa ID {narudzbenica.IdStatusNarudzbenice} ne postoji");
        var obrade = await _obradaRepo.GetByNarudzbenicaIdAsync(narudzbenicaId);

        return new NarudzbenicaDto
        {
            Id = narudzbenica.IdNarudzbenica,
            DatumKreiranja = narudzbenica.DatumKreiranja,
            NapomenaKupca = narudzbenica.NapomenaKupca,
            StatusNaziv = status.NazivStatusa,
            KupacIme = kupac.Ime,
            KupacPrezime = kupac.Prezime,
            Stavke = stavke,
            Obrade = obrade.Select(MapObradaToDto).ToList()
        };
    }

    private async Task<List<StavkaNarudzbeniceDto>> KreirajStavkeAsync(int idNarudzbenica, List<CreateStavkaNarudzbeniceDto> stavkeDto)
    {
        var stavke = new List<StavkaNarudzbeniceDto>();
        foreach (var stavkaDto in stavkeDto)
        {
            var proizvod = await _proizvodRepo.GetByIdAsync(stavkaDto.IdProizvod);
            if (proizvod == null) throw new ArgumentException($"Proizvod sa ID {stavkaDto.IdProizvod} ne postoji.");

            var stavka = new StavkaNarudzbenice(stavkaDto.Kolicina, stavkaDto.UgovorenaCena, idNarudzbenica, stavkaDto.IdProizvod, stavkaDto.RBrProizvoda);
            await _stavkaRepo.AddAsync(stavka);

            stavke.Add(new StavkaNarudzbeniceDto
            {
                IdNarudzbenica = idNarudzbenica,
                RBrProizvoda = stavkaDto.RBrProizvoda,
                IdProizvod = stavkaDto.IdProizvod,
                ProizvodNaziv = proizvod.Naziv,
                Kolicina = stavkaDto.Kolicina,
                UgovorenaCena = stavkaDto.UgovorenaCena
            });
        }
        return stavke;
    }

    private static NarudzbenicaDto MapToDto(Narudzbenica narudzbenica) => new NarudzbenicaDto
    {
        Id = narudzbenica.IdNarudzbenica,
        DatumKreiranja = narudzbenica.DatumKreiranja,
        NapomenaKupca = narudzbenica.NapomenaKupca,
        StatusNaziv = narudzbenica.StatusNarudzbenice.NazivStatusa,
        KupacIme = narudzbenica.Kupac.Ime,
        KupacPrezime = narudzbenica.Kupac.Prezime,
        Stavke = narudzbenica.ListStavkeNarudzbenica.Select(s => new StavkaNarudzbeniceDto
        {
            IdNarudzbenica = s.IdNarudzbenica,
            RBrProizvoda = s.RBrProizvoda,
            IdProizvod = s.IdProizvod,
            ProizvodNaziv = s.Proizvod.Naziv,
            Kolicina = s.Kolicina,
            UgovorenaCena = s.UgovorenaCena
        }).ToList(),
        Obrade = narudzbenica.ListObrada.Select(MapObradaToDto).ToList()
    };

    private static ObradaNarudzbeniceDto MapObradaToDto(ObradaNarudžbenice obrada) => new ObradaNarudzbeniceDto
    {
        AdminIme = obrada.Administrator?.Ime,
        StatusNaziv = obrada.StatusNarudzbenica.NazivStatusa,
        DatumObrade = obrada.DatumObrada,
        Komentar = obrada.Komentar
    };
}