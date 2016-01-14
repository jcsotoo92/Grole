using grole.src.Entidades;
using grole.src.Persistencia;
using System.Collections.Generic;
using System;

namespace grole.src.Logica
{
	public class ClasesLogica
	{
		public ClasesPersistencia _ClasesPersistencia { get; set; }
		
		public ClasesLogica(ClasesPersistencia AClasesPersistencia)
		{
			this._ClasesPersistencia = AClasesPersistencia;
		}
		
		//Retorna La Lista de clases
		public List<Clase> ListaClases()
		{
			return _ClasesPersistencia.ListaClases();
		}
		//Ingresa Clase a la Base de Datos
		public Clase ClaseInsertar(Clase AClase)
		{
			return _ClasesPersistencia.ClaseInsertar(AClase);
		}
		//Modifica Clase
		public Clase ClaseModificar(Clase AClase)
		{
			return _ClasesPersistencia.ClaseModificar(AClase);
		}
		//Eliminar Clase
		public bool ClaseEliminar(int AClave, out string AMensajeError)
		{
			return _ClasesPersistencia.ClaseEliminar(AClave, out AMensajeError);
		}
	}	
}