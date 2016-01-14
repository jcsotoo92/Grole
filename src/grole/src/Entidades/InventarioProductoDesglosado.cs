using System;

namespace grole.src.Entidades
{
	
	public class InventarioProductoDesglosado
	{
		public int Folio { get; set; }
		public string Fecha { get; set; }
		public string FechaCodigoBarras { get; set; }
		public string Producto { get; set; }
		public string Descripcion { get; set; }
		public string CodigoBarras { get; set; }
		public decimal Peso { get; set; }
		public int Camara { get; set; }
		public int Tarima { get; set; }
		
	}
	
}