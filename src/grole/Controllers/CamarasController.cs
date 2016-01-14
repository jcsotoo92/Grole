using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System;
using grole.src.Logica;
using grole.src.Entidades;

namespace grole.Controllers
{
    [Models.ChecaAutorizacion("30")]
    public class CamarasController : Controller
	{
		
		private CamarasLogica _CamarasLogica;
		private ProductosLogica _ProductosLogica;
		private CajasLogica _CajasLogica;
		
		public CamarasController(CamarasLogica _CamarasLogica,ProductosLogica _ProductosLogica, CajasLogica _CajasLogica){
			this._CamarasLogica=_CamarasLogica;
			this._ProductosLogica=_ProductosLogica;
			this._CajasLogica=_CajasLogica;
		}
		
		[HttpGet]
		public ActionResult Index(){
			return View(_CamarasLogica.ObtenerCamaras());
		}
		
		
		[HttpPost]
		public JsonResult Insertar(Camara Camara){
			return Json(_CamarasLogica.CamaraInsertar(Camara));
		}
		
		[HttpPost]
		public JsonResult Modificar(Camara Camara){
			return Json(_CamarasLogica.CamaraModificar(Camara));
		}
		
		[HttpPost]
		public JsonResult Eliminar(int Clave){
			string pMensaje = "";
			bool pResult = _CamarasLogica.CamaraEliminar(Clave, out pMensaje);
			return Json(new {Result = pResult, Mensaje = pMensaje});	
		}
		
		[HttpGet]
		public JsonResult ObtenerProductos(){
            List<Producto> pLista = _ProductosLogica.ObtenerProductosValidaciones();
			return Json(pLista);
		}
		
		[HttpGet]
		public JsonResult ObtenerValidaciones(string Clave){
			List<ValidacionCamara> pLista = _CamarasLogica.ObtenerValidacionesCamaras(Clave);
			return Json(pLista);
		}
		
		[HttpPost]
		public JsonResult EliminarValidacion(int Clave){
			string pMensaje = "";
			bool pResult = _CamarasLogica.CamaraEliminarValidacion(Clave, out pMensaje);
			return Json(new {Result = pResult, Mensaje = pMensaje});	
		}
		
		[HttpGet]
		public JsonResult MostrarInventarioProductoDesglosado(string FechaIni,string FechaFin, string Producto){
			List<InventarioProductoDesglosado> pLista = _CajasLogica.ObtenerInventarioProductoDesglosado(FechaIni,FechaFin,Producto);
			return Json(pLista);
		}
		
		[HttpGet]
		public JsonResult ObtenerInventarioProducto(string FechaIni,string FechaFin, string Producto){
			InventarioProducto pInvProd = _CajasLogica.ObtenerInventarioProducto(FechaIni,FechaFin,Producto);
			return Json(pInvProd);
		}
		
		[HttpPost]
		public JsonResult InsertarValidacionCamara(int Camara, string Producto, int Cantidad, decimal Kilos, string FechaMin,string FechaMax){
			return Json(_CamarasLogica.InsertarValidacionCamara(Camara,Producto,Cantidad,Kilos,FechaMin,FechaMax));
		}

        public ActionResult ResumenPtosCamara()
        {
            return View(_CamarasLogica.ObtenerCamarasActivas());
        }

        public JsonResult ObtenerTablaResumenPtosCamara(int[] Camaras)
        {
            return Json(_CajasLogica.ObtenerResumenPtosCamara(Camaras));
        }

        public ActionResult DetallePtosCamara(int Camara, string Producto)
        {
            return View(_CajasLogica.ObtenerDetalleProductosCamara(Camara, Producto));
        }

        public Camara ObtenerCamara(int Camara)
        {
            return _CamarasLogica.ObtenerCamara(Camara);
        }

        public ActionResult SaldoPorCamara()
        {
            return View(_CamarasLogica.obtener_lista_camaras_catalogo_activas());
        }

        public JsonResult obtener_validaciones_camaras(int ACamara)
        {
            return Json(_CamarasLogica.obtener_validaciones_camara(ACamara));
        }

        public JsonResult obtener_saldo_camara_producto(int AProducto, int ACamara)
        {
            return Json(_CamarasLogica.obtener_saldo_camara_producto(AProducto, ACamara));
        }
    }
	
}