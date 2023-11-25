using Microsoft.EntityFrameworkCore;
using PF_Ingenieria_Software_II.Interfaces;
using PF_Ingenieria_Software_II.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PF_Ingenieria_Software_II.Services
{
    public class TutorService: ITutorService
    {
        private readonly PolideportivobdContext _context;

        public TutorService(PolideportivobdContext context)
        {
            _context = context;
        }

        public Tutor FindTutor(int idUsuario)
        {
            var Tutor = (from Table in _context.Tutors
                         where Table.UsuarioId == idUsuario
                         select Table).FirstOrDefault();

            if (Tutor == null)
            {
                return new Tutor();
            }

            return Tutor;
        }

        public Tutor FindTutorTutorId(int idTutor)
        {
            var Tutor = (from Table in _context.Tutors
                         where Table.Id == idTutor
                         select Table).FirstOrDefault();

            if (Tutor == null)
            {
                return new Tutor();
            }

            return Tutor;
        }

        public int GetTutorId(int idUsuario)
        {
            var tutor = (from Tabla in _context.Tutors
                         where Tabla.UsuarioId == idUsuario
                         select Tabla).FirstOrDefault();

            if (tutor == null)
            {
                return 1;
            }

            return tutor.Id;
        }

        public int Size()
        {
            return _context.Tutors.Count() + 1;
        }
    }
}
