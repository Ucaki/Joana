using Joana.Domain;

namespace Joana.Application.Interfaces;

public interface IKategorijaRepository
{
    Task<List<Kategorija>> GetAsync();
    Task<Kategorija?> GetByIdAsync(int kategorijaId);
    Task AddAsync(Kategorija kategorija); 
}