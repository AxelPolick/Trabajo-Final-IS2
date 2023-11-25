using Microsoft.AspNetCore.Mvc;
using PF_Ingenieria_Software_II.Interfaces;
using PF_Ingenieria_Software_II.Models;

namespace PF_Ingenieria_Software_II.Controllers
{
    public class DisciplinaController : Controller
    {
        private readonly PolideportivobdContext _context;
        private readonly IDisciplinaService _disciplinaService;

        public DisciplinaController(PolideportivobdContext context, IDisciplinaService disciplinaService)
        {
            _context = context;
            _disciplinaService = disciplinaService;
        }

        public IActionResult ListarDisciplinas()
        {
            var listaDisciplinas = (from TablaDisciplina in _context.Disciplinas
                                    select TablaDisciplina).ToList();
            return View(listaDisciplinas);
        }

        public IActionResult Create() 
        {
            ViewBag.DisciplinaService = _disciplinaService;
            return View();
        }

        public async Task<ActionResult> CreateDisciplina(Disciplina obj)
        {
            if(ModelState.IsValid)
            {
                _context.Disciplinas.Add(obj);  
                await _context.SaveChangesAsync();
                return RedirectToAction("ListarDisciplinas");
            }
            return View("Create", obj);
        }
    }
}
