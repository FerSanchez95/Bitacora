using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bitacora.Migrations
{
    /// <inheritdoc />
    public partial class AgregaRelacionUsuarioBitacoras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Bitacoras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bitacoras_UsuarioId",
                table: "Bitacoras",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bitacoras_Usuarios_UsuarioId",
                table: "Bitacoras",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bitacoras_Usuarios_UsuarioId",
                table: "Bitacoras");

            migrationBuilder.DropIndex(
                name: "IX_Bitacoras_UsuarioId",
                table: "Bitacoras");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Bitacoras");
        }
    }
}
