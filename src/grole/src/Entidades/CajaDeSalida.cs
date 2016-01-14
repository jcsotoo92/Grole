using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class CajaDeSalida
    {
        public DateTime? Fecha { get; set; }
        public int Folio { get; set; }
        public string CodigoBarras { get; set; }
        public string Producto { get; set; }
        public string Descripcion { get; set; }
        public decimal Peso { get; set; }
        public decimal Tara { get; set; }
        public string Ubicacion { get; set; }
        public int Id_Salida { get; set; }

    }
}
