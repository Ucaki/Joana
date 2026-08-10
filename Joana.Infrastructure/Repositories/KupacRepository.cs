using Joana.Application.Interfaces;
using Joana.Domain;
using Joana.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Joana.Infrastructure.Repositories;

public class KupacRepository : IKupacRepository
{
    private readonly JoanaDbContext _context;

    public KupacRepository(JoanaDbContext cont)
    {
        _context = cont;
    }

    public async Task<List<Kupac>> GetByNameAsync(string ime, int page = 1, int pageSize = 10)
    {
        return await _context.Kupac
            .Where(k => k.Ime.Contains(ime) || k.Prezime.Contains(ime))
            .OrderBy(k => k.Prezime)
            .ThenBy(k => k.Ime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Kupac?> GetByEmailAsync(string email)
    {
        return await _context.Kupac
            .Where(k => k.Email == email)
            .FirstOrDefaultAsync();
    }

    public async Task<Kupac?> GetByIdAsync(int kupacId)
    {
        return await _context.Kupac.FindAsync(kupacId);
    }

    public async Task AddAsync(Kupac kupac)
    {
        _context.Kupac.Add(kupac);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Kupac kupac)
    {
        _context.Kupac.Update(kupac);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Kupac kupac)
    {
        _context.Kupac.Remove(kupac);
        await _context.SaveChangesAsync();
    }
}