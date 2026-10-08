using Joana.Application.DTOs.ProizvodDTOS;
using Joana.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joana.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProizvodController : ControllerBase
{
    private readonly IProizvodService _proizvodService;

    public ProizvodController(IProizvodService proizvodService)
    {
        _proizvodService = proizvodService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _proizvodService.GetAllAsync(page, pageSize);
        return Ok(result);
    }

    [HttpGet("kategorija/{kategorijaId:int}")]
    public async Task<IActionResult> GetAllByKategorija(int kategorijaId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _proizvodService.GetAllByKategorijaAsync(kategorijaId, page, pageSize);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _proizvodService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUpdateProizvodDto dto)
    {
        var result = await _proizvodService.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateUpdateProizvodDto dto)
    {
        var result = await _proizvodService.UpdateAsync(id, dto);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _proizvodService.DeleteAsync(id);
        return NoContent();
    }
}
