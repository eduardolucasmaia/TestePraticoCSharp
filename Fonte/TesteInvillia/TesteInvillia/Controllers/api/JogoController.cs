using Dominio.Interfaces;
using DTO.DTO;
using DTO.Ferramentas;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#if !DEBUG
using Microsoft.AspNetCore.Authorization;
#endif

namespace TesteInvillia.Controllers.api
{
#if !DEBUG
    [Authorize(Roles = Constantes.ROLE_JOGO)]
#endif
    [Produces("application/json")]
    [Route("api/Jogo/[action]")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        #region Construtor

        private readonly IJogoDominio _jogoDominio;

        public JogoController(IJogoDominio jogoDominio)
        {
            _jogoDominio = jogoDominio;
        }

        #endregion

        #region AÇÕES

        [HttpPost]
        [ActionName("SalvarJogo")]
        public async Task<RetornoGenerico<JogoDTO>> SalvarJogo([FromForm] JogoDTO jogo)
        {
            try
            {
                await _jogoDominio.SalvarJogo(jogo);
                return new RetornoGenerico<JogoDTO>()
                {
                    Retorno = jogo,
                    Sucesso = true,
                    Mensagem = jogo.Id != 0 ? Mensagens.MS_011 : Mensagens.MS_010,
                    TipoMensagem = Constantes.TIPO_MENSAGEM_SUCESSO
                };
            }
            catch (Exception ex)
            {
                return new RetornoGenerico<JogoDTO>()
                {
                    Retorno = jogo,
                    Sucesso = false,
                    Mensagem = Mensagens.MS_002,
                    TipoMensagem = Constantes.TIPO_MENSAGEM_ERRO,
                    Exception = ex
                };
            }
        }

        [HttpDelete]
        [ActionName("ExcluirJogo/[id]")]
        public async Task<RetornoGenerico<int>> ExcluirJogo([FromRoute] int id)
        {
            try
            {
                await _jogoDominio.ExcluirJogo(id);
                return new RetornoGenerico<int>()
                {
                    Retorno = id,
                    Sucesso = true,
                    Mensagem = Mensagens.MS_012,
                    TipoMensagem = Constantes.TIPO_MENSAGEM_SUCESSO
                };
            }
            catch (Exception ex)
            {
                return new RetornoGenerico<int>()
                {
                    Retorno = id,
                    Sucesso = false,
                    Mensagem = Mensagens.MS_002,
                    TipoMensagem = Constantes.TIPO_MENSAGEM_ERRO,
                    Exception = ex
                };
            }
        }

        #endregion

        #region CONSULTAS

        [HttpGet]
        [ActionName("BuscarJogosDisponiveis")]
        public async Task<RetornoGenerico<List<JogoDTO>>> BuscarJogosDisponiveis(int id)
        {
            try
            {
                var returno = await _jogoDominio.BuscarJogosDisponiveis(id);
                return new RetornoGenerico<List<JogoDTO>>()
                {
                    Retorno = returno,
                    Sucesso = true
                };
            }
            catch (Exception ex)
            {
                return new RetornoGenerico<List<JogoDTO>>()
                {
                    Retorno = null,
                    Sucesso = false,
                    Mensagem = Mensagens.MS_002,
                    TipoMensagem = Constantes.TIPO_MENSAGEM_ERRO,
                    Exception = ex
                };
            }
        }

        [HttpGet]
        [ActionName("BuscarJogos")]
        public async Task<RetornoGenerico<List<JogoDTO>>> BuscarJogos()
        {
            try
            {
                var returno = await _jogoDominio.BuscarJogos();
                return new RetornoGenerico<List<JogoDTO>>()
                {
                    Retorno = returno,
                    Sucesso = true
                };
            }
            catch (Exception ex)
            {
                return new RetornoGenerico<List<JogoDTO>>()
                {
                    Retorno = null,
                    Sucesso = false,
                    Mensagem = Mensagens.MS_002,
                    TipoMensagem = Constantes.TIPO_MENSAGEM_ERRO,
                    Exception = ex
                };
            }
        }

        [HttpGet]
        [ActionName("BuscarJogoPorNome")]
        public async Task<RetornoGenerico<List<JogoDTO>>> BuscarJogoPorNome(string nome)
        {
            try
            {
                var returno = await _jogoDominio.BuscarJogoPorNome(nome);
                return new RetornoGenerico<List<JogoDTO>>()
                {
                    Retorno = returno,
                    Sucesso = true
                };
            }
            catch (Exception ex)
            {
                return new RetornoGenerico<List<JogoDTO>>()
                {
                    Retorno = null,
                    Sucesso = false,
                    Mensagem = Mensagens.MS_002,
                    TipoMensagem = Constantes.TIPO_MENSAGEM_ERRO,
                    Exception = ex
                };
            }
        }

        [HttpGet]
        [ActionName("BuscarJogoPorId")]
        public async Task<RetornoGenerico<JogoDTO>> BuscarJogoPorId(int id)
        {
            try
            {
                var returno = await _jogoDominio.BuscarJogoPorId(id);
                return new RetornoGenerico<JogoDTO>()
                {
                    Retorno = returno,
                    Sucesso = true
                };
            }
            catch (Exception ex)
            {
                return new RetornoGenerico<JogoDTO>()
                {
                    Retorno = null,
                    Sucesso = false,
                    Mensagem = Mensagens.MS_002,
                    TipoMensagem = Constantes.TIPO_MENSAGEM_ERRO,
                    Exception = ex
                };
            }
        }

        [HttpGet]
        [ActionName("BuscarPlataformas")]
        public RetornoGenerico<List<Plataforma>> BuscarPlataformas()
        {
            return new RetornoGenerico<List<Plataforma>>()
            {
                Retorno = Constantes.PLATAFORMAS,
                Sucesso = true
            };
        }

        #endregion

    }
}