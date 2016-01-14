using grole.src.Entidades;
using grole.src.Persistencia;
using System.Collections.Generic;
using System.Linq;

namespace grole.src.Logica
{
	public class CamarasLogica{
		CamarasPersistencia _CamaraPersistencia;
		public CamarasLogica (CamarasPersistencia _CamaraPersistencia){
			this._CamaraPersistencia=_CamaraPersistencia;
		}
		
		public List<Camara> ObtenerCamaras(){
			return _CamaraPersistencia.ObtenerCamaras();
		}
		
		public List<ValidacionCamara> ObtenerValidacionesCamaras(string AClave){
			return _CamaraPersistencia.ObtenerValidacionesCamaras(AClave);
		}
	
		public Camara CamaraInsertar(Camara ACamara){
            if (_CamaraPersistencia.ExisteCamaraMismoId(ACamara))
            {
                Camara pCamara = new Camara();
                pCamara.Clave = -1;
                return pCamara;
            }
            else if (_CamaraPersistencia.ExisteCamaraMismoNombre(ACamara))
            {
                return null;
            }
            else
                return _CamaraPersistencia.CamaraInsertar(ACamara);
        }
		
		public Camara CamaraModificar(Camara ACamara){
            if (_CamaraPersistencia.ExisteCamaraMismoId(ACamara))
            {
                Camara pCamara = new Camara();
                pCamara.Clave = -1;
                return pCamara;
            }
            else if (_CamaraPersistencia.ExisteCamaraMismoNombre(ACamara))
            {
                return null;
            }
            else
                return _CamaraPersistencia.CamaraInsertar(ACamara);
        }
		
		public bool CamaraEliminar(int AClave, out string AMensajeError){
			return _CamaraPersistencia.CamaraEliminar(AClave,out AMensajeError);
		}
		
		public bool CamaraEliminarValidacion(int AClave, out string AMensajeError){
			return _CamaraPersistencia.CamaraEliminarValidacion(AClave,out AMensajeError);
		}
		
		public ValidacionCamara InsertarValidacionCamara(int ACamara, string AProducto, int ACantidad, decimal AKilos, string AFechaMin, string AFechaMax){
			if(!_CamaraPersistencia.ExisteProductoEnValidacion(ACamara,AProducto,AFechaMin,AFechaMax))
				return _CamaraPersistencia.InsertarValidacionCamara(ACamara,AProducto,ACantidad,AKilos,AFechaMin,AFechaMax);
			else
				return null;
			
		}

        public List<Camara> ObtenerCamarasActivas()
        {
            return _CamaraPersistencia.ObtenerCamarasActivas().OrderBy(x => x.Clave).ToList();
        }
        public Camara ObtenerCamara(int AClave)
        {
            return _CamaraPersistencia.ObtenerCamara(AClave);
        }

        public List<Camara> ObtenerCamarasActivasOrdenadasPorNombre()
        {
            return _CamaraPersistencia.ObtenerCamarasActivas().OrderBy(x => x.Descripcion).ToList();
        }

        public List<SaldoCamara> obtener_lista_camaras_catalogo_activas()
        {
            return _CamaraPersistencia.obtener_lista_camaras_catalogo_activas();
        }

        public List<SaldoCamara> obtener_validaciones_camara(int ACamara)
        {
            return _CamaraPersistencia.obtener_validaciones_camara(ACamara);
        }

        public SaldoCamara obtener_saldo_camara_producto(int AProducto, int ACamara)
        {
            return _CamaraPersistencia.obtener_saldo_camara_producto(AProducto, ACamara);
        }
    }
	
}