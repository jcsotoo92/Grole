using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class Empaque
    {
        public int Clave { get; set; }
        public int IdTipoEmpaque { get; set; }
        public string Nombre { get; set; }
        public string CodigoSAP { get; set; }
        public decimal Costo { get; set; }
    }
}
