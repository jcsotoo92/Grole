using grole.src.Entidades;
using grole.src.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Logica
{
    public class RastreoFoliosLogica
    {

        private RastreoFoliosPersistencia _RastreoFoliosPersistencia;

        public RastreoFoliosLogica(RastreoFoliosPersistencia _RastreoFoliosPersistencia)
        {
            this._RastreoFoliosPersistencia = _RastreoFoliosPersistencia;
        }

        public List<RastreoFolio> ObtenerRastreoFolios(int Folio, int Producto)
        {
            return this._RastreoFoliosPersistencia.ObtenerRastreoFolios(Folio, Producto);
        }
    }
}
