using Joana.Application.Interfaces;
using Joana.Domain;
using Joana.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Joana.Infrastructure.Repositories;

public class NarudzbenicaRepository : INarudzbenicaRepository
{
    private readonly JoanaDbContext _context;

    public NarudzbenicaRepository(JoanaDbContext cont)
    {
        _context = cont;
    }

    private IQueryable<Narudzbenica> QueryWithIncludes()
    {
        return _context.Narudzbenica
            .Include(n => n.Kupac)
            .Include(n => n.StatusNarudzbenice)
            .Include(n => n.ListObrada).ThenInclude(o => o.Administrator)
            .Include(n => n.ListObrada).ThenInclude(o => o.StatusNarudzbenica)
            .Include(n => n.ListStavkeNarudzbenica).ThenInclude(s => s.Proizvod)
            .AsSplitQuery();
    }

    public async Task<List<Narudzbenica>> GetAllAsync(int page = 1, int pageSize = 20)
    {
        return await QueryWithIncludes()
            .OrderByDescending(n => n.DatumKreiranja)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Narudzbenica>> GetByDateAsync(DateTimeOffset date, int page = 1, int pageSize = 20)
    {
        var start = new DateTimeOffset(date.Date, date.Offset);
        var end = start.AddDays(1);

        return await QueryWithIncludes()
            .Where(n => n.DatumKreiranja >= start && n.DatumKreiranja < end)
            .OrderByDescending(n => n.DatumKreiranja)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Narudzbenica>> GetByKupacIdAsync(int kupacId, int page = 1, int pageSize = 20)
    {
        return await QueryWithIncludes()
            .Where(n => n.IdKupac == kupacId)
            .OrderByDescending(n => n.DatumKreiranja)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Narudzbenica>> GetByStatusAsync(int statusNarudzbeniceId, int page = 1, int pageSize = 20)
    {
        return await QueryWithIncludes()
            .Where(n => n.IdStatusNarudzbenice == statusNarudzbeniceId)
            .OrderByDescending(n => n.DatumKreiranja)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Narudzbenica?> GetByIdAsync(int narudzbenicaId)
    {
        return await QueryWithIncludes()
            .FirstOrDefaultAsync(n => n.IdNarudzbenica == narudzbenicaId);
    }

    public async Task AddAsync(Narudzbenica narudzbenica)
    {
        _context.Narudzbenica.Add(narudzbenica);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Narudzbenica narudzbenica)
    {
        _context.Narudzbenica.Update(narudzbenica);
        await _context.SaveChangesAsync();
    }
}