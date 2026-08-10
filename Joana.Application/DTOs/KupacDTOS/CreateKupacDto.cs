namespace Joana.Application.DTOs.KupacDTOS;

public class CreateKupacDto
{
    public required string Ime { get; init; }
    public required string Prezime { get; init; }
    public required string Email { get; init; }
    public required string Lozinka { get; init; }
    public required string Telefon { get; init; }
    public required string Adresa { get; init; }
    public required string Grad { get; init; }
}
