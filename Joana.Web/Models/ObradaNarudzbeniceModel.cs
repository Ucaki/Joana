namespace Joana.Web.Models;

public class ObradaNarudzbeniceModel
{
    public string? AdminIme { get; set; }
    public string StatusNaziv { get; set; } = string.Empty;
    public DateTimeOffset DatumObrade { get; set; }
    public string? Komentar { get; set; }
}

public class CreateObradaNarudzbeniceModel
{
    public int IdStatusNarudzbenica { get; set; }
    public int IdNarudzbenice { get; set; }
    public string? Komentar { get; set; }
}
