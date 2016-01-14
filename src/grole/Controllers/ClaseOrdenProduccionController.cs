using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using grole.src.Entidades;
using grole.src.Logica;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace grole.Controllers
{
    [Models.ChecaAutorizacion("76")]
    public class ClaseOrdenProduccionController : Controller
    {

        public ClaseOrdenProduccionLogica _ClaseOrdenProduccionLogica { get; set; }

        public ClaseOrdenProduccionController(ClaseOrdenProduccionLogica AClaseOrdenProduccionLogica)
        {
            this._ClaseOrdenProduccionLogica = AClaseOrdenProduccionLogica;
        }
        
        public ActionResult Index()
        {
            return View(_ClaseOrdenProduccionLogica.ObtenerLista());
        }

        [HttpPost]
        public JsonResult Insertar(ClaseOrdenProduccion ClaseOrdenProduccion)
        {
            return Json(this._ClaseOrdenProduccionLogica.ClaseOrdenProduccionInsertar(ClaseOrdenProduccion));
        }

        [HttpPost]
        public JsonResult Modificar(ClaseOrdenProduccion ClaseOrdenProduccion)
        {
            return Json(this._ClaseOrdenProduccionLogica.ClaseOrdenProduccionModificar(ClaseOrdenProduccion));
        }

        [HttpPost]
        public JsonResult Eliminar(int Clave)
        {
            string pMensaje = "";
            bool pResult = _ClaseOrdenProduccionLogica.ClaseOrdenProduccionEliminar(Clave, out pMensaje);
            return Json(new { Result = pResult, Mensaje = pMensaje });
        }
    }
}
