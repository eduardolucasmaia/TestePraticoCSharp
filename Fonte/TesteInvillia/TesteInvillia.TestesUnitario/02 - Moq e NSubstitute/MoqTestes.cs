using Dominio.Interfaces;
using DTO.DTO;
using DTO.Ferramentas;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace TesteInvillia.TestesUnitario
{
    public class MoqTestes
    {
        #region NSubstitute

        #region JOGO

        [Fact]
        public void BuscarJogoPeloId_NSUb()
        {
            //Arrange
            var id = 1;
            // Act
            var _jogoDominio = Substitute.For<IJogoDominio>();
            _jogoDominio.BuscarJogoPorId(id).Returns(new JogoDTO());
            _jogoDominio.Received(1);
        }

        [Fact]
        public void BuscarJogosPeloNome_NSUb()
        {
            //Arrange
            var nome = "Super";
            // Act
            var _jogoDominio = Substitute.For<IJogoDominio>();
            _jogoDominio.BuscarJogoPorNome(nome).Returns(new List<JogoDTO>());
            _jogoDominio.Received(1);
        }

        [Fact]
        public void BuscarJogos_NSUb()
        {
            // Act
            var _jogoDominio = Substitute.For<IJogoDominio>();
             _jogoDominio.BuscarJogos().Returns(new List<JogoDTO>());
            _jogoDominio.Received(1);
        }

        [Fact]
        public void BuscarJogosDisponiveisIncluindoIdUsuarioLogado_NSUb()
        {
            //Arrange
            var idUsuario = 1;
            // Act
            var _jogoDominio = Substitute.For<IJogoDominio>();
             _jogoDominio.BuscarJogosDisponiveis(idUsuario).Returns(new List<JogoDTO>());
            _jogoDominio.Received(1);
        }

        [Fact]
        public void ExcluirJogoPorId_NSUb()
        {
            //Arrange
            var id = 1;
            // Act
            var _jogoDominio = Substitute.For<IJogoDominio>();
             _jogoDominio.ExcluirJogo(id).Returns(true);
            _jogoDominio.Received(1);
        }

        [Fact]
        public void SalvarJogoPorId_NSUb()
        {
            //Arrange
            var jogo = new JogoDTO()
            {
                DataCadastro = DateTime.Now,
                Nome = "Nome de Teste",
                Plataforma = 1
            };
            // Act
            var _jogoDominio = Substitute.For<IJogoDominio>();
             _jogoDominio.SalvarJogo(jogo).Returns(true);
            _jogoDominio.Received(1);
        }

        #endregion

        #region USUÁRIO

        [Fact]
        public void AtualizarPerfilUsuario_NSUb()
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
            var _usuarioDominio = Substitute.For<IUsuarioDominio>();
            _usuarioDominio.AtualizarPerfil(usuario, id).Returns(true);
            _usuarioDominio.Received(1);
        }

        [Fact]
        public void AutenticarUsuarioPorEmail_NSUb()
        {
            //Arrange
            var username = Constantes.EMAIL_TESTE;
            var senha = Constantes.SENHA_INICIAL;
            // Act
            var _usuarioDominio = Substitute.For<IUsuarioDominio>();
            _usuarioDominio.AutenticarUsuario(username, senha).Returns(new UsuarioDTO());
            _usuarioDominio.Received(1);
        }

        [Fact]
        public void BuscarFotoPerfilUsuarioLogado_NSUb()
        {
            //Arrange
            var id = 1;
            // Act
            var _usuarioDominio = Substitute.For<IUsuarioDominio>();
            _usuarioDominio.BuscarFotoPerfilUsuarioAtual(id).Returns(string.Empty);
            _usuarioDominio.Received(1);
        }

        [Fact]
        public void BuscarUsuarioLogado_NSUb()
        {
            //Arrange
            var id = 1;
            // Act
            var _usuarioDominio = Substitute.For<IUsuarioDominio>();
            _usuarioDominio.BuscarUsuarioAtual(id).Returns(new UsuarioDTO());
            _usuarioDominio.Received(1);
        }

        [Fact]
        public void BuscarUsuarioPorEmail_NSUb()
        {
            //Arrange
            var email = Constantes.EMAIL_TESTE;
            // Act
            var _usuarioDominio = Substitute.For<IUsuarioDominio>();
            _usuarioDominio.BuscarUsuarioPorEmail(email).Returns(new UsuarioDTO());
            _usuarioDominio.Received(1);
        }

        [Fact]
        public void BuscarUsuarioPeloId_NSUb()
        {
            //Arrange
            var id = 1;
            // Act
            var _usuarioDominio = Substitute.For<IUsuarioDominio>();
            _usuarioDominio.BuscarUsuarioPorId(id).Returns(new UsuarioDTO());
            _usuarioDominio.Received(1);
        }

        [Fact]
        public void BuscarUsuarios_NSUb()
        {
            //Arrange
            var id = 1;
            // Act
            var _usuarioDominio = Substitute.For<IUsuarioDominio>();
            _usuarioDominio.BuscarUsuarios(id).Returns(new List<UsuarioDTO>());
            _usuarioDominio.Received(1);
        }

        [Fact]
        public void ExcluirUsuarioPorId_NSUb()
        {
            //Arrange
            var id = 1;
            // Act
            var _usuarioDominio = Substitute.For<IUsuarioDominio>();
            _usuarioDominio.ExcluirUsuario(id).Returns(true);
            _usuarioDominio.Received(1);
        }

        [Fact]
        public void SalvarUsuario_NSUb()
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
            var _usuarioDominio = Substitute.For<IUsuarioDominio>();
            _usuarioDominio.SalvarUsuario(usuario).Returns(true);
            _usuarioDominio.Received(1);
        }

        [Fact]
        public void TrocarSenhaPorId_NSUb()
        {
            //Arrange
            var id = 1;
            var senhaAntiga = Constantes.SENHA_INICIAL;
            var senhaNova = Constantes.SENHA_INICIAL;
            // Act
            var _usuarioDominio = Substitute.For<IUsuarioDominio>();
            _usuarioDominio.TrocarSenha(senhaAntiga, senhaNova, id).Returns(new UsuarioDTO());
            _usuarioDominio.Received(1);
        }

        #endregion

        #endregion

        #region Moq

        #region JOGO

        [Fact]
        public void BuscarJogoPeloId_Moq()
        {
            //Arrange
            var id = 1;
            // Act
            var _jogoDominio = new Mock<IJogoDominio>();
            _jogoDominio.Object.BuscarJogoPorId(id);
            _jogoDominio.Verify(r => r.BuscarJogoPorId(id),Times.Once);
        }

        [Fact]
        public void BuscarJogosPeloNome_Moq()
        {
            //Arrange
            var nome = "Super";
            // Act
            var _jogoDominio = new Mock<IJogoDominio>();
            _jogoDominio.Object.BuscarJogoPorNome(nome);
            _jogoDominio.Verify(r => r.BuscarJogoPorNome(nome),Times.Once);
        }

        [Fact]
        public void BuscarJogos_Moq()
        {
            // Act
            var _jogoDominio = new Mock<IJogoDominio>();
            _jogoDominio.Object.BuscarJogos();
            _jogoDominio.Verify(r => r.BuscarJogos(),Times.Once);
        }

        [Fact]
        public void BuscarJogosDisponiveisIncluindoIdUsuarioLogado_Moq()
        {
            //Arrange
            var idUsuario = 1;
            // Act
            var _jogoDominio = new Mock<IJogoDominio>();
            _jogoDominio.Object.BuscarJogosDisponiveis(idUsuario);
            _jogoDominio.Verify(r => r.BuscarJogosDisponiveis(idUsuario),Times.Once);
        }

        [Fact]
        public void ExcluirJogoPorId_Moq()
        {
            //Arrange
            var id = 1;
            // Act
            var _jogoDominio = new Mock<IJogoDominio>();
            _jogoDominio.Object.ExcluirJogo(id);
            _jogoDominio.Verify(r => r.ExcluirJogo(id),Times.Once);
        }

        [Fact]
        public void SalvarJogoPorId_Moq()
        {
            //Arrange
            var jogo = new JogoDTO()
            {
                DataCadastro = DateTime.Now,
                Nome = "Nome de Teste",
                Plataforma = 1
            };
            // Act
            var _jogoDominio = new Mock<IJogoDominio>();
            _jogoDominio.Object.SalvarJogo(jogo);
            _jogoDominio.Verify(r => r.SalvarJogo(jogo),Times.Once);
        }

        #endregion

        #region USUÁRIO

        [Fact]
        public void AtualizarPerfilUsuario_Moq()
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
            var _usuarioDominio = new Mock<IUsuarioDominio>();
            _usuarioDominio.Object.AtualizarPerfil(usuario, id);
            _usuarioDominio.Verify(r => r.AtualizarPerfil(usuario, id),Times.Once);
        }

        [Fact]
        public void AutenticarUsuarioPorEmail_Moq()
        {
            //Arrange
            var username = Constantes.EMAIL_TESTE;
            var senha = Constantes.SENHA_INICIAL;
            // Act
            var _usuarioDominio = new Mock<IUsuarioDominio>();
            _usuarioDominio.Object.AutenticarUsuario(username, senha);
            _usuarioDominio.Verify(r => r.AutenticarUsuario(username, senha),Times.Once);
        }

        [Fact]
        public void BuscarFotoPerfilUsuarioLogado_Moq()
        {
            //Arrange
            var id = 1;
            // Act
            var _usuarioDominio = new Mock<IUsuarioDominio>();
            _usuarioDominio.Object.BuscarFotoPerfilUsuarioAtual(id);
            _usuarioDominio.Verify(r => r.BuscarFotoPerfilUsuarioAtual(id),Times.Once);
        }

        [Fact]
        public void BuscarUsuarioLogado_Moq()
        {
            //Arrange
            var id = 1;
            // Act
            var _usuarioDominio = new Mock<IUsuarioDominio>();
            _usuarioDominio.Object.BuscarUsuarioAtual(id);
            _usuarioDominio.Verify(r => r.BuscarUsuarioAtual(id),Times.Once);
        }

        [Fact]
        public void BuscarUsuarioPorEmail_Moq()
        {
            //Arrange
            var email = Constantes.EMAIL_TESTE;
            // Act
            var _usuarioDominio = new Mock<IUsuarioDominio>();
            _usuarioDominio.Object.BuscarUsuarioPorEmail(email);
            _usuarioDominio.Verify(r => r.BuscarUsuarioPorEmail(email),Times.Once);
        }

        [Fact]
        public void BuscarUsuarioPeloId_Moq()
        {
            //Arrange
            var id = 1;
            // Act
            var _usuarioDominio = new Mock<IUsuarioDominio>();
            _usuarioDominio.Object.BuscarUsuarioPorId(id);
            _usuarioDominio.Verify(r => r.BuscarUsuarioPorId(id),Times.Once);
        }

        [Fact]
        public void BuscarUsuarios_Moq()
        {
            //Arrange
            var id = 1;
            // Act
            var _usuarioDominio = new Mock<IUsuarioDominio>();
            _usuarioDominio.Object.BuscarUsuarios(id);
            _usuarioDominio.Verify(r => r.BuscarUsuarios(id),Times.Once);
        }

        [Fact]
        public void ExcluirUsuarioPorId_Moq()
        {
            //Arrange
            var id = 1;
            // Act
            var _usuarioDominio = new Mock<IUsuarioDominio>();
            _usuarioDominio.Object.ExcluirUsuario(id);
            _usuarioDominio.Verify(r => r.ExcluirUsuario(id),Times.Once);
        }

        [Fact]
        public void SalvarUsuario_Moq()
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
            var _usuarioDominio = new Mock<IUsuarioDominio>();
            _usuarioDominio.Object.SalvarUsuario(usuario);
            _usuarioDominio.Verify(r => r.SalvarUsuario(usuario),Times.Once);
        }

        [Fact]
        public void TrocarSenhaPorId_Moq()
        {
            //Arrange
            var id = 1;
            var senhaAntiga = Constantes.SENHA_INICIAL;
            var senhaNova = Constantes.SENHA_INICIAL;
            // Act
            var _usuarioDominio = new Mock<IUsuarioDominio>();
            _usuarioDominio.Object.TrocarSenha(senhaAntiga, senhaNova, id);
            _usuarioDominio.Verify(r => r.TrocarSenha(senhaAntiga, senhaNova, id),Times.Once);
        }

        #endregion

        #endregion

    }
}