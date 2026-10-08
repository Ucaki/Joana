using Joana.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joana.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class StatusNarudzbeniceController : ControllerBase
{
    private readonly IStatusNarudzbeniceService _statusService;

    public StatusNarudzbeniceController(IStatusNarudzbeniceService statusService)
    {
        _statusService = statusService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _statusService.GetAllAsync();
        return Ok(result);
    }
}
