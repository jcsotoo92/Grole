using Microsoft.AspNet.Mvc;
using System.Security.Claims;
using Microsoft.AspNet.Authentication.Cookies;
using System;
using grole.src.Logica;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;

namespace grole.Controllers
{
	
	public class AccountsController : Controller
	{
		
		private AccountsLogica _AccountsLogica;
		private UsuariosLogica _UsuariosLogica;
		public AccountsController(AccountsLogica _AccountsLogica, UsuariosLogica _UsuariosLogica){
			this._AccountsLogica = _AccountsLogica;
			this._UsuariosLogica = _UsuariosLogica;
		}
		
		[HttpGet]
		public ActionResult Login(){
            if (!string.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                return RedirectToAction("Index", new RouteValueDictionary(
               new { controller = "menu", action = "Index", Usuario = HttpContext.Session.GetInt32("IdUsuario") }));

            } else
                return View(); 
		}
		
		[HttpPost]
		public JsonResult Login(string Usuario, string Password){
                int estado = _AccountsLogica.ChecarUsuario(Usuario, Password);

                if (estado == 1)
                {
                    return Json(new { Resultado = 1, Mensaje = "Usuario no existe" });
                }
                else if (estado == 2)
                {
                    return Json(new { Resultado = 2, Mensaje = "Password incorrecto" });
                }

                int idUsuario = _AccountsLogica.ObtenerIdUsuario(Usuario);
                var pCI = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, Usuario), new Claim(ClaimTypes.Role, "Logger") });
                foreach (var item in _UsuariosLogica.ObtenerDerechosUsuario(idUsuario))
                {
                    pCI.AddClaim(new Claim(ClaimTypes.Role, item + ""));
                }
                var user = new ClaimsPrincipal(pCI);
                var f = HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);

            HttpContext.Session.SetInt32("IdUsuario", idUsuario);

                f.Wait();
                return Json(new { Resultado = 0, Mensaje = "Login exitoso", IdUsuario = idUsuario });
            
		}
		
		public ActionResult Logout(){
			try
			{
				var f = HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
				f.Wait();
				return RedirectToAction("Login");
			}
			catch(Exception ex)
			{
				return RedirectToAction("Error");
			}

        }

	
	}
}