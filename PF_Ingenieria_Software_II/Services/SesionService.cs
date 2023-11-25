using PF_Ingenieria_Software_II.Interfaces;
using PF_Ingenieria_Software_II.Models;

namespace PF_Ingenieria_Software_II.Services
{
    public class SesionService : ISesionService
    {
        private readonly PolideportivobdContext _context;
        private readonly IUsuarioService _usuarioService;
        private readonly IAlumnoService _alumnoService;
        private readonly IAlumnoBloqueService _alumnoBloqueService;
        private readonly IBloqueService _bloqueService;
        private readonly IHorarioSesionService _horarioSesionService;
        public SesionService(PolideportivobdContext context, IUsuarioService usuarioService, IAlumnoService alumnoService, IAlumnoBloqueService alumnoBloqueService, IBloqueService bloqueService, IHorarioSesionService horarioSesionService)
        {
            _context = context;
            _usuarioService = usuarioService;
            _alumnoService = alumnoService;
            _alumnoBloqueService = alumnoBloqueService;
            _bloqueService = bloqueService;
            _horarioSesionService = horarioSesionService;   
        }

        public Sesion FindSesion(int idSesion)
        {
            var Sesion = (from Table in _context.Sesions
                         where Table.Id == idSesion
                         select Table).FirstOrDefault();

            if (Sesion == null)
            {
                return new Sesion();
            }

            return Sesion;
        }

        public List<Sesion> ObtenerSesiones()
        {
            var usuario = _usuarioService.ObtenerUsuarioActual();
            var alumno = _alumnoService.FindAlumno(usuario.Id);
            List<BloqueAlumno> list = _alumnoBloqueService.FindBloquesPorAlumno(alumno.Id);

            List<Bloque> listaBloques = new List<Bloque>();
            foreach (var i in list)
            {
                var bloque = _bloqueService.FindBloque(i.BloqueId);
                listaBloques.Add(bloque);
            }

            List<Horario> listaHorarios = new List<Horario>();
            foreach (var s in listaBloques)
            {
                Horario horario = new Horario()
                {
                    Id = s.HorarioId
                };
                listaHorarios.Add(horario);
            }

            List<HorarioSesion> listaHorariosPorSesion = new List<HorarioSesion>();
            foreach (var j in listaHorarios)
            {
                listaHorariosPorSesion.AddRange(_horarioSesionService.FindSesionesPorHorario(j.Id));
            }

            List<Sesion> listaSesionesPorUsuario = new List<Sesion>();
            foreach(var k in listaHorariosPorSesion)
            {
                var sesion = FindSesion(k.SesionId);
                listaSesionesPorUsuario.Add(sesion);
            }

            return listaSesionesPorUsuario;
        }
    }
}
