using grole.src.Entidades;
using grole.src.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Logica
{
    public class ClasificacionCortesLogica
    {
        public ClasificacionCortesPersistencia _ClasificacionCortesPersistencia { get; set; }

        public ClasificacionCortesLogica(ClasificacionCortesPersistencia AClasificacionCortePersistencia)
        {
            this._ClasificacionCortesPersistencia = AClasificacionCortePersistencia;
        }

        public List<ClasificacionCorte> ObtenerLista()
        {
            return _ClasificacionCortesPersistencia.ObtenerLista();
        }

        public ClasificacionCorte ClasificacionCorteInsertar(ClasificacionCorte AClasificacionCorte)
        {
            if(!_ClasificacionCortesPersistencia.ExisteClasificacionCorte(AClasificacionCorte))
                return _ClasificacionCortesPersistencia.ClasificacionCorteInsertar(AClasificacionCorte);
            else
                return null;
        }
        public ClasificacionCorte ClasificacionCorteModificar(ClasificacionCorte AClasificacionCorte)
        {
            if (!_ClasificacionCortesPersistencia.ExisteClasificacionCorte(AClasificacionCorte))
                return _ClasificacionCortesPersistencia.ClasificacionCorteModificar(AClasificacionCorte);
            else
                return null;
        }
        public bool ClasificacionCorteEliminar(int AClave, out string AMensajeError)
        {
            return _ClasificacionCortesPersistencia.ClasificacionCorteEliminar(AClave, out AMensajeError);
        }

    }
}
