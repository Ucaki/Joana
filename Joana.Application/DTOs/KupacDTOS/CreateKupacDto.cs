using System.ComponentModel.DataAnnotations;

namespace Joana.Application.DTOs.KupacDTOS;

public class CreateKupacDto
{
    //DataAnnotation pristup (postoji fluentValidation pristup)
    [Required(ErrorMessage = "Ime je obavezno.")]
    [MaxLength(50, ErrorMessage = "Ime ne sme imati više od 50 karaktera.")]
    public required string Ime { get; init; }

    [Required(ErrorMessage = "Prezime je obavezno.")]
    [MaxLength(50, ErrorMessage = "Prezime ne sme imati više od 50 karaktera.")]
    public required string Prezime { get; init; }

    [EmailAddress(ErrorMessage = "Email adresa nije validna.")]
    [MaxLength(100, ErrorMessage = "Email ne sme imati više od 100 karaktera.")]
    public required string Email { get; init; }

    [Required(ErrorMessage = "Lozinka je obavezna.")]
    [MinLength(8, ErrorMessage = "Lozinka mora imati najmanje 8 karaktera.")]
    public required string Lozinka { get; init; }

    [RegularExpression(@"^\d{9,10}$", ErrorMessage = "Telefon mora imati 9 ili 10 cifara.")]
    [MaxLength(20, ErrorMessage = "Telefon ne sme imati više od 20 karaktera.")]
    public required string Telefon { get; init; }

    [Required(ErrorMessage = "Adresa je obavezna.")]
    [MaxLength(150, ErrorMessage = "Adresa ne sme imati više od 150 karaktera.")]
    public required string Adresa { get; init; }

    [Required(ErrorMessage = "Grad je obavezan.")]
    [MaxLength(50, ErrorMessage = "Grad ne sme imati više od 50 karaktera.")]
    public required string Grad { get; init; }
}
