using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class DetalleMateriaPrimaD
    {
        public int Id { get; set; }
        public int Id_Maestro { get; set; }
        public string CodigoBarras { get; set; }
        public decimal Peso { get; set; }
        public string Tipo { get; set; }
        public string Producto { get; set; }
        public int Id_Salida { get; set; }
        public int Id_Tarima { get; set; }
    }
}
