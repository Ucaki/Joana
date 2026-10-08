namespace Joana.Web.Models;

public class CartItemModel
{
    public int ProizvodId { get; set; }
    public string Naziv { get; set; } = string.Empty;
    public decimal Cena { get; set; }
    public int Kolicina { get; set; }
    public int Lager { get; set; }

    public decimal Ukupno => Cena * Kolicina;
}
