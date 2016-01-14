using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class CorteFecha
    {
        public int Camara { get; set; }
        public string NombreCamara { get; set; }
        public string Embarcado { get; set; }
        public int Cajas { get; set; }
        public decimal Peso { get; set; }
        public int Aplicadas { get; set; }
        public int Transitorias { get; set; }
        public int Diferencia { get; set; }
    }
}
