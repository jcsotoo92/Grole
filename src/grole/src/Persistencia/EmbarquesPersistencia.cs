using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Persistencia
{
    public class EmbarquesPersistencia
    {
        private Conexiones _Conexiones;

        public EmbarquesPersistencia(Conexiones _Conexiones)
        {
            this._Conexiones = _Conexiones;
        }

        public List<DetalleSalida> ObtenerDetalleSalida(int AIdSalida)
        {
            List<DetalleSalida> pResult = new List<DetalleSalida>();
            DetalleSalida pDetalleSalida = null;

            string pSentencia = "SELECT T0.FECHA, T0.FOLIO, T0.PRODUCTO, T0.CODIGOBARRAS, T0.TARIMA, T0.PESO, T1.DESCRIPCION FROM DRASCORT T0 JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO WHERE T0.ID_SALIDA = @IDSALIDA";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@IDSALIDA", FbDbType.Integer).Value = AIdSalida;
            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pDetalleSalida              = new DetalleSalida();
                    pDetalleSalida.Fecha        = reader["FECHA"] != DBNull.Value ? (DateTime?)reader["FECHA"] : null;
                    pDetalleSalida.Folio        = (reader["FOLIO"] != DBNull.Value) ? (int)reader["FOLIO"] : -1;
                    pDetalleSalida.Producto     = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pDetalleSalida.CodigoBarras = (reader["CODIGOBARRAS"] != DBNull.Value) ? (string)reader["CODIGOBARRAS"] : "";
                    pDetalleSalida.Tarima       = (reader["TARIMA"] != DBNull.Value) ? (int)reader["TARIMA"] : -1;
                    pDetalleSalida.Peso         = (reader["PESO"] != DBNull.Value) ? (decimal)reader["PESO"] : 0;
                    pDetalleSalida.Descripcion  = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.Add(pDetalleSalida);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return pResult;
        }

        public List<SalidaPorDestino> ObtenerSalidasPorDestino(string AFecha, int ADestino)
        {
            List<SalidaPorDestino> pResult = new List<SalidaPorDestino>();
            SalidaPorDestino pSalidaPorDestino = null;

            string pSentencia = "SELECT T0.ID, T0.FECHA, T0.CAJAS, T0.KILOS, T0.CONCEPTO, T1.DESTINO FROM SALIDASM T0 " +
                                "JOIN DRASDEST T1 ON T1.CLAVE = T0.ID_DESTINO " +
                                "WHERE T0.FECHA = @FECHA AND T0.ID_DESTINO = @DESTINO " +
                                "ORDER BY ID DESC";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value = AFecha;
            com.Parameters.Add("@DESTINO", FbDbType.VarChar).Value = ADestino;

            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pSalidaPorDestino          = new SalidaPorDestino();
                    pSalidaPorDestino.Id       = (reader["ID"] != DBNull.Value) ? (int)reader["ID"] : -1;
                    pSalidaPorDestino.Fecha    = reader["FECHA"] != DBNull.Value ? (DateTime?)reader["FECHA"] : null;
                    pSalidaPorDestino.Cajas    = (reader["CAJAS"] != DBNull.Value) ? (int)reader["CAJAS"] : -1;
                    pSalidaPorDestino.Kilos    = (reader["KILOS"] != DBNull.Value) ? (decimal)reader["KILOS"] : 0;
                    pSalidaPorDestino.Concepto = (reader["CONCEPTO"] != DBNull.Value) ? (string)reader["CONCEPTO"] : "";
                    pSalidaPorDestino.Destino  = (reader["DESTINO"] != DBNull.Value) ? (string)reader["DESTINO"] : "";
                    pResult.Add(pSalidaPorDestino);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return pResult;
        }

        public List<Tarima> ObtenerTarimasDeSalida(int AIdSalida)
        {
            List<Tarima> pResult = new List<Tarima>();
            Tarima pTarima = null;

            string pSentencia = "SELECT FOLIO, CAJAS, KILOS, LOTE, CONTENEDOR FROM DRASTARM WHERE ID_SALIDA = @ID";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value = AIdSalida;

            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pTarima            = new Tarima();
                    pTarima.Folio      = (reader["FOLIO"] != DBNull.Value) ? (int)reader["FOLIO"] : -1;
                    pTarima.Cajas      = (reader["CAJAS"] != DBNull.Value) ? (int)reader["CAJAS"] : -1;
                    pTarima.Kilos      = (reader["KILOS"] != DBNull.Value) ? (float)reader["KILOS"] : 0;
                    pTarima.Lote       = (reader["LOTE"] != DBNull.Value) ? (Int16)reader["LOTE"] : -1;
                    pTarima.Contenedor = (reader["CONTENEDOR"] != DBNull.Value) ? (int)reader["CONTENEDOR"] : -1;
                    pResult.Add(pTarima);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return pResult;
        }

        

    }
}
