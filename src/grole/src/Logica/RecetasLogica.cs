using grole.src.Entidades;
using grole.src.Persistencia;
using System;
using System.Collections.Generic;

namespace grole.src.Logica
{
    public class RecetasLogica
    {
        public RecetasPersistencia _RecetasPersistencia { get; set; }

        public RecetasLogica(RecetasPersistencia ARecetasPersistencia)
        {
            this._RecetasPersistencia = ARecetasPersistencia;
        }

        //Guardar RecetaM
        public RecetaM guarda_receta_productoM(RecetaM AReceta)
        {
            return _RecetasPersistencia.guarda_receta_productoM(AReceta);
        }

        //Guardar RecetaD
        public RecetaD guarda_receta_productoD(RecetaD AReceta)
        {
            return _RecetasPersistencia.guarda_receta_productoD(AReceta);
        }

        //Regresa lista de recetas(Productos en receta)
        public List<RecetaD> ObtenerLista_Receta_productoD(int Id_Receta)
        {
            return _RecetasPersistencia.ObtenerLista_receta_productoD(Id_Receta);
        }

        //Regresa lisa de recetas M
        public List<RecetaM> ObtenerLista_receta_productoM(int Producto)
        {
            return _RecetasPersistencia.ObtenerLista_receta_productoM(Producto);
        }
        //Eliminar Receta
        public bool eliminarReceta(int Id_Receta)
        {
            return _RecetasPersistencia.eliminarReceta(Id_Receta);
        }

        //Actualizar RecetaM
        public bool actualizarRecetaM(RecetaM AReceta)
        {
            return _RecetasPersistencia.actualizarRecetaM(AReceta);
        }

        //Actualizar RecetaD
        public bool actualizarRecetaD(RecetaD AReceta)
        {
            return _RecetasPersistencia.actualizarRecetaD(AReceta);
        }

        //Genera Clave
        public int GeneraClave(string campo, string tabla)
        {
            return _RecetasPersistencia.GeneraClave(campo, tabla);
        }

        //Eliminar Producto Receta
        public bool eliminarProductoReceta(int AId)
        {
            return _RecetasPersistencia.eliminarProductoReceta(AId);
        }
    }
}
