using Microsoft.AspNetCore.Mvc;

namespace SistemaVenta.WebApp.Controllers
{
    public class NegocioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
