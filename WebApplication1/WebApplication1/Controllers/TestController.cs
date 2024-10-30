using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var admins = _context.Set<Admin>().ToList(); // Obter todos os administradores
            return View(admins); // Retornar a view com a lista de administradores
        }

        // Outros métodos do controlador, se houver...
    }
}
