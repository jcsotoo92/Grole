using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using grole.src.Entidades;
using grole.src.Logica;


namespace grole.Controllers
{
    [Models.ChecaAutorizacion("77")]
    public class ClasificacionCortesController : Controller
    {
        private ClasificacionCortesLogica _ClasificacionCortesLogica;
		
		public ClasificacionCortesController(ClasificacionCortesLogica AClasificacionCortesLogica){
			this._ClasificacionCortesLogica = AClasificacionCortesLogica;
		}
	
		[HttpGet]
		public ActionResult Index(){
			List<ClasificacionCorte> pListaClasificacionCortes = this._ClasificacionCortesLogica.ObtenerLista();
			pListaClasificacionCortes = pListaClasificacionCortes.OrderBy(x => x.Descripcion).ToList();		
			return View(pListaClasificacionCortes);
		}
		
		[HttpPost]
		public JsonResult Insertar(ClasificacionCorte ClasificacionCorte){
			return Json(this._ClasificacionCortesLogica.ClasificacionCorteInsertar(ClasificacionCorte));			
		}
		
		[HttpPost]
		public JsonResult Modificar(ClasificacionCorte ClasificacionCorte){
			return Json(this._ClasificacionCortesLogica.ClasificacionCorteModificar(ClasificacionCorte));			
		}
		
		[HttpPost]
		public JsonResult Eliminar(int Clave){
			string pMensaje = "";
			bool pResult = _ClasificacionCortesLogica.ClasificacionCorteEliminar(Clave, out pMensaje);
			return Json(new {Result = pResult, Mensaje = pMensaje});			
		}
    }
}
