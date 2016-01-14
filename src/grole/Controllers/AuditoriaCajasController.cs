using Microsoft.AspNet.Mvc;
using grole.src.Entidades;
using grole.src.Logica;
using System.Collections.Generic;

namespace grole.Controllers
{
    public class AuditoriaCajasController : Controller
    {
        
        //Retorna la vista de Clase
        public ActionResult Index()
        {
            return View();
        }
        
    }
}
