using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdsController : Controller
    {
        private static List<AdViewModel> _ads = new List<AdViewModel>();
        private readonly IWebHostEnvironment _environment;

        public AdsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        // GET: /Ads
        public IActionResult Index()
        {
            return View(_ads); // Passa a lista de anúncios para a view
        }

        // GET: /Ads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Ads/Create
        [HttpPost]
        public IActionResult Create(AdViewModel ad, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
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
                        imageFile.CopyTo(stream);
                    }

                    ad.ImagePath = $"/uploads/{uniqueFileName}"; // Caminho relativo da imagem
                }

                _ads.Add(ad);
                return RedirectToAction("Index");
            }

            return View(ad);
        }
    }
}
