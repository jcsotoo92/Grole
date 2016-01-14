using grole.src.Entidades;
using grole.src.Persistencia;
using System.Collections.Generic;
using System;

namespace grole.src.Logica
{
    public class CategoriasLogica
    {
        public CategoriasPersistencia _CategoriasPersistencia { get; set; }

        public CategoriasLogica(CategoriasPersistencia ACategoriasPersistencia)
        {
            this._CategoriasPersistencia = ACategoriasPersistencia;
        }

        //Retorna Lista de Categorias
        public List<Categoria> ListaCategorias()
        {
            return _CategoriasPersistencia.ListaCategorias();
        }

        public List<Categoria> ObtenerLista()
        {
            return _CategoriasPersistencia.ObtenerLista();
        }

        public Categoria CategoriaInsertar(Categoria ACategoria)
        {
            if (!_CategoriasPersistencia.ExisteCategoria(ACategoria))
                return _CategoriasPersistencia.CategoriaInsertar(ACategoria);
            else
                return null;
        }
        public Categoria CategoriaModificar(Categoria ACategoria)
        {
            if (!_CategoriasPersistencia.ExisteCategoria(ACategoria))
                return _CategoriasPersistencia.CategoriaModificar(ACategoria);
            else
                return null;
        }
        public bool CategoriaEliminar(int AClave, out string AMensajeError)
        {
            return _CategoriasPersistencia.CategoriaEliminar(AClave, out AMensajeError);
        }
    }
}