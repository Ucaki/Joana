using System.ComponentModel.DataAnnotations;
using Joana.Application.DTOs.StavkaNarudzbeniceDTOS;

namespace Joana.Application.DTOs.NarudzbenicaDTOS;

public class CreateNarudzbenicaDto
{
    public string? NapomenaKupca { get; init; }
    //public int IdStatusNarudzbenice { get; init; } Ne treba jer je automatski, pri kreiranju je kreiran ili na čekanju...Ne može odmah odobren, mora da prodje kroz ciklus promene stanja
    [Range(1, int.MaxValue, ErrorMessage = "Id kupca mora biti pozitivan broj.")]
    public int IdKupac { get; init; }

    [MinLength(1, ErrorMessage = "Narudžbenica mora imati bar jednu stavku.")]
    public List<CreateStavkaNarudzbeniceDto> Stavke { get; init; } = new();
}
