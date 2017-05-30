using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CAEF.Migrations.MigracionUABC
{
    public partial class InicialUABC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuariosUABC",
                columns: table => new
                {
                    Numero_Empleado = table.Column<int>(nullable: false),
                    ApellidoM = table.Column<string>(nullable: true),
                    ApellidoP = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosUABC", x => x.Numero_Empleado);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuariosUABC");
        }
    }
}
