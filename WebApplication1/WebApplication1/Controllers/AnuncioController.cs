using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize] // Garante que apenas usuários autenticados possam acessar este controlador
    public class AnuncioController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AnuncioController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Anuncio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Anuncio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdViewModel ad, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                var anuncio = new Anuncio
                {
                    Titulo = ad.Titulo,
                    Descricao = ad.Descricao,
                    ImagePath = null // Inicializa como nulo
                };

                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    anuncio.ImagePath = $"/uploads/{uniqueFileName}"; // Define o caminho da imagem
                }

                _context.Anuncios.Add(anuncio); // Adiciona o anúncio ao contexto
                await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
                return RedirectToAction("Index"); // Redireciona para a lista de anúncios
            }

            return View(ad);
        }

        // GET: Anuncio/Index
        public IActionResult Index()
        {
            var anuncios = _context.Anuncios.ToList(); // Obtém todos os anúncios do banco de dados
            return View(anuncios); // Passa a lista de anúncios para a visualização
        }
    }
}
