using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Diagnostics;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ApplicationDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            _logger.LogInformation($"Tentativa de login para o usuário: {model.Username}");

            if (ModelState.IsValid)
            {
                var user = await _context.AdminUsers.FirstOrDefaultAsync(u => u.Username == model.Username);
                if (user != null)
                {
                    _logger.LogInformation($"Usuário encontrado: {user.Username}");
                    _logger.LogInformation($"Hash armazenado: {user.PasswordHash}");

                    try
                    {
                        bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);
                        _logger.LogInformation($"Resultado da verificação da senha: {isPasswordCorrect}");

                        if (isPasswordCorrect)
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, user.Username),
                                new Claim(ClaimTypes.Role, "Admin")
                            };
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                            _logger.LogInformation("Login bem-sucedido. Redirecionando para Home/Index.");
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            _logger.LogWarning("Senha incorreta.");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Erro ao verificar a senha: {ex.Message}");
                        ModelState.AddModelError(string.Empty, "Ocorreu um erro durante o login. Por favor, tente novamente.");
                    }
                }
                else
                {
                    _logger.LogWarning($"Usuário não encontrado: {model.Username}");
                }
                ModelState.AddModelError(string.Empty, "Tentativa de login inválida");
            }
            else
            {
                _logger.LogWarning("ModelState inválido");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}