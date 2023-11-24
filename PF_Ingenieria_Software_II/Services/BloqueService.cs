using PF_Ingenieria_Software_II.Interfaces;
using PF_Ingenieria_Software_II.Models;

namespace PF_Ingenieria_Software_II.Services
{
    public class BloqueService : IBloqueService
    {
        private readonly PolideportivobdContext _context;

        public BloqueService(PolideportivobdContext context)
        {
            _context = context;
        }

        public Bloque FindBloque(int id)
        {
            var Bloque = (from Table in _context.Bloques
                          where Table.Id == id
                          select Table).FirstOrDefault();

            if (Bloque == null)
            {
                return new Bloque();
            }

            return Bloque;
        }
    }
}
