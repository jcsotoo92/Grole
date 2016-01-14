using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class ProduccionProducto
    {
        public DateTime? Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Producto { get; set; }
        public int Cajas { get; set; }
        public decimal Kilos { get; set; }
    }
}
