using grole.src.Entidades;
using grole.src.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Logica
{
    public class InformacionCajaLogica
    {
        private InformacionCajaPersistencia _InformacionCajaPersistencia;
        public InformacionCajaLogica(InformacionCajaPersistencia _InformacionCajaPersistencia)
        {
            this._InformacionCajaPersistencia = _InformacionCajaPersistencia;
        }

        public InformacionCaja ObtenerDatosCaja(int Folio, string Fecha)
        {
            return this._InformacionCajaPersistencia.ObtenerDatosCaja(Folio, Fecha);
        }
    }
}
