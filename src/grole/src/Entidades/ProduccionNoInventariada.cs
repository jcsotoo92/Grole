using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class ProduccionNoInventariada
    {
        public DateTime? Fecha { get; set; }
        public int Lote { get; set; }
        public string Producto { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Peso { get; set; }
    }
}
