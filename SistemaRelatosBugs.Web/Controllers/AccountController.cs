using Microsoft.AspNetCore.Mvc;
using SistemaRelatosBugs.Infrastructure;
using SistemaRelatosBugs.Web.Models.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SistemaRelatosBugs.Domain;
using System.Linq;

namespace SistemaRelatosBugs.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _db;

        public AccountController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _db.Usuarios.FirstOrDefault(u => u.Login == model.Login
                || u.Email == model.Login
                || u.CPF == model.Login
                || u.Username == model.Login);

            if (user == null || user.SenhaHash != model.Senha)
            {
                ModelState.AddModelError(string.Empty, "Credenciais inválidas");
                return View(model);
            }

            var claims = new[] {
                new Claim(ClaimTypes.Name, user.Nome ?? user.Username ?? user.Login),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Tipo.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (user.Tipo == TipoUsuario.Gestor)
                return RedirectToAction("Index", "Manager");

            return RedirectToAction("Index", "Relator");
        }

        public async System.Threading.Tasks.Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
