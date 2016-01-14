using grole.src.Entidades;
using grole.src.Persistencia;
using System.Collections.Generic;

namespace grole.src.Logica
{
	public class LineasLogica
	{
		private LineasPersistencia _LineaPersistencia;
		
		public LineasLogica(LineasPersistencia ALineaPersistencia)
		{
			this._LineaPersistencia = ALineaPersistencia;
		}
		
		public List<Linea> ObtenerLista()
		{
			return _LineaPersistencia.ObtenerLista();
		}
		
		public Linea LineaInsertar(Linea ALinea)
		{
            if (_LineaPersistencia.ExisteLineaMismoId(ALinea)) {
                Linea pLinea = new Linea();
                pLinea.Clave = -1;
                return pLinea;
            }else if (_LineaPersistencia.ExisteLineaMismoNombre(ALinea))
            {
                return null;
            }else
                return _LineaPersistencia.LineaInsertar(ALinea);
		}
		
		public Linea LineaModificar(Linea ALinea, string AClave)
		{
            if (_LineaPersistencia.ExisteLineaMismoId(ALinea))
            {
                Linea pLinea = new Linea();
                pLinea.Clave = -1;
                return pLinea;
            }
            else if (_LineaPersistencia.ExisteLineaMismoNombre(ALinea))
            {
                return null;
            }
            else
                return _LineaPersistencia.LineaModificar(ALinea, AClave);
        }
		
		public bool LineaEliminar(int AClave, out string AMensajeError)
		{
			return _LineaPersistencia.LineaEliminar(AClave, out AMensajeError);
		}
		
	}
}