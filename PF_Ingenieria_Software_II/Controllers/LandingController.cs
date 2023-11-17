using Microsoft.AspNetCore.Mvc;

namespace PF_Ingenieria_Software_II.Controllers
{
    public class LandingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
