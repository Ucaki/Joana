using Joana.Domain;

namespace Joana.Application.DTOs.ProizvodDTOS;
public class CreateUpdateProizvodDto
{
    public required string Naziv { get; init; }
    public JedMere JedinicaMere { get; init; }
    public string? Opis { get; init; }
    public int Lager { get; init; }
    public int KategorijaId { get; init; }
}