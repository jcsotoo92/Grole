using grole.src.Entidades;
using grole.src.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Logica
{
    public class LotesNoInventariablesLogica
    {
        LotesNoInventariablesPersistencia _LotesNoInventariablesPersistencia;
        public LotesNoInventariablesLogica(LotesNoInventariablesPersistencia _LotesNoInventariablesPersistencia)
        {
            this._LotesNoInventariablesPersistencia = _LotesNoInventariablesPersistencia;
        }
        public List<LoteNoInventariable> ObtenerLotes()
        {
            return _LotesNoInventariablesPersistencia.ObtenerLotes();
        }

        public LoteNoInventariable Insertar(LoteNoInventariable ALote)
        {
            if (!_LotesNoInventariablesPersistencia.ExisteLote(ALote.Lote))
                return _LotesNoInventariablesPersistencia.InsertarLoteNoInventariable(ALote);
            else
                return null;
        }
    }
}
