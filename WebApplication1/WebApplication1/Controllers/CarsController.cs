// Caminho: Controllers/CarsController.cs
using Microsoft.AspNetCore.Mvc;
using YourNamespace.Models;
using System.Collections.Generic;

namespace YourNamespace.Controllers
{
    public class CarsController : Controller
    {
        // Simulação de dados dos carros
        private List<Car> cars = new List<Car>
        {
            new Car { Id = 1, Name = "Carro 1", Description = "Descrição breve do carro 1.", MainImageUrl = "/images/car1.jpg", AdditionalImageUrl = "/images/car1_additional.jpg" },
            new Car { Id = 2, Name = "Carro 2", Description = "Descrição breve do carro 2.", MainImageUrl = "/images/car2.jpg", AdditionalImageUrl = "/images/car2_additional.jpg" },
            new Car { Id = 3, Name = "Carro 3", Description = "Descrição breve do carro 3.", MainImageUrl = "/images/car3.jpg", AdditionalImageUrl = "/images/car3_additional.jpg" }
        };

        public IActionResult Index()
        {
            return View(cars);
        }

        public IActionResult Details(int id)
        {
            var car = cars.Find(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }
    }
}
