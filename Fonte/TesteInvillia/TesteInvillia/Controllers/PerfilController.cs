using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TesteInvillia.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Perfil";
            return View();
        }
    }
}