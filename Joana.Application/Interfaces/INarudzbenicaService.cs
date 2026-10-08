using Joana.Application.DTOs.NarudzbenicaDTOS;

namespace Joana.Application.Interfaces;

public interface INarudzbenicaService
{
    Task<List<NarudzbenicaDto>> GetAllAsync(int page = 1, int pageSize = 20);
    Task<List<NarudzbenicaDto>> GetByDateAsync(DateTimeOffset date, int page = 1, int pageSize = 20);
    Task<List<NarudzbenicaDto>> GetByKupacIdAsync(int kupacId, int page = 1, int pageSize = 20);
    Task<List<NarudzbenicaDto>> GetByStatusAsync(int statusNarudzbeniceId, int page = 1, int pageSize = 20);
    Task<NarudzbenicaDto?> GetByIdAsync(int narudzbenicaId);
    Task<NarudzbenicaDto> AddAsync(CreateNarudzbenicaDto dto);
    Task<NarudzbenicaDto?> UpdateAsync(int narudzbenicaId, CreateNarudzbenicaDto dto);
}