using grole.src.Entidades;
using grole.src.Persistencia;
using System.Collections.Generic;
using System;

namespace grole.src.Logica
{
	public class UsuarioBasculaLogica
	{
		public UsuarioBasculaPersistencia _UsuarioBasculaPersistencia { get; set; }
		
		public UsuarioBasculaLogica(UsuarioBasculaPersistencia AUsuarioBasculaPersistencia)
		{
			this._UsuarioBasculaPersistencia = AUsuarioBasculaPersistencia;
		}
		
		public List<UsuarioBascula> ListaUsuarioBascula()
		{
			return _UsuarioBasculaPersistencia.ListaUsuarioBascula();
		}
		
		
		
		public UsuarioBascula UsuarioBasculaModificar(UsuarioBascula AUsuarioBascula)
		{
			return _UsuarioBasculaPersistencia.UsuarioBasculaModificar(AUsuarioBascula);
		}
		
		public bool UsuarioBasculaEliminar(int AClave, out string AMensajeError)
		{
			return _UsuarioBasculaPersistencia.UsuarioBasculaEliminar(AClave, out AMensajeError);
		}
		
		public UsuarioBascula UsuarioBasculaInsertar(UsuarioBascula AUsuarioBascula)
		{
			return _UsuarioBasculaPersistencia.UsuarioBasculaInsertar(AUsuarioBascula);
		}
		
		public List<UsuarioBascula> busquedaUsuarioBascula(string AFiltrado)
		{
			return _UsuarioBasculaPersistencia.busquedaUsuarioBascula(AFiltrado);
		}
		
	}	
}