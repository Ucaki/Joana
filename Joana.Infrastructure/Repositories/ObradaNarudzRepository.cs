using Joana.Application.Interfaces;
using Joana.Domain;
using Joana.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Joana.Infrastructure.Repositories;

public class ObradaNarudzRepository : IObradaNarudzRepository
{
    private readonly JoanaDbContext _context;

    public ObradaNarudzRepository(JoanaDbContext cont)
    {
        _context = cont;
    }

    public async Task<List<ObradaNarudžbenice>> GetByNarudzbenicaIdAsync(int narudzbeniceId)
    {
        return await _context.ObradaNarudžbenice
            .Include(o => o.Administrator)
            .Include(o => o.StatusNarudzbenica)
            .Where(o => o.IdNarudzbenica == narudzbeniceId)
            .ToListAsync();
    }

    public async Task AddAsync(ObradaNarudžbenice obradaNarudz)
    {
        _context.ObradaNarudžbenice.Add(obradaNarudz);
        await _context.SaveChangesAsync();
    }
}