using grole.src.Entidades;
using grole.src.Logica;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;

namespace grole.Controllers
{
    [Models.ChecaAutorizacion("68")]
    public class ConfiguracionesController : Controller
    {
        private ConfiguracionesLogica _ConfiguracionesLogica;

        public ConfiguracionesController(ConfiguracionesLogica _ConfiguracionesLogica)
        {
            this._ConfiguracionesLogica = _ConfiguracionesLogica;
        }

        public ActionResult Index()
        {
            return View(_ConfiguracionesLogica.ObtenerConfiguraciones());
        }

        [HttpPost]
        public ActionResult CambiarConfiguraciones(Empresa Empresa)
        {
            _ConfiguracionesLogica.CambiarConfiguraciones(Empresa);
            return RedirectToAction("Index", new RouteValueDictionary(
                new { controller = "menu", action = "Index", Usuario = HttpContext.Session.GetInt32("IdUsuario") }));
            }

    }
}
