namespace Joana.Application.DTOs.ObradaNarudzbeniceDTOS;

public class ObradaNarudzbeniceDto
{
    public string? AdminIme { get; init; }
    public required string StatusNaziv { get; init; }
    public DateTimeOffset DatumObrade { get; init; }
    public string? Komentar  { get; init; }
    //public int idNarudzbenica { get; init; } Ne treba jer se obradaNarudzbenice uvek vaca kao lista u okviru neke Narudzbenice, pa tako znamo kome pripda, ne treba nam redundantni podaci
}