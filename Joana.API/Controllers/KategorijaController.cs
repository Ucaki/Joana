using Joana.Application.DTOs.KategorijaDTOS;
using Joana.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joana.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KategorijaController : ControllerBase
{
    private readonly IKategorijaService _kategorijaService;

    public KategorijaController(IKategorijaService kategorijaService)
    {
        _kategorijaService = kategorijaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _kategorijaService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _kategorijaService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [Authorize(Roles="Administrator")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateKategorijaDto dto)
    {
        var result = await _kategorijaService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
}
