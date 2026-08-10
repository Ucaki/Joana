namespace Joana.Domain;

public class Narudzbenica
{
    public int IdNarudzbenica { get; private set; }
    public DateTimeOffset DatumKreiranja { get; private set; }=DateTimeOffset.UtcNow;
    public string? NapomenaKupca { get; private set; }
    
    public int IdStatusNarudzbenice { get; private set; }
    public StatusNarudzbenice StatusNarudzbenice { get; private set; }
    
    public int IdKupac { get; private set; }
    public Kupac Kupac { get; private set; }
    
    public int? IdAdministrator{ get; private set; }
    public Administrator? Administrator{ get; private set; }

    public ICollection<ObradaNarudžbenice> ListObrada { get; private set; } = new List<ObradaNarudžbenice>();
    public ICollection<StavkaNarudzbenice> ListStavkeNarudzbenica { get; private set; } = new List<StavkaNarudzbenice>();
    
    protected Narudzbenica(){}

    public Narudzbenica(string? napomenaKupca, int idStatusNarudzbenice, int idKupac)
    {
        NapomenaKupca = napomenaKupca;
        IdStatusNarudzbenice=idStatusNarudzbenice; 
        IdKupac=idKupac;
    }

    public void PromeniStatus(int noviIDstatusNarudzbenice, int iDAdmin)
    {
        IdStatusNarudzbenice=noviIDstatusNarudzbenice;
        IdAdministrator=iDAdmin;
    }

    public void AzurirajNapomenu(string? napomenaKupca) => NapomenaKupca = napomenaKupca;
}