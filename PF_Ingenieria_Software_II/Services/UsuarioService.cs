using PF_Ingenieria_Software_II.Interfaces;
using System.Security.Claims;

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
    }
}
