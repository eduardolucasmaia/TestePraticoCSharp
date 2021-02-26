using AutoMapper;
using Dominio;
using Dominio.Entities.DataModels;
using Infra;
using DTO.DTO;
using DTO.Ferramentas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteInvillia.TestesUnitario.Config;
using Xunit;

namespace TesteInvillia.TestesUnitario
{
    public class TestesAssertsNormal
    {
        #region Construtor

        private readonly JogoDominio _jogoDominio;
        private readonly UsuarioDominio _usuarioDominio;

        public TestesAssertsNormal()
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

        [Fact]
        public async Task BuscarJogoPeloId()
        {
            //Arrange
            var id = 1;
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

        [Fact]
        public async Task BuscarJogosPeloNome()
        {
            //Arrange
            var nome = "Super";
            // Act
            var resultado = await _jogoDominio.BuscarJogoPorNome(nome);
            // Assert
            Assert.IsType<List<JogoDTO>>(resultado);
        }

        [Fact]
        public async Task BuscarJogos()
        {

            // Act
            var resultado = await _jogoDominio.BuscarJogos();
            // Assert
            Assert.IsType<List<JogoDTO>>(resultado);
        }

        [Fact]
        public async Task BuscarJogosDisponiveisIncluindoIdUsuarioLogado()
        {
            //Arrange
            var idUsuario = 1;
            // Act
            var resultado = await _jogoDominio.BuscarJogosDisponiveis(idUsuario);
            // Assert
            Assert.IsType<List<JogoDTO>>(resultado);
        }

        [Fact]
        public async Task ExcluirJogoPorId()
        {
            //Arrange
            var id = 1;
            // Act
            var resultado = await _jogoDominio.ExcluirJogo(id);
            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task SalvarJogoPorId()
        {
            //Arrange
            var jogo = new JogoDTO()
            {
                DataCadastro = DateTime.Now,
                Nome = "Nome de Teste",
                Plataforma = 1
            };
            // Act
            var resultado = await _jogoDominio.SalvarJogo(jogo);
            // Assert
            Assert.True(resultado);
        }

        #endregion

        #region USUÁRIO

        [Fact]
        public async Task AtualizarPerfilUsuario()
        {
            //Arrange
            var id = 1;
            var usuario = new UsuarioDTO()
            {
                Id = id,
                DataCadastro = DateTime.Now,
                Nome = "Nome de Teste",
                Email = Constantes.EMAIL_TESTE,
                Senha = Constantes.SENHA_INICIAL
            };
            // Act
            var resultado = await _usuarioDominio.AtualizarPerfil(usuario, id);
            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task AutenticarUsuarioPorEmail()
        {
            //Arrange
            var username = Constantes.EMAIL_TESTE;
            var senha = Constantes.SENHA_INICIAL;
            // Act
            var resultado = await _usuarioDominio.AutenticarUsuario(username, senha);
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

        [Fact]
        public async Task BuscarFotoPerfilUsuarioLogado()
        {
            //Arrange
            var id = 1;
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

        [Fact]
        public async Task BuscarUsuarioLogado()
        {
            //Arrange
            var id = 1;
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

        [Fact]
        public async Task BuscarUsuarioPorEmail()
        {
            //Arrange
            var email = Constantes.EMAIL_TESTE;
            // Act
            var resultado = await _usuarioDominio.BuscarUsuarioPorEmail(email);
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

        [Fact]
        public async Task BuscarUsuarioPeloId()
        {
            //Arrange
            var id = 1;
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

        [Fact]
        public async Task BuscarUsuarios()
        {
            //Arrange
            var id = 1;
            // Act
            var resultado = await _usuarioDominio.BuscarUsuarios(id);
            // Assert
            Assert.IsType<List<UsuarioDTO>>(resultado);
        }

        [Fact]
        public async Task ExcluirUsuarioPorId()
        {
            //Arrange
            var id = 1;
            // Act
            var resultado = await _usuarioDominio.ExcluirUsuario(id);
            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task SalvarUsuario()
        {
            //Arrange
            var usuario = new UsuarioDTO()
            {
                DataCadastro = DateTime.Now,
                Nome = "Nome de Teste",
                Email = Constantes.EMAIL_TESTE,
                Senha = Constantes.SENHA_INICIAL
            };
            // Act
            var resultado = await _usuarioDominio.SalvarUsuario(usuario);
            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task TrocarSenhaPorId()
        {
            //Arrange
            var id = 1;
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