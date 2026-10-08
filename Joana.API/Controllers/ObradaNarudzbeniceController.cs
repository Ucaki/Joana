using Joana.Application.DTOs.ObradaNarudzbeniceDTOS;
using Joana.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joana.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class ObradaNarudzbeniceController : ControllerBase
{
    private readonly IObradaNarudzbeniceService _obradaNarudzbeniceService;

    public ObradaNarudzbeniceController(IObradaNarudzbeniceService obradaNarudzbeniceService)
    {
        _obradaNarudzbeniceService = obradaNarudzbeniceService;
    }

    [HttpGet("narudzbenica/{narudzbenicaId:int}")]
    public async Task<IActionResult> GetByNarudzbenica(int narudzbenicaId)
    {
        var result = await _obradaNarudzbeniceService.GetByNarudzbenicaIdAsync(narudzbenicaId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateObradaNarudzbeniceDto dto)
    {
        var idAdministrator = int.Parse(User.FindFirst("sub")?.Value ?? "0");
        var result = await _obradaNarudzbeniceService.AddAsync(dto, idAdministrator);
        return CreatedAtAction(nameof(GetByNarudzbenica), new { narudzbenicaId = dto.IdNarudzbenice }, result);
    }
}
