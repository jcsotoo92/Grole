using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using grole.src.Logica;
using grole.src.Entidades;
using System.IO;
using Microsoft.Dnx.Runtime;
using Microsoft.Extensions.PlatformAbstractions;

namespace grole.Controllers
{
    [Models.ChecaAutorizacion("26")]
    public class EmpaquesController : Controller
    {
        private EmpaquesLogica _EmpaquesLogica;
        private ProductosLogica _ProductosLogica;
        //private readonly IApplicationEnvironment _appEnvironment;
        public EmpaquesController(EmpaquesLogica _EmpaquesLogica, ProductosLogica _ProductosLogica)
        {
            this._EmpaquesLogica  = _EmpaquesLogica;
            this._ProductosLogica = _ProductosLogica;
        }

        public ActionResult Index()
        {
            string pHora = DateTime.Now.ToString("hhmmss");
            Console.WriteLine(PlatformServices.Default.Application.ApplicationBasePath);
            
            string pRutaArchivo = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "wwwroot\\public\\CatalogoEmpaques_" + User.Identity.Name + "_" + pHora + ".XLSX");
            ViewBag.RutaExcel = "/Public/CatalogoEmpaques_" + User.Identity.Name + "_" + pHora + ".XLSX";
            List<Empaque> listaEmpaques = _EmpaquesLogica.ObtenerEmpaques("CODIGOSAP");
            _EmpaquesLogica.ExportarCatalogoEmpaques(listaEmpaques, pRutaArchivo);
            return View(listaEmpaques);
        }


        [HttpGet]
        public JsonResult ObtenerListaTiposEmpaques()
        {
            return Json(_EmpaquesLogica.ObtenerListaTiposEmpaques());
        }

        [HttpPost]
        public JsonResult Insertar(Empaque Empaque)
        {
            return Json(_EmpaquesLogica.Insertar(Empaque));
        }

        [HttpPost]
        public JsonResult Modificar(Empaque Empaque)
        {

            return Json(_EmpaquesLogica.Modificar(Empaque));
        }

        [HttpPost]
        public JsonResult Eliminar(int Clave)
        {
            string pMensaje = "";
            bool pResult = _EmpaquesLogica.Eliminar(Clave, out pMensaje);
            return Json(new { Result = pResult, Mensaje = pMensaje });
        }

        [HttpGet]
        public JsonResult ObtenerProductos()
        {
            List<Producto> pLista = _ProductosLogica.ObtenerProductosValidaciones();
            return Json(pLista);
        }

        public ActionResult EmpaquesProductos()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerEmpaquesProducto(string Clave)
        {
            return Json(_EmpaquesLogica.ObtenerEmpaquesProducto(Clave));
        }

        [HttpGet]
        public JsonResult ObtenerEmpaques()
        {
            return Json(_EmpaquesLogica.ObtenerEmpaques("ID_TIPOEMPAQUE"));
        }

        [HttpPost]
        public JsonResult InsertarEmpaquesProducto(string IdProducto, int[] chk, decimal[] inp)
        {
            bool pResult = _EmpaquesLogica.InsertarEmpaquesProducto(IdProducto, chk, inp);
            return Json("");
        }

        [HttpGet]
        public ActionResult TiposEmpaques() {
            return View(_EmpaquesLogica.ObtenerListaTiposEmpaques());
        }

        [HttpPost]
        public JsonResult AgregarTipoEmpaque(TipoEmpaque TipoEmpaque) {
            return Json(_EmpaquesLogica.AgregarTipoEmpaque(TipoEmpaque));
        }

        [HttpPost]
        public JsonResult ModificarTipoEmpaque(TipoEmpaque TipoEmpaque) {
            return Json(_EmpaquesLogica.ModificarTipoEmpaque(TipoEmpaque));
        }

        [HttpPost]
        public JsonResult EliminarTipoEmpaque(int Clave)
        {
            string pMensaje = "";
            bool pResult = _EmpaquesLogica.EliminarTipoEmpaque(Clave, out pMensaje);
            return Json(new { Result = pResult, Mensaje = pMensaje });
        }

        public ActionResult ProyeccionEmpaque()
        {
            return View();
        }

        public JsonResult ObtenerTablaProyeccionEmpaque()
        {
            return Json("");
        }

        public ActionResult ProyeccionProduccion()
        {
            return View();
        }
        
        [HttpGet]        
        public JsonResult ObtenerTablaProyeccionProduccion(string FechaIni, string FechaFin, string Tipo)
        {
            return Json(_EmpaquesLogica.ObtenerTablaProyeccionProduccion(FechaIni, FechaFin, Tipo));
        }

        public void ExportarCatalogoEmpaques()
        {
            
        }
        
    }
}
