using grole.src.Entidades;
using grole.src.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Logica
{
    public class OrdenesProduccionLogica
    {
        private OrdenesProduccionPersistencia _OrdenesProduccionPersistencia;

        public OrdenesProduccionLogica(OrdenesProduccionPersistencia _OrdenesProduccionPersistencia)
        {
            this._OrdenesProduccionPersistencia = _OrdenesProduccionPersistencia;
        }

        public List<TarimaOrdenProduccion> ObtenerInformacionTarimaOrdenP(int AIdTarima)
        {
            return _OrdenesProduccionPersistencia.ObtenerInformacionTarimaOrdenP(AIdTarima);
        }

        public List<OrdenProduccion> ObtenerInformacionOrdenP(int AOrdenP)
        {
            return _OrdenesProduccionPersistencia.ObtenerInformacionOrdenP(AOrdenP);
        }
    }
}
