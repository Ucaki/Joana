using Joana.Domain;

namespace Joana.Application.Interfaces;

public interface IStavkaNarudzRepository
{
    Task<List<StavkaNarudzbenice>> GetAsync(int narudzbenicaId);
    //Task<StavkaNarudzbenice?> GetByIdAsync(int stavkaNarudzbeniceId);
    Task AddAsync(StavkaNarudzbenice stavka);
    Task UpdateAsync(StavkaNarudzbenice stavka);
    Task DeleteAsync(StavkaNarudzbenice stavka);
}