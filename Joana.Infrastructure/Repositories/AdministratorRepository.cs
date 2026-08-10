using Joana.Application.Interfaces;
using Joana.Domain;
using Joana.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Joana.Infrastructure.Repositories;

public class AdministratorRepository : IAdministratorRepository
{
    private readonly JoanaDbContext _context;

    public AdministratorRepository(JoanaDbContext cont)
    {
        _context = cont;
    }

    public async Task<Administrator?> GetByEmailAsync(string email)
    {
        return await _context.Administrator
            .Where(a => a.Email == email)
            .FirstOrDefaultAsync();
    }

    public async Task<Administrator?> GetByIdAsync(int administratorId)
    {
        return await _context.Administrator.FindAsync(administratorId);
    }
}