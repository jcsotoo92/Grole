using Microsoft.AspNet.Mvc;
using grole.src.Entidades;
using grole.src.Logica;
using System.Collections.Generic;

namespace grole.Controllers
{
    [Models.ChecaAutorizacion("75")]
    public class ClaseController : Controller
	{
        
        public ClasesLogica _ClasesLogica { get; set; }
		
		public ClaseController(ClasesLogica AClasesLogica)
		{
			this._ClasesLogica = AClasesLogica;
		}
		//Retorna la vista de Clase
		public ActionResult Index()
		{
			List<Clase> pListaClase = _ClasesLogica.ListaClases();
			return View(pListaClase);
		}
		//Retorna La Lista de Clases
		[HttpPost]
		public JsonResult ListaClases()
		{
			List<Clase> pListaClase = _ClasesLogica.ListaClases();
			return Json(pListaClase);			
		}
		//Ingresa Clase a la base de datos
		public JsonResult Insertar(Clase AClase)
		{
			return Json(this._ClasesLogica.ClaseInsertar(AClase));			
		}
		//Modificar Clase
		public JsonResult Modificar(Clase AClase)
		{	
			return Json(this._ClasesLogica.ClaseModificar(AClase));			
		}
		
		//Eliminar Clase
		public JsonResult Eliminar(int CLAVE)
		{
			string pMensaje = "";
			bool pResult = _ClasesLogica.ClaseEliminar(CLAVE, out pMensaje);
			return Json(new {Result = pResult, Mensaje = pMensaje});			
		}
	}
	
	
}