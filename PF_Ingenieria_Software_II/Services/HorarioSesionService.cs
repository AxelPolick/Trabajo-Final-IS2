using PF_Ingenieria_Software_II.Interfaces;
using PF_Ingenieria_Software_II.Models;

namespace PF_Ingenieria_Software_II.Services
{
    public class HorarioSesionService : IHorarioSesionService
    {
        private readonly PolideportivobdContext _context;

        public HorarioSesionService(PolideportivobdContext context)
        {
            _context = context;
        }

        public List<HorarioSesion> FindSesionesPorHorario(int horarioId)
        {
            var listaSesiones = (from Tabla in _context.HorarioSesions
                                where Tabla.HorarioId == horarioId
                                select Tabla).ToList();
            return listaSesiones;
        }

        public int GetHorarioId(int sesionId)
        {
            var horario = (from Tabla in _context.HorarioSesions
                                 where Tabla.SesionId == sesionId
                                 select Tabla).FirstOrDefault();

            if (horario != null)
            {
                return horario.HorarioId;
            }
            else
            {
                return 1; // Temporal - Hasta arreglar bug (MOD Z)
            }
        }
    }
}
