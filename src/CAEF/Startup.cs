using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CAEF.Models.Contexts;
using Microsoft.Extensions.Configuration;
using CAEF.Models.Seed;
using CAEF.Repositories;
using AutoMapper;
using CAEF.Models.DTO;
using CAEF.Models.Entities.CAEF;
using CAEF.Models.Entities.UABC;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Mvc;
using CAEF.Services;

namespace CAEF
{
    public class Startup
    {
        private IConfigurationRoot _config;
        private IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("config.json");

            _config = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Carga archivo de configuración
            services.AddSingleton(_config);

            // Contexts de base de datos
            services.AddDbContext<UsuarioUABCContext>();
            services.AddDbContext<UsuarioFIADContext>();
            services.AddDbContext<CAEFContext>();

            // Soporte a ASP.NET Identity para autenticación
            services.AddIdentity<UsuarioUABC, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireDigit = false;
                config.Cookies.ApplicationCookie.LoginPath = "/Login";
                //config.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            })
            .AddEntityFrameworkStores<UsuarioUABCContext>();

            // Datos semilla
            services.AddTransient<SemillaUABC>();
            services.AddTransient<SemillaCAEF>();
            services.AddTransient<SemillaFIAD>();

            // Repositorios
            services.AddScoped<IUABCRepository, UABCRepository>();
            services.AddScoped<IFIADRepository, FIADRepository>();            
            services.AddScoped<UsuarioRepository>();
            services.AddScoped<CarreraRepository>();
            services.AddScoped<MateriaRepository>();

            // Servicios
            services.AddScoped<LoginServices>();
            services.AddScoped<RolServices>();
            services.AddScoped<UsuarioServices>();
            services.AddScoped<SolicitudAdministrativaServices>();

            // MVC
            services.AddMvc(config =>
            {
                if (_env.IsProduction())
                {
                    config.Filters.Add(new RequireHttpsAttribute());
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure
            (
            ILoggerFactory loggerFactory,
            IApplicationBuilder app,
            SemillaUABC semillaUABC,
            SemillaCAEF semillaCAEF,
            SemillaFIAD semillaFIAD
            )
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<UsuarioDTO, Usuario>().ReverseMap();
                config.CreateMap<ActaDocenteDTO, SolicitudDocente>().ReverseMap();
                config.CreateMap<ActaAdminDTO, SolicitudAdmin>().ReverseMap();
                config.CreateMap<AlumnoDTO, Alumno>().ReverseMap();
                config.CreateMap<SolicitudAlumnoDTO, SolicitudAlumno>().ReverseMap();
            });

            loggerFactory.AddConsole();

            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseIdentity();

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Login", action = "Inicio" });
            });

            semillaUABC.GeneraDatosSemilla().Wait();
            semillaCAEF.GeneraDatosSemilla().Wait();
            semillaFIAD.GeneraDatosSemilla().Wait();
        }
    }
}
