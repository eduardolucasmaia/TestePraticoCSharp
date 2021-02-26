using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TesteInvillia.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Página inicial";
            return View();
        }

        public IActionResult Http403()
        {
            ViewData["Title"] = "Acesso negado";
            return View();
        }

        public IActionResult Http404()
        {
            var originalPath = HttpContext.Items["caminhoOriginal"];
            ViewData["Title"] = "Página não encontrada";
            return View();
        }

        public IActionResult Http500()
        {
            ViewData["Title"] = "Erro inesperado do servidor";
            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["Title"] = "Cookies e privacidade";
            return View();
        }
    }
}