using Microsoft.AspNetCore.Mvc;
using PF_Ingenieria_Software_II.Interfaces;
using PF_Ingenieria_Software_II.Models;

public class HorarioController : Controller
{
    private readonly PolideportivobdContext _context;
    private readonly IHorarioSesionService _horarioSesionService;
    private readonly IBloqueService _bloqueService;
    private readonly IUsuarioService _usuarioService;
    private readonly IAlumnoService _alumnoService;

    public HorarioController(PolideportivobdContext context, IHorarioSesionService horarioSesionService, IBloqueService bloqueService, IUsuarioService usuarioService, IAlumnoService alumnoService)
    {
        _context = context;
        _horarioSesionService = horarioSesionService;
        _bloqueService = bloqueService;
        _usuarioService = usuarioService;
        _alumnoService = alumnoService; 
    }

    public IActionResult VisualizarHorario()
    {
        return View();
    }

    public IActionResult SeleccionarHorario()
    {
        List<Sesion> sesiones = _context.Sesions.ToList();

        return View(sesiones);
    }

    public IActionResult SeleccionarBloque()
    {
        List<Bloque> bloques = _context.Bloques.ToList();

        return View(bloques);
    }

    [HttpPost]
    public async Task<IActionResult> SeleccionarSesion(int id)
    {
        int idBloque = _horarioSesionService.GetHorarioId(id);
        var bloque = _bloqueService.FindBloque(idBloque);
        var idUsuario = _usuarioService.ObtenerUsuarioActual().Id;
        var alumnoActual = _alumnoService.FindAlumno(idUsuario);
        var obj = new BloqueAlumno()
        {
            BloqueAlumnoId = _context.BloqueAlumnos.Count() + 1,
            BloqueId = bloque.Id,
            AlumnoId = alumnoActual.Id
        };
        _context.BloqueAlumnos.Add(obj);
        await _context.SaveChangesAsync();
        return RedirectToAction("SiteMatricula", "Home", bloque);
    }
}
