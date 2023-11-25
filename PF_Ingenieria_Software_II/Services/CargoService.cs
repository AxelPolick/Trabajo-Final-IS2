using PF_Ingenieria_Software_II.Interfaces;
using PF_Ingenieria_Software_II.Models;

namespace PF_Ingenieria_Software_II.Services
{
    public class CargoService:ICargoService
    {
        private readonly PolideportivobdContext _context;

        public CargoService(PolideportivobdContext context)
        {
            _context = context;
        }

        public Cargo FindCargo(int id)
        {
            var cargo = (from Tabla in _context.Cargos
                            where Tabla.Id == id
                            select Tabla).FirstOrDefault();

            if (cargo == null)
            {
                return new Cargo();
            }

            return cargo;
        }

        public int Size()
        {
            return _context.Cargos.Count() + 1;
        }

        public int GetCargoId(string nombre)
        {
            var cargo = (from Table in _context.Cargos
                            where Table.Nombre == nombre
                            select Table).FirstOrDefault();

            if (cargo == null)
            {
                return 1;
            }

            return cargo.Id;
        }
    }
}
