using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Authorize] // Exige autenticação para todo o controlador
    public class LocationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
