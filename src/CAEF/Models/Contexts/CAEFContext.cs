using CAEF.Models.Entities.CAEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAEF.Models.Contexts
{
    public class CAEFContext : DbContext
    {
        private IConfigurationRoot _config;

        public CAEFContext(IConfigurationRoot config, DbContextOptions<CAEFContext> options) : base(options)
        {
            _config = config;
        }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<SolicitudAdmin> SolicitudesAdministrativo { get; set; }
        public DbSet<SolicitudAlumno> SolicitudesAlumno { get; set; }
        public DbSet<SolicitudDocente> SolicitudesDocente { get; set; }
        public DbSet<SubtipoExamen> SubtiposExamen { get; set; }
        public DbSet<TipoExamen> TiposExamen { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer(_config["ConexionesBD:ConexionCAEF"]);
        }
    }
}
