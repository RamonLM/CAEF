using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CAEF.Migrations.MigracionCAEF
{
    public partial class InicialCAEF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    Matricula_Alumno = table.Column<int>(nullable: false),
                    ApellidoM = table.Column<string>(nullable: false),
                    ApellidoP = table.Column<string>(nullable: false),
                    Grupo = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Promedio = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnos", x => x.Matricula_Alumno);
                });

            migrationBuilder.CreateTable(
                name: "Carreras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carreras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    Clave_Materia = table.Column<int>(nullable: false),
                    Carrera = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.Clave_Materia);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubtiposExamen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubtiposExamen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposExamen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposExamen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Numero_Empleado = table.Column<int>(nullable: false),
                    ApellidoM = table.Column<string>(nullable: true),
                    ApellidoP = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    RolId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Numero_Empleado);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesDocente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<int>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    IdCarrera = table.Column<int>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    IdEstado = table.Column<int>(nullable: false),
                    IdMateria = table.Column<int>(nullable: false),
                    IdTipoExamen = table.Column<int>(nullable: false),
                    Motivo = table.Column<string>(nullable: false),
                    Periodo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesDocente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudesDocente_Usuarios_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Usuarios",
                        principalColumn: "Numero_Empleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudesDocente_Carreras_IdCarrera",
                        column: x => x.IdCarrera,
                        principalTable: "Carreras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudesDocente_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudesDocente_Materias_IdMateria",
                        column: x => x.IdMateria,
                        principalTable: "Materias",
                        principalColumn: "Clave_Materia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudesDocente_TiposExamen_IdTipoExamen",
                        column: x => x.IdTipoExamen,
                        principalTable: "TiposExamen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesAdministrativo",
                columns: table => new
                {
                    IdSolicitud = table.Column<int>(nullable: false),
                    CalificacionLetra = table.Column<string>(nullable: false),
                    CicloEscolar = table.Column<string>(nullable: false),
                    ClaveUnidad = table.Column<string>(nullable: false),
                    Comentario = table.Column<string>(nullable: false),
                    EtapaSemestre = table.Column<string>(nullable: false),
                    FechaAceptacion = table.Column<DateTime>(nullable: false),
                    IdSubtipoExamen = table.Column<int>(nullable: false),
                    NumeroAlumnos = table.Column<int>(nullable: false),
                    PlanEstudios = table.Column<string>(nullable: false),
                    SubTipoExamenId = table.Column<int>(nullable: true),
                    URLDocumento = table.Column<string>(nullable: false),
                    UnidadAcademica = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesAdministrativo", x => x.IdSolicitud);
                    table.ForeignKey(
                        name: "FK_SolicitudesAdministrativo_SolicitudesDocente_IdSolicitud",
                        column: x => x.IdSolicitud,
                        principalTable: "SolicitudesDocente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudesAdministrativo_SubtiposExamen_SubTipoExamenId",
                        column: x => x.SubTipoExamenId,
                        principalTable: "SubtiposExamen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesAlumno",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdAlumno = table.Column<int>(nullable: false),
                    IdSolicitud = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesAlumno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudesAlumno_Alumnos_IdAlumno",
                        column: x => x.IdAlumno,
                        principalTable: "Alumnos",
                        principalColumn: "Matricula_Alumno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudesAlumno_SolicitudesDocente_IdSolicitud",
                        column: x => x.IdSolicitud,
                        principalTable: "SolicitudesDocente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesAdministrativo_SubTipoExamenId",
                table: "SolicitudesAdministrativo",
                column: "SubTipoExamenId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesAlumno_IdAlumno",
                table: "SolicitudesAlumno",
                column: "IdAlumno");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesAlumno_IdSolicitud",
                table: "SolicitudesAlumno",
                column: "IdSolicitud");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesDocente_EmpleadoId",
                table: "SolicitudesDocente",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesDocente_IdCarrera",
                table: "SolicitudesDocente",
                column: "IdCarrera");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesDocente_IdEstado",
                table: "SolicitudesDocente",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesDocente_IdMateria",
                table: "SolicitudesDocente",
                column: "IdMateria");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesDocente_IdTipoExamen",
                table: "SolicitudesDocente",
                column: "IdTipoExamen");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios",
                column: "RolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitudesAdministrativo");

            migrationBuilder.DropTable(
                name: "SolicitudesAlumno");

            migrationBuilder.DropTable(
                name: "SubtiposExamen");

            migrationBuilder.DropTable(
                name: "Alumnos");

            migrationBuilder.DropTable(
                name: "SolicitudesDocente");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Carreras");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "TiposExamen");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
