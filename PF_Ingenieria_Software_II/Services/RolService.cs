using PF_Ingenieria_Software_II.Interfaces;

namespace PF_Ingenieria_Software_II.Services
{
    public class RolService : IRolService
    {
        private readonly PolideportivobdContext _context;

        public RolService(PolideportivobdContext context)
        {
            _context = context;
        }

        public Rol FindRol(int id)
        {
            var Rol = (from RolTable in _context.Rols
                       where RolTable.Id == id
                       select RolTable).Single();

            return Rol;
        }
    }
}
