using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class RecetaD
    {
        public int Id { get; set; }
        public int Id_Receta { get; set; }
        public string Producto { get; set; }
        public float Rendimiento { get; set; }
        public string DescripcionProducto { get; set; }
    }
}
