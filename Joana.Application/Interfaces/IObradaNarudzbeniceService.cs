using Joana.Application.DTOs.ObradaNarudzbeniceDTOS;

namespace Joana.Application.Interfaces;

public interface IObradaNarudzbeniceService
{
    Task<List<ObradaNarudzbeniceDto>>  GetByNarudzbenicaIdAsync(int narudzbenicaId);
    Task<ObradaNarudzbeniceDto> AddAsync(CreateObradaNarudzbeniceDto dto, int idAdministrator);
}