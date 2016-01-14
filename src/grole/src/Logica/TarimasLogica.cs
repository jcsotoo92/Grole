using grole.src.Entidades;
using grole.src.Persistencia;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;

namespace grole.src.Logica
{
    public class TarimasLogica
    {
        private TarimasPersistencia _TarimasPersistencia;

        public TarimasLogica(TarimasPersistencia _TarimasPersistencia)
        {
            this._TarimasPersistencia = _TarimasPersistencia;
        }

        public List<Tarima> ObtenerTarimasCamara(int[] CamarasChk)
        {
            List<Tarima> pLista = new List<Tarima>();
            foreach(var item in CamarasChk)
            {
                pLista.AddRange(_TarimasPersistencia.ObtenerTarimasCamara(item));
            }
            pLista = pLista.OrderBy(x => x.Folio).ToList();
            return pLista;
        }

        public List<CajaTarima> ObtenerCajasTarima(int AFolioTarima, int ACamara)
        {
            if (AFolioTarima == 0)
                return _TarimasPersistencia.ObtenerCajasSinTarima(ACamara);
            else
                return _TarimasPersistencia.ObtenerCajasDeTarima(AFolioTarima);
        }

        public List<Tarima> ObtenerTarimasLote(string AFechaIni, string AFechaFin, int ALoteIni, int ALoteFin)
        {
            return _TarimasPersistencia.ObtenerTarimasLote(AFechaIni, AFechaFin, ALoteIni, ALoteFin);
        }

        public bool TraspasarTarima(int ACamara, int AFolio, string AUbicacion, string AUsuario, string AIp)
        {
            Tarima pTarima = _TarimasPersistencia.ObtenerTarima(AFolio);
            return _TarimasPersistencia.SetEntradaTarimaContenedor(pTarima, ACamara, AFolio, AUbicacion, DateTime.Today, AUsuario, AIp);
        }

        public Tarima ObtenerTarima(int AFolio)
        {
            return _TarimasPersistencia.ObtenerTarima(AFolio);
        }


        public List<Salida> ObtenerDatosSalidaTarima(int AFolio)
        {
            return _TarimasPersistencia.ObtenerDatosSalidaTarima(AFolio);
        }

        public int RegresarTarima(int AFolioTarima, string AMotivo, string AUsuario)
        {
            return _TarimasPersistencia.RegresarTarima(AFolioTarima, AMotivo, AUsuario);
        }

        public List<CajaTarima> ObtenerCajasDeTarima(int AFolioTarima)
        {
            return _TarimasPersistencia.ObtenerCajasDeTarima(AFolioTarima);
        }

        public List<CajaDeSalida> ObtenerCajasDeSalida(int AId_Salida)
        {
            return _TarimasPersistencia.ObtenerCajasDeSalida(AId_Salida);
        }
    }
}
