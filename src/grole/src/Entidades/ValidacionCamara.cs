using System;

namespace grole.src.Entidades
{
	
	public class ValidacionCamara
	{
		public int Id { get; set; }
		public int IdCamara { get; set; }
		public string Producto { get; set; }
		public string Descripcion { get; set; }
		public int CantidadMax { get; set; }
		public decimal KilosMax { get; set; }
		public string FechaMinProduccion { get; set; }
		public string FechaMaxProduccion { get; set; }
	}
	
}