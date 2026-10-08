 using System.ComponentModel.DataAnnotations;

 namespace Joana.Domain;


public class Korisnik
{
    public int IdKorisnik { get; private set; }
 
    public string Ime
    {
        get;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Invalid Name");
            field = value;
        }
    }
    public string Prezime { get;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Invalid Surname");
            field = value;
        }
    }
    public string Email { get;
        private set
        {
            if(!IsValidEmail(value))
                throw new ArgumentException("Invalid email");
            field=value.Trim().ToLower();
        }
    }
    public string LozinkaHash { get;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Invalid LozinkaHash, cant enter less then 8 characters!");
            field = value;
        }
    }

    protected Korisnik()
    {
    }
    public Korisnik(string  ime, string prezime, string email, string lozinkaHash)
    {
        Ime=ime;
        Prezime=prezime;
        Email=email;
        LozinkaHash=lozinkaHash;
    }

    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        return new EmailAddressAttribute().IsValid(email);
    }

    public void PromeniEmail(string noviEmail) => Email = noviEmail;
    public void PromeniLozinku(string novaLozinkaHash) => LozinkaHash = novaLozinkaHash;
    public void PromeniIme(string novoIme)=> Ime = novoIme;
    public void PromeniPrezime(string novoPrezime)=> Prezime = novoPrezime;
    
}