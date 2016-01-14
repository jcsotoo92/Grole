using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using grole.src.Logica;

namespace grole.Controllers
{
    public class InformacionCajaController: Controller
    {
        private InformacionCajaLogica _InformacionCajaLogica;

        public InformacionCajaController(InformacionCajaLogica _InformacionCajaLogica)
        {
            this._InformacionCajaLogica = _InformacionCajaLogica;
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ObtenerDatosCaja(int Folio, string Fecha)
        {
            return Json(_InformacionCajaLogica.ObtenerDatosCaja(Folio, Fecha));
        }


    }
}
