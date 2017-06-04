using CAEF.Models.Contexts;
using CAEF.Models.Entities.CAEF;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAEF.Repositories;

namespace CAEF.Services
{
    public class UsuarioServices
    {
        private CAEFContext _contextoCAEF;
        private UsuarioUABCContext _contextoUABC;
        private CRUDRepository<Usuario> _repositorioUsuario;
        public UsuarioServices(CAEFContext contextoCAEF, UsuarioUABCContext contextoUABC, UsuarioRepository repositorioUsuario)
        {
            _contextoCAEF = contextoCAEF;
            _contextoUABC = contextoUABC;
            _repositorioUsuario = repositorioUsuario;
        }

        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            return _repositorioUsuario.BuscarTodos();
            /*return _contextoCAEF.Usuarios
                .Include(u => u.Rol)
                .ToList();*/
        }

        public Usuario ObtenerUsuario(int id)
        {
            return _repositorioUsuario.BuscarID(id);
        }

        public Materia ObtenerMateria(int id)
        {
            return _contextoCAEF.Materias.Where(m => m.Id == id).FirstOrDefault();
        }

        public Carrera ObtenerCarrera(int id)
        {
            return _contextoCAEF.Carreras.Where(c => c.Id == id).FirstOrDefault();
        }

        public TipoExamen ObtenerTipoExamen(int id)
        {
            return _contextoCAEF.TiposExamen.Where(t => t.Id == id).FirstOrDefault();
        }

        public SubtipoExamen ObtenerSubtipoExamen(int id)
        {
            return _contextoCAEF.SubtiposExamen.Where(s => s.Id == id).FirstOrDefault();
        }

        public void AgregarUsuario(Usuario usuario)
        {
            // Al agregar usuario nuevo, solo se pide matrícula,
            // correo y rol, por lo tanto se tienen que extraer sus
            // nombres de la BD de UABC para poder mostrarlo en la lista
            // con toda la información correcta.
            var usuarioUABC = _contextoUABC.UsuariosUABC
                .Where(u => u.Email == usuario.Correo)
                .FirstOrDefault();

            usuario.Id = usuarioUABC.Matricula;
            usuario.Nombre = usuarioUABC.Nombre;
            usuario.ApellidoP = usuarioUABC.ApellidoP;
            usuario.ApellidoM = usuarioUABC.ApellidoM;            
            _repositorioUsuario.Agregar(usuario);
        }
        public void EditarUsuario(Usuario usuario)
        {
            var resultado = _contextoCAEF.Usuarios
                .Where(u => u.Correo == usuario.Correo)
                .FirstOrDefault();

            if (resultado != null)
            {
                resultado.RolId = usuario.RolId;
                _contextoCAEF.Usuarios.Update(resultado);
            }
        }
        public void BorrarUsuario(Usuario usuario)
        {
            var resultado = _contextoCAEF.Usuarios
                .Where(u => u.Correo == usuario.Correo)
                .FirstOrDefault();

            if (resultado != null) _contextoCAEF.Usuarios.Remove(resultado);
        }
        public bool UsuarioExiste(string Correo)
        {
            var resultado = _contextoUABC.Users
                .Where(u => u.Email == Correo)
                .FirstOrDefault();

            return resultado == null ? false : true;
        }
        public bool UsuarioDuplicado(string Correo)
        {
            var resultado = _contextoCAEF.Usuarios
                .Where(u => u.Correo == Correo)
                .FirstOrDefault();

            return resultado == null ? false : true;
        }

        public Usuario UsuarioAutenticado(string Username)
        {
            var Correo = Username + "@uabc.edu.mx";
            var Usuario = _contextoCAEF.Usuarios
                .Include(u => u.Rol)
                .Where(u => u.Correo == Correo)
                .FirstOrDefault();

            return Usuario;
        }
        public async Task<bool> GuardarCambios()
        {
            return (await _contextoCAEF.SaveChangesAsync()) > 0;
        }
    }
}
