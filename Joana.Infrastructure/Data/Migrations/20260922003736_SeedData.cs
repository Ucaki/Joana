using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Joana.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Kategorija",
                columns: new[] { "IdKategorija", "NazivKategorije" },
                values: new object[,]
                {
                    { 1, "Herbicidi" },
                    { 2, "Insekticidi" },
                    { 3, "Fungicidi" },
                    { 4, "Površinska đubriva" },
                    { 5, "Prihrane" },
                    { 6, "Seme povrća" },
                    { 7, "Seme žitarica" },
                    { 8, "Seme cveća" }
                });

            migrationBuilder.InsertData(
                table: "Korisnik",
                columns: new[] { "IdKorisnik", "Email", "Ime", "LozinkaHash", "Prezime", "Role" },
                values: new object[] { 1, "uros.andjelkovic@example.com", "Uroš", "UglTr4oWU0NX9n1dU3PUsQ==.e8ijheL9mVKylcZVtTFq7JVwZ4r9mkG5VC5CNo+3Aak==", "Anđelković", "Administrator" });

            migrationBuilder.InsertData(
                table: "Korisnik",
                columns: new[] { "IdKorisnik", "Adresa", "Email", "Grad", "Ime", "LozinkaHash", "Prezime", "Role", "Telefon" },
                values: new object[,]
                {
                    { 2, "Bulevar kralja Aleksandra 25", "jovana.djokic@example.com", "Beograd", "Jovana", "OrjBq9xBCxr5+shNrFCKoQ==.ncSTs8dWmuAw4Dft+ky7qMggIw+rVCCnsGVBto56ym8=", "Đokić", "Kupac", "0612345678" },
                    { 3, "Futoška 42", "jovana.stojanovic@example.com", "Novi Sad", "Jovana", "MelWLc9UUGs365hQLVXdiA==.5c+Ks+dDVwSxh/Saa7nkAMKicdKVBE0MpsWqWd6iI58=", "Stojanović", "Kupac", "0623456789" },
                    { 4, "Vizantijski bulevar 18", "dragoslav.stojanovic@example.com", "Niš", "Dragoslav", "mwX1T5rmx0B52Qy6QMjcVw==.3wiaonnYyMiZrvB58OIdQXzBeB+qq/cwzr3XGkeAv0=", "Stojanović", "Kupac", "0654567890" },
                    { 5, "Kralja Petra I 63", "jovan.vukojevic@example.com", "Kragujevac", "Jovan", "C6t1oPgdbbG0itJr/JLlAQ==.ggN28wYMlXxZszlqcrEYFwf/4+b0cNkC/ttSEsnsNeo=", "Vukojević", "Kupac", "0615678901" },
                    { 6, "Matije Korvina 11", "nikola.vuksanovic@example.com", "Subotica", "Nikola", "O7N8J7OZVku347nNXUxWNg==.Zm3kyTCssNJkePEjxoJWaVhzWWwhOCvdRfmEWhobZ8I=", "Vuksanović", "Kupac", "0626789012" },
                    { 7, "Miloša Obrenovića 37", "natalija.svrkovukic@example.com", "Pančevo", "Natalija", "VTacW92S/sqzgYikxVyWoA==.ynLufjkLhaaMyAt2IFrzWyNkZVcGeAWTM9MS11O51hQ==", "SvrkoVukić", "Kupac", "0657890123" },
                    { 8, "Župana Stracimira 29", "milan.mladenovic@example.com", "Čačak", "Milan", "S/SgBoMYzsmYx0S6iAchvQ==.t5RklxkgDIpAIjp+Yye/30rmhIUuZJIDwSvRzonfNf0=", "Mladenović", "Kupac", "0618901234" }
                });

            migrationBuilder.InsertData(
                table: "StatusNarudzbenice",
                columns: new[] { "IdStatus", "NazivStatusa" },
                values: new object[,]
                {
                    { 1, "Kreiranо" },
                    { 2, "Odobreno" },
                    { 3, "Odbijeno" },
                    { 4, "Isporučenо" }
                });

            migrationBuilder.InsertData(
                table: "Narudzbenica",
                columns: new[] { "IdNarudzbenica", "AdministratorIdKorisnik", "DatumKreiranja", "IdAdministrator", "IdKupac", "IdStatusNarudzbenice", "NapomenaKupca" },
                values: new object[,]
                {
                    { 1, null, new DateTimeOffset(new DateTime(2026, 3, 5, 10, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 2, 4, "Molim vas isporučiti u prepodnevnim časovima." },
                    { 2, null, new DateTimeOffset(new DateTime(2026, 3, 8, 12, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 3, 4, "Pozvati kupca pre isporuke." },
                    { 3, null, new DateTimeOffset(new DateTime(2026, 3, 12, 9, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 4, 4, "" },
                    { 4, null, new DateTimeOffset(new DateTime(2026, 3, 15, 14, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 5, 3, "Potrebno što pre." },
                    { 5, null, new DateTimeOffset(new DateTime(2026, 3, 18, 11, 10, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 6, 3, "" },
                    { 6, null, new DateTimeOffset(new DateTime(2026, 3, 21, 16, 40, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 7, 3, "Molim proveru dostupnosti proizvoda." },
                    { 7, null, new DateTimeOffset(new DateTime(2026, 3, 24, 8, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 8, 2, "Isporuka na adresu kupca." },
                    { 8, null, new DateTimeOffset(new DateTime(2026, 3, 26, 13, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 2, 2, "" },
                    { 9, null, new DateTimeOffset(new DateTime(2026, 3, 28, 10, 50, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 3, 2, "Pozvati pre slanja." },
                    { 10, null, new DateTimeOffset(new DateTime(2026, 4, 1, 9, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 4, 1, "" },
                    { 11, null, new DateTimeOffset(new DateTime(2026, 4, 3, 11, 35, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 5, 1, "Isporuka tokom radnog vremena." },
                    { 12, null, new DateTimeOffset(new DateTime(2026, 4, 5, 15, 10, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 6, 1, "" },
                    { 13, null, new DateTimeOffset(new DateTime(2026, 4, 7, 10, 5, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 7, 1, "Molim potvrdu narudžbenice." },
                    { 14, null, new DateTimeOffset(new DateTime(2026, 4, 9, 12, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 8, 1, "Pozvati kupca." },
                    { 15, null, new DateTimeOffset(new DateTime(2026, 4, 11, 14, 25, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 2, 1, "" }
                });

            migrationBuilder.InsertData(
                table: "Proizvod",
                columns: new[] { "IdProizvod", "JedinicaMere", "KategorijaId", "Lager", "Naziv", "Opis" },
                values: new object[,]
                {
                    { 1, 2, 1, 10, "Corum", "Corum® je jedini herbicid na tržištu formulisan specijalno za soju. Na najselektivniji na?in suzbija širokolisne korove i jednogodišnje travne korove i predstavlja kompletno rešenje za post-em tretman." },
                    { 2, 2, 1, 10, "Glisando", "Glisando je regulator rasta za sprečavanje poleganja useva pšenice* i ječma" },
                    { 3, 3, 1, 10, "Rimex", "Herbicid Rimsulfuron pripada grupi Sulfoniluree" },
                    { 4, 2, 1, 10, "Glifomark", "neselektivni herbicid, usvaja se lisnom masom. Kreće se naviše i naniže u biljci, inaktivirase u kontaktu sa zemljištem. Inhibira EPSP, odnosno sintezu aromatičnih kiselina čime sprečava sintezu neophodnih aminokiselina za proizvodnju proteina." },
                    { 5, 2, 1, 10, "Glifol", "Aktivna materija: glifosat IPA (480 g/l) Formulacija: Koncentrovani rastvor (SL) " },
                    { 6, 3, 2, 15, "Etiol Ultra 100 ml", "Insekticid za suzbijanje štetnih insekata." },
                    { 7, 3, 2, 12, "Bros sprej protiv osa i stršljenova 400 ml", "Sprej za suzbijanje osa i stršljenova." },
                    { 8, 3, 2, 10, "Nadzor 50 ml", "Preparat za zaštitu biljaka od štetočina." },
                    { 9, 2, 2, 8, "Alverde 1 l", "Insekticid sa kontaktnim i digestivnim delovanjem." },
                    { 10, 3, 3, 10, "Olimp 0.5 l", "Fungicid za zaštitu biljaka od biljnih bolesti." },
                    { 11, 2, 3, 10, "Semesan 1 l", "Preparat za zaštitu biljaka od gljivičnih oboljenja." },
                    { 12, 1, 3, 10, "Alijansa 1 kg", "Sistemični i kontaktni fungicid." },
                    { 13, 1, 4, 20, "KAN 27% 25 kg Genezis", "Azotno mineralno đubrivo za prihranu biljaka." },
                    { 14, 1, 4, 20, "Urea 46% ruska 25 kg", "Granulisano azotno đubrivo sa visokim sadržajem azota." },
                    { 15, 1, 4, 20, "Amonijum nitrat AN Genezis 25 kg", "Azotno mineralno đubrivo za osnovnu i dopunsku prihranu." },
                    { 16, 3, 5, 15, "Blumi za orhideje 500 ml", "Tečno đubrivo namenjeno prihrani orhideja." },
                    { 17, 3, 5, 15, "Blumi za ruže 500 ml", "Tečno đubrivo namenjeno prihrani ruža." },
                    { 18, 2, 5, 10, "Foligal kalcijum 1 l", "Tečno folijarno đubrivo sa kalcijumom." },
                    { 19, 3, 5, 15, "Blumi za muškatle 500 ml", "Tečno đubrivo namenjeno prihrani muškatli." },
                    { 20, 3, 6, 30, "Seme špinat Matador", "Seme špinata sorte Matador." },
                    { 21, 3, 6, 30, "Seme tikvica Beogradska", "Seme tikvice sorte Beogradska." },
                    { 22, 3, 6, 25, "Seme boranija Maxidor 100 g", "Seme boranije sorte Maxidor." },
                    { 23, 3, 6, 25, "Beli luk domaći 200 g", "Domaći beli luk za sadnju." },
                    { 24, 1, 7, 50, "Semenska pšenica Basmati", "Semenska pšenica sorte Basmati." },
                    { 25, 1, 7, 50, "Semenska pšenica LG Avenue", "Semenska pšenica sorte LG Avenue." },
                    { 26, 1, 7, 50, "Semenski ječam Grand", "Semenski ječam sorte Grand." },
                    { 27, 1, 7, 50, "Semenska pšenica Sofru", "Semenska pšenica sorte Sofru." },
                    { 28, 3, 8, 30, "Seme cveća Petunija", "Seme petunije za uzgoj ukrasnog cveća." },
                    { 29, 3, 8, 30, "Seme cveća Dan i noć", "Seme cveća dan i noć." },
                    { 30, 3, 8, 30, "Seme cveća Noćna frajla", "Seme ukrasnog cveća noćna frajla." },
                    { 31, 3, 8, 30, "Seme cveća Goruća ljubav", "Seme ukrasnog cveća goruća ljubav." },
                    { 32, 3, 8, 30, "Seme cveća Neven", "Seme nevena za uzgoj ukrasnog cveća." }
                });

            migrationBuilder.InsertData(
                table: "CenaProizvoda",
                columns: new[] { "IdCena", "IdProizvod", "Cena", "CreatedAt", "JeAktivna" },
                values: new object[,]
                {
                    { 1, 1, 1800.00m, new DateTimeOffset(new DateTime(2025, 5, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 1, 1950.00m, new DateTimeOffset(new DateTime(2026, 5, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 2, 1450.00m, new DateTimeOffset(new DateTime(2025, 6, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 2, 1580.00m, new DateTimeOffset(new DateTime(2026, 6, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 3, 2100.00m, new DateTimeOffset(new DateTime(2025, 4, 18, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 3, 2250.00m, new DateTimeOffset(new DateTime(2026, 4, 22, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 4, 950.00m, new DateTimeOffset(new DateTime(2025, 7, 8, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 4, 1020.00m, new DateTimeOffset(new DateTime(2026, 7, 14, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 5, 1100.00m, new DateTimeOffset(new DateTime(2025, 8, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 5, 1180.00m, new DateTimeOffset(new DateTime(2026, 8, 11, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 6, 350.00m, new DateTimeOffset(new DateTime(2025, 5, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 6, 395.00m, new DateTimeOffset(new DateTime(2026, 5, 18, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 7, 650.00m, new DateTimeOffset(new DateTime(2025, 6, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 7, 720.00m, new DateTimeOffset(new DateTime(2026, 6, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 8, 480.00m, new DateTimeOffset(new DateTime(2025, 4, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 8, 520.00m, new DateTimeOffset(new DateTime(2026, 4, 25, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 9, 8700.00m, new DateTimeOffset(new DateTime(2025, 7, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 9, 9435.00m, new DateTimeOffset(new DateTime(2026, 7, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 10, 1950.00m, new DateTimeOffset(new DateTime(2025, 5, 14, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 10, 2190.00m, new DateTimeOffset(new DateTime(2026, 5, 21, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 11, 1250.00m, new DateTimeOffset(new DateTime(2025, 6, 11, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 11, 1350.00m, new DateTimeOffset(new DateTime(2026, 6, 16, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 12, 2700.00m, new DateTimeOffset(new DateTime(2025, 8, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 12, 2950.00m, new DateTimeOffset(new DateTime(2026, 8, 18, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 13, 1600.00m, new DateTimeOffset(new DateTime(2025, 4, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 13, 1775.00m, new DateTimeOffset(new DateTime(2026, 4, 19, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 14, 1800.00m, new DateTimeOffset(new DateTime(2025, 5, 17, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 14, 1975.00m, new DateTimeOffset(new DateTime(2026, 5, 23, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 15, 1900.00m, new DateTimeOffset(new DateTime(2025, 7, 13, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 15, 2050.00m, new DateTimeOffset(new DateTime(2026, 7, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 16, 310.00m, new DateTimeOffset(new DateTime(2025, 5, 11, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 16, 350.00m, new DateTimeOffset(new DateTime(2026, 5, 17, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 17, 310.00m, new DateTimeOffset(new DateTime(2025, 6, 13, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 17, 350.00m, new DateTimeOffset(new DateTime(2026, 6, 18, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 18, 700.00m, new DateTimeOffset(new DateTime(2025, 4, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 18, 790.00m, new DateTimeOffset(new DateTime(2026, 4, 17, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 19, 310.00m, new DateTimeOffset(new DateTime(2025, 7, 16, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 19, 350.00m, new DateTimeOffset(new DateTime(2026, 7, 21, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 20, 45.00m, new DateTimeOffset(new DateTime(2025, 5, 19, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 20, 50.00m, new DateTimeOffset(new DateTime(2026, 5, 24, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 21, 55.00m, new DateTimeOffset(new DateTime(2025, 6, 14, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 21, 65.00m, new DateTimeOffset(new DateTime(2026, 6, 19, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 22, 220.00m, new DateTimeOffset(new DateTime(2025, 4, 16, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 22, 245.00m, new DateTimeOffset(new DateTime(2026, 4, 23, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 23, 250.00m, new DateTimeOffset(new DateTime(2025, 8, 14, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 23, 285.00m, new DateTimeOffset(new DateTime(2026, 8, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 24, 70.00m, new DateTimeOffset(new DateTime(2025, 5, 13, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 24, 79.00m, new DateTimeOffset(new DateTime(2026, 5, 18, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 25, 70.00m, new DateTimeOffset(new DateTime(2025, 6, 17, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 25, 79.00m, new DateTimeOffset(new DateTime(2026, 6, 22, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 26, 60.00m, new DateTimeOffset(new DateTime(2025, 4, 19, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 26, 69.00m, new DateTimeOffset(new DateTime(2026, 4, 24, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 27, 72.00m, new DateTimeOffset(new DateTime(2025, 7, 17, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 27, 82.00m, new DateTimeOffset(new DateTime(2026, 7, 23, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 28, 50.00m, new DateTimeOffset(new DateTime(2025, 5, 16, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 28, 60.00m, new DateTimeOffset(new DateTime(2026, 5, 21, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 29, 50.00m, new DateTimeOffset(new DateTime(2025, 6, 16, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 29, 60.00m, new DateTimeOffset(new DateTime(2026, 6, 21, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 30, 50.00m, new DateTimeOffset(new DateTime(2025, 4, 17, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 30, 60.00m, new DateTimeOffset(new DateTime(2026, 4, 22, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 31, 50.00m, new DateTimeOffset(new DateTime(2025, 7, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 31, 60.00m, new DateTimeOffset(new DateTime(2026, 7, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" },
                    { 1, 32, 50.00m, new DateTimeOffset(new DateTime(2025, 8, 16, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "neaktivna" },
                    { 2, 32, 60.00m, new DateTimeOffset(new DateTime(2026, 8, 22, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aktivna" }
                });

            migrationBuilder.InsertData(
                table: "ObradaNarudžbenice",
                columns: new[] { "IdObrada", "DatumObrada", "IdAdministrator", "IdNarudzbenica", "IdStatusNarudzbenica", "Komentar" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2026, 3, 5, 10, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 1, 1, "Narudžbenica kreirana." },
                    { 2, new DateTimeOffset(new DateTime(2026, 3, 5, 13, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 1, 2, "Narudžbenica odobrena." },
                    { 3, new DateTimeOffset(new DateTime(2026, 3, 7, 11, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 1, 4, "Narudžbenica isporučena kupcu." },
                    { 4, new DateTimeOffset(new DateTime(2026, 3, 8, 12, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 2, 1, "Narudžbenica kreirana." },
                    { 5, new DateTimeOffset(new DateTime(2026, 3, 8, 15, 10, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 2, 2, "Narudžbenica odobrena." },
                    { 6, new DateTimeOffset(new DateTime(2026, 3, 10, 9, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 2, 4, "Narudžbenica isporučena kupcu." },
                    { 7, new DateTimeOffset(new DateTime(2026, 3, 12, 9, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 3, 1, "Narudžbenica kreirana." },
                    { 8, new DateTimeOffset(new DateTime(2026, 3, 12, 12, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 3, 2, "Narudžbenica odobrena." },
                    { 9, new DateTimeOffset(new DateTime(2026, 3, 14, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 3, 4, "Narudžbenica isporučena kupcu." },
                    { 10, new DateTimeOffset(new DateTime(2026, 3, 15, 14, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 4, 1, "Narudžbenica kreirana." },
                    { 11, new DateTimeOffset(new DateTime(2026, 3, 15, 16, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 4, 3, "Narudžbenica odbijena zbog nedostupnosti proizvoda." },
                    { 12, new DateTimeOffset(new DateTime(2026, 3, 18, 11, 10, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 5, 1, "Narudžbenica kreirana." },
                    { 13, new DateTimeOffset(new DateTime(2026, 3, 18, 14, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 5, 3, "Narudžbenica odbijena." },
                    { 14, new DateTimeOffset(new DateTime(2026, 3, 21, 16, 40, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 6, 1, "Narudžbenica kreirana." },
                    { 15, new DateTimeOffset(new DateTime(2026, 3, 22, 9, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 6, 3, "Narudžbenica odbijena zbog nedostupnosti proizvoda." },
                    { 16, new DateTimeOffset(new DateTime(2026, 3, 24, 8, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 7, 1, "Narudžbenica kreirana." },
                    { 17, new DateTimeOffset(new DateTime(2026, 3, 24, 11, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 7, 2, "Narudžbenica odobrena." },
                    { 18, new DateTimeOffset(new DateTime(2026, 3, 26, 13, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 8, 1, "Narudžbenica kreirana." },
                    { 19, new DateTimeOffset(new DateTime(2026, 3, 26, 16, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 8, 2, "Narudžbenica odobrena." },
                    { 20, new DateTimeOffset(new DateTime(2026, 3, 28, 10, 50, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 9, 1, "Narudžbenica kreirana." },
                    { 21, new DateTimeOffset(new DateTime(2026, 3, 28, 13, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 9, 2, "Narudžbenica odobrena." }
                });

            migrationBuilder.InsertData(
                table: "StavkaNarudzbenice",
                columns: new[] { "IdNarudzbenica", "RBrProizvoda", "IdProizvod", "Kolicina", "UgovorenaCena" },
                values: new object[,]
                {
                    { 1, 1, 1, 2, 1850m },
                    { 1, 2, 13, 3, 1250m },
                    { 1, 3, 20, 5, 180m },
                    { 1, 4, 28, 2, 120m },
                    { 2, 1, 2, 1, 2100m },
                    { 2, 2, 16, 4, 350m },
                    { 2, 3, 24, 10, 95m },
                    { 3, 1, 3, 2, 1650m },
                    { 3, 2, 8, 3, 890m },
                    { 3, 3, 18, 2, 740m },
                    { 3, 4, 21, 4, 160m },
                    { 3, 5, 26, 15, 82m },
                    { 3, 6, 32, 3, 130m },
                    { 4, 1, 4, 1, 2300m },
                    { 4, 2, 14, 2, 1450m },
                    { 5, 1, 5, 2, 1950m },
                    { 5, 2, 9, 1, 620m },
                    { 5, 3, 17, 3, 360m },
                    { 5, 4, 23, 4, 240m },
                    { 5, 5, 29, 2, 110m },
                    { 6, 1, 6, 2, 480m },
                    { 6, 2, 10, 1, 910m },
                    { 6, 3, 15, 3, 1380m },
                    { 6, 4, 31, 5, 125m },
                    { 7, 1, 1, 1, 1850m },
                    { 7, 2, 7, 2, 430m },
                    { 7, 3, 11, 1, 760m },
                    { 7, 4, 16, 2, 350m },
                    { 7, 5, 22, 3, 290m },
                    { 7, 6, 25, 10, 98m },
                    { 7, 7, 30, 2, 115m },
                    { 8, 1, 12, 2, 1350m },
                    { 8, 2, 19, 4, 330m },
                    { 8, 3, 27, 20, 88m },
                    { 9, 1, 2, 2, 2100m },
                    { 9, 2, 9, 2, 620m },
                    { 9, 3, 18, 3, 740m },
                    { 9, 4, 20, 5, 180m },
                    { 9, 5, 28, 4, 120m },
                    { 10, 1, 13, 2, 1250m },
                    { 10, 2, 21, 3, 160m },
                    { 11, 1, 4, 1, 2300m },
                    { 11, 2, 8, 2, 890m },
                    { 11, 3, 17, 2, 360m },
                    { 11, 4, 32, 3, 130m },
                    { 12, 1, 5, 1, 1950m },
                    { 12, 2, 10, 2, 910m },
                    { 12, 3, 14, 2, 1450m },
                    { 12, 4, 23, 3, 240m },
                    { 12, 5, 26, 10, 82m },
                    { 12, 6, 29, 2, 110m },
                    { 13, 1, 7, 2, 430m },
                    { 13, 2, 15, 1, 1380m },
                    { 13, 3, 31, 4, 125m },
                    { 14, 1, 3, 1, 1650m },
                    { 14, 2, 11, 2, 760m },
                    { 14, 3, 19, 3, 330m },
                    { 14, 4, 24, 15, 95m },
                    { 14, 5, 30, 2, 115m },
                    { 15, 1, 6, 1, 480m },
                    { 15, 2, 22, 3, 290m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 8 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 10 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 10 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 11 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 11 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 12 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 12 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 13 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 13 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 14 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 14 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 15 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 15 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 16 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 16 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 17 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 17 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 18 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 18 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 19 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 19 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 20 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 20 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 21 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 21 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 22 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 22 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 23 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 23 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 24 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 24 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 25 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 25 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 26 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 26 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 27 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 27 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 28 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 28 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 29 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 29 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 30 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 30 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 31 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 31 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 1, 32 });

            migrationBuilder.DeleteData(
                table: "CenaProizvoda",
                keyColumns: new[] { "IdCena", "IdProizvod" },
                keyValues: new object[] { 2, 32 });

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ObradaNarudžbenice",
                keyColumn: "IdObrada",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 6, 4 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 7, 5 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 7, 6 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 7, 7 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 8, 2 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 8, 3 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 9, 2 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 9, 3 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 9, 4 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 9, 5 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 10, 2 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 11, 1 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 11, 2 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 11, 3 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 11, 4 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 12, 1 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 12, 2 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 12, 3 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 12, 4 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 12, 5 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 12, 6 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 13, 1 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 13, 2 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 13, 3 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 14, 1 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 14, 2 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 14, 3 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 14, 4 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 14, 5 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 15, 1 });

            migrationBuilder.DeleteData(
                table: "StavkaNarudzbenice",
                keyColumns: new[] { "IdNarudzbenica", "RBrProizvoda" },
                keyValues: new object[] { 15, 2 });

            migrationBuilder.DeleteData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Narudzbenica",
                keyColumn: "IdNarudzbenica",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Narudzbenica",
                keyColumn: "IdNarudzbenica",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Narudzbenica",
                keyColumn: "IdNarudzbenica",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Narudzbenica",
                keyColumn: "IdNarudzbenica",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Narudzbenica",
                keyColumn: "IdNarudzbenica",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Narudzbenica",
                keyColumn: "IdNarudzbenica",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Narudzbenica",
                keyColumn: "IdNarudzbenica",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Narudzbenica",
                keyColumn: "IdNarudzbenica",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Narudzbenica",
                keyColumn: "IdNarudzbenica",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Narudzbenica",
                keyColumn: "IdNarudzbenica",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Narudzbenica",
                keyColumn: "IdNarudzbenica",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Narudzbenica",
                keyColumn: "IdNarudzbenica",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Narudzbenica",
                keyColumn: "IdNarudzbenica",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Narudzbenica",
                keyColumn: "IdNarudzbenica",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Narudzbenica",
                keyColumn: "IdNarudzbenica",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Proizvod",
                keyColumn: "IdProizvod",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Kategorija",
                keyColumn: "IdKategorija",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Kategorija",
                keyColumn: "IdKategorija",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Kategorija",
                keyColumn: "IdKategorija",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Kategorija",
                keyColumn: "IdKategorija",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Kategorija",
                keyColumn: "IdKategorija",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Kategorija",
                keyColumn: "IdKategorija",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Kategorija",
                keyColumn: "IdKategorija",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Kategorija",
                keyColumn: "IdKategorija",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "StatusNarudzbenice",
                keyColumn: "IdStatus",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StatusNarudzbenice",
                keyColumn: "IdStatus",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StatusNarudzbenice",
                keyColumn: "IdStatus",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StatusNarudzbenice",
                keyColumn: "IdStatus",
                keyValue: 4);
        }
    }
}
