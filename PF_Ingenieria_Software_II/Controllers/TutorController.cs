using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using PF_Ingenieria_Software_II.Interfaces;
using PF_Ingenieria_Software_II.Services;

namespace PF_Ingenieria_Software_II.Controllers
{
    public class TutorController : Controller
    {
        private readonly PolideportivobdContext _context;
        private readonly ITutorService _tutorService;
        private readonly IDisciplinaService _disciplinaService;
        private readonly IDisciplinaTutorService _disciplinaTutorService;
        private readonly IContratoService _contratoService;
        private readonly ICargoService _cargoService;
        private readonly IUsuarioService _usuarioService;
        private readonly IDocumentTypeService _documentTypeService;


        public TutorController(PolideportivobdContext context, ITutorService tutorService, IDisciplinaService disciplinaService, IDisciplinaTutorService disciplinaTutorService, IContratoService contratoService, ICargoService cargoService, IUsuarioService usuarioService, IDocumentTypeService documentTypeService)
        {
            _context = context;
            _tutorService = tutorService;
            _disciplinaService = disciplinaService;
            _disciplinaTutorService = disciplinaTutorService;
            _contratoService = contratoService;
            _cargoService = cargoService;
            _usuarioService = usuarioService;
            _documentTypeService = documentTypeService;
        }

        public IActionResult ListarTutores()
        {
            var listaTutores = (from TablaTutor in _context.Usuarios
                                where TablaTutor.RolId == 2 //Id 2 es para tutor
                                select TablaTutor).ToList();
            ViewBag.TutorService = _tutorService;
            ViewBag.DisciplinaService = _disciplinaService;
            ViewBag.TutorDisciplina = _disciplinaTutorService;
            ViewBag.ContratoService = _contratoService;
            ViewBag.CargoService = _cargoService;
            return View(listaTutores);
        }

        public IActionResult Create()
        {
            TutorViewModel obj = new TutorViewModel()
            {
                Disciplinas = _context.Disciplinas.ToList(),
                Id = _tutorService.Size()+100,
                UsuarioId = _usuarioService.Size()
            };
            ViewBag.TutorService = _tutorService;
            ViewBag.UsuarioService = _usuarioService;
            ViewData["NombreDocumento"] = new SelectList(_context.TipoDocumentos, "Nombre", "Nombre");
            ViewData["NombreCargo"] = new SelectList(_context.Cargos, "Nombre", "Nombre");
            return View(obj);
        }

        public async Task<ActionResult> CreateTutor(TutorViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario()
                {
                    Id = _usuarioService.Size(),
                    Nombres = obj.Nombres,
                    ApellidoPaterno = obj.ApellidoPaterno,
                    ApellidoMaterno = obj.ApellidoMaterno,
                    Edad = obj.Edad,
                    Direccion = obj.Direccion,
                    Telefono = obj.Telefono,
                    Correo = obj.Correo,
                    Contrasena = UtilityService.Encrypt(obj.Contrasena),
                    RolId = 2, // Se crea como Tutor por defecto
                    EstadoId = 1, // Activo por defecto
                    DocumentoId = _documentTypeService.GetDocumentId(obj.NombreDocumento),
                    NroDocumento = obj.NroDocumento,
                    NombreUsuario = $"{obj.Nombres.Substring(0, 1).ToLower()}.{obj.ApellidoPaterno.ToLower()}_{obj.NombreCargo.ToLower()}"
                };
                _context.Usuarios.Add(usuario);

                var contrato = new Contrato()
                {
                    Id = _contratoService.Size(),
                    FechaInicio = obj.FechaInicio,
                    FechaFin = obj.FechaFin,
                    Salario = obj.Salario,
                    CargoId = _cargoService.GetCargoId(obj.NombreCargo)
                };
                _context.Contratos.Add(contrato);

                var tutor = new Tutor()
                {
                    Id = obj.Id,
                    UsuarioId = usuario.Id,
                    ContratoId = contrato.Id
                };
                _context.Tutors.Add(tutor);

                var maxId = _context.TutorDisciplinas.Max(td => (int?)td.TutorDisciplinaId) ?? 0;

                foreach (var item in obj.DisciplinasSeleccionadas)
                {
                    var tutorDisciplina = new TutorDisciplina()
                    {
                        TutorDisciplinaId = maxId + 1,
                        DisciplinaId = item,
                        TutorId = obj.Id
                    };
                    _context.TutorDisciplinas.Add(tutorDisciplina);
                    maxId++;
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("ListarTutores");
            }

            ViewData["NombreDocumento"] = new SelectList(_context.TipoDocumentos, "Nombre", "Nombre", obj.NombreDocumento);
            ViewData["NombreCargo"] = new SelectList(_context.Cargos, "Nombre", "Nombre", obj.NombreCargo);
            obj.Disciplinas = _context.Disciplinas.ToList();
            return View("Create", obj);
        }
    }
}
