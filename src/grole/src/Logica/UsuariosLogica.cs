using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using System;
using grole.src.Persistencia;
using grole.src.Entidades;

namespace grole.src.Logica
{
	public class UsuariosLogica
	{
		UsuariosPersistencia _UsuariosPersistencia;
		public UsuariosLogica(UsuariosPersistencia _UsuariosPersistencia){
			this._UsuariosPersistencia=_UsuariosPersistencia;
		}
		
		public List<Usuario> ObtenerListaUsuarios(){
			return _UsuariosPersistencia.ObtenerListaUsuarios();
		}
		
		public Usuario InsertarUsuario(Usuario AUsuario){
			if(!_UsuariosPersistencia.ExisteUsuario(AUsuario)){
				AUsuario.Nombre = nombreUpperCase(AUsuario.Nombre);
				return _UsuariosPersistencia.InsertarUsuario(AUsuario);
			}else{
				return null;
			}
		}
		
		public Usuario ModificarUsuario(Usuario AUsuario){
			if(!_UsuariosPersistencia.ExisteUsuarioSinSerYo(AUsuario)){
				AUsuario.Nombre = nombreUpperCase(AUsuario.Nombre);
				return _UsuariosPersistencia.ModificarUsuario(AUsuario);
			}else{
				return null;
			}
		}
		
		public bool EliminarUsuario(int AClave, out string AMensajeError){
			return _UsuariosPersistencia.EliminarUsuario(AClave,out AMensajeError);
		}

		public List<DerechoUsuario> ObtenerDerechos(){
			return _UsuariosPersistencia.ObtenerDerechos();
		}

		public List<int> ObtenerDerechosUsuario(int AClave){
			return _UsuariosPersistencia.ObtenerDerechosUsuario(AClave);
		}
		
		public List<DerechoUsuario> ObtenerDerechosUsuarioTodosLosCampos(int AClave){
			return _UsuariosPersistencia.ObtenerDerechosUsuarioTodosLosCampos(AClave);
		}
		
		private string nombreUpperCase(string AUsuario){
			string [] nombres=AUsuario.Split(' ');
			string nombre="";
			
			foreach(string item in nombres){
				nombre+=char.ToUpper(item[0]) + item.Substring(1) + " ";
			}
			
			return nombre.Trim();
		}

        public bool InsertarDerechosUsuario(int AUsuario, int[] AChk)
        {
            return _UsuariosPersistencia.InsertarDerechosUsuario(AUsuario,AChk);
        }

        public List<String> ObtenerClasificacionesDerechos()
        {
            return _UsuariosPersistencia.ObtenerClasificacionesDerechos();
        }
        

        //USUARIOS PISTOLAS
        public List<UsuarioPistola> ObtenerListaUsuariosPistolas()
        {
            return _UsuariosPersistencia.ObtenerListaUsuariosPistolas();
        }

        public string ObtenerDerechosEnString(int AUsuario){
			List<int> derechos = ObtenerDerechosUsuario(AUsuario);
			string cadena="";
			foreach(var item in derechos){
				cadena+=item+".";
			}
			
			return cadena.Remove(cadena.Length - 1);
		}
		
		public UsuarioPistola InsertarUsuarioPistola(UsuarioPistola AUsuario){
				AUsuario.Nombre = nombreUpperCase(AUsuario.Nombre);
				return _UsuariosPersistencia.InsertarUsuarioPistola(AUsuario);
		}
		
		public UsuarioPistola ModificarUsuarioPistola(UsuarioPistola AUsuario){
				AUsuario.Nombre = nombreUpperCase(AUsuario.Nombre);
				return _UsuariosPersistencia.ModificarUsuarioPistola(AUsuario);
		}
		
		public bool EliminarUsuarioPistola(int AClave, out string AMensajeError){
			return _UsuariosPersistencia.EliminarUsuarioPistola(AClave,out AMensajeError);
		}
    }			
}