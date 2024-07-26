using Microsoft.AspNetCore.Mvc;

namespace SistemaVenta.WebApp.Controllers
{
    public class CategoriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
