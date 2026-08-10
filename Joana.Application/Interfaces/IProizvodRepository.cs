using Joana.Domain;

namespace Joana.Application.Interfaces;

public interface IProizvodRepository
{
    Task<List<Proizvod>> GetAsync(int page = 1, int pageSize = 10);
    Task<List<Proizvod>> GetByKategorijaAsync(int kategorijaId, int page = 1, int pageSize = 10);
    Task<Proizvod?> GetByIdAsync(int proizvodId);
    Task AddAsync(Proizvod proizvod);
    Task UpdateAsync(Proizvod proizvod);
    Task DeleteAsync(Proizvod proizvod);
}