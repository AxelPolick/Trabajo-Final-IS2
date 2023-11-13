using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Microsoft.AspNetCore.Authorization;

using PF_Ingenieria_Software_II.Models;
using PF_Ingenieria_Software_II.Services;
using PF_Ingenieria_Software_II.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using PF_Ingenieria_Software_II.Models.Paypal_Order;
using PF_Ingenieria_Software_II.Models.Paypal_Transaction;

namespace PF_Ingenieria_Software_II.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRolService _rolService;
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IRolService rolService, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _rolService = rolService;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "Alumno,Tutor,Administrador")]
        public IActionResult Index(Usuario usuario)
        {
            ViewBag.RolService = _rolService;
            return View(usuario);
        }

        [Authorize(Roles = "Alumno,Administrador")]
        public IActionResult SiteMatricula()
        {
            return View();
        }

        [Authorize(Roles = "Alumno,Tutor,Administrador")]
        public IActionResult Cursos()
        {
            return View();
        }

        [Authorize(Roles = "Alumno,Tutor,Administrador")]
        public IActionResult Horario()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Disciplina()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Bloque()
        {
            return View();
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
                    return RedirectToAction(nameof(Index));
                }

                decimal amount = model.Amount;
                string returnUrl = "http://localhost:5009/Home/Succes";
                string cancelUrl = "http://localhost:5009/Usuario/Login";

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

            return RedirectToAction(nameof(Index));
        }
        //------------------------------------------------------

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}