using Joana.Domain;

namespace Joana.Application.Interfaces;

public interface IKupacRepository
{
    Task<List<Kupac>> GetByNameAsync(string ime, int page=1, int pageSize=10);
    Task<Kupac?> GetByEmailAsync(string  email);
    Task<Kupac?> GetByIdAsync(int kupacId);
    Task AddAsync(Kupac kupac);
    Task UpdateAsync(Kupac kupac);
    Task DeleteAsync(Kupac kupac);
}