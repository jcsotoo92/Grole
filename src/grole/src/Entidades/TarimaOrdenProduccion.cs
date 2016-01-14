using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class TarimaOrdenProduccion
    {
        public int Id { get; set; }
        public int Id_Orden { get; set; }
        public int Id_Mat_Prima { get; set; }
        public string Producto { get; set; }
        public string CodigoBarras { get; set; }
        public decimal Peso { get; set; }
        public int Tarima_Origen { get; set; }
        public string Usuario_Pistola { get; set; }
        public DateTime? FechaHoraSistema { get; set; }
        public int Id_Salida_Embarques { get; set; }
    }
}
