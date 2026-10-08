using System.ComponentModel.DataAnnotations;

namespace Joana.Application.DTOs.CenaProizvodaDTOS;

public class CreateCenaProizvodaDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Id proizvoda mora biti pozitivan broj.")]
    public int IdProizvod { get; init; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Cena mora biti veća od 0.")]
    public decimal Cena { get; init; }
}
