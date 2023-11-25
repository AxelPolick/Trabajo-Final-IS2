using PF_Ingenieria_Software_II.Interfaces;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PF_Ingenieria_Software_II.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PolideportivobdContext _context;

        public UsuarioService(IHttpContextAccessor httpContextAccessor, PolideportivobdContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public Usuario ObtenerUsuarioActual()
        {
            // Obtén el ID del usuario desde las claims
            int idUsuario = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // Busca y devuelve el objeto Usuario basado en el ID
            return _context.Usuarios.FirstOrDefault(u => u.Id == idUsuario);
        }

        public Usuario FindUsuario(int id)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            
            if(user == null) 
            {
                return new Usuario();
            }

            return user;
        }

        public int Size()
        {
            return _context.Usuarios.Count() + 1;
        }

        public int GetUsuarioId(string apellidoPaterno)
        {
            var usuario = (from Tabla in _context.Usuarios
                           where Tabla.ApellidoPaterno == apellidoPaterno
                           select Tabla).FirstOrDefault();

            if (usuario == null)
            {
                return 1;
            }

            return usuario.Id;
        }
    }
}
