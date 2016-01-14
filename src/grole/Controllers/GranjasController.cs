using Microsoft.AspNet.Mvc;
using grole.src.Entidades;
using grole.src.Logica;
using System.Collections.Generic;
using System.Linq;

namespace grole.Controllers
{
	[Models.ChecaAutorizacion("16")]
	public class GranjasController : Controller
	{
	
		private GranjasLogica _GranjasLogica;
		
		public GranjasController(GranjasLogica AGranjasLogica){
			this._GranjasLogica = AGranjasLogica;
		}
	
		[HttpGet]
		public ActionResult Index(){
			List<Granja> pListaGranjas = this._GranjasLogica.ObtenerLista();
			pListaGranjas = pListaGranjas.OrderBy(x => x.Nombre).ToList();		
			return View(pListaGranjas);
		}
		
		[HttpPost]
		public JsonResult Insertar(Granja Granja){
			return Json(this._GranjasLogica.GranjaInsertar(Granja));			
		}
		
		[HttpPost]
		public JsonResult Modificar(Granja Granja){
			return Json(this._GranjasLogica.GranjaModificar(Granja));			
		}
		
		[HttpPost]
		public JsonResult Eliminar(int Clave){
			string pMensaje = "";
			bool pResult = _GranjasLogica.GranjaEliminar(Clave, out pMensaje);
			return Json(new {Result = pResult, Mensaje = pMensaje});			
		}

        [HttpGet]
        public JsonResult ObtenerGranjas()
        {
            return Json(_GranjasLogica.ObtenerLista().OrderBy(x => x.Nombre).ToList());
        }
            
     }
}