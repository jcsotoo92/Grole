using grole.src.Entidades;
using grole.src.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Logica
{
    public class Valores_RescateLogica
    {
        private Valores_RescatePersistencia _Valores_RescatePersistencia;
        private ProductosPersistencia _ProductoPersistencia;

        public Valores_RescateLogica(Valores_RescatePersistencia _Valores_RescatePersistencia, ProductosPersistencia _ProductoPersistencia)
        {
            this._Valores_RescatePersistencia = _Valores_RescatePersistencia;
            this._ProductoPersistencia = _ProductoPersistencia;
        }

        public List<ValoresRescate> obtener_lista_desc()
        {
            return this._Valores_RescatePersistencia.obtener_lista_desc();
        }
        public List<ValoresRescateD> obtener_lista(int folio)
        {
            return this._Valores_RescatePersistencia.obtener_lista(folio);
        }

        public ValoresRescate Obtener(int AId)
        {
            return this._Valores_RescatePersistencia.Obtener(AId);
        }

        public Producto ObtenerDescripcion(string AId)
        {
            return _ProductoPersistencia.ObtenerProducto(AId);
        }
    }
}
