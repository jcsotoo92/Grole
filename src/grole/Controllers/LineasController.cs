using grole.src.Entidades;
using grole.src.Logica;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace grole.Controllers
{
    [Models.ChecaAutorizacion("16")]
    public class LineasController : Controller
	{
	
		public LineasLogica _LineasLogica { get; set; }
		
		public LineasController(LineasLogica ALineasLogica){
			this._LineasLogica = ALineasLogica;
		}
	
		[HttpGet]
		public ActionResult Index(){
			List<Linea> pListaLineas = this._LineasLogica.ObtenerLista();
			pListaLineas = pListaLineas.OrderBy(x => x.Descripcion).ToList();		
			return View(pListaLineas);
		}
		
		[HttpPost]
		public JsonResult Insertar(Linea Linea){
			return Json(this._LineasLogica.LineaInsertar(Linea));			
		}
		
		[HttpPost]
		public JsonResult Modificar(Linea Linea){
			string idMod=Linea.Descripcion.Split('-')[(Linea.Descripcion.Split('-').Count())-1];
			Linea lin=new Linea();
			lin.Clave=Linea.Clave;
			lin.Descripcion=Linea.Descripcion.Split('-')[0];
			return Json(this._LineasLogica.LineaModificar(lin, idMod));			
		}
		
		[HttpPost]
		public JsonResult Eliminar(int Clave){
			string pMensaje = "";
			bool pResult = _LineasLogica.LineaEliminar(Clave, out pMensaje);
			return Json(new {Result = pResult, Mensaje = pMensaje});			
		}

        [HttpGet]
        public JsonResult ObtenerLista()
        {
            List<Linea> pListaLineas = this._LineasLogica.ObtenerLista();
            pListaLineas = pListaLineas.OrderBy(x => x.Descripcion).ToList();
            return Json(pListaLineas);
        }
	}
	
}