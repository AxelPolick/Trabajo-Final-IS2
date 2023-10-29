using Microsoft.AspNetCore.Mvc;
using PF_Ingenieria_Software_II.Models;
using System.Diagnostics;

using Microsoft.AspNetCore.Authorization;

namespace PF_Ingenieria_Software_II.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Alumno, Administrador")]
        public IActionResult Matricula()
        {
            return View();
        }

        [Authorize(Roles = "Alumno, Administrador")]
        public IActionResult Cursos()
        {
            return View();
        }

        [Authorize(Roles = "Alumno, Administrador")]
        public IActionResult Horario()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}