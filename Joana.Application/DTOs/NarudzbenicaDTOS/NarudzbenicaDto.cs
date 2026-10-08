using Joana.Application.DTOs.ObradaNarudzbeniceDTOS;
using Joana.Application.DTOs.StavkaNarudzbeniceDTOS;

namespace Joana.Application.DTOs.NarudzbenicaDTOS;

public class NarudzbenicaDto
{
    public int Id { get; init; }
    public DateTimeOffset DatumKreiranja { get; init; }
    public string? NapomenaKupca { get; init; }
    public int IdStatusNarudzbenice { get; init; }
    public int IdKupac { get; init; }
    public required string StatusNaziv { get; init; }
    public required string KupacIme { get; init; }
    public required string KupacPrezime { get; init; }
  //  public int? IdAdministrator { get; init; } Menjao sam Model, admin više nije u direktnoj vezi sa Narudzbenicom, već preko ObradaNarudzbenice agregacije
  public List<StavkaNarudzbeniceDto> Stavke { get; init; } = new List<StavkaNarudzbeniceDto>();
  public List<ObradaNarudzbeniceDto> Obrade { get; init; } = new List<ObradaNarudzbeniceDto>();
}
