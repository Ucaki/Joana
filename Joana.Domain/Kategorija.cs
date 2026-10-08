namespace Joana.Domain;

public class Kategorija
{
    public int IdKategorija{
        get;
        private set;
    }

    public string NazivKategorije
    {
        get;
        private set
        {
            if(string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Naziv kategorije nije unešen.");
            field = value;
        }
    }

    protected Kategorija()
    {
    }

    public Kategorija(string nazivKategorije)
    {
        NazivKategorije = nazivKategorije;
    }
}