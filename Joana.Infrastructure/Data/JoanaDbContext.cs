                                                                                                                                                                          

using Joana.Domain;
using Microsoft.EntityFrameworkCore;

namespace Joana.Infrastructure.Data;

public class JoanaDbContext: DbContext
{
    public JoanaDbContext(DbContextOptions<JoanaDbContext> options) : base(options) { }
    public DbSet<Korisnik> Korisnik { get; set; }
    public DbSet<Kupac>  Kupac { get; set; }
    public DbSet<Administrator> Administrator { get; set; }
    public DbSet<Kategorija> Kategorija { get; set; }
    public DbSet<Proizvod> Proizvod { get; set; }
    public DbSet<CenaProizvoda> CenaProizvoda { get; set; }
    public DbSet<StatusNarudzbenice> StatusNarudzbenice { get; set; }
    public DbSet<Narudzbenica> Narudzbenica { get; set; }
    public DbSet<StavkaNarudzbenice> StavkaNarudzbenice { get; set; }

    public DbSet<ObradaNarudžbenice> ObradaNarudžbenice { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Korisnik>(entity =>
        {
            entity.HasKey(u => u.IdKorisnik);
            entity.Property(u => u.IdKorisnik).UseSerialColumn();
            
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(u => u.LozinkaHash).HasColumnType("text");
            
            entity.Property(u => u.Ime).IsRequired()
                .HasMaxLength(50);
            
            entity.Property(u => u.Prezime).IsRequired()
                .HasMaxLength(50);
            entity.HasDiscriminator<string>("Role")
                .HasValue<Kupac>("Kupac")
                .HasValue<Administrator>("Administrator");
        });
        modelBuilder.Entity<Administrator>(entity =>
        {
        });
        modelBuilder.Entity<Kupac>(entity =>
        {
            entity.HasIndex(u => u.Telefon).IsUnique();
            
            entity.Property(u => u.Telefon).IsRequired()
                .HasMaxLength(20);
            entity.Property(u => u.Adresa).IsRequired()
                .HasMaxLength(150);
            entity.Property(u => u.Grad).IsRequired()
                .HasMaxLength(50);
        });
        modelBuilder.Entity<Kategorija>(entity =>
        {
            entity.HasKey(u => u.IdKategorija);
            entity.Property(u => u.IdKategorija).UseSerialColumn();
            
            entity.HasIndex(u => u.NazivKategorije).IsUnique();
            
            entity.Property(u => u.NazivKategorije).IsRequired()
                .HasMaxLength(50);
        });
        modelBuilder.Entity<Proizvod>(entity =>
        {
            entity.HasOne(k => k.Kategorija)
                .WithMany()
                .HasForeignKey(k => k.KategorijaId);
            
            entity.HasKey(u => u.IdProizvod);
            entity.Property(u => u.IdProizvod).UseSerialColumn();
            
            entity.HasIndex(u => u.Naziv).IsUnique();
            entity.Property(u => u.Naziv).IsRequired()
                .HasMaxLength(150);
            
            entity.Property(u => u.Opis).HasColumnType("text");

            entity.Property(u => u.JedinicaMere).IsRequired();
            
            entity.Property(u => u.Lager).HasDefaultValue(0).IsRequired();
        });
        modelBuilder.Entity<CenaProizvoda>(entity =>
        {
            entity.HasKey(p => new
            {
                p.IdProizvod, p.IdCena
            });
            
            entity.HasOne(n=>n.Proizvod)
                .WithMany(i=> i.ListCenaProizvoda)
                .HasForeignKey(n => n.IdProizvod);
            
           
            entity.Property(u => u.IdCena).UseSerialColumn();
            entity.Property(c => c.Cena).HasPrecision(10, 2);
            entity.Property(a => a.CreatedAt).HasColumnType("timestamptz");       
            entity.Property(c => c.JeAktivna)
                .IsRequired()
                .HasDefaultValue("aktivna")
                .HasMaxLength(9);
        });
        modelBuilder.Entity<Narudzbenica>(entity =>
        {
            entity.HasKey(u => u.IdNarudzbenica);
            entity.Property(u => u.IdNarudzbenica).UseSerialColumn();
            

            entity.HasOne(u => u.Kupac)
                .WithMany(a=>a.ListaNarudzbenica)
                .HasForeignKey(u => u.IdKupac);
            
            
            entity.HasOne(u => u.StatusNarudzbenice)
                .WithMany()
                .HasForeignKey(u => u.IdStatusNarudzbenice);

            entity.Property(n => n.NapomenaKupca).HasColumnType("text");
            entity.Property(a => a.DatumKreiranja).HasColumnType("timestamptz");      

        });
        modelBuilder.Entity<StatusNarudzbenice>(entity =>
        {
            entity.HasKey(u => u.IdStatus);
            entity.Property(u => u.IdStatus).UseSerialColumn();
            
            entity.Property(u=> u.NazivStatusa).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<StavkaNarudzbenice>(entity =>
        {
            entity.HasKey(u => new
            {
                u.IdNarudzbenica,u.RBrProizvoda
            });
            
            
            entity.HasOne(p => p.Proizvod)
                .WithMany()
                .HasForeignKey(p => p.IdProizvod);
            
            entity.HasOne(n=>n.Narudzbenica)
                .WithMany(st=>st.ListStavkeNarudzbenica)
                .HasForeignKey(n=>n.IdNarudzbenica);
            
            entity.Property(st => st.UgovorenaCena).HasPrecision(10, 2);
        });

        modelBuilder.Entity<ObradaNarudžbenice>(entity =>
        {
            entity.HasKey(n => n.IdObrada);
            entity.Property(n => n.IdObrada).UseSerialColumn();
            
            entity.HasOne(a => a.Administrator)
                .WithMany()
                .HasForeignKey(a => a.IdAdministrator);
            
            entity.HasOne(a => a.StatusNarudzbenica)
                .WithMany()
                .HasForeignKey(a => a.IdStatusNarudzbenica);
            
            entity.HasOne(a => a.Narudzbenica)
                .WithMany(k=>k.ListObrada)
                .HasForeignKey(a => a.IdNarudzbenica);

            entity.Property(a => a.Komentar).HasColumnType("text");
            entity.Property(a => a.DatumObrada).HasColumnType("timestamptz");       
        });

        #region Seedovanje StatusNarudzbenice tabele
        modelBuilder.Entity<StatusNarudzbenice>().HasData(
            new {IdStatus = 1, NazivStatusa="Kreiranо"},
            new {IdStatus = 2, NazivStatusa="Odobreno"},
            new {IdStatus = 3, NazivStatusa="Odbijeno"},
            new {IdStatus = 4, NazivStatusa="Isporučenо"}
        );

        #endregion
        
        #region Seedovanje Kategorija tabele
        modelBuilder.Entity<Kategorija>().HasData(
            new  { IdKategorija = 1, NazivKategorije = "Herbicidi" },
            new  { IdKategorija = 2, NazivKategorije = "Insekticidi" },
            new  { IdKategorija = 3, NazivKategorije = "Fungicidi" },
            new  { IdKategorija = 4, NazivKategorije = "Površinska đubriva" },
            new  { IdKategorija = 5, NazivKategorije = "Prihrane" },
            new  { IdKategorija = 6, NazivKategorije = "Seme povrća" },
            new  { IdKategorija = 7, NazivKategorije = "Seme žitarica" },
            new  { IdKategorija = 8, NazivKategorije = "Seme cveća" }
            );
        #endregion

        #region Seedovanje Proizvod tabele
        modelBuilder.Entity<Proizvod>().HasData(
            new {IdProizvod = 1, 
                Naziv = "Corum", 
                JedinicaMere=JedMere.l, 
                Lager=10, KategorijaId= 1 , 
                Opis="Corum® je jedini herbicid na tržištu formulisan specijalno za soju." +
                     " Na najselektivniji na?in suzbija širokolisne korove i jednogodišnje " +
                     "travne korove i predstavlja kompletno rešenje za post-em tretman."},
            
            new {IdProizvod = 2, 
                Naziv = "Glisando", 
                JedinicaMere=JedMere.l, 
                Lager=10, KategorijaId= 1 , 
                Opis="Glisando je regulator rasta za sprečavanje poleganja useva pšenice* i ječma"},
            
            new {IdProizvod = 3, 
                Naziv = "Rimex", 
                JedinicaMere=JedMere.Komad, 
                Lager=10, KategorijaId= 1 , 
                Opis="Herbicid Rimsulfuron pripada grupi Sulfoniluree"},
            
            new {IdProizvod = 4, 
                Naziv = "Glifomark", 
                JedinicaMere=JedMere.l, 
                Lager=10, KategorijaId= 1 , 
                Opis="neselektivni herbicid, usvaja se lisnom masom. " +
                     "Kreće se naviše i naniže u biljci, inaktivirase u " +
                     "kontaktu sa zemljištem. Inhibira EPSP, odnosno sintezu aromatičnih kiselina" +
                     " čime sprečava sintezu neophodnih aminokiselina za proizvodnju proteina."},
            
            new {IdProizvod = 5, 
                Naziv = "Glifol", 
                JedinicaMere=JedMere.l, 
                Lager=10, KategorijaId= 1 , 
                Opis="Aktivna materija: glifosat IPA (480 g/l) Formulacija: Koncentrovani rastvor (SL) "
            },
            
              // INSEKTICIDI - Kategorija 2
    new
    {
        IdProizvod = 6,
        Naziv = "Etiol Ultra 100 ml",
        JedinicaMere = JedMere.Komad,
        Lager = 15,
        KategorijaId = 2,
        Opis = "Insekticid za suzbijanje štetnih insekata."
    },
    new
    {
        IdProizvod = 7,
        Naziv = "Bros sprej protiv osa i stršljenova 400 ml",
        JedinicaMere = JedMere.Komad,
        Lager = 12,
        KategorijaId = 2,
        Opis = "Sprej za suzbijanje osa i stršljenova."
    },
    new
    {
        IdProizvod = 8,
        Naziv = "Nadzor 50 ml",
        JedinicaMere = JedMere.Komad,
        Lager = 10,
        KategorijaId = 2,
        Opis = "Preparat za zaštitu biljaka od štetočina."
    },
    new
    {
        IdProizvod = 9,
        Naziv = "Alverde 1 l",
        JedinicaMere = JedMere.l,
        Lager = 8,
        KategorijaId = 2,
        Opis = "Insekticid sa kontaktnim i digestivnim delovanjem."
    },

    // FUNGICIDI - Kategorija 3

    new
    {
        IdProizvod = 10,
        Naziv = "Olimp 0.5 l",
        JedinicaMere = JedMere.Komad,
        Lager = 10,
        KategorijaId = 3,
        Opis = "Fungicid za zaštitu biljaka od biljnih bolesti."
    },
    new
    {
        IdProizvod = 11,
        Naziv = "Semesan 1 l",
        JedinicaMere = JedMere.l,
        Lager = 10,
        KategorijaId = 3,
        Opis = "Preparat za zaštitu biljaka od gljivičnih oboljenja."
    },
    new
    {
        IdProizvod = 12,
        Naziv = "Alijansa 1 kg",
        JedinicaMere = JedMere.Kg,
        Lager = 10,
        KategorijaId = 3,
        Opis = "Sistemični i kontaktni fungicid."
    },

    // POVRŠINSKA ĐUBRIVA - Kategorija 4

    new
    {
        IdProizvod = 13,
        Naziv = "KAN 27% 25 kg Genezis",
        JedinicaMere = JedMere.Kg,
        Lager = 20,
        KategorijaId = 4,
        Opis = "Azotno mineralno đubrivo za prihranu biljaka."
    },
    new
    {
        IdProizvod = 14,
        Naziv = "Urea 46% ruska 25 kg",
        JedinicaMere = JedMere.Kg,
        Lager = 20,
        KategorijaId = 4,
        Opis = "Granulisano azotno đubrivo sa visokim sadržajem azota."
    },
    new
    {
        IdProizvod = 15,
        Naziv = "Amonijum nitrat AN Genezis 25 kg",
        JedinicaMere = JedMere.Kg,
        Lager = 20,
        KategorijaId = 4,
        Opis = "Azotno mineralno đubrivo za osnovnu i dopunsku prihranu."
    },

    // PRIHRANE - Kategorija 5

    new
    {
        IdProizvod = 16,
        Naziv = "Blumi za orhideje 500 ml",
        JedinicaMere = JedMere.Komad,
        Lager = 15,
        KategorijaId = 5,
        Opis = "Tečno đubrivo namenjeno prihrani orhideja."
    },
    new
    {
        IdProizvod = 17,
        Naziv = "Blumi za ruže 500 ml",
        JedinicaMere = JedMere.Komad,
        Lager = 15,
        KategorijaId = 5,
        Opis = "Tečno đubrivo namenjeno prihrani ruža."
    },
    new
    {
        IdProizvod = 18,
        Naziv = "Foligal kalcijum 1 l",
        JedinicaMere = JedMere.l,
        Lager = 10,
        KategorijaId = 5,
        Opis = "Tečno folijarno đubrivo sa kalcijumom."
    },
    new
    {
        IdProizvod = 19,
        Naziv = "Blumi za muškatle 500 ml",
        JedinicaMere = JedMere.Komad,
        Lager = 15,
        KategorijaId = 5,
        Opis = "Tečno đubrivo namenjeno prihrani muškatli."
    },

    // SEME POVRĆA - Kategorija 6

    new
    {
        IdProizvod = 20,
        Naziv = "Seme špinat Matador",
        JedinicaMere = JedMere.Komad,
        Lager = 30,
        KategorijaId = 6,
        Opis = "Seme špinata sorte Matador."
    },
    new
    {
        IdProizvod = 21,
        Naziv = "Seme tikvica Beogradska",
        JedinicaMere = JedMere.Komad,
        Lager = 30,
        KategorijaId = 6,
        Opis = "Seme tikvice sorte Beogradska."
    },
    new
    {
        IdProizvod = 22,
        Naziv = "Seme boranija Maxidor 100 g",
        JedinicaMere = JedMere.Komad,
        Lager = 25,
        KategorijaId = 6,
        Opis = "Seme boranije sorte Maxidor."
    },
    new
    {
        IdProizvod = 23,
        Naziv = "Beli luk domaći 200 g",
        JedinicaMere = JedMere.Komad,
        Lager = 25,
        KategorijaId = 6,
        Opis = "Domaći beli luk za sadnju."
    },

    // SEME ŽITARICA - Kategorija 7

    new
    {
        IdProizvod = 24,
        Naziv = "Semenska pšenica Basmati",
        JedinicaMere = JedMere.Kg,
        Lager = 50,
        KategorijaId = 7,
        Opis = "Semenska pšenica sorte Basmati."
    },
    new
    {
        IdProizvod = 25,
        Naziv = "Semenska pšenica LG Avenue",
        JedinicaMere = JedMere.Kg,
        Lager = 50,
        KategorijaId = 7,
        Opis = "Semenska pšenica sorte LG Avenue."
    },
    new
    {
        IdProizvod = 26,
        Naziv = "Semenski ječam Grand",
        JedinicaMere = JedMere.Kg,
        Lager = 50,
        KategorijaId = 7,
        Opis = "Semenski ječam sorte Grand."
    },
    new
    {
        IdProizvod = 27,
        Naziv = "Semenska pšenica Sofru",
        JedinicaMere = JedMere.Kg,
        Lager = 50,
        KategorijaId = 7,
        Opis = "Semenska pšenica sorte Sofru."
    },

    // SEME CVEĆA - Kategorija 8

    new
    {
        IdProizvod = 28,
        Naziv = "Seme cveća Petunija",
        JedinicaMere = JedMere.Komad,
        Lager = 30,
        KategorijaId = 8,
        Opis = "Seme petunije za uzgoj ukrasnog cveća."
    },
    new
    {
        IdProizvod = 29,
        Naziv = "Seme cveća Dan i noć",
        JedinicaMere = JedMere.Komad,
        Lager = 30,
        KategorijaId = 8,
        Opis = "Seme cveća dan i noć."
    },
    new
    {
        IdProizvod = 30,
        Naziv = "Seme cveća Noćna frajla",
        JedinicaMere = JedMere.Komad,
        Lager = 30,
        KategorijaId = 8,
        Opis = "Seme ukrasnog cveća noćna frajla."
    },
    new
    {
        IdProizvod = 31,
        Naziv = "Seme cveća Goruća ljubav",
        JedinicaMere = JedMere.Komad,
        Lager = 30,
        KategorijaId = 8,
        Opis = "Seme ukrasnog cveća goruća ljubav."
    },
    new
    {
        IdProizvod = 32,
        Naziv = "Seme cveća Neven",
        JedinicaMere = JedMere.Komad,
        Lager = 30,
        KategorijaId = 8,
        Opis = "Seme nevena za uzgoj ukrasnog cveća."
    }
      
            );
        #endregion

        #region Seedovanje CenaProizvoda tabele
        modelBuilder.Entity<CenaProizvoda>().HasData(

    // PROIZVOD 1 - Corum
    new
    {
        IdProizvod = 1,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 5, 15, 10, 0, 0, TimeSpan.Zero),
        Cena = 1800.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 1,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 5, 20, 10, 0, 0, TimeSpan.Zero),
        Cena = 1950.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 2 - Glisando
    new
    {
        IdProizvod = 2,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 6, 10, 10, 0, 0, TimeSpan.Zero),
        Cena = 1450.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 2,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 6, 12, 10, 0, 0, TimeSpan.Zero),
        Cena = 1580.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 3 - Rimex
    new
    {
        IdProizvod = 3,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 4, 18, 10, 0, 0, TimeSpan.Zero),
        Cena = 2100.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 3,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 4, 22, 10, 0, 0, TimeSpan.Zero),
        Cena = 2250.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 4 - Glifomark
    new
    {
        IdProizvod = 4,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 7, 8, 10, 0, 0, TimeSpan.Zero),
        Cena = 950.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 4,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 7, 14, 10, 0, 0, TimeSpan.Zero),
        Cena = 1020.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 5 - Glifol
    new
    {
        IdProizvod = 5,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 8, 5, 10, 0, 0, TimeSpan.Zero),
        Cena = 1100.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 5,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 8, 11, 10, 0, 0, TimeSpan.Zero),
        Cena = 1180.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 6 - Etiol Ultra
    new
    {
        IdProizvod = 6,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 5, 12, 10, 0, 0, TimeSpan.Zero),
        Cena = 350.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 6,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 5, 18, 10, 0, 0, TimeSpan.Zero),
        Cena = 395.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 7 - Bros sprej
    new
    {
        IdProizvod = 7,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 6, 15, 10, 0, 0, TimeSpan.Zero),
        Cena = 650.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 7,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 6, 20, 10, 0, 0, TimeSpan.Zero),
        Cena = 720.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 8 - Nadzor
    new
    {
        IdProizvod = 8,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 4, 20, 10, 0, 0, TimeSpan.Zero),
        Cena = 480.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 8,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 4, 25, 10, 0, 0, TimeSpan.Zero),
        Cena = 520.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 9 - Alverde
    new
    {
        IdProizvod = 9,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 7, 10, 10, 0, 0, TimeSpan.Zero),
        Cena = 8700.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 9,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 7, 15, 10, 0, 0, TimeSpan.Zero),
        Cena = 9435.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 10 - Olimp
    new
    {
        IdProizvod = 10,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 5, 14, 10, 0, 0, TimeSpan.Zero),
        Cena = 1950.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 10,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 5, 21, 10, 0, 0, TimeSpan.Zero),
        Cena = 2190.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 11 - Semesan
    new
    {
        IdProizvod = 11,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 6, 11, 10, 0, 0, TimeSpan.Zero),
        Cena = 1250.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 11,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 6, 16, 10, 0, 0, TimeSpan.Zero),
        Cena = 1350.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 12 - Alijansa
    new
    {
        IdProizvod = 12,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 8, 12, 10, 0, 0, TimeSpan.Zero),
        Cena = 2700.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 12,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 8, 18, 10, 0, 0, TimeSpan.Zero),
        Cena = 2950.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 13 - KAN
    new
    {
        IdProizvod = 13,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 4, 15, 10, 0, 0, TimeSpan.Zero),
        Cena = 1600.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 13,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 4, 19, 10, 0, 0, TimeSpan.Zero),
        Cena = 1775.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 14 - Urea
    new
    {
        IdProizvod = 14,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 5, 17, 10, 0, 0, TimeSpan.Zero),
        Cena = 1800.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 14,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 5, 23, 10, 0, 0, TimeSpan.Zero),
        Cena = 1975.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 15 - Amonijum nitrat
    new
    {
        IdProizvod = 15,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 7, 13, 10, 0, 0, TimeSpan.Zero),
        Cena = 1900.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 15,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 7, 20, 10, 0, 0, TimeSpan.Zero),
        Cena = 2050.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 16 - Blumi orhideje
    new
    {
        IdProizvod = 16,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 5, 11, 10, 0, 0, TimeSpan.Zero),
        Cena = 310.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 16,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 5, 17, 10, 0, 0, TimeSpan.Zero),
        Cena = 350.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 17 - Blumi ruže
    new
    {
        IdProizvod = 17,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 6, 13, 10, 0, 0, TimeSpan.Zero),
        Cena = 310.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 17,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 6, 18, 10, 0, 0, TimeSpan.Zero),
        Cena = 350.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 18 - Foligal
    new
    {
        IdProizvod = 18,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 4, 12, 10, 0, 0, TimeSpan.Zero),
        Cena = 700.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 18,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 4, 17, 10, 0, 0, TimeSpan.Zero),
        Cena = 790.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 19 - Blumi muškatle
    new
    {
        IdProizvod = 19,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 7, 16, 10, 0, 0, TimeSpan.Zero),
        Cena = 310.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 19,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 7, 21, 10, 0, 0, TimeSpan.Zero),
        Cena = 350.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 20 - Špinat
    new
    {
        IdProizvod = 20,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 5, 19, 10, 0, 0, TimeSpan.Zero),
        Cena = 45.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 20,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 5, 24, 10, 0, 0, TimeSpan.Zero),
        Cena = 50.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 21 - Tikvica
    new
    {
        IdProizvod = 21,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 6, 14, 10, 0, 0, TimeSpan.Zero),
        Cena = 55.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 21,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 6, 19, 10, 0, 0, TimeSpan.Zero),
        Cena = 65.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 22 - Boranija
    new
    {
        IdProizvod = 22,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 4, 16, 10, 0, 0, TimeSpan.Zero),
        Cena = 220.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 22,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 4, 23, 10, 0, 0, TimeSpan.Zero),
        Cena = 245.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 23 - Beli luk
    new
    {
        IdProizvod = 23,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 8, 14, 10, 0, 0, TimeSpan.Zero),
        Cena = 250.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 23,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 8, 20, 10, 0, 0, TimeSpan.Zero),
        Cena = 285.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 24 - Basmati
    new
    {
        IdProizvod = 24,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 5, 13, 10, 0, 0, TimeSpan.Zero),
        Cena = 70.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 24,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 5, 18, 10, 0, 0, TimeSpan.Zero),
        Cena = 79.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 25 - LG Avenue
    new
    {
        IdProizvod = 25,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 6, 17, 10, 0, 0, TimeSpan.Zero),
        Cena = 70.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 25,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 6, 22, 10, 0, 0, TimeSpan.Zero),
        Cena = 79.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 26 - Ječam Grand
    new
    {
        IdProizvod = 26,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 4, 19, 10, 0, 0, TimeSpan.Zero),
        Cena = 60.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 26,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 4, 24, 10, 0, 0, TimeSpan.Zero),
        Cena = 69.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 27 - Sofru
    new
    {
        IdProizvod = 27,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 7, 17, 10, 0, 0, TimeSpan.Zero),
        Cena = 72.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 27,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 7, 23, 10, 0, 0, TimeSpan.Zero),
        Cena = 82.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 28 - Petunija
    new
    {
        IdProizvod = 28,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 5, 16, 10, 0, 0, TimeSpan.Zero),
        Cena = 50.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 28,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 5, 21, 10, 0, 0, TimeSpan.Zero),
        Cena = 60.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 29 - Dan i noć
    new
    {
        IdProizvod = 29,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 6, 16, 10, 0, 0, TimeSpan.Zero),
        Cena = 50.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 29,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 6, 21, 10, 0, 0, TimeSpan.Zero),
        Cena = 60.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 30 - Noćna frajla
    new
    {
        IdProizvod = 30,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 4, 17, 10, 0, 0, TimeSpan.Zero),
        Cena = 50.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 30,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 4, 22, 10, 0, 0, TimeSpan.Zero),
        Cena = 60.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 31 - Goruća ljubav
    new
    {
        IdProizvod = 31,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 7, 15, 10, 0, 0, TimeSpan.Zero),
        Cena = 50.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 31,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 7, 20, 10, 0, 0, TimeSpan.Zero),
        Cena = 60.00m,
        JeAktivna = "aktivna"
    },

    // PROIZVOD 32 - Neven
    new
    {
        IdProizvod = 32,
        IdCena = 1,
        CreatedAt = new DateTimeOffset(2025, 8, 16, 10, 0, 0, TimeSpan.Zero),
        Cena = 50.00m,
        JeAktivna = "neaktivna"
    },
    new
    {
        IdProizvod = 32,
        IdCena = 2,
        CreatedAt = new DateTimeOffset(2026, 8, 22, 10, 0, 0, TimeSpan.Zero),
        Cena = 60.00m,
        JeAktivna = "aktivna"
    }
);

        #endregion

        #region seedovanje KorisnikKupacAdministrator tabele
        modelBuilder.Entity<Administrator>().HasData(
    new
    {
        IdKorisnik = 1,
        Ime = "Uroš",
        Prezime = "Anđelković",
        Email = "uros.andjelkovic@example.com",
        LozinkaHash = "e++XzzeI5s1lsuspW+dLaw==.h/HKvS10tI474RD/8ZFjBdl+Cu41h24vGZs/5urigXQ="
    }
);

modelBuilder.Entity<Kupac>().HasData(
    new
    {
        IdKorisnik = 2,
        Ime = "Jovana",
        Prezime = "Đokić",
        Email = "jovana.djokic@example.com",
        LozinkaHash = "e++XzzeI5s1lsuspW+dLaw==.h/HKvS10tI474RD/8ZFjBdl+Cu41h24vGZs/5urigXQ=",
        Telefon = "0612345678",
        Grad = "Beograd",
        Adresa = "Bulevar kralja Aleksandra 25"
    },
    new
    {
        IdKorisnik = 3,
        Ime = "Jovana",
        Prezime = "Stojanović",
        Email = "jovana.stojanovic@example.com",
        LozinkaHash = "e++XzzeI5s1lsuspW+dLaw==.h/HKvS10tI474RD/8ZFjBdl+Cu41h24vGZs/5urigXQ=",
        Telefon = "0623456789",
        Grad = "Novi Sad",
        Adresa = "Futoška 42"
    },
    new
    {
        IdKorisnik = 4,
        Ime = "Dragoslav",
        Prezime = "Stojanović",
        Email = "dragoslav.stojanovic@example.com",
        LozinkaHash = "e++XzzeI5s1lsuspW+dLaw==.h/HKvS10tI474RD/8ZFjBdl+Cu41h24vGZs/5urigXQ=",
        Telefon = "0654567890",
        Grad = "Niš",
        Adresa = "Vizantijski bulevar 18"
    },
    new
    {
        IdKorisnik = 5,
        Ime = "Jovan",
        Prezime = "Vukojević",
        Email = "jovan.vukojevic@example.com",
        LozinkaHash = "e++XzzeI5s1lsuspW+dLaw==.h/HKvS10tI474RD/8ZFjBdl+Cu41h24vGZs/5urigXQ=",
        Telefon = "0615678901",
        Grad = "Kragujevac",
        Adresa = "Kralja Petra I 63"
    },
    new
    {
        IdKorisnik = 6,
        Ime = "Nikola",
        Prezime = "Vuksanović",
        Email = "nikola.vuksanovic@example.com",
        LozinkaHash = "e++XzzeI5s1lsuspW+dLaw==.h/HKvS10tI474RD/8ZFjBdl+Cu41h24vGZs/5urigXQ=",
        Telefon = "0626789012",
        Grad = "Subotica",
        Adresa = "Matije Korvina 11"
    },
    new
    {
        IdKorisnik = 7,
        Ime = "Natalija",
        Prezime = "SvrkoVukić",
        Email = "natalija.svrkovukic@example.com",
        LozinkaHash = "e++XzzeI5s1lsuspW+dLaw==.h/HKvS10tI474RD/8ZFjBdl+Cu41h24vGZs/5urigXQ=",
        Telefon = "0657890123",
        Grad = "Pančevo",
        Adresa = "Miloša Obrenovića 37"
    },
    new
    {
        IdKorisnik = 8,
        Ime = "Milan",
        Prezime = "Mladenović",
        Email = "milan.mladenovic@example.com",
        LozinkaHash = "e++XzzeI5s1lsuspW+dLaw==.h/HKvS10tI474RD/8ZFjBdl+Cu41h24vGZs/5urigXQ=",
        Telefon = "0618901234",
        Grad = "Čačak",
        Adresa = "Župana Stracimira 29"
    }
);

        #endregion

        #region Seedovanje Narudzbenice tabele
        modelBuilder.Entity<Narudzbenica>().HasData(
    
    // ISPORUČENE
    //-------------------------
    new
    {
        IdNarudzbenica = 1,
        DatumKreiranja = new DateTimeOffset(2026, 3, 5, 10, 15, 0, TimeSpan.Zero),
        NapomenaKupca = "Molim vas isporučiti u prepodnevnim časovima.",
        IdStatusNarudzbenice = 4,
        IdKupac = 2,
        IdAdministrator = 1
    },
    new
    {
        IdNarudzbenica = 2,
        DatumKreiranja = new DateTimeOffset(2026, 3, 8, 12, 30, 0, TimeSpan.Zero),
        NapomenaKupca = "Pozvati kupca pre isporuke.",
        IdStatusNarudzbenice = 4,
        IdKupac = 3,
        IdAdministrator = 1
    },
    new
    {
        IdNarudzbenica = 3,
        DatumKreiranja = new DateTimeOffset(2026, 3, 12, 9, 45, 0, TimeSpan.Zero),
        NapomenaKupca = "",
        IdStatusNarudzbenice = 4,
        IdKupac = 4,
        IdAdministrator = 1
    },

    // --------------------------
    // ODBIJENE
    // ------------------------
    new
    {
        IdNarudzbenica = 4,
        DatumKreiranja = new DateTimeOffset(2026, 3, 15, 14, 20, 0, TimeSpan.Zero),
        NapomenaKupca = "Potrebno što pre.",
        IdStatusNarudzbenice = 3,
        IdKupac = 5,
        IdAdministrator = 1
    },
    new
    {
        IdNarudzbenica = 5,
        DatumKreiranja = new DateTimeOffset(2026, 3, 18, 11, 10, 0, TimeSpan.Zero),
        NapomenaKupca = "",
        IdStatusNarudzbenice = 3,
        IdKupac = 6,
        IdAdministrator = 1
    },
    new
    {
        IdNarudzbenica = 6,
        DatumKreiranja = new DateTimeOffset(2026, 3, 21, 16, 40, 0, TimeSpan.Zero),
        NapomenaKupca = "Molim proveru dostupnosti proizvoda.",
        IdStatusNarudzbenice = 3,
        IdKupac = 7,
        IdAdministrator = 1
    },

 
    // ODOBRENE
    // --------------------------
    new
    {
        IdNarudzbenica = 7,
        DatumKreiranja = new DateTimeOffset(2026, 3, 24, 8, 30, 0, TimeSpan.Zero),
        NapomenaKupca = "Isporuka na adresu kupca.",
        IdStatusNarudzbenice = 2,
        IdKupac = 8,
        IdAdministrator = 1
    },
    new
    {
        IdNarudzbenica = 8,
        DatumKreiranja = new DateTimeOffset(2026, 3, 26, 13, 15, 0, TimeSpan.Zero),
        NapomenaKupca = "",
        IdStatusNarudzbenice = 2,
        IdKupac = 2,
        IdAdministrator = 1
    },
    new
    {
        IdNarudzbenica = 9,
        DatumKreiranja = new DateTimeOffset(2026, 3, 28, 10, 50, 0, TimeSpan.Zero),
        NapomenaKupca = "Pozvati pre slanja.",
        IdStatusNarudzbenice = 2,
        IdKupac = 3,
        IdAdministrator = 1
    },


    // KREIRANE
    // --------------------------
    new
    {
        IdNarudzbenica = 10,
        DatumKreiranja = new DateTimeOffset(2026, 4, 1, 9, 20, 0, TimeSpan.Zero),
        NapomenaKupca = "",
        IdStatusNarudzbenice = 1,
        IdKupac = 4,
        IdAdministrator = (int?)null
    },
    new
    {
        IdNarudzbenica = 11,
        DatumKreiranja = new DateTimeOffset(2026, 4, 3, 11, 35, 0, TimeSpan.Zero),
        NapomenaKupca = "Isporuka tokom radnog vremena.",
        IdStatusNarudzbenice = 1,
        IdKupac = 5,
        IdAdministrator = (int?)null
    },
    new
    {
        IdNarudzbenica = 12,
        DatumKreiranja = new DateTimeOffset(2026, 4, 5, 15, 10, 0, TimeSpan.Zero),
        NapomenaKupca = "",
        IdStatusNarudzbenice = 1,
        IdKupac = 6,
        IdAdministrator = (int?)null
    },
    new
    {
        IdNarudzbenica = 13,
        DatumKreiranja = new DateTimeOffset(2026, 4, 7, 10, 5, 0, TimeSpan.Zero),
        NapomenaKupca = "Molim potvrdu narudžbenice.",
        IdStatusNarudzbenice = 1,
        IdKupac = 7,
        IdAdministrator = (int?)null
    },
    new
    {
        IdNarudzbenica = 14,
        DatumKreiranja = new DateTimeOffset(2026, 4, 9, 12, 45, 0, TimeSpan.Zero),
        NapomenaKupca = "Pozvati kupca.",
        IdStatusNarudzbenice = 1,
        IdKupac = 8,
        IdAdministrator = (int?)null
    },
    new
    {
        IdNarudzbenica = 15,
        DatumKreiranja = new DateTimeOffset(2026, 4, 11, 14, 25, 0, TimeSpan.Zero),
        NapomenaKupca = "",
        IdStatusNarudzbenice = 1,
        IdKupac = 2,
        IdAdministrator = (int?)null
    }
);
        #endregion

        #region Seedovanje StavkeNarudzbenice tabele
        modelBuilder.Entity<StavkaNarudzbenice>().HasData(

    //==========================
    new { IdNarudzbenica = 1, RBrProizvoda = 1, IdProizvod = 1, Kolicina = 2, UgovorenaCena = 1850m },
    new { IdNarudzbenica = 1, RBrProizvoda = 2, IdProizvod = 13, Kolicina = 3, UgovorenaCena = 1250m },
    new { IdNarudzbenica = 1, RBrProizvoda = 3, IdProizvod = 20, Kolicina = 5, UgovorenaCena = 180m },
    new { IdNarudzbenica = 1, RBrProizvoda = 4, IdProizvod = 28, Kolicina = 2, UgovorenaCena = 120m },

    //==========================
    new { IdNarudzbenica = 2, RBrProizvoda = 1, IdProizvod = 2, Kolicina = 1, UgovorenaCena = 2100m },
    new { IdNarudzbenica = 2, RBrProizvoda = 2, IdProizvod = 16, Kolicina = 4, UgovorenaCena = 350m },
    new { IdNarudzbenica = 2, RBrProizvoda = 3, IdProizvod = 24, Kolicina = 10, UgovorenaCena = 95m },

    //==========================
    // -------------------------
    new { IdNarudzbenica = 3, RBrProizvoda = 1, IdProizvod = 3, Kolicina = 2, UgovorenaCena = 1650m },
    new { IdNarudzbenica = 3, RBrProizvoda = 2, IdProizvod = 8, Kolicina = 3, UgovorenaCena = 890m },
    new { IdNarudzbenica = 3, RBrProizvoda = 3, IdProizvod = 18, Kolicina = 2, UgovorenaCena = 740m },
    new { IdNarudzbenica = 3, RBrProizvoda = 4, IdProizvod = 21, Kolicina = 4, UgovorenaCena = 160m },
    new { IdNarudzbenica = 3, RBrProizvoda = 5, IdProizvod = 26, Kolicina = 15, UgovorenaCena = 82m },
    new { IdNarudzbenica = 3, RBrProizvoda = 6, IdProizvod = 32, Kolicina = 3, UgovorenaCena = 130m },

    //==========================
    new { IdNarudzbenica = 4, RBrProizvoda = 1, IdProizvod = 4, Kolicina = 1, UgovorenaCena = 2300m },
    new { IdNarudzbenica = 4, RBrProizvoda = 2, IdProizvod = 14, Kolicina = 2, UgovorenaCena = 1450m },

    //==========================
    new { IdNarudzbenica = 5, RBrProizvoda = 1, IdProizvod = 5, Kolicina = 2, UgovorenaCena = 1950m },
    new { IdNarudzbenica = 5, RBrProizvoda = 2, IdProizvod = 9, Kolicina = 1, UgovorenaCena = 620m },
    new { IdNarudzbenica = 5, RBrProizvoda = 3, IdProizvod = 17, Kolicina = 3, UgovorenaCena = 360m },
    new { IdNarudzbenica = 5, RBrProizvoda = 4, IdProizvod = 23, Kolicina = 4, UgovorenaCena = 240m },
    new { IdNarudzbenica = 5, RBrProizvoda = 5, IdProizvod = 29, Kolicina = 2, UgovorenaCena = 110m },

    //==========================
    new { IdNarudzbenica = 6, RBrProizvoda = 1, IdProizvod = 6, Kolicina = 2, UgovorenaCena = 480m },
    new { IdNarudzbenica = 6, RBrProizvoda = 2, IdProizvod = 10, Kolicina = 1, UgovorenaCena = 910m },
    new { IdNarudzbenica = 6, RBrProizvoda = 3, IdProizvod = 15, Kolicina = 3, UgovorenaCena = 1380m },
    new { IdNarudzbenica = 6, RBrProizvoda = 4, IdProizvod = 31, Kolicina = 5, UgovorenaCena = 125m },

   //==========================
    new { IdNarudzbenica = 7, RBrProizvoda = 1, IdProizvod = 1, Kolicina = 1, UgovorenaCena = 1850m },
    new { IdNarudzbenica = 7, RBrProizvoda = 2, IdProizvod = 7, Kolicina = 2, UgovorenaCena = 430m },
    new { IdNarudzbenica = 7, RBrProizvoda = 3, IdProizvod = 11, Kolicina = 1, UgovorenaCena = 760m },
    new { IdNarudzbenica = 7, RBrProizvoda = 4, IdProizvod = 16, Kolicina = 2, UgovorenaCena = 350m },
    new { IdNarudzbenica = 7, RBrProizvoda = 5, IdProizvod = 22, Kolicina = 3, UgovorenaCena = 290m },
    new { IdNarudzbenica = 7, RBrProizvoda = 6, IdProizvod = 25, Kolicina = 10, UgovorenaCena = 98m },
    new { IdNarudzbenica = 7, RBrProizvoda = 7, IdProizvod = 30, Kolicina = 2, UgovorenaCena = 115m },

    //==========================
    new { IdNarudzbenica = 8, RBrProizvoda = 1, IdProizvod = 12, Kolicina = 2, UgovorenaCena = 1350m },
    new { IdNarudzbenica = 8, RBrProizvoda = 2, IdProizvod = 19, Kolicina = 4, UgovorenaCena = 330m },
    new { IdNarudzbenica = 8, RBrProizvoda = 3, IdProizvod = 27, Kolicina = 20, UgovorenaCena = 88m },

    // -------------------------
    new { IdNarudzbenica = 9, RBrProizvoda = 1, IdProizvod = 2, Kolicina = 2, UgovorenaCena = 2100m },
    new { IdNarudzbenica = 9, RBrProizvoda = 2, IdProizvod = 9, Kolicina = 2, UgovorenaCena = 620m },
    new { IdNarudzbenica = 9, RBrProizvoda = 3, IdProizvod = 18, Kolicina = 3, UgovorenaCena = 740m },
    new { IdNarudzbenica = 9, RBrProizvoda = 4, IdProizvod = 20, Kolicina = 5, UgovorenaCena = 180m },
    new { IdNarudzbenica = 9, RBrProizvoda = 5, IdProizvod = 28, Kolicina = 4, UgovorenaCena = 120m },

    // ---------------------------
    new { IdNarudzbenica = 10, RBrProizvoda = 1, IdProizvod = 13, Kolicina = 2, UgovorenaCena = 1250m },
    new { IdNarudzbenica = 10, RBrProizvoda = 2, IdProizvod = 21, Kolicina = 3, UgovorenaCena = 160m },

    // --------------------------------------------------
    new { IdNarudzbenica = 11, RBrProizvoda = 1, IdProizvod = 4, Kolicina = 1, UgovorenaCena = 2300m },
    new { IdNarudzbenica = 11, RBrProizvoda = 2, IdProizvod = 8, Kolicina = 2, UgovorenaCena = 890m },
    new { IdNarudzbenica = 11, RBrProizvoda = 3, IdProizvod = 17, Kolicina = 2, UgovorenaCena = 360m },
    new { IdNarudzbenica = 11, RBrProizvoda = 4, IdProizvod = 32, Kolicina = 3, UgovorenaCena = 130m },

    
    // ====-------------------------
    new { IdNarudzbenica = 12, RBrProizvoda = 1, IdProizvod = 5, Kolicina = 1, UgovorenaCena = 1950m },
    new { IdNarudzbenica = 12, RBrProizvoda = 2, IdProizvod = 10, Kolicina = 2, UgovorenaCena = 910m },
    new { IdNarudzbenica = 12, RBrProizvoda = 3, IdProizvod = 14, Kolicina = 2, UgovorenaCena = 1450m },
    new { IdNarudzbenica = 12, RBrProizvoda = 4, IdProizvod = 23, Kolicina = 3, UgovorenaCena = 240m },
    new { IdNarudzbenica = 12, RBrProizvoda = 5, IdProizvod = 26, Kolicina = 10, UgovorenaCena = 82m },
    new { IdNarudzbenica = 12, RBrProizvoda = 6, IdProizvod = 29, Kolicina = 2, UgovorenaCena = 110m },

    //-------------------------
    new { IdNarudzbenica = 13, RBrProizvoda = 1, IdProizvod = 7, Kolicina = 2, UgovorenaCena = 430m },
    new { IdNarudzbenica = 13, RBrProizvoda = 2, IdProizvod = 15, Kolicina = 1, UgovorenaCena = 1380m },
    new { IdNarudzbenica = 13, RBrProizvoda = 3, IdProizvod = 31, Kolicina = 4, UgovorenaCena = 125m },

    // =-------------------------
    new { IdNarudzbenica = 14, RBrProizvoda = 1, IdProizvod = 3, Kolicina = 1, UgovorenaCena = 1650m },
    new { IdNarudzbenica = 14, RBrProizvoda = 2, IdProizvod = 11, Kolicina = 2, UgovorenaCena = 760m },
    new { IdNarudzbenica = 14, RBrProizvoda = 3, IdProizvod = 19, Kolicina = 3, UgovorenaCena = 330m },
    new { IdNarudzbenica = 14, RBrProizvoda = 4, IdProizvod = 24, Kolicina = 15, UgovorenaCena = 95m },
    new { IdNarudzbenica = 14, RBrProizvoda = 5, IdProizvod = 30, Kolicina = 2, UgovorenaCena = 115m },

    // -------------------------
    new { IdNarudzbenica = 15, RBrProizvoda = 1, IdProizvod = 6, Kolicina = 1, UgovorenaCena = 480m },
    new { IdNarudzbenica = 15, RBrProizvoda = 2, IdProizvod = 22, Kolicina = 3, UgovorenaCena = 290m }
);

        #endregion

        #region Seedovanje ObradaNarudzbenice tabele
        modelBuilder.Entity<ObradaNarudžbenice>().HasData(

    
    //  KREIRANA -> ODOBRENA -> ISPORUČENA
    new
    {
        IdObrada = 1,
        DatumObrada = new DateTimeOffset(2026, 3, 5, 10, 15, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica kreirana.",
        IdAdministrator = (int?)null,
        IdNarudzbenica = 1,
        IdStatusNarudzbenica = 1
    },
    new
    {
        IdObrada = 2,
        DatumObrada = new DateTimeOffset(2026, 3, 5, 13, 20, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica odobrena.",
        IdAdministrator = 1,
        IdNarudzbenica = 1,
        IdStatusNarudzbenica = 2
    },
    new
    {
        IdObrada = 3,
        DatumObrada = new DateTimeOffset(2026, 3, 7, 11, 0, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica isporučena kupcu.",
        IdAdministrator = 1,
        IdNarudzbenica = 1,
        IdStatusNarudzbenica = 4
    },

    //  KREIRANA -> ODOBRENA -> ISPORUČENA
    new
    {
        IdObrada = 4,
        DatumObrada = new DateTimeOffset(2026, 3, 8, 12, 30, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica kreirana.",
        IdAdministrator = (int?)null,
        IdNarudzbenica = 2,
        IdStatusNarudzbenica = 1
    },
    new
    {
        IdObrada = 5,
        DatumObrada = new DateTimeOffset(2026, 3, 8, 15, 10, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica odobrena.",
        IdAdministrator = 1,
        IdNarudzbenica = 2,
        IdStatusNarudzbenica = 2
    },
    new
    {
        IdObrada = 6,
        DatumObrada = new DateTimeOffset(2026, 3, 10, 9, 30, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica isporučena kupcu.",
        IdAdministrator = 1,
        IdNarudzbenica = 2,
        IdStatusNarudzbenica = 4
    },

    // KREIRANA -> ODOBRENA -> ISPORUČENA
    new
    {
        IdObrada = 7,
        DatumObrada = new DateTimeOffset(2026, 3, 12, 9, 45, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica kreirana.",
        IdAdministrator = (int?)null,
        IdNarudzbenica = 3,
        IdStatusNarudzbenica = 1
    },
    new
    {
        IdObrada = 8,
        DatumObrada = new DateTimeOffset(2026, 3, 12, 12, 15, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica odobrena.",
        IdAdministrator = 1,
        IdNarudzbenica = 3,
        IdStatusNarudzbenica = 2
    },
    new
    {
        IdObrada = 9,
        DatumObrada = new DateTimeOffset(2026, 3, 14, 10, 0, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica isporučena kupcu.",
        IdAdministrator = 1,
        IdNarudzbenica = 3,
        IdStatusNarudzbenica = 4
    },

    //  KREIRANA -> ODBIJENA
    new
    {
        IdObrada = 10,
        DatumObrada = new DateTimeOffset(2026, 3, 15, 14, 20, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica kreirana.",
        IdAdministrator = (int?)null,
        IdNarudzbenica = 4,
        IdStatusNarudzbenica = 1
    },
    new
    {
        IdObrada = 11,
        DatumObrada = new DateTimeOffset(2026, 3, 15, 16, 0, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica odbijena zbog nedostupnosti proizvoda.",
        IdAdministrator = 1,
        IdNarudzbenica = 4,
        IdStatusNarudzbenica = 3
    },

    //  KREIRANA -> ODBIJENA
    new
    {
        IdObrada = 12,
        DatumObrada = new DateTimeOffset(2026, 3, 18, 11, 10, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica kreirana.",
        IdAdministrator = (int?)null,
        IdNarudzbenica = 5,
        IdStatusNarudzbenica = 1
    },
    new
    {
        IdObrada = 13,
        DatumObrada = new DateTimeOffset(2026, 3, 18, 14, 30, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica odbijena.",
        IdAdministrator = 1,
        IdNarudzbenica = 5,
        IdStatusNarudzbenica = 3
    },

    //  KREIRANA -> ODBIJENA
    new
    {
        IdObrada = 14,
        DatumObrada = new DateTimeOffset(2026, 3, 21, 16, 40, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica kreirana.",
        IdAdministrator = (int?)null,
        IdNarudzbenica = 6,
        IdStatusNarudzbenica = 1
    },
    new
    {
        IdObrada = 15,
        DatumObrada = new DateTimeOffset(2026, 3, 22, 9, 15, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica odbijena zbog nedostupnosti proizvoda.",
        IdAdministrator = 1,
        IdNarudzbenica = 6,
        IdStatusNarudzbenica = 3
    },

    //  KREIRANA -> ODOBRENA
    new
    {
        IdObrada = 16,
        DatumObrada = new DateTimeOffset(2026, 3, 24, 8, 30, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica kreirana.",
        IdAdministrator = (int?)null,
        IdNarudzbenica = 7,
        IdStatusNarudzbenica = 1
    },
    new
    {
        IdObrada = 17,
        DatumObrada = new DateTimeOffset(2026, 3, 24, 11, 0, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica odobrena.",
        IdAdministrator = 1,
        IdNarudzbenica = 7,
        IdStatusNarudzbenica = 2
    },

    // KREIRANA -> ODOBRENA
    new
    {
        IdObrada = 18,
        DatumObrada = new DateTimeOffset(2026, 3, 26, 13, 15, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica kreirana.",
        IdAdministrator = (int?)null,
        IdNarudzbenica = 8,
        IdStatusNarudzbenica = 1
    },
    new
    {
        IdObrada = 19,
        DatumObrada = new DateTimeOffset(2026, 3, 26, 16, 0, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica odobrena.",
        IdAdministrator = 1,
        IdNarudzbenica = 8,
        IdStatusNarudzbenica = 2
    },

    // KREIRANA -> ODOBRENA
    new
    {
        IdObrada = 20,
        DatumObrada = new DateTimeOffset(2026, 3, 28, 10, 50, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica kreirana.",
        IdAdministrator = (int?)null,
        IdNarudzbenica = 9,
        IdStatusNarudzbenica = 1
    },
    new
    {
        IdObrada = 21,
        DatumObrada = new DateTimeOffset(2026, 3, 28, 13, 30, 0, TimeSpan.Zero),
        Komentar = "Narudžbenica odobrena.",
        IdAdministrator = 1,
        IdNarudzbenica = 9,
        IdStatusNarudzbenica = 2
    }
);
        
        #endregion
    }


}