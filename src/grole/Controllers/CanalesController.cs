using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using grole.src.Logica;
using grole.src.Entidades;

namespace grole.Controllers
{
    public class CanalesController : Controller
    {
        private CanalesLogica _CanalesLogica;
        private GranjasLogica _GranjasLogica;

        public CanalesController(CanalesLogica _CanalesLogica, GranjasLogica _GranjasLogica)
        {
            this._CanalesLogica = _CanalesLogica;
            this._GranjasLogica = _GranjasLogica;
        }

        public ActionResult ProgramacionSacrificio()
        {
            DateTime fecha = new DateTime();
            fecha = DateTime.Today;
            return View(_CanalesLogica.ObtenerListaCanalesProgramados(fecha.ToShortDateString()));
        }

        [HttpGet]
        public JsonResult ObtenerTablaCanalesProgramados(string Fecha)
        {
            return Json(_CanalesLogica.ObtenerListaCanalesProgramados(Fecha));
        }


        [HttpPost]
        public JsonResult InsertarLoteSacrificio(CCall Canal)
        {
            return Json(_CanalesLogica.InsertarLoteSacrificio(Canal));
        }

        public ActionResult CortarCanales()
        {
            List<Granja> pListaGranjas = _GranjasLogica.ObtenerGranjas();
            return View(pListaGranjas);
        }

        public JsonResult DescortarCanal(int AGranja, DateTime AFecha, int ALote, int ACanal)
        {

            Console.WriteLine("Resultado: " + AGranja + AFecha + ALote + ACanal);
            //existe = canal_existe ? (params[:Granja], params[:Fecha], params[:Lote], params[:Canal])
            Boolean existe = _CanalesLogica.CanalExiste(AGranja, AFecha, ALote, ACanal);
            if (existe)
                return Json(_CanalesLogica.DescortarCanal(AGranja, AFecha, ALote, ACanal));
            else
                return Json(false);
        }

        public ActionResult LotesPie(string Fecha, int Granja)
        {

            if (Granja == 0 && Fecha == null)
            {
                ViewBag.Fecha = DateTime.Today.ToString("dd/MM/yyyy");
                return View(_GranjasLogica.ObtenerGranjas());
            }
            else
            {
                ViewBag.Fecha = Fecha;
                ViewBag.Granja = Granja;
                return View(_GranjasLogica.ObtenerGranjas());
            }
        }
        public JsonResult TablaLotesPie(string Fecha, int Granja)
        {
            return Json(_CanalesLogica.TablaLotesPie(Fecha, Fecha, Granja));
        }

        public ActionResult NuevoLotePie(string Fecha, int Granja)
        {
            Granja pGranja = _GranjasLogica.ObtenerGranja(Granja);
            ViewBag.Fecha = Fecha;
            ViewBag.Granja = Granja;
            ViewBag.Nombre = pGranja.Nombre;
            return View();
        }

        public JsonResult EliminarLotePie(string AFecha, int AGranja, int ALote)
        {
            return Json(_CanalesLogica.EliminarLotePie(AFecha, AGranja, ALote));
        }

        public ActionResult AgregarBajasLotePie(string AFecha, int AGranja, int ALote)
        {
            Granja pGranja = _GranjasLogica.ObtenerGranja(AGranja);
            ViewBag.Granja = AGranja; ViewBag.Nombre = pGranja.Nombre;

            LotesPie pLotesPie = _CanalesLogica.ObtenerLoteEnPie(AFecha, AFecha, AGranja, ALote);
            if (pLotesPie.Bajas == -1 )
            { ViewBag.Bajas = ""; }
            else { ViewBag.Bajas = pLotesPie.Bajas; }
            ViewBag.Fecha = AFecha; ViewBag.Lote = ALote; ViewBag.PesoBajas = pLotesPie.PesoBajas; ViewBag.MotivoBaja = pLotesPie.MotivoBaja;
            return View();
        }

        public ActionResult InsertarLotePie(int Granja, string Fecha, int Lote, int Cantidad, decimal Peso, string Tipo, string Jaula, int CerdosObservacion, int CerdosFaltantes, string Vehiculo, int MuertosEnCorral, int MuertosEnTrayecto, decimal CostoBajas, string Observaciones, string HoraRecepcion, string HoraLlegada, string InicioDescarga, string FinDescarga, int TiempoEstancia, string HoraSalida, int TiempoRealDescarga, int CanalesRetenidos, string NumeroCorrales, decimal PesoPromedio, string ObservacionesSacrificio)
        {
            _CanalesLogica.InsertarLotePie(Granja, Fecha, Lote, Cantidad, Peso, Tipo, Jaula, CerdosObservacion, CerdosFaltantes, Vehiculo, MuertosEnCorral, MuertosEnTrayecto, CostoBajas, Observaciones, HoraRecepcion, HoraLlegada, InicioDescarga, FinDescarga, TiempoEstancia, HoraSalida, TiempoRealDescarga, CanalesRetenidos, NumeroCorrales, PesoPromedio, ObservacionesSacrificio);
            return RedirectToAction("LotesPie", "Canales", new { Fecha = Fecha, Granja = Granja });

        }
        public ActionResult ModificarLotePie(int Granja, string Fecha, int Lote, int Cantidad, decimal Peso, string Tipo, string Jaula, int CerdosObservacion, int CerdosFaltantes, string Vehiculo, int MuertosEnCorral, int MuertosEnTrayecto, decimal CostoBajas, string Observaciones, string HoraRecepcion, string HoraLlegada, string InicioDescarga, string FinDescarga, int TiempoEstancia, string HoraSalida, int TiempoRealDescarga, int CanalesRetenidos, string NumeroCorrales, decimal PesoPromedio, string ObservacionesSacrificio)
        {
            _CanalesLogica.ModificarLotePie(Granja, Fecha, Lote, Cantidad, Peso, Tipo, Jaula, CerdosObservacion, CerdosFaltantes, Vehiculo, MuertosEnCorral, MuertosEnTrayecto, CostoBajas, Observaciones, HoraRecepcion, HoraLlegada, InicioDescarga, FinDescarga, TiempoEstancia, HoraSalida, TiempoRealDescarga, CanalesRetenidos, NumeroCorrales, PesoPromedio, ObservacionesSacrificio);
            return RedirectToAction("LotesPie", "Canales", new { Fecha = Fecha, Granja = Granja });

        }
        public ActionResult EditarLotePie(string Dia, string Mes, string Año, int AGranja, int ALote)
        {

            string AFecha = Dia + "/" + Mes + "/" + Año;
            Console.WriteLine(AFecha);
            Granja pGranja = _GranjasLogica.ObtenerGranja(AGranja);
            ViewBag.Granja = AGranja; ViewBag.Nombre = pGranja.Nombre; ViewBag.Fecha = AFecha;
            LotesPie pLotePie = _CanalesLogica.ObtenerLoteEnPieMod(AFecha, AFecha, AGranja, ALote);
            return View(pLotePie);
        }

        public ActionResult AgregaBajaLotePie(int Granja, string Fecha, int Lote, short Bajas, float PesoBaja, string MotivoBaja)
        {
            _CanalesLogica.AgregaBajaLotePie(Granja, Fecha, Lote, Bajas, PesoBaja, MotivoBaja);

            return RedirectToAction("LotesPie", "Canales", new { Fecha = Fecha, Granja = Granja });
        }
        public ActionResult QuitarBajasLotePie(int Granja, string Fecha, int Lote, short Bajas, float PesoBaja, string MotivoBaja)
        {
            Console.WriteLine("Fecha: " + Fecha + " Granja=" + Granja);
            _CanalesLogica.AgregaBajaLotePie(Granja, Fecha, Lote, Bajas, PesoBaja, MotivoBaja);

            return RedirectToAction("LotesPie", "Canales", new { Fecha = Fecha, Granja = Granja });
        }
    }
}
