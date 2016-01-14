using grole.src.Entidades;
using grole.src.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Logica
{
    public class CortesLogica
    {
        private CajasPersistencia _CajasPersistencia;
        private SalidaInventarioPersistencia _SalidaInventarioPersistencia;
        private BasculasPersistencia _BasculasPersistencia;
        private ResumenCortesPersistencia _ResumenCortesPersistencia;

        public CortesLogica(CajasPersistencia _CajasPersistencia, SalidaInventarioPersistencia _SalidaInventarioPersistencia,BasculasPersistencia _BasculasPersistencia, ResumenCortesPersistencia _ResumenCortesPersistencia)
        {
            this._CajasPersistencia            = _CajasPersistencia;
            this._SalidaInventarioPersistencia = _SalidaInventarioPersistencia;
            this._BasculasPersistencia         = _BasculasPersistencia;
            this._ResumenCortesPersistencia    = _ResumenCortesPersistencia;
        }

        public List<SaldoPedimento> ObtenerSaldosPedimento(int APedimento)
        {
            return _CajasPersistencia.ObtenerSaldosPedimento(APedimento);
        }

        public List<CorteFecha> ObtenerCorteFecha(string AFecha)
        {
            return _CajasPersistencia.ObtenerCorteFecha(AFecha);
        }

        public int ObtenerPendientesCorte(string AFecha)
        {
            return _CajasPersistencia.ObtenerPendientesCorte(AFecha);
        }

        public List<DetallePendienteCorte> ObtenerDetallePendientesCorte(string AFecha)
        {
            return _CajasPersistencia.ObtenerDetallePendientesCorte(AFecha);
        }

        public List<DetalleCorteFecha> ObtenerDetalleCorteFecha(string AFecha, int ACamara, string AEmbarcado)
        {
            return _CajasPersistencia.ObtenerDetalleCorteFecha(AFecha, ACamara, AEmbarcado);
        }

        public List<SalidaDelDia> ObtenerSalidaDelDia(string AFechaIni, string AFechaFin)
        {

            return _SalidaInventarioPersistencia.ObtenerSalidaDelDia(AFechaIni, AFechaFin).OrderBy(x => int.Parse(x.Clave)).ToList();
        }

        public List<Bascula> ObtenerBasculasActivas()
        {
            return _BasculasPersistencia.ObtenerBasculasActivas();
        }

        public List<ProduccionPorBascula> ObtenerProduccionPorBascula(string AFecha, int ABascula)
        {
            return _CajasPersistencia.ObtenerProduccionPorBascula(AFecha,ABascula);
        }
        
        public List<AreaCortes> ObtenerAreasUsuario(int AId)
        {
            _ResumenCortesPersistencia.ObtenerLotesArea(1);
            return _ResumenCortesPersistencia.ObtenerAreasUsuario(AId);
        }

        public List<AreaCortes> ObtenerResumenCortesArea(DateTime AFecha, int AArea)
        {
            return _ResumenCortesPersistencia.ObtenerResumenCortesArea(AFecha, AArea);
        }
        public string CambiarResumenVerificado(DateTime AFecha, int AArea, int AUsuario)
        {
            return _ResumenCortesPersistencia.CambiarResumenVerificado(AFecha, AArea, AUsuario);
        }
        public Boolean ProduccionVerificada(DateTime AFecha, int AArea)
        {
            return _ResumenCortesPersistencia.ProduccionVerificada(AFecha, AArea);
        }

        public List<ClasificacionCorte> ObtenerListaClasificaciones()
        {
            return _ResumenCortesPersistencia.ObtenerListaClasificaciones();
        }
        public List<AreaCortes> ObtenerResumenCortesAdmin(DateTime AFecha, string ALotes)
        {
            return _ResumenCortesPersistencia.ObtenerResumenCortesAdmin(AFecha, ALotes);
        }

        public AreaCortes ObtenerResumenCortesPorId(int AId)
        {
            return _ResumenCortesPersistencia.ObtenerResumenCortesPorId(AId);
        }

        public string CambiaFechaProduccion(DateTime fecha, int id, DateTime fecha_ant)
        {
            return _ResumenCortesPersistencia.CambiaFechaProduccion(fecha, id, fecha_ant);
        }
    }
}
