using Microsoft.AspNetCore.Mvc;

public class SignupController : Controller
{
    
    public IActionResult Index()
    {
        return View();
    }

    
    [HttpPost]
    public IActionResult Index(string name, string email, string password, string confirm_password)
    {
        if (password != confirm_password)
        {
            ViewBag.Error = "Passwords do not match.";
            return View();
        }

        
        return RedirectToAction("Index", "Home");
    }
}
