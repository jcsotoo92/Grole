

namespace grole.src.Entidades
{
    public class OrdenProduccion
    {
        public string Producto { get; set; }
        public string CodigoSap { get; set; }
        public string Descripcion { get; set; }
        public string Embarcado { get; set; }
        public int ID_Salida { get; set; }
        public int Cajas { get; set; }
        public decimal Kilos { get; set; }
    }
}
