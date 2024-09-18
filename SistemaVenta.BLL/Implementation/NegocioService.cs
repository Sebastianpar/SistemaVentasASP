using SistemaVenta.BLL.Interface;
using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.BLL.Interface;
using SistemaVenta.DAL.Interface;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Implementation
{
    public class NegocioService : INegocioService
    {
        private readonly IGenericRepository<Negocio> _repository;
        private readonly IFireBaseService _fireBaseService;

        public NegocioService(IGenericRepository<Negocio> repository, IFireBaseService fireBaseService )
        {
            _repository = repository;
            _fireBaseService = fireBaseService;
        }
        public async Task<Negocio> GuardarCambios(Negocio entidad, Stream Logo = null, string NombreLogo = "")
        {
            try
            {
                Negocio negocio_encontrado = await _repository.Obtener(n => n.IdNegocio == 1);
                
                negocio_encontrado.NumeroDocumento = entidad.NumeroDocumento;
                negocio_encontrado.Nombre = entidad.Nombre;
                negocio_encontrado.Correo = entidad.Correo;
                negocio_encontrado.Direccion = entidad.Direccion;
                negocio_encontrado.Telefono = entidad.Telefono;
                negocio_encontrado.PorcentajeImpuesto = entidad.PorcentajeImpuesto;
                negocio_encontrado.SimboloMoneda = entidad.SimboloMoneda;

                negocio_encontrado.NombreLogo = entidad.NombreLogo == "" ? NombreLogo : negocio_encontrado.NombreLogo;

                if (Logo != null)
                {
                    string urlLogo = await _fireBaseService.SubirStorage(Logo, "capeta_logo", negocio_encontrado.NombreLogo);
                    negocio_encontrado.UrlLogo = urlLogo;
                }
                await _repository.Editar(negocio_encontrado);
                return negocio_encontrado;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Negocio> Obtener()
        {
            try
            {
                Negocio negocio_encontrado = await _repository.Obtener(n => n.IdNegocio == 1);
                return negocio_encontrado;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
