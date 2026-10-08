namespace Joana.Web.Models;

public class CenaProizvodaModel
{
    public string ProizvodNaziv { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public decimal Cena { get; set; }
}

public class CreateCenaProizvodaModel
{
    public int IdProizvod { get; set; }
    public decimal Cena { get; set; }
}
