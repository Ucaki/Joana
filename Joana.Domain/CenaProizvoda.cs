namespace Joana.Domain;

public class CenaProizvoda
{
    public int IdProizvod { get; private set; }
    public int IdCena { get; private set; }
    public Proizvod Proizvod { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }=DateTimeOffset.UtcNow;
    public decimal Cena { get;
        private set
        {
            if (value <= 0) throw new ArgumentException("Cena ne može biti manja od 0");
            field=value;
        }
    }
    protected CenaProizvoda() { }
    public CenaProizvoda(int idProizvod,decimal cena )
    {
        IdProizvod = idProizvod;
        Cena = cena;
    }
}