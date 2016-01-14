using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class CCall
    {
        public int Clave { get; set; }
        public int Granja { get; set; }
        public int Lote { get; set; }
        public DateTime? Fecha { get; set; }
        public Int16 Canales { get; set; }
        public Int16 Acumulados { get; set; }
    }
}
