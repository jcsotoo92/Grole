using Microsoft.AspNet.Mvc;
using grole.src.Entidades;
using grole.src.Logica;
using System.Collections.Generic;
using System;

namespace grole.Controllers
{
    [Models.ChecaAutorizacion("47")]
    public class CostosMaquilaController : Controller
	{
		public CostosMaquilaLogica _CostosMaquilaLogica { get; set; }
		
		public CostosMaquilaController(CostosMaquilaLogica ACostosMaquilaLogica)
		{
			this._CostosMaquilaLogica = ACostosMaquilaLogica;
		}
		
		//Retorna la vista de Clase
		public ActionResult Index()
		{
			List<CostoMaquilaM> pListaCostoMaquilaM = _CostosMaquilaLogica.ListaCostosMaquila();
			return View(pListaCostoMaquilaM);
		}
		public ActionResult Nueva()
		{
			return View();
		}
		//Ingresa nueva lista de Costos Maquila
		public JsonResult Insertar(CostoMaquilaM ACostoMaquilaM)
		{
			return Json(this._CostosMaquilaLogica.CostosMaquilaInsertarM(ACostoMaquilaM));			
		}
		//Inserta Productos de Maquila
		public JsonResult InsertarProductos(CostoMaquilaD ACostoMaquilaD)
		{
			return Json(this._CostosMaquilaLogica.CostosMaquilaInsertarD(ACostoMaquilaD));			
		}
		//Obtener Productos
		public JsonResult ObtenerProductos(int Id_Costo)
		{
			return Json(this._CostosMaquilaLogica.ObtenerProductos(Id_Costo));			
		}
		//Elimina Maquila
		public JsonResult EliminarMaquila(int Id)
		{
			return Json(this._CostosMaquilaLogica.EliminarMaquila(Id));
		}
		
		//Elimina Productos de Maquila
		public JsonResult EliminarProductosMaquila(int Id)
		{
			return Json(this._CostosMaquilaLogica.EliminarProductosMaquila(Id));
		}
		
		//Carga Vista Modificar
		public ActionResult Modificar(int Id)
		{
			CostoMaquilaM pCostoMaquilaM = _CostosMaquilaLogica.ObtenerCostosMaquila(Id);
  			return View(pCostoMaquilaM);
		}
		
		//Modifica Maquila Detalles
		public bool ModificarCostoMaquilaM(CostoMaquilaM MaquilaM)
		{
			return _CostosMaquilaLogica.ModificarCostoMaquilaM(MaquilaM);
		}
		
		//Modifica Maquila Productos
		public bool ModificarCostoMaquilaD(CostoMaquilaD MaquilaD)
		{
			return _CostosMaquilaLogica.ModificarCostoMaquilaD(MaquilaD);
		}
	}
}