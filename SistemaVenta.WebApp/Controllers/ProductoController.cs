using Microsoft.AspNetCore.Mvc;

namespace SistemaVenta.WebApp.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
