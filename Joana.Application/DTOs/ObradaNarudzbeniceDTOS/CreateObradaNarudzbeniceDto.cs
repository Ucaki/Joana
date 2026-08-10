namespace Joana.Application.DTOs.ObradaNarudzbeniceDTOS;

public class CreateObradaNarudzbeniceDto
{
    public int IdStatusNarudzbenica { get; init; }
    public int IdNarudzbenice { get; init; }
    public string? Komentar { get; init; }
    //public int IdAdministrator { get; init; } Ne šalje se, čita se iz jwt tokena ko je ulogovan ko obradjuje narudzbenice
}