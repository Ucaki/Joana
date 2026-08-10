using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Joana.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorija",
                columns: table => new
                {
                    IdKategorija = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NazivKategorije = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorija", x => x.IdKategorija);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    IdKorisnik = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Ime = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LozinkaHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    Telefon = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Adresa = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Grad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.IdKorisnik);
                });

            migrationBuilder.CreateTable(
                name: "StatusNarudzbenice",
                columns: table => new
                {
                    IdStatus = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NazivStatusa = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusNarudzbenice", x => x.IdStatus);
                });

            migrationBuilder.CreateTable(
                name: "Proizvod",
                columns: table => new
                {
                    IdProizvod = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Naziv = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Opis = table.Column<string>(type: "text", nullable: true),
                    JedinicaMere = table.Column<int>(type: "integer", nullable: false),
                    Lager = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    KategorijaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvod", x => x.IdProizvod);
                    table.ForeignKey(
                        name: "FK_Proizvod_Kategorija_KategorijaId",
                        column: x => x.KategorijaId,
                        principalTable: "Kategorija",
                        principalColumn: "IdKategorija",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Narudzbenica",
                columns: table => new
                {
                    IdNarudzbenica = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DatumKreiranja = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    NapomenaKupca = table.Column<string>(type: "text", nullable: true),
                    IdStatusNarudzbenice = table.Column<int>(type: "integer", nullable: false),
                    IdKupac = table.Column<int>(type: "integer", nullable: false),
                    IdAdministrator = table.Column<int>(type: "integer", nullable: true),
                    AdministratorIdKorisnik = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narudzbenica", x => x.IdNarudzbenica);
                    table.ForeignKey(
                        name: "FK_Narudzbenica_Korisnik_AdministratorIdKorisnik",
                        column: x => x.AdministratorIdKorisnik,
                        principalTable: "Korisnik",
                        principalColumn: "IdKorisnik");
                    table.ForeignKey(
                        name: "FK_Narudzbenica_Korisnik_IdKupac",
                        column: x => x.IdKupac,
                        principalTable: "Korisnik",
                        principalColumn: "IdKorisnik",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Narudzbenica_StatusNarudzbenice_IdStatusNarudzbenice",
                        column: x => x.IdStatusNarudzbenice,
                        principalTable: "StatusNarudzbenice",
                        principalColumn: "IdStatus",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CenaProizvoda",
                columns: table => new
                {
                    IdProizvod = table.Column<int>(type: "integer", nullable: false),
                    IdCena = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    Cena = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenaProizvoda", x => new { x.IdProizvod, x.IdCena });
                    table.ForeignKey(
                        name: "FK_CenaProizvoda_Proizvod_IdProizvod",
                        column: x => x.IdProizvod,
                        principalTable: "Proizvod",
                        principalColumn: "IdProizvod",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObradaNarudžbenice",
                columns: table => new
                {
                    IdObrada = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DatumObrada = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    Komentar = table.Column<string>(type: "text", nullable: true),
                    IdAdministrator = table.Column<int>(type: "integer", nullable: false),
                    IdNarudzbenica = table.Column<int>(type: "integer", nullable: false),
                    IdStatusNarudzbenica = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObradaNarudžbenice", x => x.IdObrada);
                    table.ForeignKey(
                        name: "FK_ObradaNarudžbenice_Korisnik_IdAdministrator",
                        column: x => x.IdAdministrator,
                        principalTable: "Korisnik",
                        principalColumn: "IdKorisnik",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObradaNarudžbenice_Narudzbenica_IdNarudzbenica",
                        column: x => x.IdNarudzbenica,
                        principalTable: "Narudzbenica",
                        principalColumn: "IdNarudzbenica",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObradaNarudžbenice_StatusNarudzbenice_IdStatusNarudzbenica",
                        column: x => x.IdStatusNarudzbenica,
                        principalTable: "StatusNarudzbenice",
                        principalColumn: "IdStatus",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StavkaNarudzbenice",
                columns: table => new
                {
                    IdNarudzbenica = table.Column<int>(type: "integer", nullable: false),
                    RBrProizvoda = table.Column<int>(type: "integer", nullable: false),
                    IdProizvod = table.Column<int>(type: "integer", nullable: false),
                    Kolicina = table.Column<int>(type: "integer", nullable: false),
                    UgovorenaCena = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkaNarudzbenice", x => new { x.IdNarudzbenica, x.RBrProizvoda });
                    table.ForeignKey(
                        name: "FK_StavkaNarudzbenice_Narudzbenica_IdNarudzbenica",
                        column: x => x.IdNarudzbenica,
                        principalTable: "Narudzbenica",
                        principalColumn: "IdNarudzbenica",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StavkaNarudzbenice_Proizvod_IdProizvod",
                        column: x => x.IdProizvod,
                        principalTable: "Proizvod",
                        principalColumn: "IdProizvod",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kategorija_NazivKategorije",
                table: "Kategorija",
                column: "NazivKategorije",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_Email",
                table: "Korisnik",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_Telefon",
                table: "Korisnik",
                column: "Telefon",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Narudzbenica_AdministratorIdKorisnik",
                table: "Narudzbenica",
                column: "AdministratorIdKorisnik");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzbenica_IdKupac",
                table: "Narudzbenica",
                column: "IdKupac");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzbenica_IdStatusNarudzbenice",
                table: "Narudzbenica",
                column: "IdStatusNarudzbenice");

            migrationBuilder.CreateIndex(
                name: "IX_ObradaNarudžbenice_IdAdministrator",
                table: "ObradaNarudžbenice",
                column: "IdAdministrator");

            migrationBuilder.CreateIndex(
                name: "IX_ObradaNarudžbenice_IdNarudzbenica",
                table: "ObradaNarudžbenice",
                column: "IdNarudzbenica");

            migrationBuilder.CreateIndex(
                name: "IX_ObradaNarudžbenice_IdStatusNarudzbenica",
                table: "ObradaNarudžbenice",
                column: "IdStatusNarudzbenica");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_KategorijaId",
                table: "Proizvod",
                column: "KategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_Naziv",
                table: "Proizvod",
                column: "Naziv",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StavkaNarudzbenice_IdProizvod",
                table: "StavkaNarudzbenice",
                column: "IdProizvod");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CenaProizvoda");

            migrationBuilder.DropTable(
                name: "ObradaNarudžbenice");

            migrationBuilder.DropTable(
                name: "StavkaNarudzbenice");

            migrationBuilder.DropTable(
                name: "Narudzbenica");

            migrationBuilder.DropTable(
                name: "Proizvod");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "StatusNarudzbenice");

            migrationBuilder.DropTable(
                name: "Kategorija");
        }
    }
}
