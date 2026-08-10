using Joana.Application.DTOs.KupacDTOS;
using Joana.Domain;

namespace Joana.Application.Interfaces;

public interface IKupacService
{
    Task<List<KupacDto>> GetByNameAsync(string ime, int page=1, int pageSize=10);
    Task<KupacDto?> GetByEmailAsync(string email);
    Task<KupacDto> AddAsync(CreateKupacDto dto);
    Task<KupacDto?> UpdateAsync(int kupacId, CreateKupacDto dto);
    Task DeleteAsync(int id);
}