using Joana.Application.DTOs.KupacDTOS;
using Joana.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joana.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class KupacController : ControllerBase
{
    private readonly IKupacService _kupacService;

    public KupacController(IKupacService kupacService)
    {
        _kupacService = kupacService;
    }

    [HttpGet("by-name")]
    public async Task<IActionResult> GetByName([FromQuery] string ime, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _kupacService.GetByNameAsync(ime, page, pageSize);
        return Ok(result);
    }

    [HttpGet("by-email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var result = await _kupacService.GetByEmailAsync(email);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateKupacDto dto)
    {
        var result = await _kupacService.AddAsync(dto);
        return CreatedAtAction(nameof(GetByEmail), new { email = result.Email }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateKupacDto dto)
    {
        var result = await _kupacService.UpdateAsync(id, dto);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _kupacService.DeleteAsync(id);
        return NoContent();
    }
}
