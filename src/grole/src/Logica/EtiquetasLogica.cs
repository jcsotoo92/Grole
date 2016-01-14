using grole.src.Entidades;
using grole.src.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Logica
{
    public class EtiquetasLogica
    {
        private CajasPersistencia _CajasPersistencia;

        public EtiquetasLogica(CajasPersistencia _CajasPersistencia)
        {
            this._CajasPersistencia = _CajasPersistencia;
        }

        public List<Corte> ObtenerCajasPorFolio(int AFolio)
        {
            return _CajasPersistencia.ObtenerCajasPorFolio(AFolio);
        }

        public List<ProduccionNoInventariada> ObtenerProduccionNoInventariadas(string AProducto, string AFechaIni, string AFechaFin)
        {
            return _CajasPersistencia.ObtenerProduccionNoInventariadas(AProducto, AFechaIni, AFechaFin);
        }

        public bool EliminarProduccionNoInventariable(string AFecha, int ALote, string AProducto, out string AMensajeError)
        {
            return _CajasPersistencia.EliminarProduccionNoInventariable(AFecha, ALote, AProducto, out AMensajeError);
        }
    }
}
