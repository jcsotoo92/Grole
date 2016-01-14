using grole.src.Entidades;
using grole.src.Persistencia;
using System.Collections.Generic;

namespace grole.src.Logica
{
	public class DestinosLogica
	{
		public DestinosPersistencia _DestinoPersistencia { get; set; }
		
		public DestinosLogica(DestinosPersistencia ADestinoPersistencia){
			this._DestinoPersistencia = ADestinoPersistencia;
		}
		
		public List<Destino> ObtenerLista(){
			return _DestinoPersistencia.ObtenerLista();
		}
		
		public Destino DestinoInsertar(Destino ADestino){
            if (!_DestinoPersistencia.ExisteDestino(ADestino))
                return _DestinoPersistencia.DestinoInsertar(ADestino);
            else
                return null;
		}
		public Destino DestinoModificar(Destino ADestino){
            if (!_DestinoPersistencia.ExisteDestino(ADestino))
                return _DestinoPersistencia.DestinoModificar(ADestino);
            else
                return null;
		}
		public bool DestinoEliminar(int AClave, out string AMensajeError){
			return _DestinoPersistencia.DestinoEliminar(AClave, out AMensajeError);
		}
		
	}
}