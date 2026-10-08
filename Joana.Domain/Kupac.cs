namespace Joana.Domain;

public class Kupac:Korisnik
{
    public string Telefon { get;
        private set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Telefon ne sme biti prazan");
            if(value.Length<9 ||  value.Length > 10) throw new ArgumentException("Telefon mora imati 9 ili 10 cifara, format 0651231234");
            if(!value.All(char.IsDigit)) throw new ArgumentException("Telefon mora sadržati samo cifre");
            
            field =  value;
        }
    }
    public string Adresa { get;
        private set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Invalid Adresa");
            field =  value;
        }
    }
    public string Grad { get;
        private set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Invalid Grad");
            field =  value;
        }
    }
    
    public ICollection<Narudzbenica> ListaNarudzbenica { get; private set; } = new List<Narudzbenica>();
    
    protected Kupac() { }

    public Kupac(string ime, string prezime, string email, string lozinkaHash, string telefon, string grad, string adresa) : base(ime, prezime, email,
        lozinkaHash)
    {
        Adresa = adresa;
        Grad = grad;
        Telefon = telefon;
    }
    
    public void PromeniAdresu(string novaAdresa) => Adresa = novaAdresa;
    public void PromeniGrad(string noviGrad) => Grad = noviGrad;
    public void PromeniTelefon(string noviTelefon) => Telefon = noviTelefon;
}