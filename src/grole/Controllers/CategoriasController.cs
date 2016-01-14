using Microsoft.AspNet.Mvc;
using grole.src.Entidades;
using grole.src.Logica;
using System.Collections.Generic;
using System;

namespace grole.Controllers
{
    [Models.ChecaAutorizacion("43")]
    public class CategoriasController : Controller
    {
        public CategoriasLogica _CategoriasLogica { get; set; }

        public CategoriasController(CategoriasLogica ACategoriasLogica)
        {
            this._CategoriasLogica = ACategoriasLogica;
        }

        //Retorna la lista de Categorias
        public JsonResult ListaCategorias()
        {
            List<Categoria> pListaCategorias = _CategoriasLogica.ListaCategorias();
            return Json(pListaCategorias);
        }

        public ActionResult Index()
        {
            return View(_CategoriasLogica.ListaCategorias());
        }

        [HttpPost]
        public JsonResult Insertar(Categoria Categoria)
        {
            return Json(this._CategoriasLogica.CategoriaInsertar(Categoria));
        }

        [HttpPost]
        public JsonResult Modificar(Categoria Categoria)
        {
            return Json(this._CategoriasLogica.CategoriaModificar(Categoria));
        }

        [HttpPost]
        public JsonResult Eliminar(int Clave)
        {
            string pMensaje = "";
            bool pResult = _CategoriasLogica.CategoriaEliminar(Clave, out pMensaje);
            return Json(new { Result = pResult, Mensaje = pMensaje });
        }
    }
}
