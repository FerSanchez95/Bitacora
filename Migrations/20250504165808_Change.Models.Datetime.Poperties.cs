using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bitacora.Migrations
{
    /// <inheritdoc />
    public partial class ChangeModelsDatetimePoperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaDeCreacion",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "HoraDeCreacion",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "FechaDeCreacion",
                table: "Bitacoras");

            migrationBuilder.DropColumn(
                name: "HoraDeCreacion",
                table: "Bitacoras");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaModificacion",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "Bitacoras",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaModificacion",
                table: "Bitacoras",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UltimaModificacion",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Bitacoras");

            migrationBuilder.DropColumn(
                name: "UltimaModificacion",
                table: "Bitacoras");

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaDeCreacion",
                table: "Posts",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "HoraDeCreacion",
                table: "Posts",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaDeCreacion",
                table: "Bitacoras",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "HoraDeCreacion",
                table: "Bitacoras",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }
    }
}
