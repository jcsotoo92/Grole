using System.Collections.Generic;
using grole.src.Persistencia;
using grole.src.Entidades;
using System.Linq;
using System;

namespace grole.src.Logica
{
	public class CajasLogica{
		CajasPersistencia _CajasPersistencia;
        OrdenesProduccionPersistencia _OrdenesProduccionPersistencia;
        MateriaPrimaPersistencia _MateriaPrimaPersistencia;
        EliminadasPersistencia _EliminadasPersistencia;
		public CajasLogica (EliminadasPersistencia _EliminadasPersistencia, CajasPersistencia _CajasPersistencia, OrdenesProduccionPersistencia _OrdenesProduccionPersistencia, MateriaPrimaPersistencia _MateriaPrimaPersistencia)
        {
			this._CajasPersistencia             = _CajasPersistencia;
            this._OrdenesProduccionPersistencia = _OrdenesProduccionPersistencia;
            this._MateriaPrimaPersistencia      = _MateriaPrimaPersistencia;
            this._EliminadasPersistencia        = _EliminadasPersistencia;
		}
		
		public InventarioProducto ObtenerInventarioProducto(string AFechaIni, string AFechaFin, string AProducto)
		{
			return _CajasPersistencia.ObtenerInventarioProducto(AFechaIni, AFechaFin, AProducto);
		}
		
		public List<InventarioProductoDesglosado> ObtenerInventarioProductoDesglosado(string AFechaIni, string AFechaFin, string AProducto)
		{
			return _CajasPersistencia.ObtenerInventarioProductoDesglosado(AFechaIni, AFechaFin, AProducto);
		}

        public List<ProduccionNoInventariada> ObtenerProduccionNoInventariadasPorProducto(string AProducto, string AFechaIni, string AFechaFin)
        {
            return _CajasPersistencia.ObtenerProduccionNoInventariadasPorProducto(AProducto, AFechaIni, AFechaFin);
        }

        public List<DesgloseProductoCamara> ObtenerDesgloseProductoPorCamara(string AProducto)
        {
            return _CajasPersistencia.ObtenerDesgloseProductoPorCamara(AProducto);
        }

        public List<ResumenProductoPorCamara> ObtenerResumenPtosCamara(int[] ACamara)
        {
            List<ResumenProductoPorCamara> pLista = new List<ResumenProductoPorCamara>();

            foreach(var item in ACamara)
            {
                pLista.AddRange(_CajasPersistencia.ObtenerResumenPtosCamara(item));
            }
            pLista = pLista.OrderBy(x => x.Producto).ToList();
            return pLista;
        }

        public List<Corte> ObtenerDetalleProductosCamara(int ACamara, string AProducto)
        {
            return _CajasPersistencia.ObtenerDetalleProductosCamara(ACamara, AProducto);
        }

        public List<CajaPendienteRecepcionEmbarque> ObtenerCajasPendientesRecepcionEmbarques(string AFecha)
        {
            return _CajasPersistencia.ObtenerCajasPendientesRecepcionEmbarques(AFecha);
        }

        public List<Corte> ObtenerDetalleCajasPendientesRecepcionEmbarques(string AFecha, string AProducto)
        {
            return _CajasPersistencia.ObtenerDetalleCajasPendientesRecepcionEmbarques(AFecha, AProducto);
        }

        public List<CajaLote> ObtenerCajasLote(string AFecha, int ALote)
        {
            return _CajasPersistencia.ObtenerCajasLote(AFecha, ALote);
        }

        public int CambiarCajasDeLote(int ALoteNuevo, string[] AChk)
        {
            int pAffected = 0;
            foreach(var item in AChk)
            {
                pAffected += _CajasPersistencia.CambiarCajaDeLote(ALoteNuevo, item);
            }
            return pAffected;
        }

        public Corte ObtenerInformacionCaja(string ACodigoBarras)
        {
            return _CajasPersistencia.ObtenerCaja(ACodigoBarras);
        }

        public List<ProgramacionCorteD> ObtenerProgramacionLC(string AFecha)
        {
            if(AFecha != null)
            {
                return _CajasPersistencia.ObtenerProgramacionLCFecha(AFecha);
            }
            else
            {
                return _CajasPersistencia.ObtenerProgramacionLCFecha(DateTime.Today.ToShortDateString());
            }
        }

        public ProgramacionCorteD ObtenerProgramacionLCIndividual(int AId)
        {
            return _CajasPersistencia.ObtenerProgramacionLCIndividual(AId);
        }

        public ProgramacionCorteD InsertarLoteProgramado(string AFecha, int ALote, string AProductos)
        {
            int Id_Maestro = _CajasPersistencia.ObtenerIdFechaProgramada(AFecha);

            if(_CajasPersistencia.InsertarLoteProgramado(Id_Maestro, ALote, AProductos) > 0)
            {
                return ObtenerProgramacionLCIndividual(_OrdenesProduccionPersistencia.ObtenerUltimoIndice("DRASPROGCORTED"));
            }
            else
            {
                return null;
            }
        }

        public int LoteProgramado(string AFecha, int ALote)
        {
            return _CajasPersistencia.LoteProgramado(AFecha, ALote);
        }

        public ProgramacionCorteD ActualizarLoteProgramado(int AId, string Productos)
        {
            return _CajasPersistencia.ActualizarLoteProgramado(AId, Productos);
        }

        public bool EliminarLp(int AId, out string pMensaje)
        {
            return _CajasPersistencia.EliminarLp(AId, out pMensaje);
        }

        public List<ResumenCajaSalida> ObtenerResumenCajasSalida(int AIdSalida)
        {
            return _CajasPersistencia.ObtenerResumenCajasSalida(AIdSalida);
        }

        public List<ProgramacionFechaLote> ObtenerProgramacionFechaLote(string AFecha, int ALote)
        {
            return _CajasPersistencia.ObtenerProgramacionFechaLote(AFecha, ALote);
        }

        public Corte ObtenerDatosCaja(int AFolio, string AFecha)
        {
            return _CajasPersistencia.ObtenerDatosCaja(AFolio, AFecha);

        }

        public bool BorrarEtiqueta(string ACodigoBarras, string AMotivo, string ACodigoAlta, int AUsuario, out string AMensajeError)
        {

            Corte caja = _CajasPersistencia.ObtenerCaja(ACodigoBarras);
            if (caja.Embarcado == "Si")
            {
                AMensajeError = "Caja Embarcada";
                return false;
            }
            bool res = _CajasPersistencia.BorrarEtiqueta(ACodigoBarras, AMotivo, ACodigoAlta, AUsuario, out AMensajeError);
            _EliminadasPersistencia.inserta_eliminada(caja, AMotivo, ACodigoAlta, AUsuario);
            _CajasPersistencia.EliminaCajaDeTarima(ACodigoBarras, out AMensajeError);

            return res;
        }

        public Corte ObtenerCaja(string ACodigoBarras)
        {
            return _CajasPersistencia.ObtenerCaja(ACodigoBarras);
        }

        public bool RegresaCaja(string AIp, int AUsuario, string ACodigoBbarras)
        {
            return _CajasPersistencia.RegresaCaja(AIp, AUsuario, ACodigoBbarras);
        }
    }
}