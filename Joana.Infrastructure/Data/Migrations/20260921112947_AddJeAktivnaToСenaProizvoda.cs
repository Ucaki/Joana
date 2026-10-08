using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Joana.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddJeAktivnaToСenaProizvoda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JeAktivna",
                table: "CenaProizvoda",
                type: "character varying(9)",
                maxLength: 9,
                nullable: false,
                defaultValue: "aktivna");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JeAktivna",
                table: "CenaProizvoda");
        }
    }
}
