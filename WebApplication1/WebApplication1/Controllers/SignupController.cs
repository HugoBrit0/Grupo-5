using Microsoft.AspNetCore.Mvc;

public class SignupController : Controller
{
    // GET: Signup
    public IActionResult Index()
    {
        return View();
    }

    // POST: Signup
    [HttpPost]
    public IActionResult Index(string name, string email, string password, string confirm_password)
    {
        if (password != confirm_password)
        {
            ViewBag.Error = "Passwords do not match.";
            return View();
        }

        // Aqui poderias processar o registo (sem base de dados por enquanto)
        // Por exemplo, criar uma sessão ou apenas redirecionar para uma página

        // Redireciona para a página inicial (ou outra página que queiras)
        return RedirectToAction("Index", "Home");
    }
}
