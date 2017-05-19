using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CAEF.Models.Contexts;

namespace CAEF.Migrations.MigracionCAEF
{
    [DbContext(typeof(CAEFContext))]
    [Migration("20170501215926_InicialCAEF")]
    partial class InicialCAEF
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CAEF.Models.Entities.CAEF.Alumno", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("Matricula_Alumno");

                    b.Property<string>("ApellidoM")
                        .IsRequired();

                    b.Property<string>("ApellidoP")
                        .IsRequired();

                    b.Property<int>("Grupo");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.Property<int>("Promedio");

                    b.HasKey("Id");

                    b.ToTable("Alumnos");
                });

            modelBuilder.Entity("CAEF.Models.Entities.CAEF.Carrera", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Carreras");
                });

            modelBuilder.Entity("CAEF.Models.Entities.CAEF.Estado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Estados");
                });

            modelBuilder.Entity("CAEF.Models.Entities.CAEF.Materia", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("Clave_Materia");

                    b.Property<string>("Carrera")
                        .IsRequired();

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Materias");
                });

            modelBuilder.Entity("CAEF.Models.Entities.CAEF.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CAEF.Models.Entities.CAEF.SolicitudAdmin", b =>
                {
                    b.Property<int>("IdSolicitud");

                    b.Property<string>("CalificacionLetra")
                        .IsRequired();

                    b.Property<string>("CicloEscolar")
                        .IsRequired();

                    b.Property<string>("ClaveUnidad")
                        .IsRequired();

                    b.Property<string>("Comentario")
                        .IsRequired();

                    b.Property<string>("EtapaSemestre")
                        .IsRequired();

                    b.Property<DateTime>("FechaAceptacion");

                    b.Property<int>("IdSubtipoExamen");

                    b.Property<int>("NumeroAlumnos");

                    b.Property<string>("PlanEstudios")
                        .IsRequired();

                    b.Property<int?>("SubTipoExamenId");

                    b.Property<string>("URLDocumento")
                        .IsRequired();

                    b.Property<string>("UnidadAcademica")
                        .IsRequired();

                    b.HasKey("IdSolicitud");

                    b.HasIndex("SubTipoExamenId");

                    b.ToTable("SolicitudesAdministrativo");
                });

            modelBuilder.Entity("CAEF.Models.Entities.CAEF.SolicitudAlumno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdAlumno");

                    b.Property<int>("IdSolicitud");

                    b.HasKey("Id");

                    b.HasIndex("IdAlumno");

                    b.HasIndex("IdSolicitud");

                    b.ToTable("SolicitudesAlumno");
                });

            modelBuilder.Entity("CAEF.Models.Entities.CAEF.SolicitudDocente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EmpleadoId");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<int>("IdCarrera");

                    b.Property<int>("IdEmpleado");

                    b.Property<int>("IdEstado");

                    b.Property<int>("IdMateria");

                    b.Property<int>("IdTipoExamen");

                    b.Property<string>("Motivo")
                        .IsRequired();

                    b.Property<string>("Periodo")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("EmpleadoId");

                    b.HasIndex("IdCarrera");

                    b.HasIndex("IdEstado");

                    b.HasIndex("IdMateria");

                    b.HasIndex("IdTipoExamen");

                    b.ToTable("SolicitudesDocente");
                });

            modelBuilder.Entity("CAEF.Models.Entities.CAEF.SubtipoExamen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("SubtiposExamen");
                });

            modelBuilder.Entity("CAEF.Models.Entities.CAEF.TipoExamen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("TiposExamen");
                });

            modelBuilder.Entity("CAEF.Models.Entities.CAEF.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("Numero_Empleado");

                    b.Property<string>("ApellidoM");

                    b.Property<string>("ApellidoP");

                    b.Property<string>("Correo")
                        .IsRequired();

                    b.Property<string>("Nombre");

                    b.Property<int>("RolId");

                    b.HasKey("Id");

                    b.HasIndex("RolId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("CAEF.Models.Entities.CAEF.SolicitudAdmin", b =>
                {
                    b.HasOne("CAEF.Models.Entities.CAEF.SolicitudDocente", "SolicitudDocente")
                        .WithMany()
                        .HasForeignKey("IdSolicitud")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CAEF.Models.Entities.CAEF.SubtipoExamen", "SubTipoExamen")
                        .WithMany()
                        .HasForeignKey("SubTipoExamenId");
                });

            modelBuilder.Entity("CAEF.Models.Entities.CAEF.SolicitudAlumno", b =>
                {
                    b.HasOne("CAEF.Models.Entities.CAEF.Alumno", "Alumno")
                        .WithMany()
                        .HasForeignKey("IdAlumno")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CAEF.Models.Entities.CAEF.SolicitudDocente", "SolicitudDocente")
                        .WithMany()
                        .HasForeignKey("IdSolicitud")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CAEF.Models.Entities.CAEF.SolicitudDocente", b =>
                {
                    b.HasOne("CAEF.Models.Entities.CAEF.Usuario", "Empleado")
                        .WithMany()
                        .HasForeignKey("EmpleadoId");

                    b.HasOne("CAEF.Models.Entities.CAEF.Carrera", "Carrera")
                        .WithMany()
                        .HasForeignKey("IdCarrera")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CAEF.Models.Entities.CAEF.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("IdEstado")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CAEF.Models.Entities.CAEF.Materia", "Materia")
                        .WithMany()
                        .HasForeignKey("IdMateria")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CAEF.Models.Entities.CAEF.TipoExamen", "TipoExamen")
                        .WithMany()
                        .HasForeignKey("IdTipoExamen")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CAEF.Models.Entities.CAEF.Usuario", b =>
                {
                    b.HasOne("CAEF.Models.Entities.CAEF.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
