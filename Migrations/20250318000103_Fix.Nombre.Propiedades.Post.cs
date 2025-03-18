using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bitacora.Migrations
{
    /// <inheritdoc />
    public partial class FixNombrePropiedadesPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HoraDeCreación",
                table: "Posts",
                newName: "HoraDeCreacion");

            migrationBuilder.RenameColumn(
                name: "FechaDeCreación",
                table: "Posts",
                newName: "FechaDeCreacion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HoraDeCreacion",
                table: "Posts",
                newName: "HoraDeCreación");

            migrationBuilder.RenameColumn(
                name: "FechaDeCreacion",
                table: "Posts",
                newName: "FechaDeCreación");
        }
    }
}
