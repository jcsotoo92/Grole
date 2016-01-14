using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class MateriaPrimaM
    {
        public int Id { get; set; }
        public int Id_Prog { get; set; }
        public string Producto { get; set; }
        public string Descripcion { get; set; }
        public decimal Decomiso_Kilos { get; set; }
        public decimal Merma { get; set; }
        public decimal Rendimiento { get; set; }
        public decimal Decomiso_Kilos_Moldeo { get; set; }
        public decimal Merma_Moldeo { get; set; }
        public decimal Decomiso_Moldeo { get; set; }
        public decimal Rendimiento_Moldeo { get; set; }
        public string Estado { get; set; }

    }
}
