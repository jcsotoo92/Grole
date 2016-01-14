using Microsoft.AspNet.Mvc;
using grole.src.Entidades;
using grole.src.Logica;
using System.Collections.Generic;
using System;

namespace grole.Controllers
{
    public class Valores_RescateController: Controller
    {
        private Valores_RescateLogica _Valores_RescateLogica;
        public Valores_RescateController(Valores_RescateLogica _Valores_RescateLogica)
        {
            this._Valores_RescateLogica = _Valores_RescateLogica;
        }

        //Retorna la vista de Clase
        public ActionResult Index()
        {
            return View();
        }
        [Route ("/rest/rescate/ValorRescate_obtener_lista")]
        public JsonResult ValorRescate_obtener_lista()
        {
            return Json(_Valores_RescateLogica.obtener_lista_desc());
        }

        [Route("/cat_valor_rescate/{id:int}")]
        public ActionResult cat_valor_rescate(int id)
        {
            ValoresRescate pValoresRescate = new ValoresRescate();
            if (id == 0)
            {
                Console.WriteLine("ingresamos aqui");
                pValoresRescate.fecha = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00 -0700";
                pValoresRescate.fechafinal = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00 -0700";
                pValoresRescate.descripcion = "";
                pValoresRescate.id = 0;
                pValoresRescate.activo = "Si";
                pValoresRescate.fechahorasistema = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00 -0700";
            }
            else
            {
                pValoresRescate = _Valores_RescateLogica.Obtener(id);
            }
            return View(pValoresRescate);
        }
        [Route("/rest/rescate/ValorRescateD_obtener_lista/{folio:int}")]
        public JsonResult ValorRescateD_ObtenerLista(int folio)
        {
            if (folio == 0)
            {
                return Json("");
            }
            else
            {
                return Json(_Valores_RescateLogica.obtener_lista(folio));
            }
        }

        [Route("/rest/productos/obtener_descripcion/{id:int}")]
        public JsonResult obtener_descripcion(string id)
        {
            Console.WriteLine("valor de obtener producto: "+id);
           Producto pProducto = _Valores_RescateLogica.ObtenerDescripcion(id);

            if (pProducto != null)
                return Json(new { clave = pProducto.Clave, descripcion = pProducto.Descripcion });
            else
                return Json(new { clave = '0', descripcion = "???"});

        }
        /*
        [Route("/rest/rescate/ValorRescateD_insertar")]
        public JsonResult ValorRescateD_insertar()
        {

        }*/


    }
}
