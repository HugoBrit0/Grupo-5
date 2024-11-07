using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        //login
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Primeiro, verifique na tabela AdminUsers
            var adminUser = await _context.AdminUsers.FirstOrDefaultAsync(u => u.Username == username);

            if (adminUser != null && BCrypt.Net.BCrypt.Verify(password, adminUser.PasswordHash))
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, adminUser.Username),
            new Claim(ClaimTypes.Role, "Admin")
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            // Agora verifique na tabela UserProfiles
            var user = await _context.UserProfiles.FirstOrDefaultAsync(u => u.Username == username);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, "User")
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Nome de usuário ou senha inválidos");
            return View();
        }



        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        
        //Registo
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserProfile model, string password)
        {
            if (ModelState.IsValid)
            {
                // Verifica se o nome de usuário ou email já estão em uso
                if (await _context.UserProfiles.AnyAsync(u => u.Username == model.Username || u.Email == model.Email))
                {
                    ModelState.AddModelError("", "Nome de usuário ou email já estão em uso.");
                    return View(model);
                }

                // Cria o hash da senha
                model.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
                model.DateJoined = DateTime.Now;
                model.IsAdmin = false; // Define como usuário regular

                // Adiciona o novo usuário ao contexto
                _context.UserProfiles.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login", "Account"); // Redireciona para a página de login
            }

            return View(model); // Retorna a view com erros se houver
        }


    }
}
