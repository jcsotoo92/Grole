using grole.src.Entidades;
using grole.src.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Logica
{
    public class CanalesLogica
    {
        private CanalesPersistencia _CanalesPersistencia;
        
        public CanalesLogica(CanalesPersistencia _CanalesPersistencia)
        {
            this._CanalesPersistencia = _CanalesPersistencia;
        }

        public List<CanalProgramado> ObtenerListaCanalesProgramados(string AFecha)
        {
            return _CanalesPersistencia.ObtenerListaCanalesProgramados(AFecha).OrderBy(x=> x.Clave_Granja).ToList();
        }

        public CanalProgramado InsertarLoteSacrificio(CCall ACanal)
        {
            return _CanalesPersistencia.InsertarLoteSacrificio(ACanal);
        }

        public Boolean CanalExiste(int AGranja, DateTime AFecha, int ALote, int ACanal)
        {
            return _CanalesPersistencia.CanalExiste(AGranja, AFecha, ALote, ACanal);
        }

        public Boolean DescortarCanal(int AGranja, DateTime AFecha, int ALote, int ACanal)
        {
            return _CanalesPersistencia.DescortarCanal(AGranja, AFecha, ALote, ACanal);
        }

        public List<LotesPie> TablaLotesPie(string AFechaIni, string AFechaFin, int AGranja)
        {
            return _CanalesPersistencia.ObtenerListaLotesEnPie(AFechaIni, AFechaFin, AGranja);
        }
        public Boolean EliminarLotePie(string AFecha, int AGranja, int ALote)
        {
            return _CanalesPersistencia.EliminarLotePie(AFecha, AGranja, ALote);
        }

        public LotesPie ObtenerLoteEnPie(string AFechaIni, string AFechaFin, int AGranja, int Lote)
        {
            return _CanalesPersistencia.ObtenerLoteEnPie(AFechaIni, AFechaFin, AGranja, Lote);
        }
        public bool InsertarLotePie(int AGranja, string AFecha, int ALote, int ACantidad, decimal AKilos, string ATipo, string AJaula, int ACerdosObservacion, int ACerdosFaltantes, string AVehiculo, int AMuertosEnCorral, int AMuertosEnTrayecto, decimal ACostoBajas, string AObservaciones, string AHoraRecepcion, string AHoraLlegada, string AInicioDescarga, string AFinDescarga, int ATiempoEstancia, string AHoraSalida, int ATiempoRealDescarga, int ACanalesRetenidos, string ANumeroCorrales, decimal APesoPromedio, string AObservacionesSacrificio)
        {
            return _CanalesPersistencia.InsertarLotePie(AGranja, AFecha, ALote, ACantidad, AKilos, ATipo, AJaula, ACerdosObservacion, ACerdosFaltantes, AVehiculo, AMuertosEnCorral, AMuertosEnTrayecto, ACostoBajas, AObservaciones, AHoraRecepcion, AHoraLlegada, AInicioDescarga, AFinDescarga, ATiempoEstancia, AHoraSalida, ATiempoRealDescarga, ACanalesRetenidos, ANumeroCorrales, APesoPromedio, AObservacionesSacrificio);
        }
        public bool ModificarLotePie(int AGranja, string AFecha, int ALote, int ACantidad, decimal AKilos, string ATipo, string AJaula, int ACerdosObservacion, int ACerdosFaltantes, string AVehiculo, int AMuertosEnCorral, int AMuertosEnTrayecto, decimal ACostoBajas, string AObservaciones, string AHoraRecepcion, string AHoraLlegada, string AInicioDescarga, string AFinDescarga, int ATiempoEstancia, string AHoraSalida, int ATiempoRealDescarga, int ACanalesRetenidos, string ANumeroCorrales, decimal APesoPromedio, string AObservacionesSacrificio)
        {
            return _CanalesPersistencia.ModificarLotePie(AGranja, AFecha, ALote, ACantidad, AKilos, ATipo, AJaula, ACerdosObservacion, ACerdosFaltantes, AVehiculo, AMuertosEnCorral, AMuertosEnTrayecto, ACostoBajas, AObservaciones, AHoraRecepcion, AHoraLlegada, AInicioDescarga, AFinDescarga, ATiempoEstancia, AHoraSalida, ATiempoRealDescarga, ACanalesRetenidos, ANumeroCorrales, APesoPromedio, AObservacionesSacrificio);
        }
        public LotesPie ObtenerLoteEnPieMod(string AFechaIni, string AFechaFin, int AGranja, int ALote)
        {
            return _CanalesPersistencia.ObtenerLoteEnPieMod(AFechaIni, AFechaFin, AGranja, ALote);
        }

        public bool AgregaBajaLotePie(int AGranja, string AFecha, int ALote, short ABaja, float APesoBaja, string AMotivoBaja)
        {
            return _CanalesPersistencia.AgregaBajaLotePie(AGranja, AFecha, ALote, ABaja, APesoBaja, AMotivoBaja);
        }
    }
}