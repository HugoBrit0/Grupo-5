using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class AnuncioController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AnuncioController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Create()
        {
            ViewBag.EstadosCarro = new List<string> { "Novo", "Usado", "Seminovo" };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titulo,Descricao,EstadoCarro,Quilometragem,Telefone")] Anuncio anuncio, IFormFile imagem)
        {
            if (ModelState.IsValid)
            {
                if (imagem != null && imagem.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + imagem.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imagem.CopyToAsync(fileStream);
                    }
                    anuncio.ImagePath = "/uploads/" + uniqueFileName;
                }
                _context.Add(anuncio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.EstadosCarro = new List<string> { "Novo", "Usado", "Seminovo" };
            return View(anuncio);
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var anuncios = from a in _context.Anuncios
                           select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                anuncios = anuncios.Where(s => s.Titulo.Contains(searchString));
            }

            return View(await anuncios.ToListAsync());
        }

        public IActionResult Edit(int id)
        {
            var anuncio = _context.Anuncios.FirstOrDefault(a => a.Id == id);
            if (anuncio == null)
            {
                return NotFound();
            }
            return View(anuncio);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var anuncio = await _context.Anuncios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anuncio == null)
            {
                return NotFound();
            }
            return View(anuncio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anuncio = await _context.Anuncios.FindAsync(id);
            if (anuncio != null)
            {
                if (!string.IsNullOrEmpty(anuncio.ImagePath))
                {
                    var imagePath = Path.Combine(_environment.WebRootPath, anuncio.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                _context.Anuncios.Remove(anuncio);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
