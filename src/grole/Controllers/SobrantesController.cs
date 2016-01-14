using Microsoft.AspNet.Mvc;
using grole.src.Entidades;
using grole.src.Logica;
using System.Collections.Generic;
using System;

namespace grole.Controllers
{
    public class SobrantesController: Controller
    {
        private SobrantesMLogica _SobrantesMLogica;
        public SobrantesController(SobrantesMLogica _SobrantesMLogica)
        {
            this._SobrantesMLogica = _SobrantesMLogica;
        }
        //Retorna la vista de Clase
        public ActionResult Index()
        {
            return View();
        }

        [Route("/rest/sobrantes")]
        public JsonResult RestSobrantes()
        {
            return Json(_SobrantesMLogica.obtener_lista());
        }

        [Route("/rest/sobrantes/{id:int}")]
        public JsonResult ResSobrantaesID(int id)
        {
            SobrantesM SobComb = _SobrantesMLogica.obtener(id);
            SobComb.detalle = _SobrantesMLogica.obtener_listaD(id);
            return Json(SobComb);
        }

        [Route("/sobrantes/{id:int}")]
        public ActionResult detalle_sobrantes(int id)
        {
            return View(_SobrantesMLogica.obtener(id));
        }

        [Route("/rest/existe_sobrantes_producto/{producto:int}")]
        public JsonResult existe_sobrantes_producto(int producto)
        {
            Console.WriteLine("Ingreso a existe sobrantes producto");
            List<SobrantesM> pSobranteMtmp = _SobrantesMLogica.obtener_por_producto(producto);
                if (pSobranteMtmp == null)
                    return Json(false);
                else
                    return Json(true);
        }

        [Route("/rest/sobrantes/eliminar/{id:int}")]
        public JsonResult eliminar(int id)
        {
            bool p_result = _SobrantesMLogica.eliminar(id);
            p_result      = _SobrantesMLogica.eliminar_por_sobrante(id);
            return Json(p_result);
        }



       /* [Route("/rest/sobrantes/insertar")] // Como el insertar recive los datos MODULO_SOBRANTES.rb Linea 58
        public JsonResult insertar()
        {

        }*/
    }
}
