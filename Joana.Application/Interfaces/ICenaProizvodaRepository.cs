using Joana.Domain;

namespace Joana.Application.Interfaces;

public interface ICenaProizvodaRepository
{
    Task<List<CenaProizvoda>> GetByProizvodIdAsync(int proizvodId);
    Task<CenaProizvoda?> GetAktivnaCenaAsync(int cenaProizvodaId);
    Task AddAsync(CenaProizvoda cenaProizvoda);
    
    Task DeactivateAllAsync(int proizvodId);
}