using Microsoft.AspNet.Mvc;
using grole.src.Entidades;
using grole.src.Logica;
using System.Collections.Generic;
using System;


namespace grole.Controllers
{
    public class RecetasController : Controller
    {
        public RecetasLogica _RecetasLogica { get; set; }

        public RecetasController(RecetasLogica ARecetasLogica)
        {
            this._RecetasLogica = ARecetasLogica;
        }

        //Guarda RecetaM
        [HttpPost]
        public JsonResult guarda_receta_productoM(RecetaM AReceta)
        {
            return Json(_RecetasLogica.guarda_receta_productoM(AReceta));
        }

        //Guarda RecetaD
        [HttpPost]
        public JsonResult guarda_receta_productoD(RecetaD AReceta)
        {
            return Json(_RecetasLogica.guarda_receta_productoD(AReceta));
        }

        //Regresa Lista de Recetas (Productos en la receta)
        
        public JsonResult ObtenerLista_Receta_ProductoD(int Id_Receta)
        {
            return Json(_RecetasLogica.ObtenerLista_Receta_productoD(Id_Receta));
        }

        //Regresa Lista de Recetas M
        
        public JsonResult ObtenerLista_receta_productoM(int Producto)
        {
            return Json(_RecetasLogica.ObtenerLista_receta_productoM(Producto));
        }

        //Eliminar Receta
        public bool eliminarReceta(int Id_Receta)
        {
            return _RecetasLogica.eliminarReceta(Id_Receta);
        }

        //Actualizar RecetaM
        public bool actualizarRecetaM(RecetaM AReceta)
        {
            return _RecetasLogica.actualizarRecetaM(AReceta);
        }

        //Actualizar RecetaD
        public bool actualizarRecetaD(RecetaD AReceta)
        {
            return _RecetasLogica.actualizarRecetaD(AReceta);
        }
        //Genera Clave
        public int GeneraClave(string campo, string tabla)
        {
            return _RecetasLogica.GeneraClave(campo, tabla);
        }

        //Eliminar Producto Receta
        public bool eliminarProductoReceta(int AId)
        {
            return _RecetasLogica.eliminarProductoReceta(AId);
        }
    }
}
