using AutoMapper;
using Dominio;
using Dominio.Entities.DataModels;
using Dominio.Interfaces;
using Infra;
using Infra.Interfaces;
using DTO.DTO;
using DTO.Ferramentas;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TesteInvillia
{
    public class StartupApiTests
    {
        public StartupApiTests(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Salvar nas configurações a connection string
            ConfiguracaoString.Conexao.Add("DefaultConnection", Configuration.GetConnectionString("DefaultConnection"));

            services.AddHttpContextAccessor();
            services.AddScoped<IUsuarioDominio, UsuarioDominio>();
            services.AddScoped<IUsuarioInfra, UsuarioInfra>();
            services.AddScoped<IJogoDominio, JogoDominio>();
            services.AddScoped<IJogoInfra, JogoInfra>();
            services.AddScoped<ILogErroInfra, LogErroInfra>();
            services.AddScoped<ILogErroDominio, LogErroDominio>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Usuario, UsuarioDTO>()
                .ForMember(x => x.Senha, opt => opt.Ignore())
                .ForMember(x => x.InverseIdCriadoUsuarioNavigation, opt => opt.Ignore());

                cfg.CreateMap<Role, RoleDTO>()
                .ForMember(x => x.VinculoUsuarioRole, opt => opt.Ignore());

                cfg.CreateMap<VinculoUsuarioRole, VinculoUsuarioRoleDTO>()
                .ForMember(x => x.IdUsuarioNavigation, opt => opt.Ignore());

                cfg.CreateMap<Jogo, JogoDTO>();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.AccessDeniedPath = new PathString("/Home/Http403");
                options.LoginPath = new PathString("/Conta/Login");
                options.Cookie.Expiration = TimeSpan.FromDays(7);
                options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
            });
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
