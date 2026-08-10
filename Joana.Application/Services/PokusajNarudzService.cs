using Joana.Application.DTOs.NarudzbenicaDTOS;
using Joana.Application.DTOs.ObradaNarudzbeniceDTOS;
using Joana.Application.DTOs.StavkaNarudzbeniceDTOS;
using Joana.Application.Interfaces;
using Joana.Domain;

namespace Joana.Application.Services;

public class PokusajNarudzService:INarudzbenicaService
{
    private const int  idStatusKreiran=1; //ili koji id je za kreirano, treba da vidim iz baze
    
    private readonly INarudzbenicaRepository _narudzbenicaRepository;
    private readonly IKupacRepository _kupacRepository;
    private readonly IProizvodRepository _proizvodRepository;
    private readonly IObradaNarudzRepository _obradaNarudzRepository;
    private readonly IStatusNarudzbeniceRepository _statusNarudzRepository;
    private readonly IStavkaNarudzRepository _stavkaRepo;
    
    public PokusajNarudzService(INarudzbenicaRepository nar,IKupacRepository kup, IAdministratorRepository adm, 
        IObradaNarudzRepository obr, IStatusNarudzbeniceRepository stat, IProizvodRepository pro, IStavkaNarudzRepository stavka)
    {
            _narudzbenicaRepository = nar;
            _kupacRepository = kup;
            _obradaNarudzRepository = obr;
            _statusNarudzRepository =  stat;
            _proizvodRepository = pro;
            _stavkaRepo = stavka;
    }
    public async Task<List<NarudzbenicaDto>> GetByDateAsync(DateTimeOffset date, int page = 1, int pageSize = 20)
    {
        var nar = await _narudzbenicaRepository.GetByDateAsync(date, page, pageSize);
        return nar.Select(MapToDto).ToList();
    }

    public async  Task<List<NarudzbenicaDto>> GetByKupacIdAsync(int kupacId, int page = 1, int pageSize = 20)
    {
        var nar = await _narudzbenicaRepository.GetByKupacIdAsync(kupacId, page, pageSize);
        return nar.Select(MapToDto).ToList();
    }
    public async Task<List<NarudzbenicaDto>> GetByStatusAsync(int statusNarudzbeniceId, int page = 1, int pageSize = 20)
    {
        var nar = await _narudzbenicaRepository.GetByStatusAsync(statusNarudzbeniceId, page, pageSize);
        return nar.Select(MapToDto).ToList();
    }
    public async Task<NarudzbenicaDto?> GetByIdAsync(int narudzbenicaId)
    {
        var nar = await _narudzbenicaRepository.GetByIdAsync(narudzbenicaId);
        return nar == null ? null : MapToDto(nar);
    }

    public async Task<NarudzbenicaDto> AddAsync(CreateNarudzbenicaDto dto)
    {
        var kup = await _kupacRepository.GetByIdAsync(dto.IdKupac);
        if (kup == null) throw new  ArgumentException($"Kupac  sa ID {dto.IdKupac} ne postoji");
        var stat = await _statusNarudzRepository.GetByIdAsync(idStatusKreiran);
        if (stat == null) throw new  ArgumentException($"Status narudzbenice sa ID {idStatusKreiran} ne postoji");

        var narudz = new Narudzbenica(dto.NapomenaKupca, idStatusKreiran, dto.IdKupac);
        _narudzbenicaRepository.AddAsync(narudz);

        var stavke = await KreirajStavkeAsync(narudz.IdNarudzbenica, dto.Stavke);

        var obrada = new ObradaNarudžbenice(narudz.IdNarudzbenica, stat.IdStatus,null,null);
        await _obradaNarudzRepository.AddAsync(obrada);
        narudz.ListObrada.Add(obrada);
        return new NarudzbenicaDto
        {
            StatusNaziv = stat.NazivStatusa,
            KupacIme = kup.Ime,
            KupacPrezime = kup.Prezime,
            NapomenaKupca = narudz.NapomenaKupca,
            DatumKreiranja = narudz.DatumKreiranja,
            Id = narudz.IdNarudzbenica,
            Obrade = new List<ObradaNarudzbeniceDto>
            {
                new ObradaNarudzbeniceDto
                {
                    StatusNaziv = stat.NazivStatusa,
                    AdminIme = null,
                    DatumObrade = obrada.DatumObrada,
                    Komentar = obrada.Komentar
                }
            },
            Stavke = stavke
        };
        
    }
    public async Task<NarudzbenicaDto?> UpdateAsync(int narudzbenicaId, CreateNarudzbenicaDto dto)
    {
        var narudz = await _narudzbenicaRepository.GetByIdAsync(narudzbenicaId);
        if (narudz == null) return null;
        
        var kup =  await _kupacRepository.GetByIdAsync(dto.IdKupac);
        if (kup == null) throw new ArgumentException($"Kupac sa ID {dto.IdKupac} ne postoji.");
        narudz.AzurirajNapomenu(dto.NapomenaKupca);
        
        var currStavke = await _stavkaRepo.GetAsync(narudzbenicaId);
        foreach (var stavka in currStavke)
        {
            await _stavkaRepo.DeleteAsync(stavka);
        }
        var stavke = await KreirajStavkeAsync(narudzbenicaId, dto.Stavke);
        await _narudzbenicaRepository.UpdateAsync(narudz);
        var stat = await _statusNarudzRepository.GetByIdAsync(idStatusKreiran);
        var obrade = await _obradaNarudzRepository.GetByNarudzbenicaIdAsync(narudzbenicaId);
        
        return new NarudzbenicaDto
        {
            Id = narudz.IdNarudzbenica,
            DatumKreiranja = narudz.DatumKreiranja,
            NapomenaKupca = narudz.NapomenaKupca,
            StatusNaziv = stat!.NazivStatusa,
            KupacIme = kup.Ime,
            KupacPrezime = kup.Prezime,
            Stavke = stavke,
            Obrade = obrade.Select(MapToObradaDto).ToList()
        };
    }
    private static NarudzbenicaDto MapToDto(Narudzbenica nar)
    {
        return new NarudzbenicaDto
        {
            KupacIme = nar.Kupac.Ime,
            KupacPrezime = nar.Kupac.Prezime,
            StatusNaziv = nar.StatusNarudzbenice.NazivStatusa,
            DatumKreiranja = nar.DatumKreiranja,
            Id = nar.IdNarudzbenica,
            NapomenaKupca = nar.NapomenaKupca,
            Obrade = nar.ListObrada.Select(MapToObradaDto).ToList(),
            Stavke = nar.ListStavkeNarudzbenica.Select(s=> new StavkaNarudzbeniceDto()
            {
                IdNarudzbenica = s.IdNarudzbenica,
                IdProizvod = s.IdProizvod,
                Kolicina = s.Kolicina,
                RBrProizvoda = s.RBrProizvoda,
                UgovorenaCena = s.UgovorenaCena,
                ProizvodNaziv = s.Proizvod.Naziv
            }).ToList()
        };
    }
    private static ObradaNarudzbeniceDto MapToObradaDto(ObradaNarudžbenice obr)
    {
        return new ObradaNarudzbeniceDto()
        {
            StatusNaziv = obr.StatusNarudzbenica.NazivStatusa,
            AdminIme = obr.Administrator?.Ime,
            DatumObrade = obr.DatumObrada,
            Komentar = obr.Komentar
        };
    }
    
    private async Task<List<StavkaNarudzbeniceDto>> KreirajStavkeAsync(int narudzIdNarudzbenica, List<CreateStavkaNarudzbeniceDto> listDtoStavke)
    {
        var stavke = new List<StavkaNarudzbeniceDto>();
        foreach (var st in listDtoStavke)
        {
            var proizvod = await _proizvodRepository.GetByIdAsync(st.IdProizvod);
            if (proizvod == null) throw new ArgumentException($"Proizvod sa ID {st.IdProizvod} ne postoji");
            
            var stavka = new StavkaNarudzbenice(st.Kolicina,st.UgovorenaCena, st.IdNarudzbenica, proizvod.IdProizvod, st.RBrProizvoda);
            await _stavkaRepo.AddAsync(stavka);
            
            stavke.Add(new StavkaNarudzbeniceDto
            {
                ProizvodNaziv = stavka.Proizvod.Naziv,
                IdNarudzbenica =  stavka.IdNarudzbenica,
                IdProizvod = stavka.IdProizvod,
                Kolicina = stavka.Kolicina,
                RBrProizvoda =  stavka.RBrProizvoda,
                UgovorenaCena =  stavka.UgovorenaCena
            });
        }

        return stavke;
    }
}