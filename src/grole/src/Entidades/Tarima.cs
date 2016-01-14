using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class Tarima
    {
        public int Folio { get; set; }
        public DateTime Fecha { get; set; }
        public string Producto { get; set; }
        public int Cajas { get; set; }
        public float Kilos { get; set; }
        public string Ip { get; set; }
        public string Estatus { get; set; }
        public int Lote { get; set; }
        public DateTime HoraCreacion { get; set; }
        public DateTime HoraImpet { get; set; }
        public int Contenedor { get; set; }
        public string Ubicacion { get; set; }
        public DateTime FechaEntrada { get; set; }
        public string Usuario { get; set; }
        public int Id_Salida { get; set; }
        public DateTime FechaHoraSistema { get; set; }
        public DateTime Fecha_Salida { get; set; }
        public decimal PesoReal { get; set; }
        public int Camara_Origen { get; set; }
        public int Orden_Salida { get; set; }
        public int Orden_Salida_P { get; set; }
        public int Lote_Ant { get; set; }
        public string ContenedorDescripcion { get; set; }
    }
}
