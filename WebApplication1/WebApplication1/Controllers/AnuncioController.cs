using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AnuncioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnuncioController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titulo,Descricao,EstadoCarro,Quilometragem,Telefone,ImagePath")] Anuncio anuncio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anuncio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(anuncio);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Anuncios.ToListAsync());
        }
    }
}
