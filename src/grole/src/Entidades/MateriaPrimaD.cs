using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class MateriaPrimaD
    {
        public int Id { get; set; }
        public int Id_MatPrima { get; set; }
        public string Producto { get; set; }
        public decimal Cajas { get; set; }
        public decimal Kilos { get; set; }
        public string Tipo { get; set; }
        public int Id_Salida { get; set; }
        public int Id_Destino { get; set; }
        public string SubTipo { get; set; }
        public string Descripcion { get; set; }
    }
}
