using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interface
{
    public interface IUtilidadesService
    {
        string GenerarClave();
        string ConvertirSha256(string texto);
    }
}
