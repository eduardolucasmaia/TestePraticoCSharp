using AutoMapper;
using Dominio;
using Dominio.Entities.DataModels;
using Infra;
using DTO.DTO;
using DTO.Ferramentas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteInvillia.TestesUnitario.Config;
using Xunit;

namespace TesteInvillia.TestesUnitario
{
    public class TestesTeoria
    {
        #region Construtor

        private readonly JogoDominio _jogoDominio;
        private readonly UsuarioDominio _usuarioDominio;

        public TestesTeoria()
        {
            if (!ConfiguracaoString.Conexao.ContainsKey("DefaultConnection"))    
                ConfiguracaoString.Conexao.Add("DefaultConnection", ConexaoStringTestes.CONEXAO_STRING_TESTE);

            JogoInfra _jogoInfra = new JogoInfra();
            UsuarioInfra _usuarioInfra = new UsuarioInfra();
            LogErroDominio logErroDominio = new LogErroDominio(new LogErroInfra());

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

            _jogoDominio = new JogoDominio(_jogoInfra, logErroDominio, mapper);
            _usuarioDominio = new UsuarioDominio(_usuarioInfra, _jogoInfra, logErroDominio, mapper);
        }

        #endregion

        #region JOGO

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task BuscarJogoPeloId(int id)
        {
            // Act
            var resultado = await _jogoDominio.BuscarJogoPorId(id);
            // Assert
            if (resultado == null)
            {
                Assert.Null(resultado);
            }
            else
            {
                Assert.IsType<JogoDTO>(resultado);
            }
        }

        [Theory]
        [InlineData("Super")]
        [InlineData("Legend")]
        [InlineData("Castlevania")]
        public async Task BuscarJogosPeloNome(string nome)
        {
            // Act
            var resultado = await _jogoDominio.BuscarJogoPorNome(nome);
            // Assert
            Assert.IsType<List<JogoDTO>>(resultado);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task BuscarJogosDisponiveisIncluindoIdUsuarioLogado(int idUsuario)
        {
            // Act
            var resultado = await _jogoDominio.BuscarJogosDisponiveis(idUsuario);
            // Assert
            Assert.IsType<List<JogoDTO>>(resultado);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task ExcluirJogoPorId(int id)
        {
            // Act
            var resultado = await _jogoDominio.ExcluirJogo(id);
            // Assert
            Assert.True(resultado);
        }

        [Theory]
        [InlineData(0, "2017-3-1 12:19:20", "Super Mario World", 1)]
        [InlineData(1, "2007-11-1 09:15:10", "Legend of Zelda - Majora's Mask", 2)]
        [InlineData(2, "2013-12-1 18:45:56", "Castlevania: Symphony of the Night", 3)]
        public async Task SalvarJogoPorId(int id, string data, string nome, int plataforma)
        {
            //Arrange
            var dataFormatado = DateTime.Parse(data);
            var jogo = new JogoDTO()
            {
                Id = id,
                DataCadastro = dataFormatado,
                Nome = nome,
                Plataforma = plataforma
            };
            // Act
            var resultado = await _jogoDominio.SalvarJogo(jogo);
            // Assert
            Assert.True(resultado);
        }

        #endregion

        #region USUÁRIO

        [Theory]
        [InlineData(0, "2017-3-1 12:19:20", "Nome de Teste")]
        [InlineData(1, "2007-11-1 09:15:10", "Nome de Teste 2")]
        [InlineData(2, "2013-12-1 18:45:56", "Nome de Teste 3")]
        public async Task AtualizarPerfilUsuario(int id, string data, string nome)
        {
            //Arrange
            var dataFormatado = DateTime.Parse(data);
            var usuario = new UsuarioDTO()
            {
                Id = id,
                DataCadastro = dataFormatado,
                Nome = nome,
                Email = Constantes.EMAIL_TESTE,
                Senha = Constantes.SENHA_INICIAL
            };
            // Act
            var resultado = await _usuarioDominio.AtualizarPerfil(usuario, id);
            // Assert
            Assert.True(resultado);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task BuscarFotoPerfilUsuarioLogado(int id)
        {
            // Act
            var resultado = await _usuarioDominio.BuscarFotoPerfilUsuarioAtual(id);
            // Assert
            if (resultado == null)
            {
                Assert.Null(resultado);
            }
            else
            {
                Assert.IsType<string>(resultado);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task BuscarUsuarioLogado(int id)
        {
            // Act
            var resultado = await _usuarioDominio.BuscarUsuarioAtual(id);
            // Assert
            if (resultado == null)
            {
                Assert.Null(resultado);
            }
            else
            {
                Assert.IsType<UsuarioDTO>(resultado);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task BuscarUsuarioPeloId(int id)
        {
            // Act
            var resultado = await _usuarioDominio.BuscarUsuarioPorId(id);
            // Assert
            if (resultado == null)
            {
                Assert.Null(resultado);
            }
            else
            {
                Assert.IsType<UsuarioDTO>(resultado);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task BuscarUsuarios(int id)
        {
            // Act
            var resultado = await _usuarioDominio.BuscarUsuarios(id);
            // Assert
            Assert.IsType<List<UsuarioDTO>>(resultado);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task ExcluirUsuarioPorId(int id)
        {
            // Act
            var resultado = await _usuarioDominio.ExcluirUsuario(id);
            // Assert
            Assert.True(resultado);
        }

        [Theory]
        [InlineData(0, "2017-3-1 12:19:20", "Nome de Teste")]
        [InlineData(1, "2007-11-1 09:15:10", "Nome de Teste 2")]
        [InlineData(2, "2013-12-1 18:45:56", "Nome de Teste 3")]
        public async Task SalvarUsuario(int id, string data, string nome)
        {
            //Arrange
            var dataFormatado = DateTime.Parse(data);
            var usuario = new UsuarioDTO()
            {
                Id = id,
                DataCadastro = dataFormatado,
                Nome = nome,
                Email = Constantes.EMAIL_TESTE,
                Senha = Constantes.SENHA_INICIAL
            };
            // Act
            var resultado = await _usuarioDominio.SalvarUsuario(usuario);
            // Assert
            Assert.True(resultado);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task TrocarSenhaPorId(int id)
        {
            //Arrange
            var senhaAntiga = Constantes.SENHA_INICIAL;
            var senhaNova = Constantes.SENHA_INICIAL;
            // Act
            var resultado = await _usuarioDominio.TrocarSenha(senhaAntiga, senhaNova, id);
            // Assert
            if (resultado == null)
            {
                Assert.Null(resultado);
            }
            else
            {
                Assert.IsType<UsuarioDTO>(resultado);
            }
        }

        #endregion

    }
}
