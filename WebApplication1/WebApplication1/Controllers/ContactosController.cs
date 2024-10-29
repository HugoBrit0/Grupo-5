using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ContactosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnviarMensagem(string nome, string email, string mensagem)
        {
            // Lógica para processar a mensagem (exemplo: enviar email ou guardar no banco de dados)
            TempData["MensagemEnviada"] = "A sua mensagem foi enviada com sucesso!";
            return RedirectToAction("Index");
        }
    }
}
