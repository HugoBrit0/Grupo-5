using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using WebApplication1.Models; // Certifique-se de que este namespace corresponda ao local do AdViewModel



namespace WebApplication1.Controllers
{
    [Authorize]
    public class AdsController : Controller
    {

        private static List<AdViewModel> _ads = new List<AdViewModel>();
        private readonly IWebHostEnvironment _environment;


        public AdsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }


        public IActionResult Index()
        {
            return View(_ads);
        }


        public IActionResult Create()
        {
            return View();
        }

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

                    ad.ImagePath = $"/uploads/{uniqueFileName}";
                }

                _ads.Add(ad);
                return RedirectToAction("Index");
            }

            return View(ad);
        }
    }
}