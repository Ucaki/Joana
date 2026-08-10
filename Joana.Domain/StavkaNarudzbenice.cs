namespace Joana.Domain;

public class StavkaNarudzbenice
{
    public int IdNarudzbenica { get; private set; }
    public Narudzbenica Narudzbenica { get; private set; }
    
    public int RBrProizvoda { get; private set; }
    
    public int IdProizvod { get; private set; }
    public Proizvod Proizvod { get; private set; }
    
    public int Kolicina { get;
        private set
        {
            if (value <=0) throw new ArgumentException("Količina ne sme biti 0 ili manje od 0");
            field=value;
        }
    } 
    public decimal UgovorenaCena { get;
        private set
        {
            if (value <= 0) throw new ArgumentException("Ugovorena cena ne može biti negativna");
            field=value;
        }
    }
    protected StavkaNarudzbenice(){}

    public StavkaNarudzbenice(int kolicina, decimal ugovorenaCena, int idNarudzbenica, int idProizvod, int rBrProizvoda)
    {
        Kolicina=kolicina;
        UgovorenaCena=ugovorenaCena;
        IdProizvod=idProizvod;
        IdNarudzbenica=idNarudzbenica;
        RBrProizvoda=rBrProizvoda;
    }
}
