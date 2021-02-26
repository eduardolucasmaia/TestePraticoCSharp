using DTO.Ferramentas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace TesteInvillia.Controllers.api
{
    public class HttpContextAcessorController : ControllerBase
    {
        #region Construtor

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextAcessorController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        public int RecuperarIdUsuario()
        {
            try
            {
                if (_httpContextAccessor.HttpContext != null)
                    return Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                throw new Exception(Mensagens.MS_002);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
