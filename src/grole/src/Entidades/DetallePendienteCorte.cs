using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class DetallePendienteCorte
    {
        public int Folio { get; set; }
        public DateTime? Fecha { get; set; }
        public string Producto { get; set; }
        public string Descripcion { get; set; }
        public string CodigoBarras { get; set; }
        public int Lote { get; set; }
        public decimal Peso { get; set; }
    }
}
