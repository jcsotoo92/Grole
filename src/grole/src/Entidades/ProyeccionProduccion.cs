using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class ProyeccionProduccion
    {
        public int Id { get; set; }
        public DateTime Fecha_Embarque { get; set; }
        public string Producto { get; set; }
        public string CodigoSAP { get; set; }
        public string Descripcion { get; set; }
        public string Descripcion2 { get; set; }
        public int Cajas { get; set; }
        public int Inventario { get; set; }
    }
}
