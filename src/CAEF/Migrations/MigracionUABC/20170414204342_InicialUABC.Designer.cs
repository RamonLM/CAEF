using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CAEF.Models.Contexts;

namespace CAEF.Migrations.MigracionUABC
{
    [DbContext(typeof(UsuarioUABCContext))]
    [Migration("20170414204342_InicialUABC")]
    partial class InicialUABC
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CAEF.Models.Entities.UABC.UsuarioUABC", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("Numero_Empleado");

                    b.Property<string>("ApellidoM");

                    b.Property<string>("ApellidoP");

                    b.Property<string>("Correo");

                    b.Property<string>("Nombre");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("UsuariosUABC");
                });
        }
    }
}
