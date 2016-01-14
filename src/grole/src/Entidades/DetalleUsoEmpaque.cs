using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class DetalleUsoEmpaque
    {
        public string RProducto { get; set; }
        public string Descripcion { get; set; }
        public int Rempaque { get; set; }
        public string Nombre { get; set; }
        public decimal RCantidad { get; set; }
        public decimal RCantidadEmpaque { get; set; }

    }
}
