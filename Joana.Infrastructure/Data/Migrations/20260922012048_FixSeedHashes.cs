using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Joana.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedHashes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 1,
                column: "LozinkaHash",
                value: "e++XzzeI5s1lsuspW+dLaw==.h/HKvS10tI474RD/8ZFjBdl+Cu41h24vGZs/5urigXQ=");

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 2,
                column: "LozinkaHash",
                value: "e++XzzeI5s1lsuspW+dLaw==.h/HKvS10tI474RD/8ZFjBdl+Cu41h24vGZs/5urigXQ=");

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 3,
                column: "LozinkaHash",
                value: "e++XzzeI5s1lsuspW+dLaw==.h/HKvS10tI474RD/8ZFjBdl+Cu41h24vGZs/5urigXQ=");

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 4,
                column: "LozinkaHash",
                value: "e++XzzeI5s1lsuspW+dLaw==.h/HKvS10tI474RD/8ZFjBdl+Cu41h24vGZs/5urigXQ=");

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 5,
                column: "LozinkaHash",
                value: "e++XzzeI5s1lsuspW+dLaw==.h/HKvS10tI474RD/8ZFjBdl+Cu41h24vGZs/5urigXQ=");

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 6,
                column: "LozinkaHash",
                value: "e++XzzeI5s1lsuspW+dLaw==.h/HKvS10tI474RD/8ZFjBdl+Cu41h24vGZs/5urigXQ=");

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 7,
                column: "LozinkaHash",
                value: "e++XzzeI5s1lsuspW+dLaw==.h/HKvS10tI474RD/8ZFjBdl+Cu41h24vGZs/5urigXQ=");

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 8,
                column: "LozinkaHash",
                value: "e++XzzeI5s1lsuspW+dLaw==.h/HKvS10tI474RD/8ZFjBdl+Cu41h24vGZs/5urigXQ=");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 1,
                column: "LozinkaHash",
                value: "UglTr4oWU0NX9n1dU3PUsQ==.e8ijheL9mVKylcZVtTFq7JVwZ4r9mkG5VC5CNo+3Aak==");

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 2,
                column: "LozinkaHash",
                value: "OrjBq9xBCxr5+shNrFCKoQ==.ncSTs8dWmuAw4Dft+ky7qMggIw+rVCCnsGVBto56ym8=");

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 3,
                column: "LozinkaHash",
                value: "MelWLc9UUGs365hQLVXdiA==.5c+Ks+dDVwSxh/Saa7nkAMKicdKVBE0MpsWqWd6iI58=");

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 4,
                column: "LozinkaHash",
                value: "mwX1T5rmx0B52Qy6QMjcVw==.3wiaonnYyMiZrvB58OIdQXzBeB+qq/cwzr3XGkeAv0=");

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 5,
                column: "LozinkaHash",
                value: "C6t1oPgdbbG0itJr/JLlAQ==.ggN28wYMlXxZszlqcrEYFwf/4+b0cNkC/ttSEsnsNeo=");

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 6,
                column: "LozinkaHash",
                value: "O7N8J7OZVku347nNXUxWNg==.Zm3kyTCssNJkePEjxoJWaVhzWWwhOCvdRfmEWhobZ8I=");

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 7,
                column: "LozinkaHash",
                value: "VTacW92S/sqzgYikxVyWoA==.ynLufjkLhaaMyAt2IFrzWyNkZVcGeAWTM9MS11O51hQ==");

            migrationBuilder.UpdateData(
                table: "Korisnik",
                keyColumn: "IdKorisnik",
                keyValue: 8,
                column: "LozinkaHash",
                value: "S/SgBoMYzsmYx0S6iAchvQ==.t5RklxkgDIpAIjp+Yye/30rmhIUuZJIDwSvRzonfNf0=");
        }
    }
}
