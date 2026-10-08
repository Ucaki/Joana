using Joana.Domain;

namespace Joana.Application.Interfaces;

public interface INarudzbenicaRepository
{
    Task<List<Narudzbenica>> GetAllAsync(int page = 1, int pageSize = 20);
    Task<List<Narudzbenica>> GetByDateAsync(DateTimeOffset date, int page = 1, int pageSize = 20);
    Task<List<Narudzbenica>> GetByKupacIdAsync(int kupacId, int page = 1, int pageSize = 20);
    Task<List<Narudzbenica>> GetByStatusAsync(int statusNarudzbeniceId, int page = 1, int pageSize = 20);
    Task<Narudzbenica?> GetByIdAsync(int narudzbenicaId);
    Task AddAsync(Narudzbenica narudzbenica);
    Task UpdateAsync(Narudzbenica narudzbenica);
}