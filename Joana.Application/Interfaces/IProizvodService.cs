using Joana.Application.DTOs.ProizvodDTOS;

namespace Joana.Application.Interfaces;
public interface IProizvodService
{
    public Task<List<ProizvodDto>> GetAllAsync(int  page = 1, int pageSize = 10);
    public Task<List<ProizvodDto>> GetAllByKategorijaAsync(int kategorijaId,int  page = 1, int pageSize = 10);
    public Task<ProizvodDto?> GetByIdAsync(int proizvodId);
    public Task<ProizvodDto> AddAsync(CreateUpdateProizvodDto proizvodDto);
    public Task<ProizvodDto?> UpdateAsync(int id, CreateUpdateProizvodDto proizvodDto);
    public Task DeleteAsync(int proizvodId);
}