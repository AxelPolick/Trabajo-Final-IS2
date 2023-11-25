using Microsoft.AspNetCore.Mvc;
using NuGet.Versioning;
using PF_Ingenieria_Software_II.Interfaces;

namespace PF_Ingenieria_Software_II.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiSesionController : ControllerBase
    {
        private readonly ISesionService _sesionService;

        public ApiSesionController(ISesionService sesionService)
        { 
            _sesionService = sesionService;
        }


        // GET: api/<ApiSesionController>
        [HttpGet]
        public List<EventoViewModel> ObtenerSesionesPorUsuario()
        {
            List<EventoViewModel> lstEvent = new List<EventoViewModel>();
            var sesiones = _sesionService.ObtenerSesiones();
            foreach (var item in sesiones) 
            { 
                EventoViewModel obj = new EventoViewModel();
                obj.id = item.Id;
                obj.title = item.Titulo;
                obj.start = item.HoraInicio?.ToString("yyyy-MM-dd HH:mm:ss");
				obj.end = item.HoraFin?.ToString("yyyy-MM-dd HH:mm:ss");
                lstEvent.Add(obj);
			}
            return lstEvent;
        }

        
    }
}
