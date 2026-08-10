using Joana.Application.Interfaces;
using Joana.Domain;
using Joana.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Joana.Infrastructure.Repositories;

public class StatusNarudzbeniceRepository : IStatusNarudzbeniceRepository
{
    private readonly JoanaDbContext _context;

    public StatusNarudzbeniceRepository(JoanaDbContext cont)
    {
        _context = cont;
    }

    public async Task<List<StatusNarudzbenice>> GetAsync()
    {
        return await _context.StatusNarudzbenice.ToListAsync();
    }

    public async Task<StatusNarudzbenice?> GetByIdAsync(int statusNarudzbeniceId)
    {
        return await _context.StatusNarudzbenice.FindAsync(statusNarudzbeniceId);
    }
}