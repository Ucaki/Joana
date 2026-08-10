namespace Joana.Domain;


public class Proizvod
{
    public int IdProizvod { get; private set; }
    public string Naziv { get;
        private set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Naziv proizvoda mora da postoji.");
            field = value;
        }
    }
    public string? Opis { get; private set; }

    public  JedMere JedinicaMere
    {
        get;
        private set
        {
            if(!Enum.IsDefined(typeof(JedMere),value)) throw new ArgumentException("Jedinica mora da postoji.");
            field=value;
        }
    }
    public int Lager {get;
        private set
        {
            if(value<0) throw new ArgumentException("Lager ne može biti negativan!");
            field = value;
        }
    }
    public Kategorija Kategorija{get; private set;}
    public int KategorijaId {get; private set;}

    public ICollection<CenaProizvoda>ListCenaProizvoda {get; private set;}=new List<CenaProizvoda>();
    
    protected Proizvod()
    {
    }

    public Proizvod(string naziv, string? opis, JedMere jedinicaMere, int lager, int  kategorijaId)
    {
        Naziv = naziv;
        Opis = opis;
        JedinicaMere = jedinicaMere;
        Lager = lager;
        KategorijaId = kategorijaId;
    }

    public void AzurirajProizvod(string naziv, string? opis, JedMere jedinicaMere, int lager, int kategorijaId)
    {
        Naziv = naziv;
        Opis = opis;
        JedinicaMere = jedinicaMere;
        Lager = lager;
        KategorijaId = kategorijaId;
    }

    public void AzurirajLager(int novaKolicina)=> Lager = novaKolicina;
    public void AzurirajOpis(string noviOpis) => Opis = noviOpis;
    public void AzurirajNaziv(string naziv) => Naziv = naziv;
}