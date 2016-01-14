using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using grole.src.Logica;
using grole.src.Entidades;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace grole.Controllers
{
    public class EmbarquesController : Controller
    {
        private EmbarquesLogica _EmbarquesLogica;
        private TarimasLogica _TarimasLogica;
        private CajasLogica _CajasLogica;
        private MateriaPrimaLogica _MateriaPrimaLogica;
        public EmbarquesController(EmbarquesLogica _EmbarquesLogica, TarimasLogica _TarimasLogica, CajasLogica _CajasLogica, MateriaPrimaLogica _MateriaPrimaLogica)
        {
            this._EmbarquesLogica = _EmbarquesLogica;
            this._TarimasLogica   = _TarimasLogica;
            this._CajasLogica     = _CajasLogica;
            this._MateriaPrimaLogica = _MateriaPrimaLogica;
        }
        // GET: /<controller>/
        public IActionResult DetalleSalida()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerTablaDetalleSalida(int IdSalida)
        {
            return Json(_EmbarquesLogica.ObtenerDetalleSalida(IdSalida));
        }

        [HttpGet]
        public JsonResult SalidasPorDestino(string Fecha, int Destino, string Tipo, int Id)
        {
            return Json(_EmbarquesLogica.ObtenerSalidasPorDestino(Fecha, Destino));
        }

        [HttpGet]
        public JsonResult TarimasDeSalida(int IdSalida, int Destino, string FechaSalida, string Tipo, int Id)
        {
            return Json(_EmbarquesLogica.ObtenerTarimasDeSalida(IdSalida));
        }

        [HttpGet]
        public JsonResult CajasTarima(string Fecha, int Destino, int FolioTarima, string Tipo, int Id)
        {
            return Json(_TarimasLogica.ObtenerCajasDeTarima(FolioTarima));
        }

        [HttpPost]
        public JsonResult InsertarDetalleSalidaMatPrima(int Id, string Tipo, int Id_Salida)
        {
            List<CajaDeSalida> pCajasTarima = _TarimasLogica.ObtenerCajasDeSalida(Id_Salida);

            string pExistentes = "";
            int pCantidad = 0;

            List<ResumenCajaSalida> pResumenSalida = _CajasLogica.ObtenerResumenCajasSalida(Id_Salida);

            foreach (var item in pResumenSalida)
            {
                _MateriaPrimaLogica.InsertarDetalleMateriaPrima(Id, item.Producto, item.Cajas, item.Kilos, "produccion", Id_Salida, 111);
            }

            foreach (var item in pCajasTarima)
            {
                DetalleMateriaPrimaD pCaja = _MateriaPrimaLogica.ObtenerCajaMatPrima(item.CodigoBarras);

                if (pCaja == null)
                {
                   _MateriaPrimaLogica.InsertarDetalleCajaMatPrima(Id, item.CodigoBarras, item.Peso, item.Producto, Tipo, Id_Salida, item.Folio);
                }
                else
                {
                    pExistentes = pExistentes + item.CodigoBarras + ",";
                    pCantidad++;
                }

            }

            if (pExistentes.Length == 0)
            {
                return Json(new { codigo = 0, mensaje = "Salida agregada correctamente" });
            }
            else if (pCantidad == pCajasTarima.Count)
            {
                return Json(new { codigo = 1, mensaje = "No se agregó la salida porque las cajas ya están ocupadas" });
            }
            else
            {
                return Json(new { codigo = 0, mensaje = "Salida agregada con éxito, pero " + pCantidad + " de cajas no se agregaron de las " + pCajasTarima.Count + " porque estaba ocupadas" });
            }
        }

        [HttpPost]
        public JsonResult InsertarDetalleTarimaMatPrima(int Id, string Tipo, int Id_Salida, int Id_Tarima)
        {
            List<CajaTarima> pCajasTarima = _TarimasLogica.ObtenerCajasDeTarima(Id_Tarima);

            string pExistentes = "";
            int pCantidad = 0;

            foreach (var item in pCajasTarima)
            {
                DetalleMateriaPrimaD pCaja = _MateriaPrimaLogica.ObtenerCajaMatPrima(item.CodigoBarras);

                if (pCaja == null)
                {
                    _MateriaPrimaLogica.InsertarDetalleCajaMatPrima(Id, item.CodigoBarras, item.Peso, item.Producto, Tipo, Id_Salida, Id_Tarima);
                }
                else
                {
                    pExistentes = pExistentes + item.CodigoBarras + ",";
                    pCantidad++;
                }

            }

            if (pExistentes.Length == 0)
            {
                return Json(new { codigo = 0, mensaje = "Tarima agregada correctamente" });
            }
            else if (pCantidad == pCajasTarima.Count)
            {
                return Json(new { codigo = 1, mensaje = "No se agregó la tarima porque las cajas ya están ocupadas" });
            }
            else
            {
                return Json(new { codigo = 0, mensaje = "Tarima agregada con éxito, pero " + pCantidad + " de cajas no se agregaron de las " + pCajasTarima.Count + " porque estaba ocupadas" });
            }
        }

        [HttpPost]
        public JsonResult InsertarDetalleCajaMatPrima(int Id, string CodigoBarras, decimal Peso, string Producto, string Tipo, int Id_Salida, int Id_Tarima)
        {
                DetalleMateriaPrimaD pCaja = _MateriaPrimaLogica.ObtenerCajaMatPrima(CodigoBarras);

                if (pCaja == null)
                {
                    _MateriaPrimaLogica.InsertarDetalleCajaMatPrima(Id, CodigoBarras, Peso, Producto, Tipo, Id_Salida, Id_Tarima);
                    return Json(new { codigo = 0, mensaje = "Caja agregada correctamente" });
                }
                else
                {
                    return Json(new { codigo = 0, mensaje = "No se pudo agregar la caja porque se encuentra ya en el folio " + pCaja.Id_Maestro });
                }

        }
    }
}
