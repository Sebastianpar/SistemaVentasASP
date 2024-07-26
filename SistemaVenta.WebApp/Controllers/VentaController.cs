using Microsoft.AspNetCore.Mvc;

namespace SistemaVenta.WebApp.Controllers
{
    public class VentaController : Controller
    {
        public IActionResult NuevaVenta()
        {
            return View();
        }
        public IActionResult HistorialVenta()
        {
            return View();
        }
    }
}
