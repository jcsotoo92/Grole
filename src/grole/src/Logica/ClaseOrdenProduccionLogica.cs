using grole.src.Entidades;
using grole.src.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Logica
{
    public class ClaseOrdenProduccionLogica
    {
        public ClaseOrdenProduccionPersistencia _ClaseOrdenProduccionPersistencia { get; set; }

        public ClaseOrdenProduccionLogica(ClaseOrdenProduccionPersistencia AClaseOrdenProduccionPersistencia)
        {
            this._ClaseOrdenProduccionPersistencia = AClaseOrdenProduccionPersistencia;
        }

        public List<ClaseOrdenProduccion> ObtenerLista()
        {
            return _ClaseOrdenProduccionPersistencia.ObtenerLista();
        }

        public ClaseOrdenProduccion ClaseOrdenProduccionInsertar(ClaseOrdenProduccion AClaseOrdenProduccion)
        {
            if (!_ClaseOrdenProduccionPersistencia.ExisteClaseOrdenProduccion(AClaseOrdenProduccion))
                return _ClaseOrdenProduccionPersistencia.ClaseOrdenProduccionInsertar(AClaseOrdenProduccion);
            else
                return null;
        }
        public ClaseOrdenProduccion ClaseOrdenProduccionModificar(ClaseOrdenProduccion AClaseOrdenProduccion)
        {
            if (!_ClaseOrdenProduccionPersistencia.ExisteClaseOrdenProduccion(AClaseOrdenProduccion))
                return _ClaseOrdenProduccionPersistencia.ClaseOrdenProduccionModificar(AClaseOrdenProduccion);
            else
                return null;
        }
        public bool ClaseOrdenProduccionEliminar(int AClave, out string AMensajeError)
        {
            return _ClaseOrdenProduccionPersistencia.ClaseOrdenProduccionEliminar(AClave, out AMensajeError);
        }
    }
}
