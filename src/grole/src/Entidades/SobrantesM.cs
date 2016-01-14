using System.Collections.Generic;
namespace grole.src.Entidades
{
    public class SobrantesM
    {
        public int id { get; set; }
        public string producto{ get; set; }
        public string activo { get; set; }
        public string fechahorasistema { get; set; }
        public string descripcion { get; set; }
        public List <SobrantesD> detalle { get; set; }
    }
}
