using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class SaldoPedimento
    {
        public string Producto { get; set; }
        public string Descripcion { get; set; }
        public decimal Kilos { get; set; }
        public decimal Salidas { get; set; }
    }
}
