using Joana.Application.DTOs.KategorijaDTOS;

namespace Joana.Application.Interfaces;

public interface IKategorijaService
{
    Task<List<KategorijaDto>> GetAllAsync();
    Task<KategorijaDto?> GetByIdAsync(int id);
    Task<KategorijaDto> CreateAsync(CreateKategorijaDto kategorijaDto); 
}