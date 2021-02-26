using AutoMapper;
using Dominio;
using Dominio.Entities.DataModels;
using Dominio.Interfaces;
using Infra;
using Infra.Interfaces;
using DTO.DTO;
using DTO.Ferramentas;
using Microsoft.Extensions.DependencyInjection;

namespace TesteInvillia.TestesUnitario.Config
{
    public class TestesUnitarioFixture
    {
        public TestesUnitarioFixture()
        {
            ConfiguracaoString.Conexao.Add("DefaultConnection", ConexaoStringTestes.CONEXAO_STRING_TESTE);

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHttpContextAccessor();
            serviceCollection.AddScoped<IUsuarioDominio, UsuarioDominio>();
            serviceCollection.AddScoped<IUsuarioInfra, UsuarioInfra>();
            serviceCollection.AddScoped<IJogoDominio, JogoDominio>();
            serviceCollection.AddScoped<IJogoInfra, JogoInfra>();
            serviceCollection.AddScoped<ILogErroDominio, LogErroDominio>();
            serviceCollection.AddScoped<ILogErroInfra, LogErroInfra>();

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
            serviceCollection.AddSingleton(mapper);

            ServiceProvider = serviceCollection.BuildServiceProvider();

        }
        public ServiceProvider ServiceProvider { get; private set; }
    }
}
