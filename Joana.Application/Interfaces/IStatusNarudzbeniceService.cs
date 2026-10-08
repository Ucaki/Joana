using Joana.Application.DTOs.StatusNarudzbeniceDTOS;

namespace Joana.Application.Interfaces;

public interface IStatusNarudzbeniceService
{
    Task<List<StatusNarudzbeniceDto>> GetAllAsync();
}
