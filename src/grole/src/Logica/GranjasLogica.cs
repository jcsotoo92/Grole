using grole.src.Entidades;
using grole.src.Persistencia;
using System.Collections.Generic;

namespace grole.src.Logica
{
	public class GranjasLogica
	{
		public GranjasPersistencia _GranjaPersistencia { get; set; }
		
		public GranjasLogica(GranjasPersistencia AGranjaPersistencia){
			this._GranjaPersistencia = AGranjaPersistencia;
		}
		
		public List<Granja> ObtenerLista(){
			return _GranjaPersistencia.ObtenerLista();
		}
		
		public Granja GranjaInsertar(Granja AGranja){
            if (!_GranjaPersistencia.ExisteGranja(AGranja))
                return _GranjaPersistencia.GranjaInsertar(AGranja);
            else
                return null;
		}
		public Granja GranjaModificar(Granja AGranja){
            if (!_GranjaPersistencia.ExisteGranja(AGranja))
                return _GranjaPersistencia.GranjaModificar(AGranja);
            else
                return null;
		}
		public bool GranjaEliminar(int AClave, out string AMensajeError){
			return _GranjaPersistencia.GranjaEliminar(AClave, out AMensajeError);
		}

        public Granja ObtenerGranja(int AClave)
        {
            return _GranjaPersistencia.GranjaObtener(AClave);
        }

        public List<Granja> ObtenerGranjas()
        {
            return _GranjaPersistencia.ObtenerGranjas();
        }
    }
}