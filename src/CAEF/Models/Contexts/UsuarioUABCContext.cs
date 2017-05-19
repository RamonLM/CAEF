using CAEF.Models.Entities.UABC;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CAEF.Models.Contexts
{
    public class UsuarioUABCContext : IdentityDbContext<UsuarioUABC>
    {
        private IConfigurationRoot _config;

        public UsuarioUABCContext(IConfigurationRoot config, DbContextOptions<UsuarioUABCContext> options) : base(options)
        {
            _config = config;
        }
        public DbSet<UsuarioUABC> UsuariosUABC { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConexionesBD:ConexionUsuariosUABC"]);
        }
    }
}
