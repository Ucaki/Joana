using Joana.Application.DTOs.AuthDTOS;
using Joana.Application.DTOs.KupacDTOS;
using Joana.Application.Interfaces;
using Joana.Application.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joana.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IKupacService _kupacService;

    public AuthController(IAuthService authService, IKupacService kupacService)
    {
        _authService = authService;
        _kupacService = kupacService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _authService.LoginAsync(dto);
        return Ok(new TokenResponseDto { Token = token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateKupacDto dto)
    {
        var result = await _kupacService.AddAsync(dto);
        return CreatedAtAction("GetByEmail", "Kupac", new { email = result.Email }, result);
    }
    [HttpGet("hash/{lozinka}")]
    [AllowAnonymous]
    public IActionResult GetHash(string lozinka)
    {
        return Ok(PasswordHasher.Hash(lozinka));
    }
}
