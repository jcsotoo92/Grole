using grole.src.Logica;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.Controllers
{
    public class EstimacionEmpaquesController : Controller
    {
        private EstimacionEmpaquesLogica _EstimacionEmpaquesLogica;
        public EstimacionEmpaquesController(EstimacionEmpaquesLogica _EstimacionEmpaquesLogica)
        {
            this._EstimacionEmpaquesLogica = _EstimacionEmpaquesLogica;
        }

        public ActionResult Index()
        {
            return View(_EstimacionEmpaquesLogica.ObtenerLista());
        }
    }
}
