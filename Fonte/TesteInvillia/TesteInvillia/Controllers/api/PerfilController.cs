using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Dominio.Interfaces;
using DTO.DTO;
using DTO.Ferramentas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TesteInvillia.Models;
#if !DEBUG
using Microsoft.AspNetCore.Authorization;
#endif

namespace TesteInvillia.Controllers.api
{
#if !DEBUG
    [Authorize]
#endif
    [Produces("application/json")]
    [Route("api/Perfil/[action]")]
    [ApiController]
    public class PerfilController : HttpContextAcessorController
    {
        #region Construtor

        private readonly IUsuarioDominio _usuarioDominio;

        public PerfilController(IUsuarioDominio usuarioDominio, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _usuarioDominio = usuarioDominio;
        }

        #endregion

        #region AÇÕES

        [HttpPost]
        [ActionName("AtualizarPerfil")]
        public async Task<RetornoGenerico<UsuarioDTO>> AtualizarPerfil([FromForm] UsuarioDTO usuario)
        {
            try
            {
                await _usuarioDominio.AtualizarPerfil(usuario, RecuperarIdUsuario());
                return new RetornoGenerico<UsuarioDTO>()
                {
                    Retorno = usuario,
                    Sucesso = true,
                    Mensagem = Mensagens.MS_011,
                    TipoMensagem = Constantes.TIPO_MENSAGEM_SUCESSO
                };
            }
            catch (Exception ex)
            {
                return new RetornoGenerico<UsuarioDTO>()
                {
                    Retorno = usuario,
                    Sucesso = false,
                    Mensagem = Mensagens.MS_002,
                    TipoMensagem = Constantes.TIPO_MENSAGEM_ERRO,
                    Exception = ex
                };
            }
        }

        [HttpPost]
        [ActionName("TrocarSenha")]
        public async Task<object> TrocarSenha([FromForm] TrocarSenhaViewModel usuario)
        {
            try
            {
                var usuarioRetorno = await _usuarioDominio.TrocarSenha(usuario.SenhaAntiga, usuario.NovaSenha, RecuperarIdUsuario());
                if (usuarioRetorno == null)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    ModelState.AddModelError("SenhaAntiga", Mensagens.MS_007);
                    return new Dictionary<string, object>() { ["SenhaAntiga"] = new Dictionary<string, string>() { ["0"] = Mensagens.MS_007 } };
                }
                else
                {
                    return new RetornoGenerico<TrocarSenhaViewModel>()
                    {
                        Retorno = usuario,
                        Sucesso = true,
                        Mensagem = Mensagens.MS_011,
                        TipoMensagem = Constantes.TIPO_MENSAGEM_SUCESSO
                    };
                }
            }
            catch (Exception ex)
            {
                return new RetornoGenerico<TrocarSenhaViewModel>()
                {
                    Retorno = usuario,
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
        [ActionName("BuscarFotoPerfilUsuarioAtual")]
        public async Task<RetornoGenerico<string>> BuscarFotoPerfilUsuarioAtual()
        {
            try
            {
                var returno = await _usuarioDominio.BuscarFotoPerfilUsuarioAtual(RecuperarIdUsuario());
                return new RetornoGenerico<string>()
                {
                    Retorno = returno,
                    Sucesso = true
                };
            }
            catch (Exception ex)
            {
                return new RetornoGenerico<string>()
                {
                    Retorno = "@DTO.Ferramentas.Constantes.IMAGEM_SEM_IMAGEM",
                    Sucesso = false,
                    Mensagem = Mensagens.MS_002,
                    TipoMensagem = Constantes.TIPO_MENSAGEM_ERRO,
                    Exception = ex
                };
            }
        }

        [HttpGet]
        [ActionName("BuscarUsuarioAtual")]
        public async Task<RetornoGenerico<UsuarioDTO>> BuscarUsuarioAtual()
        {
            try
            {
                var returno = await _usuarioDominio.BuscarUsuarioAtual(RecuperarIdUsuario());
                return new RetornoGenerico<UsuarioDTO>()
                {
                    Retorno = returno,
                    Sucesso = true
                };
            }
            catch (Exception ex)
            {
                return new RetornoGenerico<UsuarioDTO>()
                {
                    Retorno = null,
                    Sucesso = false,
                    Mensagem = Mensagens.MS_002,
                    TipoMensagem = Constantes.TIPO_MENSAGEM_ERRO,
                    Exception = ex
                };
            }
        }

        #endregion

    }
}