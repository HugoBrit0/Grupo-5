using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Authorize] 
    public class ContactosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnviarMensagem(string nome, string email, string mensagem)
        {
           
            TempData["MensagemEnviada"] = "A sua mensagem foi enviada com sucesso!";
            return RedirectToAction("Index");
        }
    }
}