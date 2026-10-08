using Joana.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joana.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles="Administrator")]
public class AdministratorController : ControllerBase
{
    private readonly IAdministratorService _administratorService;

    public AdministratorController(IAdministratorService administratorService)
    {
        _administratorService = administratorService;
    }

    [HttpGet("by-email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var result = await _administratorService.GetByEmailAsync(email);
        if (result == null) return NotFound();
        return Ok(result);
    }
}
