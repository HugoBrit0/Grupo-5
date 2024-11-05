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

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.AdminUsers.FirstOrDefaultAsync(u => u.Username == username);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, "Admin")
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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserProfile model, string password)
        {
            if (ModelState.IsValid)
            {
                if (await _context.UserProfiles.AnyAsync(u => u.Username == model.Username || u.Email == model.Email))
                {
                    ModelState.AddModelError("", "Username ou Email já estão em uso.");
                    return View(model);
                }

                model.DateJoined = DateTime.Now;

                // Crie um novo AdminUser
                var adminUser = new AdminUser
                {
                    Username = model.Username,
                    Email = model.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                    CreatedAt = DateTime.Now
                };

                _context.AdminUsers.Add(adminUser);
                _context.UserProfiles.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            return View(model);
        }
    }
}
