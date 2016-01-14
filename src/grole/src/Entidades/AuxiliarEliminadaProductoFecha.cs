using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class AuxiliarEliminadaProductoFecha
    {
        public DateTime Fecha  { get; set; }
        public string Producto { get; set; }
        public string Descripcion { get; set; }
        public int Reetiquetadas_Cajas { get; set; }
        public decimal Reetiquetadas_Kilos { get; set; }
        public int Eliminadas_Cajas { get; set; }
        public decimal Eliminadas_Kilos { get; set; }
    }
}
