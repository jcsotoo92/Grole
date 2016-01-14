using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using grole.src.Logica;

namespace grole.Controllers
{
    public class RastreoFoliosController : Controller
    {
        private RastreoFoliosLogica _RastreoFoliosLogica;

        public RastreoFoliosController(RastreoFoliosLogica _RastreoFoliosLogica)
        {
            this._RastreoFoliosLogica = _RastreoFoliosLogica;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ObtenerRastreoFolios(int Folio, int Producto)
        {
            return Json(_RastreoFoliosLogica.ObtenerRastreoFolios(Folio, Producto));
        }
    }
}
