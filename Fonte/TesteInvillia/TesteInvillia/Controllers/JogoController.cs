using DTO.Ferramentas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TesteInvillia.Controllers
{
    [Authorize(Roles = Constantes.ROLE_JOGO)]
    public class JogoController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Jogos";
            return View();
        }
    }
}