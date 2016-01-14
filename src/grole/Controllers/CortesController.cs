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
    public class CortesController : Controller
    {
        private CortesLogica _CortesLogica;
        private CajasLogica _CajasLogica;
        private MateriaPrimaLogica _MateriaPrimaLogica;
        public CortesController(CortesLogica _CortesLogica, CajasLogica _CajasLogica, MateriaPrimaLogica _MateriaPrimaLogica)
        {
            this._CortesLogica = _CortesLogica;
            this._CajasLogica = _CajasLogica;
            this._MateriaPrimaLogica = _MateriaPrimaLogica;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConsultaPedimentos()
        {
            return View();
        }

        public JsonResult ObtenerTablaPedimento(int Pedimento)
        {
            return Json(_CortesLogica.ObtenerSaldosPedimento(Pedimento));
        }

        public ActionResult CorteFecha()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerTablaCorteFecha(string Fecha)
        {
            return Json(_CortesLogica.ObtenerCorteFecha(Fecha));
        }

        [HttpGet]
        public JsonResult ObtenerPendientesCorte(string Fecha)
        {
            return Json(_CortesLogica.ObtenerPendientesCorte(Fecha));
        }

        public ActionResult PendientesFecha(string Fecha)
        {
            return View(_CortesLogica.ObtenerDetallePendientesCorte(Fecha));
        }

        [HttpGet]
        public ActionResult ResumenFecha(string Fecha, int Camara, string Embarcado)
        {
            return View(_CortesLogica.ObtenerDetalleCorteFecha(Fecha, Camara, Embarcado));
        }

        [HttpGet]
        public ActionResult ConsultaSalidas()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerTablaSalidas(string FechaIni, string FechaFin)
        {
            return Json(_CortesLogica.ObtenerSalidaDelDia(FechaIni, FechaFin));
        }

        [HttpGet]
        public ActionResult ResumenCortesArea()
        {
            int id_usuario = (int)HttpContext.Session.GetInt32("IdUsuario");
            Console.WriteLine(id_usuario);
            var areas_usuario = _CortesLogica.ObtenerAreasUsuario(id_usuario);
            return View(areas_usuario);
        }

        [HttpGet]
        public ActionResult ResumenCortes()
        {
            return View(_CortesLogica.ObtenerListaClasificaciones());
        }

        public ActionResult ProduccionPorBascula()
        {
            return View(_CortesLogica.ObtenerBasculasActivas());
        }

        public JsonResult ObtenerTablaProduccionPorBascula(int Bascula, string Fecha)
        {
            return Json(_CortesLogica.ObtenerProduccionPorBascula(Fecha, Bascula));
        }

        public ActionResult DesgloseProductoPorCamara(string Producto)
        {
            return View(_CajasLogica.ObtenerDesgloseProductoPorCamara(Producto));
        }

        public ActionResult RecepcionCajasEmbarques()
        {
            return View();
        }

        public JsonResult ObtenerTablaPendientesRecepcionEmbarques(string Fecha)
        {
            return Json(_CajasLogica.ObtenerCajasPendientesRecepcionEmbarques(Fecha));
        }

        public ActionResult DetalleRecepcionCajasEmbarque(string Fecha, string Producto)
        {
            return View(_CajasLogica.ObtenerDetalleCajasPendientesRecepcionEmbarques(Fecha, Producto));
        }

        public ActionResult CambiarCajasDeLote()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerTablaLote(string Fecha, int Lote)
        {
            return Json(_CajasLogica.ObtenerCajasLote(Fecha, Lote));
        }

        [HttpPost]
        public JsonResult CambiarCajasDeLote(int LoteNuevo, string[] Chk)
        {
            return Json(_CajasLogica.CambiarCajasDeLote(LoteNuevo, Chk));
        }

        public ActionResult RegresarCajas()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerInformacionCaja(string CodigoBarras)
        {
            return Json(_CajasLogica.ObtenerInformacionCaja(CodigoBarras));
        }

        public ActionResult ProgramacionLC()
        {
            return View(_CajasLogica.ObtenerProgramacionLC(DateTime.Today.ToShortDateString()));
        }

        [HttpGet]
        public JsonResult ObtenerProgramacionLC(string Fecha)
        {
            return Json(_CajasLogica.ObtenerProgramacionLC(Fecha));
        }

        [HttpPost]
        public JsonResult NuevoLP(string Fecha, int Lote, string Productos)
        {
            int Id_FechaProg = _CajasLogica.LoteProgramado(Fecha, Lote);
            if (Id_FechaProg > 0)
            {
                return Json(new { objeto = new Object(), mensaje = "El lote " + Lote + " ya está programado en la fecha " + Fecha });
            }
            return Json(new { objeto = _CajasLogica.InsertarLoteProgramado(Fecha, Lote, Productos), mensaje = "" });
        }

        [HttpPost]
        public JsonResult EditarLP(int Id, string Productos)
        {
            return Json(_CajasLogica.ActualizarLoteProgramado(Id, Productos));
        }

        [HttpPost]
        public JsonResult EliminarLp(int Id)
        {
            string pMensaje = "";
            bool pResult = _CajasLogica.EliminarLp(Id, out pMensaje);
            return Json(new { Result = pResult, Mensaje = pMensaje });
        }

        [HttpGet]
        public ActionResult MtoMateriaPrima(int Id, string Fecha, int Lote)
        {
            return View(_MateriaPrimaLogica.ObtenerMtoMateriaPrima(Id, Fecha));
        }

        [HttpGet]
        public JsonResult ObtenerTablaMateriaPrima(int Id, int Destino)
        {
            return Json(_MateriaPrimaLogica.ObtenerTablaMateriaPrima(Id, Destino));
        }

        [HttpPost]
        public JsonResult ActualizarEstado(int Id, string Estado)
        {
            return Json(_MateriaPrimaLogica.ActualizarEstadoMateriaPrima(Id, Estado));
        }

        [HttpPost]
        public JsonResult ActualizarSubtipo(int Id, string Subtipo)
        {
            return Json(_MateriaPrimaLogica.ActualizarSubtipoMateriaPrima(Id, Subtipo));
        }

        [HttpGet]
        public JsonResult ObtenerDesgloseMateriaPrima(int IdMatPrima, string Producto)
        {
            return Json(_MateriaPrimaLogica.ObtenerDesgloseMateriaPrima(IdMatPrima, Producto));
        }

        [HttpPost]
        public JsonResult EliminarProductoProduccion(int Id_Salida, string Producto)
        {
            string pMensaje = "";
            bool pResult = _MateriaPrimaLogica.EliminarProductoProduccion(Id_Salida, Producto, out pMensaje);
            return Json(new { Result = pResult, Mensaje = pMensaje });
        }

        [HttpGet]
        public JsonResult ObtenerTablaProgramacionCortes(int Lote, int IdMatPrima, string Fecha)
        {
            return Json(_CajasLogica.ObtenerProgramacionFechaLote(Fecha,Lote));
        }

        [HttpPost]
        public JsonResult AgregarProduccion(int Id, int IdProduccion, string Fecha, int Lote)
        {
            return Json(_MateriaPrimaLogica.AgregarProduccion(Id, IdProduccion, Fecha, Lote));
        }

        [HttpGet]
        public JsonResult ObtenerProduccionLote(int Id, string Fecha, int Lote)
        {
            return Json(_MateriaPrimaLogica.ObtenerProduccionLote(Id, Fecha, Lote));
        }

        [HttpGet]
        public JsonResult TablaInfoCaja(string ACodigo)
        {
            return Json(_CajasLogica.ObtenerCaja(ACodigo));
        }

        public JsonResult RegresarCaja(string ACodigo)
        {
            int id_usuario = (int)HttpContext.Session.GetInt32("IdUsuario");
            string IP = HttpContext.Connection.RemoteIpAddress.ToString();
            bool rest = _CajasLogica.RegresaCaja(IP, id_usuario, ACodigo);
            if (rest)
                return Json("La caja se regreso exitosamente");
            else
                return Json("La caja no tiene salida");
        }

    }
}
