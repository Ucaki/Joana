namespace Joana.Domain;

public class StatusNarudzbenice
{
    public int IdStatus { get; private set; }
    public string NazivStatusa { get;
       private set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("NazivStatusa ne moze biti prazan");
            field=value;
        }
    }
    protected StatusNarudzbenice(){}
    public StatusNarudzbenice (string nazivStatus)
    {
        NazivStatusa=nazivStatus;
    }
}