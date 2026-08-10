                                                                                                                                                                          

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
    }


}