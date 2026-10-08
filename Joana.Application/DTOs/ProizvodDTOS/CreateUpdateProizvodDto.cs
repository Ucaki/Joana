using System.ComponentModel.DataAnnotations;
using Joana.Domain;

namespace Joana.Application.DTOs.ProizvodDTOS;
public class CreateUpdateProizvodDto
{
    [Required(ErrorMessage = "Naziv proizvoda je obavezan.")]
    [MaxLength(150, ErrorMessage = "Naziv proizvoda ne sme imati više od 150 karaktera.")]
    public required string Naziv { get; init; }
    public JedMere JedinicaMere { get; init; }
    public string? Opis { get; init; }

    [Range(0, int.MaxValue, ErrorMessage = "Lager ne sme biti negativan.")]
    public int Lager { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "Id kategorije mora biti pozitivan broj.")]
    public int KategorijaId { get; init; }
}
