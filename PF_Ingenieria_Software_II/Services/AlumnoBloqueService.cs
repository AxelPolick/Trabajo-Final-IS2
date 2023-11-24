using PayPal.Api;
using PF_Ingenieria_Software_II.Interfaces;
using PF_Ingenieria_Software_II.Models;

namespace PF_Ingenieria_Software_II.Services
{
    public class AlumnoBloqueService : IAlumnoBloqueService
    {
        private readonly PolideportivobdContext _context;
        private readonly IAlumnoService _alumnoService;
        private readonly IBloqueService _bioqueService;

        public AlumnoBloqueService(PolideportivobdContext context, IAlumnoService alumnoService, IBloqueService bioqueService)
        {
            _context = context;
            _alumnoService = alumnoService;
            _bioqueService = bioqueService; 
        }

        public List<BloqueAlumno> FindAlumnosPorBloque(int idBloque)
        {
            var listaBloques = (from Tabla in _context.BloqueAlumnos
                                where Tabla.BloqueId == idBloque
                                select Tabla).ToList();
            return listaBloques;
        }

        public List<BloqueAlumno> FindBloquesPorAlumno(int idAlumno)
        {
            var listaBloques = (from Tabla in _context.BloqueAlumnos
                                    where Tabla.AlumnoId == idAlumno
                                    select Tabla).ToList();
            return listaBloques;
        }

        public List<Alumno> GetAlumnos(List<BloqueAlumno> lstBloqueAlumno)
        {
            List<Alumno> listaAlumno = new List<Alumno>();
            foreach (var k in lstBloqueAlumno)
            {
                var Alumno = _alumnoService.FindAlumno(k.AlumnoId);
                listaAlumno.Add(Alumno);
            }
            return listaAlumno;
        }

        public List<Bloque> GetBloques(List<BloqueAlumno> lstBloqueAlumno)
        {
            List<Bloque> listaBloque = new List<Bloque>();
            foreach (var k in lstBloqueAlumno)
            {
                var Bloque = _bioqueService.FindBloque(k.BloqueId);
                listaBloque.Add(Bloque);
            }
            return listaBloque;
        }
    }
}
