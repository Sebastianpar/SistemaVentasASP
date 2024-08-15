using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net;
using SistemaVenta.BLL.Interface;
using SistemaVenta.DAL.Interface;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Implementation
{
    public class UsuarioService : IUsauarioService
    {
        private readonly IGenericRepository<Usuario> _repository;
        private readonly IFireBaseService _fireBaseService;
        private readonly IUtilidadesService _utilidadesService;
        private readonly ICorreoService _correoService;

        public UsuarioService(IGenericRepository<Usuario> repository, IFireBaseService fireBaseService, IUtilidadesService utilidadesService, ICorreoService correoService)
        {
            _repository = repository;
            _fireBaseService = fireBaseService;
            _utilidadesService = utilidadesService;
            _correoService = correoService;
        }

        public async Task<List<Usuario>> Lista()
        {
            IQueryable<Usuario> query = await _repository.Consultar();
            return query.Include(r=>r.IdRolNavigation).ToList();

        }
        public async Task<Usuario> Create(Usuario entity, Stream Foto = null, string NombreFoto = "", string UrlPlantillaCorreo = "")
        {
            Usuario usuario_existe = await _repository.Obtener(u=> u.Correo == entity.Correo);

            if (usuario_existe != null)
                throw new TaskCanceledException("El correo ya existe");

            try
            {
                string clave_generada = _utilidadesService.GenerarClave();
                entity.Clave = _utilidadesService.ConvertirSha256(clave_generada);
                entity.NombreFoto = NombreFoto;

                if (Foto != null)
                {
                    string urlFoto = await _fireBaseService.SubirStorage(Foto, "carpeta_usuario", NombreFoto);
                    entity.UrlFoto = urlFoto;
                }

                Usuario usuario_creado = await _repository.Crear(entity);

                if(usuario_creado.IdUsuario == 0)
                    throw new TaskCanceledException("No se pudo crear el usuario");

                if (UrlPlantillaCorreo != "")
                {
                    UrlPlantillaCorreo = UrlPlantillaCorreo.Replace("[correo]", usuario_creado.Correo).Replace("[clave]", clave_generada);

                    string htmlCorreo = "";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UrlPlantillaCorreo);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream dataStream = response.GetResponseStream()) {
                            StreamReader readerStream = null;
                            if (response.CharacterSet == null)
                                readerStream = new StreamReader(dataStream);
                            else
                                readerStream = new StreamReader(dataStream, Encoding.GetEncoding(response.CharacterSet);
                            htmlCorreo = readerStream.ReadToEnd();
                            response.Close();
                            readerStream.Close();

                        }
                    }

                    if (htmlCorreo != "")
                        await _correoService.EnviarCorreo(usuario_creado.Correo, "Cuenta Creada", htmlCorreo);
                }

                IQueryable<Usuario> query = await _repository.Consultar(u => u.IdUsuario == usuario_creado.IdUsuario);
                usuario_creado = query.Include(r => r.IdRolNavigation).First();

                return usuario_creado;

            }
            catch (Exception ex) 
            {
                throw; 
            }
        }
        public async Task<Usuario> Edit(Usuario entity, Stream Foto = null, string NombreFoto = "")
        {
            Usuario usuario_existe = await _repository.Obtener(u => u.Correo == entity.Correo && u.IdUsuario != entity.IdUsuario);

            if (usuario_existe != null)
                throw new TaskCanceledException("El correo ya existe");
            try
            {
                IQueryable<Usuario> queryUsuario = await _repository.Consultar(u => u.IdUsuario == entity.IdUsuario);

                Usuario usuario_editar = queryUsuario.First();
                usuario_editar.Nombre = entity.Nombre;
                usuario_editar.Correo = entity.Correo;
                usuario_editar.Telefono = entity.Telefono;
                usuario_editar.IdRol = entity.IdRol;

                if (usuario_editar.NombreFoto == "")
                    usuario_editar.NombreFoto = NombreFoto;

                if(Foto != null)
                {
                    string urlFoto = await _fireBaseService.SubirStorage(Foto, "carpeta_usuario", NombreFoto);
                    usuario_editar.UrlFoto = urlFoto;
                }

                bool respuesta = await _repository.Editar(usuario_editar);

                if(!respuesta)
                    throw new TaskCanceledException("No se pudo modificar el usuario");

                Usuario usuario_editado = queryUsuario.Include(r => r.IdRolNavigation).First();

                return usuario_editado;
            }
            catch (Exception ex) {
                throw;
            }
            
        }
        public async Task<bool> Delete(int IdUsuario)
        {
            try
            {
                Usuario usuario_encontrado = await _repository.Obtener(u=> u.IdUsuario == IdUsuario);
                if (usuario_encontrado == null)
                    throw new TaskCanceledException("El usuario no existe");

                string nombreFoto = usuario_encontrado.NombreFoto;
                bool respuesta = await _repository.ELiminar(usuario_encontrado);

                if(respuesta)
                    await _fireBaseService.EliminarStorage("carpeta_usuario",nombreFoto);

                return true;
            }
            catch
            {
                throw;
            }
        }

        public Task<Usuario> GetByCredentials(string mail, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> GetById(int Id)
        {
            throw new NotImplementedException();
        }
        public Task<bool> SaveProfile(Usuario entity)
        {
            throw new NotImplementedException();
        }        
        public Task<bool> ChangePassword(int IdUsuario, string ClaveActual, string ClaveNueva)
        {
            throw new NotImplementedException();
        }
        public Task<bool> RestorePassword(string Correo, string UrlPlantillaCorreo)
        {
            throw new NotImplementedException();
        }

    }
}
