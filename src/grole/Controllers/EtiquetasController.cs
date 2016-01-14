using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using grole.src.Logica;
using grole.src.Entidades;
using Microsoft.AspNet.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace grole.Controllers
{
    public class EtiquetasController : Controller
    {

        private EtiquetasLogica _EtiquetasLogica;
        private ProductosLogica _ProductosLogica;
        private CajasLogica _CajasLogica;
        private EliminadasLogica _EliminadasLogica;
        public EtiquetasController(EtiquetasLogica _EtiquetasLogica, ProductosLogica _ProductosLogica, CajasLogica _CajasLogica, EliminadasLogica _EliminadasLogica)
        {
            this._EtiquetasLogica = _EtiquetasLogica;
            this._ProductosLogica = _ProductosLogica;
            this._CajasLogica     = _CajasLogica;
            this._EliminadasLogica = _EliminadasLogica;
        }

        // GET: /<controller>/
        public ActionResult ConsultaFolios()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerTablaFolio(int Folio)
        {
            return Json(_EtiquetasLogica.ObtenerCajasPorFolio(Folio));
        }

        [HttpGet]
        public ActionResult ConsultaNoInvent()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerTablaNoInventariadas(string Producto, string FechaIni, string FechaFin)
        {
            return Json(_EtiquetasLogica.ObtenerProduccionNoInventariadas(Producto, FechaIni, FechaFin));
        }

        [HttpPost]
        public JsonResult EliminarLoteNoInventariable(string Fecha, int Lote, string Producto)
        {
            string pMensaje = "";
            bool pResult = _EtiquetasLogica.EliminarProduccionNoInventariable(Fecha, Lote, Producto, out pMensaje);
            return Json(new { Result = pResult, Mensaje = pMensaje });
        }

        public ActionResult ConsultaProdNoInvent()
        {
            return View(_ProductosLogica.ObtenerListaProductosNoInventariables());
        }

        public JsonResult ObtenerTablaNoInventariadasProducto(string Producto, string FechaIni, string FechaFin)
        {
            return Json(_CajasLogica.ObtenerProduccionNoInventariadasPorProducto(Producto, FechaIni, FechaFin));
        }

        public ActionResult AuxiliarEliminadas()
        {
            return View(_ProductosLogica.DameListaProductos());
        }

        public JsonResult ObtenerTablaAuxiliarEliminadas(string Producto, string FechaIni, string FechaFin)
        {
            return Json(_EliminadasLogica.ObtenerAuxiliarEliminadas(Producto, FechaIni, FechaFin));
        }

        public ActionResult BorrarErrores()
        {
            return View();
        }

        public JsonResult InfoCaja(int AFolio, string AFecha)
        {
            
            Corte caja = _CajasLogica.ObtenerDatosCaja(AFolio, AFecha);
            if (caja == null)
            {
                caja              = new Corte();
                caja.Producto     = "-1";
                caja.CodigoBarras = "-1";
            }
            else {
               caja.Producto = caja.Producto + " " + _ProductosLogica.DameDescripcionProducto(caja.Producto);
            }
            Console.WriteLine(caja.Producto + " " + caja.CodigoBarras);

            return Json(caja);
        }
        public JsonResult BorrarError(int AFolio, string AFecha, string AMotivo)
        {
            string pMensaje="";
            Corte info_caja = _CajasLogica.ObtenerDatosCaja(AFolio, AFecha);
            int id_usuario = (int)HttpContext.Session.GetInt32("IdUsuario");

            _CajasLogica.BorrarEtiqueta(info_caja.CodigoBarras, AMotivo, "", id_usuario, out pMensaje);
            return Json(new { Mensaje1="Caja borrada con éxito", Mensaje2 = pMensaje });
        }
    }
}
