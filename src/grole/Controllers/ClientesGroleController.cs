using Microsoft.AspNet.Mvc;
using grole.src.Entidades;
using grole.src.Logica;
using System.Collections.Generic;

namespace grole.Controllers
{
    [Models.ChecaAutorizacion("57")]
    public class ClientesGroleController : Controller
	{
	
		public ClientesGroleLogica _ClientesGroleLogica { get; set; }
		
		public ClientesGroleController(ClientesGroleLogica AClientesGroleLogica)
		{
			this._ClientesGroleLogica = AClientesGroleLogica;
		}
		//Retorna la vista de ClientesGrole
		public ActionResult Index()
		{
			List<ClienteGrole> pListaClientesGrole = _ClientesGroleLogica.ListaClientesGrole();
			return View(pListaClientesGrole);
		}
		//Retorna La Lista de ClientesGrole
		[HttpPost]
		public JsonResult ListaClientesGrole()
		{
			List<ClienteGrole> pListaClientesGrole = _ClientesGroleLogica.ListaClientesGrole();
			return Json(pListaClientesGrole);			
		}
		//Ingresa ClientesGrole a la base de datos
		public JsonResult Insertar(ClienteGrole AClienteGrole)
		{
			return Json(this._ClientesGroleLogica.ClientesGroleInsertar(AClienteGrole));			
		}
		//Modificar ClientesGrole
		public JsonResult Modificar(ClienteGrole AClienteGrole)
		{	
			return Json(this._ClientesGroleLogica.ClientesGroleModificar(AClienteGrole));			
		}
		
		//Eliminar ClientesGrole
		public JsonResult Eliminar(int Id)
		{
			string pMensaje = "";
			bool pResult = _ClientesGroleLogica.ClientesGroleEliminar(Id, out pMensaje);
			return Json(new {Result = pResult, Mensaje = pMensaje});			
		}
		
		//Filtro Busqueda
		public JsonResult Buscar(string Filtrado)
		{
			return Json(_ClientesGroleLogica.busquedaClienteGrole(Filtrado));			
		}
	}
	
	
}