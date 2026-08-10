using Joana.Application.DTOs.CenaProizvodaDTOS;
using Joana.Application.Interfaces;
using Joana.Domain;
using Joana.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Joana.Infrastructure.Repositories;

public class CenaProizvodaRepository : ICenaProizvodaRepository
{
    private readonly JoanaDbContext _context;

    public CenaProizvodaRepository(JoanaDbContext cont)
    {
        _context = cont;
    }

    public async Task<List<CenaProizvoda>> GetByProizvodIdAsync(int proizvodId)
    {
        return await _context.CenaProizvoda
            .Include(c => c.Proizvod)
            .Where(c => c.IdProizvod == proizvodId)
            .ToListAsync();
    }
    public async Task<CenaProizvoda?> GetAktivnaCenaAsync(int cenaProizvodaId)
    {
        return await _context.CenaProizvoda
            .Where(c => c.IdProizvod == cenaProizvodaId)
            .OrderByDescending(c => c.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(CenaProizvoda cenaProizvoda)
    {
        _context.CenaProizvoda.Add(cenaProizvoda);
        await _context.SaveChangesAsync();
    }
}