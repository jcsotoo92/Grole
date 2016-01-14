using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class AreaCortes
    {
        public int Id { get; set; }
        public int Id_Area { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public string Producto { get; set; }
        public int Lote { get; set; }
        public Decimal Peso { get; set; }
        public Decimal PesoNeto { get; set; }
        public int Cantidad { get; set; }

    }
}
