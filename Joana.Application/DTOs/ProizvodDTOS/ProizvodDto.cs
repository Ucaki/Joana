using Joana.Domain;

namespace Joana.Application.DTOs.ProizvodDTOS;
public class ProizvodDto
{
    public int Id { get; init; }
    public required string Naziv { get; init; }
    public JedMere JedinicaMere { get; init; }
    public string? Opis { get; init; }
    public int Lager { get; init; }
    public required string Kategorija { get; init; }
    public decimal? AktivnaCena { get; init; }  
}