using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class DetalleCorteFecha
    {
        public int Folio { get; set; }
        public int Lote { get; set; }
        public string Producto { get; set; }
        public string Descripcion { get; set; }
        public string CodigoBarras { get; set; }
        public string Embarcado { get; set; }
        public decimal Peso { get; set; }
        public int Tarima { get; set; }
        public string Ubicacion { get; set; }
        public string Entrada_Aplicada { get; set; }
        public int Id_Acum { get; set; }
    }
}
