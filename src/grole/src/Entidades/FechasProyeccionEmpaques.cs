using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class FechasProyeccionEmpaques
    {
        public DateTime Fecha { get; set; } 
        public int Count { get; set; }
        public List<PIds> PIds { get; set; }
    }
}
