using System.ComponentModel.DataAnnotations;

namespace Joana.Application.DTOs.StavkaNarudzbeniceDTOS;

public class CreateStavkaNarudzbeniceDto
{
    
    [Range(1, int.MaxValue, ErrorMessage = "Id proizvoda mora biti pozitivan broj.")]
    public int IdProizvod { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "Količina mora biti veća od 0.")]
    public int Kolicina { get; init; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Ugovorena cena mora biti veća od 0.")]
    public decimal UgovorenaCena { get; init; }
}
