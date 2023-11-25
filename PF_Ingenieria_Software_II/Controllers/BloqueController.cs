using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PF_Ingenieria_Software_II.Interfaces;
using PF_Ingenieria_Software_II.Services;

namespace PF_Ingenieria_Software_II.Controllers
{
    public class BloqueController : Controller
    {
        private readonly PolideportivobdContext _context;
        private readonly ITutorService _tutorService;
        private readonly IDisciplinaService _disciplinaService;
        private readonly IUsuarioService _usuarioService;
        private readonly IAlumnoService _alumnoService;
        private readonly IAlumnoBloqueService _alumnoBloqueService;
        private readonly IRolService _rolService;

        public BloqueController(PolideportivobdContext context, ITutorService tutorService, IDisciplinaService disciplinaService, IUsuarioService usuarioService, IAlumnoService alumnoService, IAlumnoBloqueService alumnoBloqueService, IRolService rolService)
        {
            _context = context;
            _tutorService = tutorService;
            _disciplinaService = disciplinaService;
            _usuarioService = usuarioService;
            _alumnoService = alumnoService;
            _alumnoBloqueService = alumnoBloqueService;
            _rolService = rolService;
        }

        public IActionResult ListarBloques()
        {
            var listaBloques = (from TablaBloque in _context.Bloques
                                select TablaBloque).ToList();
            ViewBag.TutorService = _tutorService;
            ViewBag.DisciplinaService = _disciplinaService;
            ViewBag.UserService = _usuarioService;
            return View(listaBloques);
        }

        public IActionResult Create()
        {
            BloqueViewModel obj = new BloqueViewModel()
            {
                Id = _context.Bloques.Count() + 1,
            };
            obj.Dias = new List<DayOfWeek>
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday
            };
            ViewBag.TutorService = _tutorService;
            ViewBag.UsuarioService = _usuarioService;
            ViewData["NombreDisciplina"] = new SelectList(_context.Disciplinas, "Nombre", "Nombre");
            ViewData["ApellidoTutores"] = (from TablaUsuario in _context.Usuarios
                                           where TablaUsuario.RolId == 2
                                           select new SelectListItem
                                           {
                                               Text = TablaUsuario.Nombres + " " + TablaUsuario.ApellidoPaterno,
                                               Value = TablaUsuario.ApellidoPaterno
                                           }).ToList();
            return View(obj);
        }

        public async Task<IActionResult> CreateBloque(BloqueViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var diasSeleccionados = obj.DiasSeleccionados;
                var horaInicio = obj.HoraInicio;
                var horaFin = obj.HoraFin;
                var fechaActual = obj.FechaInicio;
                var numeroSemanas = obj.NumeroSemanas;

                var nombreDisciplina = obj.NombreDisciplina;

                var horario = new Horario()
                {
                    Id = _context.Horarios.Count() + 1
                };
                _context.Horarios.Add(horario);

                int ultimoIdSesion = _context.Sesions.Count();
                int ultimoHorarioSesionId = _context.HorarioSesions.Count();

                for (int semana = 0; semana < numeroSemanas; semana++)
                {
                    foreach (var dia in diasSeleccionados)
                    {
                        ultimoIdSesion++;
                        ultimoHorarioSesionId++;

                        // Calcula la fecha de inicio sumando la diferencia de días y semanas
                        DateTime fechaInicioSesion = fechaActual.AddDays(semana * 7 + (int)dia - (int)fechaActual.DayOfWeek).Date + horaInicio;

                        // Calcula la fecha de fin sumando la diferencia de días y semanas y estableciendo la hora de fin
                        DateTime fechaFinSesion = fechaActual.AddDays(semana * 7 + (int)dia - (int)fechaActual.DayOfWeek).Date + horaFin;

                        // Crea el objeto Sesion y almacénalo en la base de datos
                        var sesion = new Sesion()
                        {
                            Id = ultimoIdSesion,
                            Titulo = "Sesión de " + dia.ToString() + " - " + nombreDisciplina,
                            HoraInicio = fechaInicioSesion,
                            HoraFin = fechaFinSesion
                        };
                        _context.Sesions.Add(sesion);

                        var horarioSesion = new HorarioSesion()
                        {
                            HorarioSesionId = ultimoHorarioSesionId,
                            HorarioId = horario.Id,
                            SesionId = sesion.Id
                        };
                        _context.HorarioSesions.Add(horarioSesion);

                    }
                }

                var bloque = new Bloque()
                {
                    Id = _context.Bloques.Count() + 1,
                    Nombre = $"#{_context.Bloques.Count() + 1}_{obj.NombreDisciplina}",
                    DisciplinaId = _disciplinaService.GetDisciplinaId(obj.NombreDisciplina),
                    TutorId = _tutorService.GetTutorId(_usuarioService.GetUsuarioId(obj.ApellidoTutor)),
                    HorarioId = horario.Id,
                    Ubicacion = obj.Ubicacion,
                    Precio = obj.Precio
                };
                _context.Bloques.Add(bloque);

                await _context.SaveChangesAsync();
                return RedirectToAction("ListarBloques");
            }

            ViewBag.TutorService = _tutorService;
            ViewBag.UsuarioService = _usuarioService;
            ViewData["NombreDisciplina"] = new SelectList(_context.Disciplinas, "Nombre", "Nombre");
            ViewData["ApellidoTutores"] = (from TablaUsuario in _context.Usuarios
                                           where TablaUsuario.RolId == 2
                                           select TablaUsuario.ApellidoPaterno).ToList();
            obj.Dias = new List<DayOfWeek>
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday
            };
            return View("Create", obj);
        }

        public IActionResult CursoPorUsuario()
        {
            int id = _usuarioService.ObtenerUsuarioActual().Id;
            var alumno = _alumnoService.FindAlumno(id);
            var lstAlumnoBloques = _alumnoBloqueService.FindBloquesPorAlumno(alumno.Id);
            var lstBloque = _alumnoBloqueService.GetBloques(lstAlumnoBloques);

            ViewBag.TutorService = _tutorService;
            ViewBag.DisciplinaService = _disciplinaService;
            ViewBag.UserService = _usuarioService;
            ViewBag.AlumnoBloqueService = _alumnoBloqueService;
            return View(lstBloque);
        }

        public IActionResult Dashboard() 
        {
            int id = _usuarioService.ObtenerUsuarioActual().Id;
            var alumno = _alumnoService.FindAlumno(id);
            var lstAlumnoBloques = _alumnoBloqueService.FindBloquesPorAlumno(alumno.Id);
            var lstBloque = _alumnoBloqueService.GetBloques(lstAlumnoBloques);

            ViewBag.TutorService = _tutorService;
            ViewBag.DisciplinaService = _disciplinaService;
            ViewBag.UserService = _usuarioService;
            ViewBag.AlumnoBloqueService = _alumnoBloqueService;
            ViewBag.RolService = _rolService;
            return View(lstBloque);
        }
    }
}
