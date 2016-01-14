using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class Bascula
    {
        public int Clave { get; set; }
        public int Puerto { get; set; }
        public int Baud { get; set; }
        public int DataBits { get; set; }
        public int Paridad { get; set; }
        public int StopBits { get; set; }
        public string Descripcion { get; set; }
        public int Tipo { get; set; }
        public Int16 TabSheet { get; set; }
        public Int16 Grupo { get; set; }
        public Int16 Activo { get; set; }
        public string Script { get; set; }
        public Int16 PesoTara { get; set; }
        public DateTime? FecCaducidad { get; set; }
        public string TipoBascula { get; set; }
        public float PesoMinimo { get; set; }
        public float PesoMaximo { get; set; }
        public float PesoFijo { get; set; }
    }
}
