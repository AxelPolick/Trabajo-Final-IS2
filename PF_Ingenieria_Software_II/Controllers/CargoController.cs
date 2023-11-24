using Microsoft.AspNetCore.Mvc;
using PF_Ingenieria_Software_II.Interfaces;

namespace PF_Ingenieria_Software_II.Controllers
{
    public class CargoController : Controller
    {
        private readonly PolideportivobdContext _context;
        private readonly ICargoService _cargoService;

        public CargoController(PolideportivobdContext context, ICargoService cargoService)
        {
            _context = context;
            _cargoService = cargoService;   
        }

        public IActionResult ListarCargos()
        {
            var lista = (from Tabla in _context.Cargos
                         select Tabla).ToList();
            return View(lista);
        }

        public IActionResult Create()
        {
            ViewBag.CargoService = _cargoService;
            return View();
        }

        public async Task<ActionResult> CreateCargo(Cargo obj)
        {
            if (ModelState.IsValid)
            {
                _context.Cargos.Add(obj);
                await _context.SaveChangesAsync();
                return RedirectToAction("ListarCargos");
            }
            return View("Create", obj);
        }
    }
}
