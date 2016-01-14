using System;

namespace grole.src.Entidades{
	
	public class Camara
	{
		public int Clave { get; set; }
		public string Descripcion { get; set; }
		public int Columnas { get; set; }
		public int Filas { get; set; }
		public int Profundidad { get; set; }
		public string PermiteSalida { get; set; }
		public string ValidaPosicion { get; set; }
		public string ValidaProductos { get; set; }
		public string Activo { get; set; }
		public string Embarque { get; set; }
		public string FechaEmbarque { get; set; }
		
	}
	
}