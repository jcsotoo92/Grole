using grole.src.Entidades;
using grole.src.Logica;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;

namespace grole.Controllers
{
	public class UsuariosController : Controller
	{
		private UsuariosLogica _UsuariosLogica;
		
		public UsuariosController(UsuariosLogica _UsuariosLogica){
			this._UsuariosLogica=_UsuariosLogica;
		}
        [Models.ChecaAutorizacion("31")]
        public ActionResult Index(){
			return View(_UsuariosLogica.ObtenerListaUsuarios());	
		}
        [Models.ChecaAutorizacion("31")]
        [HttpPost]
		public JsonResult Insertar(Usuario Usuario){
			return Json(_UsuariosLogica.InsertarUsuario(Usuario));
		}
        [Models.ChecaAutorizacion("31")]
        [HttpPost]
		public JsonResult Modificar(Usuario Usuario){
			return Json(_UsuariosLogica.ModificarUsuario(Usuario));
		}
        [Models.ChecaAutorizacion("31")]
        [HttpPost]
		public JsonResult Eliminar(int Clave){
			string pMensaje = "";
			bool pResult = _UsuariosLogica.EliminarUsuario(Clave, out pMensaje);
			return Json(new {Result = pResult, Mensaje = pMensaje});	
		}
        [Models.ChecaAutorizacion("31")]
        [HttpGet]
		public JsonResult ObtenerDerechos(){
			return Json(_UsuariosLogica.ObtenerDerechos());
		}
        [Models.ChecaAutorizacion("31")]
        [HttpGet]
		public JsonResult ObtenerDerechosUsuario(int Clave){
			return Json(_UsuariosLogica.ObtenerDerechosUsuario(Clave));
		}
        [Models.ChecaAutorizacion("31")]
        [HttpGet]
		public JsonResult ObtenerDerechosUsuarioTodosLosCampos(int Clave){
			return Json(_UsuariosLogica.ObtenerDerechosUsuarioTodosLosCampos(Clave));
		}
        [Models.ChecaAutorizacion("31")]
        [HttpPost]
		public JsonResult InsertarDerechosUsuario(int Usuario,int[] chk){
			bool pResult = _UsuariosLogica.InsertarDerechosUsuario(Usuario, chk);
			return Json(new {Result = pResult});
		}
        [Models.ChecaAutorizacion("31")]
        [HttpGet]
        public JsonResult ObtenerClasificacionesDerechos()
        {
            return Json(_UsuariosLogica.ObtenerClasificacionesDerechos());
        }
        [Models.ChecaAutorizacion("35")]
        //USUARIOS PISTOLAS
        [HttpGet]
        public ActionResult UsuariosPistolas()
        {
            return View(_UsuariosLogica.ObtenerListaUsuariosPistolas());
        }
        [Models.ChecaAutorizacion("35")]
        [HttpPost]
		public JsonResult InsertarUsuarioPistola(UsuarioPistola Usuario){
            var contra = _UsuariosLogica.InsertarUsuarioPistola(Usuario).Contrasena;

            return Json(_UsuariosLogica.InsertarUsuarioPistola(Usuario));
		}
        [Models.ChecaAutorizacion("35")]
        [HttpPost]
		public JsonResult ModificarUsuarioPistola(UsuarioPistola Usuario){
			return Json(_UsuariosLogica.ModificarUsuarioPistola(Usuario));
		}
        [Models.ChecaAutorizacion("35")]
        [HttpPost]
		public JsonResult EliminarUsuarioPistola(int Clave){
			string pMensaje = "";
			bool pResult = _UsuariosLogica.EliminarUsuarioPistola(Clave, out pMensaje);
			return Json(new {Result = pResult, Mensaje = pMensaje});	
		}
	}
}