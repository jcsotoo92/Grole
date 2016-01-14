using System;

namespace grole.src.Entidades{
	
	public class DerechoUsuario
	{
		public int Clave { get; set; }
		public int IdUsuario { get; set; }
		public int IdDerecho { get; set; }
		public string Controlador { get; set; }
		public string Menu { get; set; }
		public string Clasificacion { get; set; }
		public string PuedeModificarOrden { get; set; }
		public string PuedeEliminarOrden { get; set; }
		
	}
	
}