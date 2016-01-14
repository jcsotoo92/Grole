using grole.src.Entidades;
using grole.src.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Logica
{
    public class MateriaPrimaLogica
    {
        private MateriaPrimaPersistencia _MateriaPrimaPersistencia;
        public MateriaPrimaLogica(MateriaPrimaPersistencia _MateriaPrimaPersistencia)
        {
            this._MateriaPrimaPersistencia = _MateriaPrimaPersistencia;
        }

        public MateriaPrimaM ObtenerMtoMateriaPrima(int AId, string AFecha)
        {
            MateriaPrimaM MateriaPrima = _MateriaPrimaPersistencia.ObtenerMateriaPrima(AId);

            if (MateriaPrima == null)
            {
                MateriaPrima = _MateriaPrimaPersistencia.InsertarMateriaPrima(AId, "", "", 0, 0, 0, 0, 0, 0);
                string Productos = _MateriaPrimaPersistencia.ObtenerProductosMateriaPrima(AId);
                int pLote = _MateriaPrimaPersistencia.ObtenerLoteMateriaPrima(AId);
                foreach (var item in _MateriaPrimaPersistencia.ObtenerProduccionProductos(AFecha, Productos, pLote))
                {
                    _MateriaPrimaPersistencia.InsertarDetalleMateriaPrima(MateriaPrima.Id, item.Producto, item.Cajas, item.Kilos, "produccion", AId, 0);
                }
            }

            List<DetalleMateriaPrima> DetalleMateriaPrima = _MateriaPrimaPersistencia.ObtenerDetalleMateriaPrima(MateriaPrima.Id, 111);
            List<DetalleMateriaPrima> ProduccionMoldeo = _MateriaPrimaPersistencia.ObtenerDetalleMateriaPrima(MateriaPrima.Id, 112);
            List<DetalleMateriaPrima> ProduccionProducto = _MateriaPrimaPersistencia.ObtenerDetalleMateriaPrima(MateriaPrima.Id, 0);

            return MateriaPrima;
        }

        public TablaDetalleMateriaPrima ObtenerTablaMateriaPrima(int AId, int ADestino)
        {
            TablaDetalleMateriaPrima pResult = new TablaDetalleMateriaPrima();
            decimal pTotalKilosMatPrima = 0;
            List<DetalleMateriaPrima> pDetalleMateriaPrima = null;
            string tipo = "";

            if (ADestino == 111) {
                pTotalKilosMatPrima = _MateriaPrimaPersistencia.ObtenerTotalKilosMateriaPrima(AId, 111);
                pDetalleMateriaPrima = _MateriaPrimaPersistencia.ObtenerDetalleMateriaPrimaTipo(AId, "MatPrima");
                tipo = "MatPrima";
            }

            if (ADestino == 112) {
                pTotalKilosMatPrima = _MateriaPrimaPersistencia.ObtenerTotalKilosMateriaPrima(AId, 112);
                pDetalleMateriaPrima = _MateriaPrimaPersistencia.ObtenerDetalleMateriaPrimaTipo(AId, "Moldeo");
                tipo = "Moldeo";
            }

            if (ADestino == 0) {
                pDetalleMateriaPrima = _MateriaPrimaPersistencia.ObtenerDetalleMateriaPrima(AId, ADestino);
                pDetalleMateriaPrima.AddRange(_MateriaPrimaPersistencia.ObtenerDetalleMateriaPrima(AId, 113));
            }

            decimal TotalCajas = 0;
            decimal TotalKilos = 0;
            decimal TotalRendimiento;

            foreach (var item in pDetalleMateriaPrima) {
                TotalCajas += item.Cajas;
                TotalKilos += item.Kilos;

                try {
                    if (pTotalKilosMatPrima > 0) {
                        item.Rendimiento = ((item.Kilos / pTotalKilosMatPrima) * 100) / 100;
                    } else {
                        item.Rendimiento = 0;
                    }
                } catch (Exception e) {
                    item.Rendimiento = 0;
                }
            }

            if (pTotalKilosMatPrima > 0) {
                TotalRendimiento = ((TotalKilos / pTotalKilosMatPrima) * 100) / 100;
            } else {
                TotalRendimiento = 0;
            }
            pResult.DetalleMateriaPrima = pDetalleMateriaPrima;
            if (ADestino < 0) {
                pResult.TotalRendimientto = TotalRendimiento;
            }
            return pResult;
        }

        public string ActualizarEstadoMateriaPrima(int AId, string AEstado)
        {
            return _MateriaPrimaPersistencia.ActualizarEstadoMateriaPrima(AId, AEstado);
        }

        public string ActualizarSubtipoMateriaPrima(int AId, string ASubtipo)
        {
            return _MateriaPrimaPersistencia.ActualizarSubtipoMateriaPrima(AId, ASubtipo);
        }

        public List<DesgloseMateriaPrima> ObtenerDesgloseMateriaPrima(int AIdMatPrima, string AProducto)
        {
            return _MateriaPrimaPersistencia.ObtenerDesgloseMateriaPrima(AIdMatPrima, AProducto);
        }

        public bool EliminarProductoProduccion(int AId_Salida, string AProducto, out string AMensaje)
        {
            return _MateriaPrimaPersistencia.EliminarProductoProduccion(AId_Salida, AProducto, out AMensaje);
        }

        public void InsertarDetalleMateriaPrima(int AIdMatPrima, string AProducto, int ACajas, decimal AKilos, string ATipo, int AIdSalida, int ADestino)
        {
            _MateriaPrimaPersistencia.InsertarDetalleMateriaPrima(AIdMatPrima, AProducto, ACajas, AKilos, ATipo, AIdSalida, ADestino);
        }

        public DetalleMateriaPrimaD ObtenerCajaMatPrima(string ACodigoBarras)
        {
            return _MateriaPrimaPersistencia.ObtenerCajaMateriaPrima(ACodigoBarras);
        }

        public void InsertarDetalleCajaMatPrima(int AIdMatPrima, string ACodigoBarras, decimal APeso, string AProducto, string ATipo, int AIdSalida, int AIdTarima)
        {
            _MateriaPrimaPersistencia.InsertarDetalleCajaMatPrima(AIdMatPrima, ACodigoBarras, APeso, AProducto, ATipo, AIdSalida, AIdTarima);
        }

        public string AgregarProduccion(int AId, int AIdProduccion, string AFecha, int ALote)
        {
            string result = "";

            int pFolioProduccion = _MateriaPrimaPersistencia.SalidaEnDetalleMateriaPrima(AIdProduccion);

            if (pFolioProduccion > 0) {
                result = "Producción ya se encuentra en el folio " + pFolioProduccion;
            }
            else {
                string pProductos = _MateriaPrimaPersistencia.ObtenerProductosMateriaPrima(AIdProduccion);

                foreach(var item in _MateriaPrimaPersistencia.ObtenerProduccionProductos(AFecha, pProductos, ALote)) {
                    _MateriaPrimaPersistencia.InsertarDetalleMateriaPrima(AId, item.Producto, item.Cajas, item.Kilos, "produccion", AIdProduccion, 0);
                }

                result = null;
            }
            return result;
        }

        public List<ProduccionProducto> ObtenerProduccionLote(int AId, string AFecha, int ALote)
        {
            string pProductos = _MateriaPrimaPersistencia.ObtenerProductosMateriaPrima(AId);
            
            return _MateriaPrimaPersistencia.ObtenerProduccionProductos(AFecha, pProductos, ALote);
        }
    }
}
