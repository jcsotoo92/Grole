using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class SalidaPorDestino
    {
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public int Cajas { get; set; }
        public decimal Kilos { get; set; }
        public string Concepto { get; set; }
        public string Destino { get; set; }
    }
}
