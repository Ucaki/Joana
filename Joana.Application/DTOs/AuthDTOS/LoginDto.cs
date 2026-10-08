using System.ComponentModel.DataAnnotations;

namespace Joana.Application.DTOs.AuthDTOS;

public class LoginDto
{
    [Required(ErrorMessage = "Email je obavezan.")]
    [EmailAddress(ErrorMessage = "Email adresa nije validna.")]
    public required string Email { get; init; }

    [Required(ErrorMessage = "Lozinka je obavezna.")]
    public required string Lozinka { get; init; }
}
