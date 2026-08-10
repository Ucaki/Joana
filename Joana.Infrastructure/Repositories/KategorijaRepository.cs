using Joana.Application.Interfaces;
using Joana.Domain;
using Joana.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Joana.Infrastructure.Repositories;

public class KategorijaRepository : IKategorijaRepository
{
    private readonly JoanaDbContext _context;

    public KategorijaRepository(JoanaDbContext cont)
    {
        _context = cont;
    }

    public async Task<List<Kategorija>> GetAsync()
    {
        return await _context.Kategorija.ToListAsync();
    }

    public async Task<Kategorija?> GetByIdAsync(int kategorijaId)
    {
        return await _context.Kategorija.FindAsync(kategorijaId);
    }

    public async Task AddAsync(Kategorija kategorija)
    {
        _context.Kategorija.Add(kategorija);
        await _context.SaveChangesAsync();
    }
}