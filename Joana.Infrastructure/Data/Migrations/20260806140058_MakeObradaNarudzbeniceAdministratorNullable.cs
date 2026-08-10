using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Joana.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeObradaNarudzbeniceAdministratorNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObradaNarudžbenice_Korisnik_IdAdministrator",
                table: "ObradaNarudžbenice");

            migrationBuilder.AlterColumn<int>(
                name: "IdAdministrator",
                table: "ObradaNarudžbenice",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ObradaNarudžbenice_Korisnik_IdAdministrator",
                table: "ObradaNarudžbenice",
                column: "IdAdministrator",
                principalTable: "Korisnik",
                principalColumn: "IdKorisnik");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObradaNarudžbenice_Korisnik_IdAdministrator",
                table: "ObradaNarudžbenice");

            migrationBuilder.AlterColumn<int>(
                name: "IdAdministrator",
                table: "ObradaNarudžbenice",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ObradaNarudžbenice_Korisnik_IdAdministrator",
                table: "ObradaNarudžbenice",
                column: "IdAdministrator",
                principalTable: "Korisnik",
                principalColumn: "IdKorisnik",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
