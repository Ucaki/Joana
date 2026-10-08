using Joana.Application.DTOs.NarudzbenicaDTOS;
using Joana.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Joana.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NarudzbenicaController : ControllerBase
{
    private readonly INarudzbenicaService _narudzbenicaService;

    public NarudzbenicaController(INarudzbenicaService narudzbenicaService)
    {
        _narudzbenicaService = narudzbenicaService;
    }

    #region Ostale metode

    

    [Authorize(Roles = "Administrator")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _narudzbenicaService.GetAllAsync(page, pageSize);
        return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet("by-date")]
    public async Task<IActionResult> GetByDate([FromQuery] string date, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        if (!DateOnly.TryParseExact(date, "dd.MM.yyyy", 
                System.Globalization.CultureInfo.InvariantCulture, 
                System.Globalization.DateTimeStyles.None, 
                out var parsedDate))
            return BadRequest("Neispravan format datuma. Koristite format: dd.MM.yyyy (npr. 15.01.2026)");
        
        var dateTimeOffset = new DateTimeOffset(
            parsedDate.ToDateTime(TimeOnly.MinValue), 
            TimeSpan.Zero);
        
        var result = await _narudzbenicaService.GetByDateAsync(dateTimeOffset, page, pageSize);
        return Ok(result);
    }

    [HttpGet("by-kupac/{kupacId:int}")]
    public async Task<IActionResult> GetByKupac(int kupacId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        if (!IsAllowedFor(kupacId)) return Forbid();

        var result = await _narudzbenicaService.GetByKupacIdAsync(kupacId, page, pageSize);
        return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet("by-status/{statusId:int}")]
    public async Task<IActionResult> GetByStatus(int statusId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _narudzbenicaService.GetByStatusAsync(statusId, page, pageSize);
        return Ok(result);
    }
    #endregion

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _narudzbenicaService.GetByIdAsync(id);
        if (result == null) return NotFound();
        if (!IsAllowedFor(result.IdKupac)) return Forbid();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNarudzbenicaDto dto)
    {
        var kupacId = GetUserId();
        if (kupacId is null) return Forbid();

        var sanitizedDto = new CreateNarudzbenicaDto
        {
            NapomenaKupca = dto.NapomenaKupca,
            IdKupac = kupacId.Value,
            Stavke = dto.Stavke
        };

        var result = await _narudzbenicaService.AddAsync(sanitizedDto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateNarudzbenicaDto dto)
    {
        var existing = await _narudzbenicaService.GetByIdAsync(id);
        if (existing == null) return NotFound();
        if (!IsAllowedFor(existing.IdKupac)) return Forbid();

        var result = await _narudzbenicaService.UpdateAsync(id, dto);
        if (result == null) return NotFound();
        return Ok(result);
    }

    private int? GetUserId() =>
        int.TryParse(User.FindFirst("sub")?.Value, out var id) ? id : null;

    private bool IsAllowedFor(int kupacId) =>
        User.IsInRole("Administrator") || GetUserId() == kupacId;
}
