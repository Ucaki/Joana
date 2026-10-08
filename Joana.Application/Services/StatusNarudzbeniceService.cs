using Joana.Application.DTOs.StatusNarudzbeniceDTOS;
using Joana.Application.Interfaces;

namespace Joana.Application.Services;

public class StatusNarudzbeniceService : IStatusNarudzbeniceService
{
    private readonly IStatusNarudzbeniceRepository _statusRepo;

    public StatusNarudzbeniceService(IStatusNarudzbeniceRepository statusRepo)
    {
        _statusRepo = statusRepo;
    }

    public async Task<List<StatusNarudzbeniceDto>> GetAllAsync()
    {
        var statusi = await _statusRepo.GetAsync();
        return statusi.Select(s => new StatusNarudzbeniceDto
        {
            Id = s.IdStatus,
            NazivStatusa = s.NazivStatusa
        }).ToList();
    }
}
