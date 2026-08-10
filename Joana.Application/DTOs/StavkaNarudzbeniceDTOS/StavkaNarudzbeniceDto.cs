namespace Joana.Application.DTOs.StavkaNarudzbeniceDTOS;

public class StavkaNarudzbeniceDto
{
    public int IdNarudzbenica { get; init; }
    public int RBrProizvoda { get; init; }
    public int IdProizvod { get; init; }
    public required string ProizvodNaziv { get; init; }
    public int Kolicina { get; init; }
    public decimal UgovorenaCena { get; init; }
}
