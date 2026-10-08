using System.ComponentModel.DataAnnotations;

namespace Joana.Application.DTOs.ObradaNarudzbeniceDTOS;

public class CreateObradaNarudzbeniceDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Id statusa narudžbenice mora biti pozitivan broj.")]
    public int IdStatusNarudzbenica { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "Id narudžbenice mora biti pozitivan broj.")]
    public int IdNarudzbenice { get; init; }

    public string? Komentar { get; init; }
    //public int IdAdministrator { get; init; } Ne šalje se, čita se iz jwt tokena ko je ulogovan ko obradjuje narudzbenice
}
