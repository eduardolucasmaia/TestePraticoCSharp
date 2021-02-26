using DTO.Ferramentas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TesteInvillia.Controllers
{
    [Authorize(Roles = Constantes.ROLE_USUARIO)]
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Usuários";
            return View();
        }
    }
}