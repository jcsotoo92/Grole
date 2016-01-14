using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Persistencia
{
    public class OrdenesProduccionPersistencia
    {
        private Conexiones _Conexiones;

        public OrdenesProduccionPersistencia(Conexiones _Conexiones)
        {
            this._Conexiones = _Conexiones;
        }

        public List<TarimaOrdenProduccion> ObtenerInformacionTarimaOrdenP(int AIdTarima)
        {
            List<TarimaOrdenProduccion> pResult = new List<TarimaOrdenProduccion>();
            TarimaOrdenProduccion pTarimaOrdenProduccion = null;

            string pSentencia = "SELECT T0.*, T1.ID_SALIDA_EMBARQUES FROM DRASORDENP_DETALLE_MP T0 JOIN DRASORDENPM T1 ON T1.ID = T0.ID_ORDEN WHERE TARIMA_ORIGEN = @TARIMAORIGEN";
            FbConnection con  = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@TARIMAORIGEN", FbDbType.Integer).Value = AIdTarima;
            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pTarimaOrdenProduccion                     = new TarimaOrdenProduccion();
                    pTarimaOrdenProduccion.Id                  = reader["ID"] != DBNull.Value ? (int)reader["ID"] : -1;
                    pTarimaOrdenProduccion.Id_Orden            = (reader["ID_ORDEN"] != DBNull.Value) ? (int)reader["ID_ORDEN"] : -1;
                    pTarimaOrdenProduccion.Id_Mat_Prima        = (reader["ID_MAT_PRIMA"] != DBNull.Value) ? (int)reader["ID_MAT_PRIMA"] : -1;
                    pTarimaOrdenProduccion.Producto            = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pTarimaOrdenProduccion.CodigoBarras        = (reader["CODIGOBARRAS"] != DBNull.Value) ? (string)reader["CODIGOBARRAS"] : "";
                    pTarimaOrdenProduccion.Peso                = (reader["PESO"] != DBNull.Value) ? (decimal)reader["PESO"] : 0;
                    pTarimaOrdenProduccion.Tarima_Origen       = (reader["TARIMA_ORIGEN"] != DBNull.Value) ? (int)reader["TARIMA_ORIGEN"] : -1;
                    pTarimaOrdenProduccion.Usuario_Pistola     = (reader["USUARIO_PIESTOLA"] != DBNull.Value) ? (string)reader["USUARIO_PIESTOLA"] : "";
                    pTarimaOrdenProduccion.FechaHoraSistema    = (reader["FECHAHORASISTEMA"] != DBNull.Value) ? (DateTime?)reader["FECHAHORASISTEMA"] : null;
                    pTarimaOrdenProduccion.Id_Salida_Embarques = (reader["ID_SALIDA_EMBARQUES"] != DBNull.Value) ? (int)reader["ID_SALIDA_EMBARQUES"] : -1;
                    pResult.Add(pTarimaOrdenProduccion);
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

        public List<OrdenProduccion> ObtenerInformacionOrdenP(int AOrdenP)
        {
            List<OrdenProduccion> pResult = new List<OrdenProduccion>();
            OrdenProduccion pOrdenProduccion = null;

            string pSentencia = "SELECT T0.PRODUCTO, T1.CODIGOSAP, T1.DESCRIPCION, T0.EMBARCADO, COALESCE(T0.ID_SALIDA, 0) AS ID_SALIDA, COUNT(*) AS CAJAS, SUM(PESO) AS KILOS FROM DRASCORT T0 JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO WHERE ORDEN_PRODUCCION = @ORDEN_PRODUCCION GROUP BY T0.PRODUCTO, T1.CODIGOSAP, T1.DESCRIPCION, T0.EMBARCADO, T0.ID_SALIDA";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ORDEN_PRODUCCION", FbDbType.Integer).Value = AOrdenP;
            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pOrdenProduccion             = new OrdenProduccion();
                    pOrdenProduccion.Producto    = reader["PRODUCTO"] != DBNull.Value ? (string)reader["PRODUCTO"] : "";
                    pOrdenProduccion.CodigoSap   = (reader["CODIGOSAP"] != DBNull.Value) ? (string)reader["CODIGOSAP"] : "";
                    pOrdenProduccion.Descripcion = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pOrdenProduccion.Embarcado   = (reader["EMBARCADO"] != DBNull.Value) ? (string)reader["EMBARCADO"] : "";
                    pOrdenProduccion.ID_Salida   = (reader["ID_SALIDA"] != DBNull.Value) ? (int)reader["ID_SALIDA"] : -1;
                    pOrdenProduccion.Cajas       = (reader["CAJAS"] != DBNull.Value) ? (int)reader["CAJAS"] : -1;
                    pOrdenProduccion.Kilos       = (reader["KILOS"] != DBNull.Value) ? (decimal)reader["KILOS"] : 0;
                    pResult.Add(pOrdenProduccion);
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

        public int ObtenerUltimoIndice(string ATabla)
        {
            string pSentencia = "SELECT MAX(ID) FROM " + ATabla;
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    return reader["MAX"] != DBNull.Value ? (int)reader["MAX"] : 0;
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return 0;
        }
    }
}
