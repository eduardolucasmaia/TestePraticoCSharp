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
using System.Net;

namespace TesteInvillia
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Salvar nas configurações a connection string
            ConfiguracaoString.Conexao.Add("DefaultConnection", Configuration.GetConnectionString("DefaultConnection"));
            //ConfiguracaoString.Conexao.Add("DefaultConnection", Configuration.GetConnectionString("DockerConnection"));

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyMethod().AllowAnyHeader()
                        .WithOrigins("http://localhost:44318/", "https://localhost:44318/", "http://localhost:49640/", "https://localhost:49640/")
                        .AllowCredentials();
            }));

            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

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

            DependencyInjection(services);
        }

        /// <summary>
        /// Explicação:
        /// Transient objects are always different; a new instance is provided to every controller and every service.
        /// Scoped objects are the same within a request, but different across different requests.
        /// Singleton objects are the same for every object and every request.
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        private static void DependencyInjection(IServiceCollection services)
        {
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
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Http500");
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == (int)HttpStatusCode.NotFound && !context.Response.HasStarted)
                {
                    context.Items["caminhoOriginal"] = context.Request.Path.Value;
                    context.Request.Path = "/Home/Http404";
                    await next();
                }
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
