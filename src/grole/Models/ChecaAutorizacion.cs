using System.Security.Claims;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using System;
using grole.src.Logica;
using Microsoft.AspNet.Mvc.Filters;

namespace grole.Models
{
    public class ChecaAutorizacion : ActionFilterAttribute
    {
        public string _Derecho;
        private UsuariosLogica _UsuariosLogica;

        public ChecaAutorizacion(string ADerecho) : base() {
            this._Derecho = ADerecho;
        }
        
        public ChecaAutorizacion(UsuariosLogica _UsuariosLogica){
            this._UsuariosLogica = _UsuariosLogica;
        }

        public override void OnActionExecuting(ActionExecutingContext context) {
            base.OnActionExecuting(context);
            
            string derechos = context.HttpContext.Session.GetString("Derechos");
            
            if (string.IsNullOrEmpty(context.HttpContext.User.Identity.Name))
            {
                context.Result = new RedirectToActionResult("login", "accounts", null);
                return;
            }
            else{
                bool pPasa = false;
                foreach (var item in context.HttpContext.User.Claims) {
                    if (_Derecho.Equals(item.Value) && item.Type == ClaimTypes.Role){
                        pPasa = true;
                        break;
                    }
                };

                if (!pPasa) {
                    context.Result = new RedirectToActionResult("login", "accounts", null);
                }
            }
        }
    }
}
