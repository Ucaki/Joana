using System.ComponentModel.DataAnnotations;

namespace Joana.Application.DTOs.KupacDTOS;

public class KupacDto
{
    public int Id { get; init; }
    public required string Ime { get; init; }
    public required string Prezime { get; init; }
    [EmailAddress]
    public required string Email { get; init; }
    public required string Telefon { get; init; }
    public required string Adresa { get; init; }
    public required string Grad { get; init; }
}
