using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interface
{
    public interface IUsauarioService
    {
        Task<List<Usuario>> Lista();
        Task<Usuario> Create(Usuario entity, Stream Foto = null, string NombreFoto="",string UrlPlantilla="");
        Task<Usuario> Edit(Usuario entity, Stream Foto = null, string NombreFoto = "");
        Task<bool> Delete(int IdUsuario);
        Task<Usuario> GetByCredentials(string mail, string password);
        Task<Usuario> GetById(int Id);
        Task<bool> SaveProfile(Usuario entity);
        Task<bool> ChangePassword(int IdUsuario, string ClaveActual, string ClaveNueva);
        Task<bool> RestorePassword(string Correo, string UrlPlantillaCorreo);
    }
}
