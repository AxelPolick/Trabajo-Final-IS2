using Microsoft.AspNetCore.Mvc;
using PF_Ingenieria_Software_II.Models;

public class HorarioController : Controller
{
    private readonly PolideportivobdContext _context;

    public HorarioController(PolideportivobdContext context)
    {
        _context = context;
    }

    public IActionResult Horario()
    {
        return View();
    }

    public IActionResult SeleccionarHorario()
    {
        List<Sesion> sesiones = _context.Sesions.ToList();

        return View(sesiones);
    }

    [HttpPost]
    public IActionResult SeleccionarSesion(int idSesion)
    {
        return RedirectToAction("AlgunaAccion", "AlgunaVista");
    }
}
