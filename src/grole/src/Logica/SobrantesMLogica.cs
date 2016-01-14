using grole.src.Entidades;
using grole.src.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Logica
{
    public class SobrantesMLogica
    {
        private SobrantesMPersistencia _SobrantesMPersistencia;
        private SobrantesDPersistencia _SobrantesDPersistencia;

        public SobrantesMLogica (SobrantesMPersistencia _SobrantesMPersistencia, SobrantesDPersistencia _SobrantesDPersistencia)
        {
            this._SobrantesMPersistencia = _SobrantesMPersistencia;
            this._SobrantesDPersistencia = _SobrantesDPersistencia;
        }

        public List<SobrantesM> obtener_lista()
        {
            return this._SobrantesMPersistencia.obtener_lista();
        }
        public SobrantesM obtener(int Aid)
        {
            return this._SobrantesMPersistencia.obtener(Aid);
        }
        public List<SobrantesD> obtener_listaD(int Aid)
        {
            return this._SobrantesDPersistencia.obtener_lista(Aid);
        }

        public List<SobrantesM> obtener_por_producto(int producto)
        {
            return this._SobrantesMPersistencia.obtener_por_producto(producto);
        }

        public bool eliminar(int Aid)
        {
            return this._SobrantesMPersistencia.eliminar(Aid);
        }

        public bool eliminar_por_sobrante(int Aid)
        {
            return this._SobrantesDPersistencia.eliminar_por_sobrante(Aid);
        }
    }
}
