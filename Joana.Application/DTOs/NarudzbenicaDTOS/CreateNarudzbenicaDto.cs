using Joana.Application.DTOs.StavkaNarudzbeniceDTOS;

namespace Joana.Application.DTOs.NarudzbenicaDTOS;

public class CreateNarudzbenicaDto
{
    public string? NapomenaKupca { get; init; }
    //public int IdStatusNarudzbenice { get; init; } Ne treba jer je automatski, pri kreiranju je kreiran ili na čekanju...Ne može odmah odobren, mora da prodje kroz ciklus promene stanja
    public int IdKupac { get; init; }
    public List<CreateStavkaNarudzbeniceDto> Stavke { get; init; } = new();
}
