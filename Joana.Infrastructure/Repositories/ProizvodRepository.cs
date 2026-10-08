using Joana.Application.Interfaces;
using Joana.Domain;
using Joana.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Joana.Infrastructure.Repositories;

public class ProizvodRepository: IProizvodRepository
{
    private readonly JoanaDbContext _context;

    public ProizvodRepository(JoanaDbContext cont)
    {
        _context = cont;
    }
    public async Task<List<Proizvod>> GetAsync(int page = 1, int pageSize = 10)
    {
        return await _context.Proizvod
            .Include(p => p.Kategorija)
            .Include(p=>p.ListCenaProizvoda)
            .OrderBy(p => p.IdProizvod)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Proizvod>> GetByKategorijaAsync(int kategorijaId, int page = 1, int pageSize = 10)
    {
        return await _context.Proizvod
            .Include(p => p.Kategorija)
            .Include(p=>p.ListCenaProizvoda)
            .Where(p => p.KategorijaId == kategorijaId)
            .OrderBy(p => p.IdProizvod)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Proizvod?> GetByIdAsync(int proizvodId)
    {
        return await _context.Proizvod
            .Include(p => p.Kategorija)
            .Include(p => p.ListCenaProizvoda)
            .FirstOrDefaultAsync(p=> p.IdProizvod==proizvodId);
        // return _context.Proizvod
        //     .Where(p => p.IdProizvod == proizvodId)
        //     .FirstOrDefaultAsync();
    }

    public async Task AddAsync(Proizvod proizvod)
    {
        _context.Proizvod.Add(proizvod);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Proizvod proizvod)
    {
         _context.Proizvod.Update(proizvod);
         await  _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Proizvod proizvod)
    {
        _context.Proizvod.Remove(proizvod);
        await _context.SaveChangesAsync();
    }
}