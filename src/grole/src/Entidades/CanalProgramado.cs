using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class CanalProgramado
    {
        public DateTime? Fecha { get; set; }
        public int Clave_Granja { get; set; }
        public string Granja { get; set; }
        public int Lote { get; set; }
        public Int16 Canales { get; set; }
        public Int16 Acumulados { get; set; }
    }
}
