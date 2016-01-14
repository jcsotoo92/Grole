using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class ResumenProductoPorCamara
    {
        public string Producto { get; set; }
        public string Descripcion { get; set; }
        public string CodigoSAP { get; set; }
        public int Cajas { get; set; }
        public decimal Kilos { get; set; }
        public int Camara { get; set; }
    }
}
