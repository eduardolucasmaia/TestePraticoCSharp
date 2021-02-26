using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Dominio.Interfaces;
using DTO.DTO;
using DTO.Ferramentas;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TesteInvillia.Models;

namespace TesteInvillia.Controllers
{
    public class ContaController : Controller
    {
        #region Contrutor

        private readonly IUsuarioDominio _usuarioDominio;
        private readonly ILogErroDominio _logErroDominio;

        public ContaController(IUsuarioDominio usuarioDominio, ILogErroDominio logErroDominio)
        {
            _logErroDominio = logErroDominio;
            _usuarioDominio = usuarioDominio;
        }

        #endregion

        #region GET

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            ViewData["Title"] = "Login";
            if (User.Identity.IsAuthenticated)
            {
                if (Url.IsLocalUrl(ReturnUrl) && ReturnUrl.Length > 1 && ReturnUrl.StartsWith("/")
                       && !ReturnUrl.StartsWith("//") && !ReturnUrl.StartsWith("/\\"))
                {
                    return Redirect(ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            try
            {
                ViewData["CompileDate"] = "Data último build: " + System.IO.File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location).ToString("dd/MM/yyyy HH:mm:ss");
            }
            catch
            {
                ViewData["CompileDate"] = string.Empty;
            }
            var login = new LoginViewModel();
            return View(login);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult TrocarSenha(string codigo)
        {
            ViewData["Title"] = "Trocar senha";
            var model = new TrocarSenhaViewModel()
            {
                //Codigo = codigo,
                SenhaAntiga = "**********"
            };
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult EsqueceuSenha()
        {
            ViewData["Title"] = "Esqueceu a senha";
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var esqueceuSenha = new EsqueceuSenhaViewModel();
            return View(esqueceuSenha);
        }

        #endregion

        #region POST

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login, string ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            ViewData["Title"] = "Login";
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = await _usuarioDominio.AutenticarUsuario(login.UserName.Trim(), login.Senha.Trim());
                    if (usuario != null)
                    {
                        await CriarCookie(usuario, login.LembrarMe);

                        if (Url.IsLocalUrl(ReturnUrl) && ReturnUrl.Length > 1 && ReturnUrl.StartsWith("/")
                       && !ReturnUrl.StartsWith("//") && !ReturnUrl.StartsWith("/\\"))
                        {
                            return Redirect(ReturnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "O nome de usuário ou senha estão incorretos.");
                    }
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, Mensagens.MS_002);
                }
            }
            return View(login);
        }

        #endregion

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(scheme: CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch { }
            return RedirectToAction("Login", "Conta");
        }

        private async Task CriarCookie(UsuarioDTO usuario, bool lembrarMe)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString(),ClaimValueTypes.Integer),
                    new Claim(ClaimTypes.Name, usuario.Nome,ClaimValueTypes.String),
                    new Claim(ClaimTypes.Email, usuario.Email, ClaimValueTypes.String)
                };

                foreach (var loop in usuario.VinculoUsuarioRole)
                {
                    if (loop.IdRoleNavigation != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, loop.IdRoleNavigation.NomeRole, ClaimValueTypes.String));
                    }
                }

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(Constantes.TEMPO_DIA_EXPIRAR_LOGIN),
                    IsPersistent = lembrarMe
                };

                await HttpContext.SignInAsync(
                   CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
            }
            catch (Exception ex)
            {
                await _logErroDominio.SalvarLog(ex);
                throw;
            }
        }

    }
}