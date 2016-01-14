using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class SaldoCamara
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        //Valida Productos Camara
        public int Id_Camara { get; set; }
        public string Producto { get; set; }
        public int Cantidad_Maxim { get; set; }
        public decimal Kilos_Maxim { get; set; }
        public string Fecha_Min_Produccion { get; set; }
        public string Fecha_Max_Produccion { get; set; }

        //Saldo Camara
        public int Cajas { get; set; }
        public decimal Kilos { get; set; }
        public string Fecha_Min { get; set; }
        public string Fecha_Max { get; set; }
    }
}
