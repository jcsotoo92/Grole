using System;

namespace grole.src.Entidades
{
	
	public class Producto
	{
		public string Clave { get; set; }
		public string Descripcion { get; set; }
		public int Almacen  { get; set; }
		public string Clave_Alm { get; set; }
		public string Mercado { get; set; }
		public string Codbarras { get; set; }
		public float Pesofijo { get; set; }
		public string Clase { get; set; }
		public float Pesotara { get; set; }
		public float Diascad { get; set; }
		public string Linea { get; set; }
		public float Pesomaximo { get; set; }
		public float Pesominimo { get; set; }
		public float Rendimiento { get; set; }
		public int Grupo { get; set; }
		public int Operador { get; set; }
		public int Fondo { get; set; }
		public int Tapa { get; set; }
		public int Pliego { get; set; }
		public int Panal { get; set; }
		public int Separador { get; set; }
		public int Bolsa { get; set; }
		public int Huleespuma { get; set; }
		public int Palillos { get; set; }
		public int Huleburbuja { get; set; }
		public int Otros { get; set; }
		public int OtrosDos { get; set; }
		public string Fondo_D { get; set; }
		public string Tapa_D { get; set; }
		public string Pliego_D { get; set; }
		public string Panal_D { get; set; }
		public string Separador_D { get; set; }
		public string Bolsa_D { get; set; }
		public string Huleespuma_D { get; set; }
		public string Palillos_D { get; set; }
		public string Huleburbuja_D { get; set; }
		public string Otros_D { get; set; }
		public string OtrosDos_D { get; set; }
		public string Temperatura { get; set; }
		public int Copias { get; set; }
		public string AplicaTemperatura { get; set; }
		public string CodigoProveedor { get; set; }
		public int Camara_Default { get; set; }
		public string Posicion_Default { get; set; }
		public string Usa_Camara_Default { get; set; }
		public decimal Etiqueta { get; set; }
		public string Etiqueta_D { get; set; }
		public string Inventariable { get; set; }
		public int Formato_Etiqueta { get; set; }
		public string Calcula_Dias_Cad { get; set; }
		public int Decimales { get; set; }
		public string CodigoSap { get; set; }
		public int Tipo_Redondeo { get; set; }
		public int Dias_Sacrificio { get; set; }
		public string Moldeado { get; set; }
		public string Fecha_Sacrificio { get; set; }
		public int Tipo_Fecha_Sacrificio { get; set; }
		public DateTime? Fecha_Produccion { get; set; }
		public int Tipo_Fecha_Produccion { get; set; }
		public int Tipo_Fecha_Caducidad { get; set; }
		public DateTime? Fecha_Caducidad { get; set; }
		public decimal Kilos_Por_Caja { get; set; }
		public string Captura_Pedimento { get; set; }
		public int Categoria { get; set; }
		public DateTime? FechaHoraSistema { get; set; }
		public string Usuario_Cambio { get; set; }
		public string Fec_Cad_Manual { get; set; }
		public int Decimales_Etiqueta { get; set; }
		public string Lectura_E1 { get; set; }
		public string Lectura_E2 { get; set; }
		public string Lectura_E3 { get; set; }
		public string Lectura_Sagarpa { get; set; }
		public string Piezas_Por_Caja { get; set; }
	}
	
}