namespace Joana.Web.Models;

public enum JedinicaMere
{
    Kg = 1,
    L = 2,
    Komad = 3
}

public static class JedinicaMereExtensions
{
    public static string ToDisplayString(this JedinicaMere jedinicaMere) => jedinicaMere switch
    {
        JedinicaMere.Kg => "kg",
        JedinicaMere.L => "l",
        JedinicaMere.Komad => "kom",
        _ => jedinicaMere.ToString()
    };
}

public class ProizvodModel
{
    public int Id { get; set; }
    public string Naziv { get; set; } = string.Empty;
    public JedinicaMere JedinicaMere { get; set; }
    public string? Opis { get; set; }
    public int Lager { get; set; }
    public string Kategorija { get; set; } = string.Empty;
    public decimal? AktivnaCena { get; set; }
}

public class CreateUpdateProizvodModel
{
    public string Naziv { get; set; } = string.Empty;
    public JedinicaMere JedinicaMere { get; set; }
    public string? Opis { get; set; }
    public int Lager { get; set; }
    public int KategorijaId { get; set; }
}
