using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CAEF.Models.Contexts;

namespace CAEF.Migrations.MigracionFIAD
{
    [DbContext(typeof(UsuarioFIADContext))]
    [Migration("20170414204413_InicialFIAD")]
    partial class InicialFIAD
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CAEF.Models.Entities.FIAD.UsuarioFIAD", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("Numero_Empleado");

                    b.Property<string>("ApellidoM");

                    b.Property<string>("ApellidoP");

                    b.Property<string>("Correo");

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("UsuariosFIAD");
                });
        }
    }
}
