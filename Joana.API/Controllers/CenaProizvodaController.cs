using Joana.Application.DTOs.CenaProizvodaDTOS;
using Joana.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joana.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class CenaProizvodaController : ControllerBase
{
    private readonly ICenaProizvodaService _cenaProizvodaService;

    public CenaProizvodaController(ICenaProizvodaService cenaProizvodaService)
    {
        _cenaProizvodaService = cenaProizvodaService;
    }

    [HttpGet("proizvod/{proizvodId:int}")]
    public async Task<IActionResult> GetByProizvod(int proizvodId)
    {
        var result = await _cenaProizvodaService.GetAsync(proizvodId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCenaProizvodaDto dto)
    {
        var result = await _cenaProizvodaService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByProizvod), new { proizvodId = dto.IdProizvod }, result);
    }
}
