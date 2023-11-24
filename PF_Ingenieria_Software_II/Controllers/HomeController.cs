using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Microsoft.AspNetCore.Authorization;

using PF_Ingenieria_Software_II.Models;
using PF_Ingenieria_Software_II.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Security.Claims;
using PF_Ingenieria_Software_II.Services;

namespace PF_Ingenieria_Software_II.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRolService _rolService;
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuarioService _usuarioService;

        public HomeController(ILogger<HomeController> logger, IRolService rolService, IUnitOfWork unitOfWork, IUsuarioService usuarioService)
        {
            _logger = logger;
            _rolService = rolService;
            _unitOfWork = unitOfWork;
            _usuarioService = usuarioService;   
        }

        [Authorize(Roles = "Alumno,Tutor,Administrador")]
        public IActionResult Index()
        {
            return RedirectToAction("Dashboard", "Bloque");
        }

        [Authorize(Roles = "Alumno,Administrador")]
        public IActionResult SiteMatricula(Bloque bloque)
        {
            ViewData["Bloque"] = bloque;
            return View();
        }

        [Authorize(Roles = "Alumno,Tutor,Administrador")]
        public IActionResult Cursos()
        {
            return RedirectToAction("CursoPorUsuario", "Bloque");
        }

        [Authorize(Roles = "Alumno,Administrador")]
        public IActionResult ListarHorario()
        {
            return RedirectToAction("VisualizarHorario", "Horario");
        }


        [Authorize(Roles = "Administrador")]
        public IActionResult VisualizarDisiplinas()
        {
            return RedirectToAction("ListarDisciplinas", "Disciplina");
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult RegistrarDisciplina()
        {
            return RedirectToAction("Create", "Disciplina");
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult VisualizarBloques()
        {
            return RedirectToAction("ListarBloques", "Bloque");
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult RegistrarBloque()
        {
            return RedirectToAction("Create", "Bloque");
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult VisualizarTutores()
        {
            return RedirectToAction("ListarTutores", "Tutor");
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult RegistrarTutor()
        {
            return RedirectToAction("Create", "Tutor");
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult VisualizarCargos()
        {
            return RedirectToAction("ListarCargos", "Cargo");
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult RegistrarCargo()
        {
            return RedirectToAction("Create", "Cargo");
        }

        //-------------------------------------------------------

        public IActionResult Succes(string paymentId, string token, string PayerId)
        {
            ViewData["PaymentId"] = paymentId;
            ViewData["token"] = token;
            ViewData["payerId"] = PayerId;

            return View();
        }

        public async Task<IActionResult> PayUsingCard(PaymentViewModel model)
        {
            try 
            { 
                if(model.Amount == 0) 
                {
                    TempData["error"] = "Monto no valido...";
                    return RedirectToAction(nameof(SiteMatricula));
                }

                decimal amount = model.Amount;
                string returnUrl = "http://localhost:5009/Home/Succes";
                string cancelUrl = "http://localhost:5009/Home/SiteMatricula";

                var createdPayment = await _unitOfWork.PaypalServices.CreateOrderAysnc(amount, returnUrl, cancelUrl);

                string approvalUrl = createdPayment.links.FirstOrDefault(x => x.rel.ToLower() == "approval_url")?.href;

                if (!string.IsNullOrEmpty(approvalUrl))
                {
                    return Redirect(approvalUrl);
                }
                else
                {
                    TempData["error"] = "Fallo el intento de pago...";
                }
            }
            catch (Exception ex) 
            {
                TempData["error"] = ex.Message;
            }

            return RedirectToAction(nameof(SiteMatricula));
        }
        //------------------------------------------------------

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}