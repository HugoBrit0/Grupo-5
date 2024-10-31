using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YourNamespace.Models;

public class CarsController : Controller
{
    private readonly IWebHostEnvironment _env;

  
    private static List<Car> Cars = new List<Car>
    {
        new Car { Id = 1, Name = "Carro 1", Description = "Descrição inicial do carro 1" },
        new Car { Id = 2, Name = "Carro 2", Description = "Descrição inicial do carro 2" },
        new Car { Id = 3, Name = "Carro 3", Description = "Descrição inicial do carro 3" }
        
    };

    public CarsController(IWebHostEnvironment env)
    {
        _env = env;
    }

    public IActionResult Details(int id)
    {
        var car = Cars.FirstOrDefault(c => c.Id == id);
        if (car == null) return NotFound();

        return View(car);
    }

    [HttpPost]
    public IActionResult UpdateDescription(int id, string description)
    {
        var car = Cars.FirstOrDefault(c => c.Id == id);
        if (car == null) return NotFound();

        car.Description = description;
        return RedirectToAction("Details", new { id }); 
    }

    [HttpPost]
    public IActionResult UploadImage(int id, IFormFile imageFile)
    {
        var car = Cars.FirstOrDefault(c => c.Id == id);
        if (car == null || imageFile == null) return RedirectToAction("Details", new { id });

        
        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
        Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            imageFile.CopyTo(fileStream);
        }

        car.ImageUrls.Add($"/uploads/{uniqueFileName}");
        return RedirectToAction("Details", new { id });
    }
}
