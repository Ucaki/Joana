using Joana.Domain;

namespace Joana.Application.Interfaces;

public interface IObradaNarudzRepository
{
    Task<List<ObradaNarudžbenice>> GetByNarudzbenicaIdAsync(int NarudzbeniceId);
    Task AddAsync(ObradaNarudžbenice obradaNarudz);
}