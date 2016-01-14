using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class LotesPie
    {
        public int Granja { get; set; }
        public string Fecha { get; set; }
        public int Lote { get; set; }
        public int Cantidad { get; set; }
        public decimal Peso { get; set; }
        public string Tipo { get; set; }
        public string Jaula { get; set; }
        public int CerdosObservaciones { get; set; }
        public int CerdosFaltantes { get; set; }
        public string Vehiculo { get; set; }
        public int MuertosEnCorral { get; set; }
        public int MuertosEnTrayecto { get; set; }
        public decimal CostoBajas { get; set; }
        public string Observaciones { get; set; }
        public string HoraRecepcion { get; set; }
        public string HoraLlegada { get; set; }
        public string InicioDescarga { get; set; }
        public string FinDescarga { get; set; }
        public int TiempoEstancia { get; set; }
        public string HoraSalida { get; set; }
        public int TiempoRealDescarga { get; set; }
        public int CanalesRetenidos { get; set; }
        public string NumeroCorrales { get; set; }
        public decimal PesoPromedio { get; set; }
        public string ObservacionesSacrificio { get; set; }
        public string Estatus { get; set; }
        public string MotivoBaja { get; set; }
        public int Bajas { get; set; }
        public int Id { get; set; }
        public float PesoBajas{ get; set; }
        public int Indice { get; set; }

    }
}
