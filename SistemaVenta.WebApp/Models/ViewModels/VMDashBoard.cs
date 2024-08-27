namespace SistemaVenta.WebApp.Models.ViewModels
{
    public class VMDashBoard
    {
        public int TotalVentas { get; set; }
        public string? TotalIngresos { get; set; }
        public int TotalProductos { get; set; }
        public int TotalCategorias { get; set; }
        public List<VMVentasSemana> VentasUltimaSEmana { get; set; }
        public List<VMProductosSemana> ProductosTopUltimaSemana { get; set; }
    }
}
