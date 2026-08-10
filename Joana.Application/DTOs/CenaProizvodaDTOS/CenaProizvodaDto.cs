namespace Joana.Application.DTOs.CenaProizvodaDTOS;

public class CenaProizvodaDto
{
    public required string ProizvodNaziv { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public decimal Cena { get; init; }
}
