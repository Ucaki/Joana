using System.ComponentModel.DataAnnotations;

namespace Joana.Application.DTOs.KategorijaDTOS;

public class CreateKategorijaDto
{
    [Required(ErrorMessage = "Naziv kategorije je obavezan.")]
    [MaxLength(50, ErrorMessage = "Naziv kategorije ne sme imati više od 50 karaktera.")]
    public required string NazivKategorije { get; init; }
}
