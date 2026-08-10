using Joana.Application.DTOs.CenaProizvodaDTOS;
using Joana.Domain;

namespace Joana.Application.Interfaces;

public interface ICenaProizvodaService
{
    Task<List<CenaProizvodaDto>> GetAsync(int proizvodId);
    Task<CenaProizvodaDto> CreateAsync(CreateCenaProizvodaDto dto);
}