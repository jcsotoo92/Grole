using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class EmpaqueProducto
    {
        public int Id { get; set; }
        public string ClaveProducto { get; set; }
        public int IdEmpaque { get; set; }
        public decimal Cantidad { get; set; }
    }
}
