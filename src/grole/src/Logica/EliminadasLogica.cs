using grole.src.Entidades;
using grole.src.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Logica
{
    public class EliminadasLogica
    {
        private EliminadasPersistencia _EliminadasPersistencia;
        private ProductosLogica _ProductosLogica;

        public EliminadasLogica(EliminadasPersistencia _EliminadasPersistencia, ProductosLogica _ProductosLogica)
        {
            this._EliminadasPersistencia = _EliminadasPersistencia;
            this._ProductosLogica = _ProductosLogica;
        }

        public List<AuxiliarEliminadaProductoFecha> ObtenerAuxiliarEliminadas(string AProducto, string AFechaIni, string AFechaFin)
        {
            List<Producto> listaProductos = new List<Producto>();

            if (!AProducto.Equals("-1"))
            {
                Producto prod = new Producto();
                prod.Descripcion = _ProductosLogica.DameDescripcionProducto(AProducto);
                listaProductos.Add(prod);
            }

            return _EliminadasPersistencia.ObtenerAuxiliarEliminadas(listaProductos, AFechaIni, AFechaFin);
        }

        public Boolean inserta_eliminada(Corte ACaja, string AMotivo, string ACodigoAlta, int AUsuario)
        {
            return _EliminadasPersistencia.inserta_eliminada(ACaja, AMotivo, ACodigoAlta, AUsuario);
        }
    }
}
