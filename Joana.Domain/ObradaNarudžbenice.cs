namespace Joana.Domain;

public class ObradaNarudžbenice
{
    public int IdObrada { get; private set; }
    public DateTimeOffset DatumObrada { get; private set; }=DateTimeOffset.UtcNow;
    public string? Komentar { get; private set; }
    
    public int? IdAdministrator{ get; private set; }
    public Administrator? Administrator { get; private set; }

    public int IdNarudzbenica { get; private set; }
    public Narudzbenica Narudzbenica { get; private set; }

    public int IdStatusNarudzbenica { get; private set; }
    public StatusNarudzbenice StatusNarudzbenica { get; private set; }

    protected ObradaNarudžbenice(){}

    public ObradaNarudžbenice(int idNarudzbenica, int idStatusNarudzbenica, int? idAdministrator, string? komentar)
    {
        IdNarudzbenica = idNarudzbenica;
        IdStatusNarudzbenica = idStatusNarudzbenica;
        IdAdministrator = idAdministrator;
        Komentar = komentar;
    }

}