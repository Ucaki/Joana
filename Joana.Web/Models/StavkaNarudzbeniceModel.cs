namespace Joana.Web.Models;

public class StavkaNarudzbeniceModel
{
    public int IdNarudzbenica { get; set; }
    public int RBrProizvoda { get; set; }
    public int IdProizvod { get; set; }
    public string ProizvodNaziv { get; set; } = string.Empty;
    public int Kolicina { get; set; }
    public decimal UgovorenaCena { get; set; }
}

public class CreateStavkaNarudzbeniceModel
{
    public int IdProizvod { get; set; }
    public int Kolicina { get; set; }
    public decimal UgovorenaCena { get; set; }
}
