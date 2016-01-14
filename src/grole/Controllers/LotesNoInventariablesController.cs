using grole.src.Entidades;
using grole.src.Logica;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.Controllers
{
    [Models.ChecaAutorizacion("22")]
    public class LotesNoInventariablesController : Controller
    {
        private LotesNoInventariablesLogica _LotesNoInventariablesLogica;

        public LotesNoInventariablesController(LotesNoInventariablesLogica _LotesNoInventariablesLogica)
        {
            this._LotesNoInventariablesLogica = _LotesNoInventariablesLogica;
        }

        public ActionResult Index()
        {
            return View(_LotesNoInventariablesLogica.ObtenerLotes());
        }

        [HttpPost]
        public JsonResult Insertar(LoteNoInventariable lote)
        {
            return Json(_LotesNoInventariablesLogica.Insertar(lote));
        }
    }
}
