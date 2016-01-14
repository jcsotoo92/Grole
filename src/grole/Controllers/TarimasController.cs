using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using grole.src.Logica;
using Microsoft.AspNet.Http;
using grole.src.Entidades;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace grole.Controllers
{
    public class TarimasController : Controller
    {
        private CamarasLogica _CamarasLogica;
        private TarimasLogica _TarimasLogica;
        public TarimasController(CamarasLogica _CamarasLogica, TarimasLogica _TarimasLogica)
        {
            this._CamarasLogica = _CamarasLogica;
            this._TarimasLogica = _TarimasLogica;
        }
        // GET: /<controller>/
        public ActionResult TarimasPorCamara()
        {
            return View(_CamarasLogica.ObtenerCamarasActivas());
        }

        [HttpGet]
        public JsonResult ObtenerTarimasCamara(int[] CamarasChk)
        {
            return Json(_TarimasLogica.ObtenerTarimasCamara(CamarasChk));
        }

        [HttpGet]
        public ActionResult CajasTarima(int FolioTarima, int Camara)
        {
            return View(_TarimasLogica.ObtenerCajasTarima(FolioTarima,Camara));
        }

        public ActionResult ListadoTarimas()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerListadoTablaTarimas(string Fecha, int Lote)
        {
            return Json(_TarimasLogica.ObtenerTarimasLote(Fecha, Fecha, Lote, Lote));
        }

        public ActionResult Traspasar(int FolioTarima, int Camara, string Ubicacion)
        {
            return View(_CamarasLogica.ObtenerCamarasActivasOrdenadasPorNombre());
        }

        [HttpPost]
        public void TraspasarTarima(int Camara, int Folio, string Ubicacion)
        {
            _TarimasLogica.TraspasarTarima(Camara, Folio, Ubicacion, HttpContext.User.Identity.Name, HttpContext.Connection.RemoteIpAddress.ToString());
           
        }

        public ActionResult RegresarTarimas()
        {
            return View();
        }

        [HttpGet]
        public JsonResult InformacionGeneral(int FolioTarima)
        {
            Tarima pTarima = _TarimasLogica.ObtenerTarima(FolioTarima);
            List<Salida> pDatosSalidas = _TarimasLogica.ObtenerDatosSalidaTarima(FolioTarima);

            if (pTarima == null)
            {
                return Json(new {codigo = "<p>La tarima con el folio <strong>" + FolioTarima + "</strong> no se encontró<p/> " +
                        "<br/> " +
                        "<input type = \"button\" id = \"btnCancelar\" value = \"Cancelar\" onclick = \"cancelar();\"> ", tarima = pTarima });
            }

            if (pTarima.Estatus != "S")
            {
                return Json(new
                {
                    codigo = "<p>La tarima con el folio <strong>" + FolioTarima + "</strong> no está embarcada<p/>" +
                      "<br/> " +
                      "<input type = \"button\" id = \"btnCancelar\" value = \"Cancelar\" onclick = \"cancelar()\"> ",
                    tarima = pTarima
                });
            }

            if(pTarima.Fecha < new DateTime(2012, 03, 03))
            {
                return Json(new
                {
                    codigo = "<p>No se puede regresar tarimas cuya fecha de salida sea mayor o igual al <strong>3 de Marzo 2012</strong> la fecha de la tarima con el folio <strong>" + FolioTarima + "</strong>" +
                        "es <strong>" + pTarima.Fecha.ToShortDateString() + "</strong><p/> " +
                      "<br/> " +
                      "<input type = \"button\" id = \"btnCancelar\" value = \"Cancelar\" onclick = \"cancelar()\"> ",
                    tarima = pTarima
                });
            }
            if (pDatosSalidas.Count == 0)
            {
                return Json(new { codigo = "<p>La tarima no tiene datos de salida</p><br/><input type=\"button\" id=\"btnCancelar\" value=\"Cancelar\" onclick=\"cancelar(); \">", tarima = pTarima });
            }
            
            return Json(new { codigo = "", tarima = pTarima });
        }
        [HttpPost]
        public JsonResult RegresarTarima(int FolioTarima, string Motivo)
        {
            if(_TarimasLogica.RegresarTarima(FolioTarima, Motivo, HttpContext.User.Identity.Name) > 0)
            {
                return Json(new { mensaje = "Se regresó la tarima con éxito" });
            }
            else
            {
                return Json(new { mensaje = "Hubo algún problema, reportelo al dpto. de sistemas"});
            }
        }
    }
}
