using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Persistencia
{
    public class CambiosTaraPersistencia
    {
        private Conexiones _Conexiones;

        public CambiosTaraPersistencia(Conexiones _Conexiones)
        {
            this._Conexiones = _Conexiones;
        }

        public List<CambioTara> ObtenerListaCambiosTara(string AProducto, string AFechaIni, string AFechaFin)
        {
            List<CambioTara> pCambioTara = new List<CambioTara>();
            CambioTara pResult = null;
            string pSentencia = "SELECT * FROM DRASCAMBIOS_TARA WHERE PRODUCTO = @PRODUCTO AND FECHA_CAMBIO >= @FECHAINI AND FECHA_CAMBIO <= @FECHAFIN";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@PRODUCTO", FbDbType.VarChar).Value   = AProducto;
            com.Parameters.Add("@FECHAINI", FbDbType.TimeStamp).Value = AFechaIni;
            com.Parameters.Add("@FECHAFIN", FbDbType.TimeStamp).Value = AFechaFin;

            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult                  = new CambioTara();
                    pResult.Id               = (reader["ID"] != DBNull.Value) ? (int)reader["ID"] : -1;
                    pResult.Producto         = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pResult.Fecha_Cambio     = (reader["FECHA_CAMBIO"] != DBNull.Value) ? (DateTime?)reader["FECHA_CAMBIO"] : null;
                    pResult.Tara_Anterior    = (reader["TARA_ANTERIOR"] != DBNull.Value) ? (decimal)reader["TARA_ANTERIOR"] : 0;
                    pResult.Tara_Nueva       = (reader["TARA_NUEVA"] != DBNull.Value) ? (decimal)reader["TARA_NUEVA"] : 0;
                    pResult.Usuario          = (reader["USUARIO"] != DBNull.Value) ? (string)reader["USUARIO"] : "";
                    pResult.FechaHoraSistema = (reader["FECHAHORASISTEMA"] != DBNull.Value) ? (DateTime?)reader["FECHAHORASISTEMA"] : null;

                    pCambioTara.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return pCambioTara;
        }

        public int InsertarCambiosTara(string AProducto, DateTime AFecha, float ATaraAnterior, float ATaraNueva,string AUsuario)
        {
            int pAffected = 0;
            string pSentencia = "INSERT INTO DRASCAMBIOS_TARA (PRODUCTO, FECHA_CAMBIO, TARA_ANTERIOR, TARA_NUEVA, USUARIO) VALUES (@PRODUCTO, @FECHACAMBIO, @TARAANTERIOR, @TARANUEVA, @USUARIO)";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@PRODUCTO", FbDbType.VarChar).Value      = AProducto;
            com.Parameters.Add("@FECHACAMBIO", FbDbType.TimeStamp).Value = AFecha.ToShortDateString();
            com.Parameters.Add("@TARAANTERIOR", FbDbType.Numeric).Value  = ATaraAnterior;
            com.Parameters.Add("@TARANUEVA", FbDbType.Numeric).Value     = ATaraNueva;
            com.Parameters.Add("@USUARIO", FbDbType.VarChar).Value       = AUsuario;
            
            try
            {
                con.Open();
                pAffected = com.ExecuteNonQuery();
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return pAffected;
        }
    }
}
