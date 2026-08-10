using Joana.Application.Interfaces;
using Joana.Domain;
using Joana.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Joana.Infrastructure.Repositories;

public class StavkaNarudzRepository : IStavkaNarudzRepository
{
    private readonly JoanaDbContext _context;

    public StavkaNarudzRepository(JoanaDbContext cont)
    {
        _context = cont;
    }

    public async Task<List<StavkaNarudzbenice>> GetAsync(int narudzbenicaID)
    {
        return await _context.StavkaNarudzbenice
            .Include(s => s.Proizvod)
            .Where(s => s.IdNarudzbenica == narudzbenicaID)
            .ToListAsync();
    }

    public async Task AddAsync(StavkaNarudzbenice stavka)
    {
        _context.StavkaNarudzbenice.Add(stavka);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(StavkaNarudzbenice stavka)
    {
        _context.StavkaNarudzbenice.Update(stavka);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(StavkaNarudzbenice stavka)
    {
        _context.StavkaNarudzbenice.Remove(stavka);
        await _context.SaveChangesAsync();
    }
}