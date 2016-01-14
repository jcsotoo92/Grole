using grole.src.Entidades;
using grole.src.Logica;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace grole.Controllers
{
    [Models.ChecaAutorizacion("78")]
    public class DestinosController : Controller
	{
	
		public DestinosLogica _DestinosLogica { get; set; }
		
		public DestinosController(DestinosLogica ADestinosLogica){
			this._DestinosLogica = ADestinosLogica;
		}
	
		[HttpGet]
		public ActionResult Index(){
			List<Destino> pListaDestinos = this._DestinosLogica.ObtenerLista();
			pListaDestinos = pListaDestinos.OrderBy(x => x._Destino).ToList();		
			return View(pListaDestinos);
		}
		
		[HttpPost]
		public JsonResult Insertar(Destino Destino){
			return Json(this._DestinosLogica.DestinoInsertar(Destino));			
		}
		
		[HttpPost]
		public JsonResult Modificar(Destino Destino){
			return Json(this._DestinosLogica.DestinoModificar(Destino));			
		}
		
		[HttpPost]
		public JsonResult Eliminar(int Clave){
			string pMensaje = "";
			bool pResult = _DestinosLogica.DestinoEliminar(Clave, out pMensaje);
			return Json(new {Result = pResult, Mensaje = pMensaje});			
		}

        [HttpGet]
        public JsonResult ObtenerLista()
        {
            List<Destino> pListaDestinos = this._DestinosLogica.ObtenerLista();
            pListaDestinos = pListaDestinos.OrderBy(x => x._Destino).ToList();
            return Json(pListaDestinos);
        }
	}
	
}