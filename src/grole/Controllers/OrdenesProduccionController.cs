using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using grole.src.Logica;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace grole.Controllers
{
    public class OrdenesProduccionController : Controller
    {
        private OrdenesProduccionLogica _OrdenesProduccionLogica;

        public OrdenesProduccionController(OrdenesProduccionLogica _OrdenesProduccionLogica)
        {
            this._OrdenesProduccionLogica = _OrdenesProduccionLogica;
        }
        public IActionResult InformacionTarimaOrdenP()
        {
            return View();
        }

        public JsonResult ObtenerTablaInformacionTarimaOrdenP(int IdTarima)
        {
            return Json(_OrdenesProduccionLogica.ObtenerInformacionTarimaOrdenP(IdTarima));
        }

        public ActionResult InformacionOrdenP()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerTablaInformacionOrdenP(int AOrdenP)
        {
            return Json(_OrdenesProduccionLogica.ObtenerInformacionOrdenP(AOrdenP));
        }


    }
}
