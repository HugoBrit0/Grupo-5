using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System;

public class AdsController : Controller
{
    private readonly IWebHostEnvironment _environment;

    public AdsController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    // GET: /Ads
    public IActionResult Index()
    {
        return View();
    }

    // GET: /Ads/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Ads/Create
    [HttpPost]
    public async Task<IActionResult> Create(IFormFile imageFile)
    {
        if (imageFile != null && imageFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            ViewBag.ImagePath = $"/images/{fileName}";
        }

        return View();
    }
}
