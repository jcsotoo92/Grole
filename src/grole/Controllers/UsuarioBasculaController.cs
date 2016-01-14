using Microsoft.AspNet.Mvc;
using grole.src.Entidades;
using grole.src.Logica;
using System.Collections.Generic;
using System;

namespace grole.Controllers
{
	
	public class UsuarioBasculaController : Controller
	{
	
		public UsuarioBasculaLogica _UsuarioBasculaLogica { get; set; }
		
		public UsuarioBasculaController(UsuarioBasculaLogica AUsuarioBasculaLogica)
		{
			this._UsuarioBasculaLogica = AUsuarioBasculaLogica;
		}
		
		public ActionResult Index()
		{
			List<UsuarioBascula> pListaUsuarioBascula = _UsuarioBasculaLogica.ListaUsuarioBascula();
			return View(pListaUsuarioBascula);
		}
		public JsonResult Modificar(UsuarioBascula AUsuarioBascula)
		{	
			return Json(this._UsuarioBasculaLogica.UsuarioBasculaModificar(AUsuarioBascula));			
		}
		
		[HttpPost]
		public JsonResult Eliminar(int CLAVE)
		{
			string pMensaje = "";
			bool pResult = _UsuarioBasculaLogica.UsuarioBasculaEliminar(CLAVE, out pMensaje);
			return Json(new {Result = pResult, Mensaje = pMensaje});			
		}
		
		[HttpPost]
		public JsonResult Insertar(UsuarioBascula UsuarioBascula)
		{
			return Json(this._UsuarioBasculaLogica.UsuarioBasculaInsertar(UsuarioBascula));			
		}
		
		[HttpPost]
		public JsonResult Buscar(string Filtrado)
		{
			return Json(_UsuarioBasculaLogica.busquedaUsuarioBascula(Filtrado));			
		}
	
	}
	
}