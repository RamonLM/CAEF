using CAEF.Models.Entities.FIAD;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CAEF.Models.Contexts
{
    public class UsuarioFIADContext : DbContext
    {
        private IConfigurationRoot _config;

        public UsuarioFIADContext(IConfigurationRoot config, DbContextOptions<UsuarioFIADContext> options) : base(options)
        {
            _config = config;
        }
        public DbSet<UsuarioFIAD> UsuariosFIAD { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConexionesBD:ConexionUsuariosFIAD"]);
        }
    }
}
