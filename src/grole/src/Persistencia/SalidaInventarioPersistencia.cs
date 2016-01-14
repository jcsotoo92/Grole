using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Persistencia
{
    public class SalidaInventarioPersistencia
    {
        private Conexiones _Conexion;

        public SalidaInventarioPersistencia(Conexiones _Conexion)
        {
            this._Conexion = _Conexion;
        }

        public List<SalidaDelDia> ObtenerSalidaDelDia(string AFechaIni, string AFechaFin)
        {
            List<SalidaDelDia> pSalidaDelDia = new List<SalidaDelDia>();
            SalidaDelDia pResult = null;
            string pSentencia = "SELECT PRODUCTO AS CLAVE, (SELECT DESCRIPCION FROM DRASPROD WHERE CLAVE = PRODUCTO) AS DESCRIPCION,  SUM(CAJAS) AS CAJAS, SUM(KILOS) AS KILOS FROM DRASSALIDAS WHERE FECHA >= @FECHAINI AND FECHA <= @FECHAFIN GROUP BY PRODUCTO";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHAINI", FbDbType.TimeStamp).Value = AFechaIni;
            com.Parameters.Add("@FECHAFIN", FbDbType.TimeStamp).Value = AFechaFin;

            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult             = new SalidaDelDia();
                    pResult.Clave       = (reader["CLAVE"] != DBNull.Value) ? (string)reader["CLAVE"] : "";
                    pResult.Descripcion = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.Cajas       = (reader["CAJAS"] != DBNull.Value) ? (int)reader["CAJAS"] : 0;
                    pResult.Kilos       = (reader["KILOS"] != DBNull.Value) ? (decimal)reader["KILOS"] : 0;

                    pSalidaDelDia.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return pSalidaDelDia;
        }
    }
}
