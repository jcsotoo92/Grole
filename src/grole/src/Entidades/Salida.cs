using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class Salida
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Producto { get; set; }
        public int Cajas { get; set; }
        public decimal Kilos { get; set; }
        public string Concepto { get; set; }
        public int Id_Salida { get; set; }
        public DateTime FechaHoraSistema { get; set; }
        public int Id_Tarima { get; set; }
    }
}
