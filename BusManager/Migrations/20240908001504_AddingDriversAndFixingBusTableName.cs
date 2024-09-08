using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BusManager.Migrations
{
    /// <inheritdoc />
    public partial class AddingDriversAndFixingBusTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Bus",
                table: "Bus");

            migrationBuilder.RenameTable(
                name: "Bus",
                newName: "Buses");

            migrationBuilder.RenameIndex(
                name: "IX_Bus_Plate",
                table: "Buses",
                newName: "IX_Buses_Plate");

            migrationBuilder.RenameIndex(
                name: "IX_Bus_Id",
                table: "Buses",
                newName: "IX_Buses_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Buses",
                table: "Buses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CPF = table.Column<string>(type: "VARCHAR(14)", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(15)", nullable: false),
                    Surname = table.Column<string>(type: "VARCHAR(15)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "DATE", nullable: false),
                    HireDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    Active = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_CPF",
                table: "Drivers",
                column: "CPF");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_Id",
                table: "Drivers",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Buses",
                table: "Buses");

            migrationBuilder.RenameTable(
                name: "Buses",
                newName: "Bus");

            migrationBuilder.RenameIndex(
                name: "IX_Buses_Plate",
                table: "Bus",
                newName: "IX_Bus_Plate");

            migrationBuilder.RenameIndex(
                name: "IX_Buses_Id",
                table: "Bus",
                newName: "IX_Bus_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bus",
                table: "Bus",
                column: "Id");
        }
    }
}
