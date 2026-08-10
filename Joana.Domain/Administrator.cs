namespace Joana.Domain;

public class Administrator:Korisnik
{
    protected Administrator() { }

    public Administrator(string ime, string prezime, string email, string lozinkaHash) : base(ime, prezime, email,
        lozinkaHash)
    {
        
    }
}