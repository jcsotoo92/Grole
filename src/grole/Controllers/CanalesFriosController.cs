using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System;
using grole.src.Logica;
using grole.src.Entidades;

namespace grole.Controllers
{
    public class CanalesFriosController : Controller
    {
        private GranjasLogica _GranjasLogica;
        public CanalesFriosController(GranjasLogica _GranjasLogica)
        {
            this._GranjasLogica = _GranjasLogica;
        }
        [Route("/canales_frios")]
        public ActionResult CanalesFrios()
        {
            return View(this._GranjasLogica.ObtenerGranjas());
        }

    }
}
