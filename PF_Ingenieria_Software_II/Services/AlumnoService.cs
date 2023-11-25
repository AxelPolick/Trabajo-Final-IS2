using Microsoft.EntityFrameworkCore;
using PF_Ingenieria_Software_II.Interfaces;
using PF_Ingenieria_Software_II.Models;

namespace PF_Ingenieria_Software_II.Services
{
    public class AlumnoService : IAlumnoService
    {
        private readonly PolideportivobdContext _context;

        public AlumnoService(PolideportivobdContext context)
        {
            _context = context;
        }

        public Alumno FindAlumno(int usuarioId)
        {
            var Alumno = (from Table in _context.Alumnos
                         where Table.UsuarioId == usuarioId
                         select Table).FirstOrDefault();

            if (Alumno == null)
            {
                return new Alumno();
            }

            return Alumno;
        }
    }
}
