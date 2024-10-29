using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers  // O namespace do teu projeto
{
    public class LocationController : Controller
    {
        // GET: Location
        public IActionResult Index()
        {
            return View();
        }
    }
}
