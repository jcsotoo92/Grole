using grole.src.Logica;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Routing;
using System.Collections.Generic;

namespace grole.Controllers
{
	public class MenuController : Controller
	{
		private UsuariosLogica _UsuariosLogica;
		
		public MenuController(UsuariosLogica _UsuariosLogica){
			this._UsuariosLogica=_UsuariosLogica;
		}
		
		[HttpGet]
		public ActionResult Index(int Usuario){
                return View(_UsuariosLogica.ObtenerDerechosUsuarioTodosLosCampos(Usuario));
        }
	}
	
}