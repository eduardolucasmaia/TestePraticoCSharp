using System.Threading.Tasks;
using Xunit;
using TesteInvillia.TestesIntegracao.Config;
using DTO.DTO;
using DTO.Ferramentas;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System;

namespace TesteInvillia.TestesIntegracao
{
    [Collection(nameof(TestesIntegracaoApiFixtureCollection))]
    public class FixturesTestes
    {
        private readonly TestesIntegracaoFixture<StartupApiTests> _integrationTestFixture;
        public FixturesTestes(TestesIntegracaoFixture<StartupApiTests> integrationTestFixture)
        {
            _integrationTestFixture = integrationTestFixture;
        }

        #region JOGO

        [Theory(), PrioridadeTeste(1)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task BuscarJogoPeloId(int id)
        {
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/Jogo/BuscarJogoPorId?id={id}");
            var resultado = await requisicao.Content.ReadAsStringAsync();
            var objeto = JsonConvert.DeserializeObject<RetornoGenerico<JogoDTO>>(resultado);
            Assert.True(requisicao.IsSuccessStatusCode);
            Assert.IsType<RetornoGenerico<JogoDTO>>(objeto);
        }

        [Theory]
        [InlineData("Super")]
        [InlineData("Legend")]
        [InlineData("Castlevania")]
        public async Task BuscarJogosPeloNome(string nome)
        {
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/Jogo/BuscarJogoPorNome?nome={nome}");
            var resultado = await requisicao.Content.ReadAsStringAsync();
            var objeto = JsonConvert.DeserializeObject<RetornoGenerico<List<JogoDTO>>>(resultado);
            Assert.True(requisicao.IsSuccessStatusCode);
            Assert.IsType<RetornoGenerico<List<JogoDTO>>>(objeto);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task BuscarJogosDisponiveisIncluindoIdUsuarioLogado(int idUsuario)
        {
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/Jogo/BuscarJogosDisponiveis?id={idUsuario}");
            var resultado = await requisicao.Content.ReadAsStringAsync();
            var objeto = JsonConvert.DeserializeObject<RetornoGenerico<List<JogoDTO>>>(resultado);
            Assert.True(requisicao.IsSuccessStatusCode);
            Assert.IsType<RetornoGenerico<List<JogoDTO>>>(objeto);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task ExcluirJogoPorId(int id)
        {
            var requisicao = await _integrationTestFixture.Client.DeleteAsync($"/api/Jogo/ExcluirJogo/{id}");
            var resultado = await requisicao.Content.ReadAsStringAsync();
            var objeto = JsonConvert.DeserializeObject<RetornoGenerico<int>>(resultado);
            //Assert.True(requisicao.IsSuccessStatusCode);
            //Assert.IsType<RetornoGenerico<int>>(objeto);
            Assert.False(requisicao.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData(0, "2017-3-1 12:19:20", "Super Mario World", 1)]
        [InlineData(1, "2007-11-1 09:15:10", "Legend of Zelda - Majora's Mask", 2)]
        [InlineData(2, "2013-12-1 18:45:56", "Castlevania: Symphony of the Night", 3)]
        public async Task SalvarJogoPorId(int id, string data, string nome, int plataforma)
        {
            var dataFormatado = DateTime.Parse(data);
            string json = JsonConvert.SerializeObject(new JogoDTO()
            {
                Id = id,
                DataCadastro = dataFormatado,
                Nome = nome,
                Plataforma = plataforma
            });
            var requisicao = await _integrationTestFixture.Client.PostAsJsonAsync($"/api/Jogo/SalvarJogo", json);
            var resultado = await requisicao.Content.ReadAsStringAsync();
            var objeto = JsonConvert.DeserializeObject<RetornoGenerico<JogoDTO>>(resultado);
            //Assert.True(requisicao.IsSuccessStatusCode);
            //Assert.IsType<RetornoGenerico<JogoDTO>>(objeto);
            Assert.False(requisicao.IsSuccessStatusCode);
        }

        #endregion

        #region USUÁRIO

        [Theory]
        [InlineData(0, "2017-3-1 12:19:20", "Nome de Teste")]
        [InlineData(1, "2007-11-1 09:15:10", "Nome de Teste 2")]
        [InlineData(2, "2013-12-1 18:45:56", "Nome de Teste 3")]
        public async Task AtualizarPerfilUsuario(int id, string data, string nome)
        {
            var dataFormatado = DateTime.Parse(data);
            string json = JsonConvert.SerializeObject(new UsuarioDTO()
            {
                Id = id,
                DataCadastro = dataFormatado,
                Nome = nome,
                Email = Constantes.EMAIL_TESTE,
                Senha = Constantes.SENHA_INICIAL
            });
            var requisicao = await _integrationTestFixture.Client.PostAsJsonAsync($"/api/Usuario/AtualizarPerfil", json);
            var resultado = await requisicao.Content.ReadAsStringAsync();
            var objeto = JsonConvert.DeserializeObject<RetornoGenerico<UsuarioDTO>>(resultado);
            //Assert.True(requisicao.IsSuccessStatusCode);
            //Assert.IsType<RetornoGenerico<UsuarioDTO>>(objeto);
            Assert.False(requisicao.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task BuscarUsuarioPeloId(int id)
        {
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/Usuario/BuscarUsuarioPorId?id={id}");
            var resultado = await requisicao.Content.ReadAsStringAsync();
            var objeto = JsonConvert.DeserializeObject<RetornoGenerico<UsuarioDTO>>(resultado);
            Assert.True(requisicao.IsSuccessStatusCode);
            Assert.IsType<RetornoGenerico<UsuarioDTO>>(objeto);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task BuscarUsuarios(int id)
        {
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/Usuario/BuscarUsuarios");
            var resultado = await requisicao.Content.ReadAsStringAsync();
            var objeto = JsonConvert.DeserializeObject<RetornoGenerico<List<UsuarioDTO>>>(resultado);
            Assert.True(requisicao.IsSuccessStatusCode);
            Assert.IsType<RetornoGenerico<List<UsuarioDTO>>>(objeto);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task ExcluirUsuarioPorId(int id)
        {
            var requisicao = await _integrationTestFixture.Client.DeleteAsync($"/api/Usuario/ExcluirUsuario/{id}");
            var resultado = await requisicao.Content.ReadAsStringAsync();
            var objeto = JsonConvert.DeserializeObject<RetornoGenerico<int>>(resultado);
            //Assert.True(requisicao.IsSuccessStatusCode);
            //Assert.IsType<RetornoGenerico<int>>(objeto);
            Assert.False(requisicao.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData(0, "2017-3-1 12:19:20", "Nome de Teste")]
        [InlineData(1, "2007-11-1 09:15:10", "Nome de Teste 2")]
        [InlineData(2, "2013-12-1 18:45:56", "Nome de Teste 3")]
        public async Task SalvarUsuario(int id, string data, string nome)
        {
            var dataFormatado = DateTime.Parse(data);
            string json = JsonConvert.SerializeObject(new UsuarioDTO()
            {
                Id = id,
                DataCadastro = dataFormatado,
                Nome = nome,
                Email = Constantes.EMAIL_TESTE,
                Senha = Constantes.SENHA_INICIAL
            });
            var requisicao = await _integrationTestFixture.Client.PostAsJsonAsync($"/api/Usuario/SalvarUsuario", json);
            var resultado = await requisicao.Content.ReadAsStringAsync();
            var objeto = JsonConvert.DeserializeObject<RetornoGenerico<UsuarioDTO>>(resultado);
            //Assert.True(requisicao.IsSuccessStatusCode);
            //Assert.IsType<RetornoGenerico<UsuarioDTO>>(objeto);
            Assert.False(requisicao.IsSuccessStatusCode);
        }

        #endregion

    }

}