using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class ProgramacionCorteD
    {
        public int Id {get; set; }
        public int Id_FechaProg { get; set; }
        public int Lote { get; set; }
        public string Productos { get; set; }
        public DateTime? FechaHoraSistema { get; set; }
        public DateTime? FechaHoraModificacion { get; set; }
    }
}
