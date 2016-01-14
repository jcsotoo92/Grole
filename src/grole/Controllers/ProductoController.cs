using Microsoft.AspNet.Mvc;
using grole.src.Entidades;
using grole.src.Logica;
using System.Collections.Generic;
using System;
using Microsoft.AspNet.Http;

namespace grole.Controllers
{
    
    public class ProductoController : Controller
	{
	
		public ProductosLogica _ProductosLogica { get; set; }
		
		public ProductoController(ProductosLogica AProductosLogica)
		{
			this._ProductosLogica = AProductosLogica;
		}
        [Models.ChecaAutorizacion("9")]
        //Vista Principal de Producto
        public ActionResult Index()
		{
			return View();
		}
		//Ingresar Nuevo Producto
		public ActionResult ProductoNuevo()
		{
			return View();
		}
		//Modificar Producto
		public ActionResult ProductoModificar()
		{
			return View();
		}
        //Modificar Producto Seleccion
        public ActionResult ProductoModificarSeleccion(string AClave)
		{
            Producto pProducto = _ProductosLogica.ObtenerProducto(AClave);
			return View(pProducto);
		}
        //Retorna bool para Validar Clave, False=No Existe True=Existe 
        public JsonResult ValidarClave(int AClave)
        {
            return Json(_ProductosLogica.ValidarClave(AClave));
        }


        //Retorna La Lista de Productos
        [HttpPost]
		public JsonResult ListaProductos()
		{
			List<Producto> pProducto = _ProductosLogica.ListaProductos();
			return Json(pProducto);			
		}

        //Insertar Producto
        [HttpPost]
        public void ProductoInsertar(Producto AProducto)
        {
            _ProductosLogica.ProductoInsertar(AProducto);
        }
        //Insertar Producto
        [HttpPost]
        public void ProductoActualizar(Producto AProducto)
        {
            _ProductosLogica.ProductoActualizar(AProducto);
        }
        [HttpGet]
		//Retorna Producto Especifico
		public JsonResult ObtenerProducto(string Clave){
			Producto pProducto = _ProductosLogica.ObtenerProducto(Clave+"");
			return Json(pProducto);
		}

        //JUAN CARLOS
       [Models.ChecaAutorizacion("36")]
        public ActionResult FechaSacrificio()
        {
            return View();
        }
        [Models.ChecaAutorizacion("36")]
        [HttpGet]
        public JsonResult ObtenerFechaDeSacrificioDeProducto(string Clave)
        {
            Producto pProducto = _ProductosLogica.ObtenerFechaDeSacrificioDeProducto(Clave);
            return Json(pProducto);
        }

        [HttpGet]
        public JsonResult ObtenerProductos()
        {
            return Json(_ProductosLogica.ObtenerProductosValidaciones());
        }
        [Models.ChecaAutorizacion("36")]
        [HttpPost]
        public JsonResult ModificarFechaSacrificio(Producto Producto)
        {
            return Json(_ProductosLogica.CambiarFechaSacrificio(Producto));
        }

        public ActionResult ConsultaCambiosTara()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerTablaCambiosTaras(string Producto, string FechaIni, string FechaFin)
        {
            return Json(_ProductosLogica.ObtenerListaCambiosTara(Producto, FechaIni, FechaFin));
        }

        public ActionResult ConsultaUsoEmpaque()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerTablaUsoEmpaque(string FechaIni, string FechaFin)
        {
            return Json(_ProductosLogica.ObtenerUsoEmpaque(FechaIni, FechaFin));
        }

        [HttpGet]
        public ActionResult DetalleUsoEmpaque(int Empaque, string FechaIni, string FechaFin)
        {
            return View(_ProductosLogica.ObtenerDetalleUsoEmpaque(Empaque, FechaIni, FechaFin));
        }

        public ActionResult CambiarTara()
        {
            
            return View(_ProductosLogica.DameListaProductos());
        }

        [HttpPost]
        public JsonResult CambiarTaraProducto(string Producto, float Tara)
        {
            if (_ProductosLogica.CambiarTaraProducto(Producto, Tara, HttpContext.User.Identity.Name) > 0)
                return Json(new { Codigo = 1, Mensaje = "Se cambio la tara del producto" });
            else
                return Json(new { Codigo = 0, Mensaje = "Error al cambiar la tara, reportelo al depto. de sistemas" });
        }
    }
}