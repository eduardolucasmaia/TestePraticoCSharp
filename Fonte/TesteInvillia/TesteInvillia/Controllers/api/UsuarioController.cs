using Dominio.Interfaces;
using DTO.DTO;
using DTO.Ferramentas;
using Microsoft.AspNetCore.Http;
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
    [Authorize(Roles = Constantes.ROLE_USUARIO)]
#endif
    [Produces("application/json")]
    [Route("api/Usuario/[action]")]
    [ApiController]
    public class UsuarioController : HttpContextAcessorController
    {
        #region Construtor

        private readonly IUsuarioDominio _usuarioDominio;

        public UsuarioController(IUsuarioDominio usuarioDominio, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _usuarioDominio = usuarioDominio;
        }

        #endregion

        #region AÇÕES

        [HttpPost]
        [ActionName("SalvarUsuario")]
        public async Task<RetornoGenerico<UsuarioDTO>> SalvarUsuario([FromForm] UsuarioDTO usuario)
        {
            try
            {
                if (usuario.Id == 0)
                {
                    var usuarioRetorno = await _usuarioDominio.BuscarUsuarioPorEmail(usuario.Email);
                    if (usuarioRetorno != null)
                    {
                        return new RetornoGenerico<UsuarioDTO>()
                        {
                            Retorno = usuario,
                            Sucesso = false,
                            Mensagem = Mensagens.MS_014,
                            TipoMensagem = Constantes.TIPO_MENSAGEM_ALERTA
                        };
                    }
                }

                usuario.IdCriadoUsuario = RecuperarIdUsuario();
                await _usuarioDominio.SalvarUsuario(usuario);
                return new RetornoGenerico<UsuarioDTO>()
                {
                    Retorno = usuario,
                    Sucesso = true,
                    Mensagem = usuario.Id != 0 ? Mensagens.MS_011 : Mensagens.MS_010,
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

        [HttpDelete]
        [ActionName("ExcluirUsuario")]
        public async Task<RetornoGenerico<int>> ExcluirUsuario([FromForm] int id)
        {
            try
            {
                await _usuarioDominio.ExcluirUsuario(id);
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
        [ActionName("BuscarUsuarios")]
        public async Task<RetornoGenerico<List<UsuarioDTO>>> BuscarUsuarios()
        {
            try
            {
                var returno = await _usuarioDominio.BuscarUsuarios(RecuperarIdUsuario());
                return new RetornoGenerico<List<UsuarioDTO>>()
                {
                    Retorno = returno,
                    Sucesso = true
                };
            }
            catch (Exception ex)
            {
                return new RetornoGenerico<List<UsuarioDTO>>()
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
        [ActionName("BuscarUsuarioPorId")]
        public async Task<RetornoGenerico<UsuarioDTO>> BuscarUsuarioPorId(int id)
        {
            try
            {
                var returno = await _usuarioDominio.BuscarUsuarioPorId(id);
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