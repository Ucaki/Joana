using Joana.Application.DTOs.ObradaNarudzbeniceDTOS;
using Joana.Application.Interfaces;
using Joana.Domain;

namespace Joana.Application.Services;

public class ObradaNarudzbeniceService : IObradaNarudzbeniceService
{
    private readonly IObradaNarudzRepository _obradaRepo;
    private readonly INarudzbenicaRepository _narudzbenicaRepo;
    private readonly IStatusNarudzbeniceRepository _statusRepo;
    private readonly IAdministratorRepository _administratorRepo;

    public ObradaNarudzbeniceService(
        IObradaNarudzRepository obradaRepo,
        INarudzbenicaRepository narudzbenicaRepo,
        IStatusNarudzbeniceRepository statusRepo,
        IAdministratorRepository administratorRepo)
    {
        _obradaRepo = obradaRepo;
        _narudzbenicaRepo = narudzbenicaRepo;
        _statusRepo = statusRepo;
        _administratorRepo = administratorRepo;
    }

    public async Task<List<ObradaNarudzbeniceDto>> GetByNarudzbenicaIdAsync(int narudzbenicaId)
    {
        var obrade = await _obradaRepo.GetByNarudzbenicaIdAsync(narudzbenicaId);
        return obrade.Select(o => new ObradaNarudzbeniceDto
        {
            AdminIme = o.Administrator?.Ime,
            StatusNaziv = o.StatusNarudzbenica.NazivStatusa,
            DatumObrade = o.DatumObrada,
            Komentar = o.Komentar
        }).ToList();
    }

    public async Task<ObradaNarudzbeniceDto> AddAsync(CreateObradaNarudzbeniceDto dto, int idAdministrator)
    {
        var narudzbenica = await _narudzbenicaRepo.GetByIdAsync(dto.IdNarudzbenice);
        if (narudzbenica == null) throw new ArgumentException($"Narudžbenica sa ID {dto.IdNarudzbenice} ne postoji.");

        var status = await _statusRepo.GetByIdAsync(dto.IdStatusNarudzbenica);
        if (status == null) throw new ArgumentException($"Status sa ID {dto.IdStatusNarudzbenica} ne postoji.");

        var administrator = await _administratorRepo.GetByIdAsync(idAdministrator);
        if (administrator == null) throw new ArgumentException($"Administrator sa ID {idAdministrator} ne postoji.");

        var obrada = new ObradaNarudžbenice(dto.IdNarudzbenice, dto.IdStatusNarudzbenica, idAdministrator, dto.Komentar);
        await _obradaRepo.AddAsync(obrada);

        narudzbenica.PromeniStatus(dto.IdStatusNarudzbenica, idAdministrator);
        await _narudzbenicaRepo.UpdateAsync(narudzbenica);

        return new ObradaNarudzbeniceDto
        {
            AdminIme = administrator.Ime,
            StatusNaziv = status.NazivStatusa,
            DatumObrade = obrada.DatumObrada,
            Komentar = obrada.Komentar
        };
    }
}