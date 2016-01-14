using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class TablaProyeccionProduccion
    {
        public List<FechasProyeccionEmpaques> Fechas { get; set; }
        public List<ProyeccionProduccion> Lista { get; set; }
        public List<ProyeccionProduccion> ListaDetalle { get; set; }
    }
}
