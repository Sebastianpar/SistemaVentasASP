using Microsoft.AspNetCore.Mvc;

namespace SistemaVenta.WebApp.Controllers
{
    public class ReporteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
