using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class InformacionCaja
    {
        public string Fecha { get; set; }
        public Decimal Peso { get; set; }
        public int Bascula { get; set; }
        public int Tarima { get; set; }
        public int Id_Salida { get; set; }
        public string Producto { get; set; }
        public string CodigoBarras { get; set; }
        public string Embarcado { get; set; }
        public string Entrada_Aplicada { get; set; }
        public string Fecha_Sacrificio { get; set; }
}
}
