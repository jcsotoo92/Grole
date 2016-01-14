using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class CambioTara
    {
        public int Id { get; set; }
        public string Producto { get; set; }
        public DateTime? Fecha_Cambio { get; set; }
        public decimal Tara_Anterior { get; set; }
        public decimal Tara_Nueva { get; set; }
        public string Usuario { get; set; }
        public DateTime? FechaHoraSistema { get; set; }

    }
}
