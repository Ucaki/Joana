namespace Joana.Web.Models;

public class NarudzbenicaModel
{
    public int Id { get; set; }
    public DateTimeOffset DatumKreiranja { get; set; }
    public string? NapomenaKupca { get; set; }
    public int IdStatusNarudzbenice { get; set; }
    public string StatusNaziv { get; set; } = string.Empty;
    public string KupacIme { get; set; } = string.Empty;
    public string KupacPrezime { get; set; } = string.Empty;
    public List<StavkaNarudzbeniceModel> Stavke { get; set; } = new();
    public List<ObradaNarudzbeniceModel> Obrade { get; set; } = new();
}

public class CreateNarudzbenicaModel
{
    public string? NapomenaKupca { get; set; }
    public int IdKupac { get; set; }
    public List<CreateStavkaNarudzbeniceModel> Stavke { get; set; } = new();
}
