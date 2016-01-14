using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Persistencia
{
    public class InformacionCajaPersistencia
    {
        private Conexiones _Conexiones;

        public InformacionCajaPersistencia(Conexiones _Conexiones)
        {
            this._Conexiones = _Conexiones;
        }

        public InformacionCaja ObtenerDatosCaja(int Folio, string Fecha)
        {
            InformacionCaja pResult = new InformacionCaja();
            InformacionCaja pInformacionCaja = null;

            string pSentencia = "SELECT FECHA, PESO, BASCULA, TARIMA, ID_SALIDA, PRODUCTO, CODIGOBARRAS, EMBARCADO, ENTRADA_APLICADA, FECHA_SACRIFICIO FROM DRASCORT WHERE FOLIO=@FOLIO AND FECHA=@FECHA";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FOLIO", FbDbType.Integer).Value = Folio;
            com.Parameters.Add("@FECHA", FbDbType.Integer).Value = Fecha;
            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pInformacionCaja                  = new InformacionCaja();
                    pInformacionCaja.Producto         = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pInformacionCaja.CodigoBarras     = (reader["CODIGOBARRAS"] != DBNull.Value) ? (string)reader["CODIGOBARRAS"] : "";
                    pInformacionCaja.Fecha            = reader["FECHA"] != DBNull.Value ? Utilerias.dateTimeToString((DateTime)reader["FECHA"]) : "";
                    pInformacionCaja.Fecha_Sacrificio = reader["FECHA_SACRIFICIO"] != DBNull.Value ? Utilerias.dateTimeToString((DateTime)reader["FECHA_SACRIFICIO"]) : "";
                    pInformacionCaja.Peso             = reader["PESO"] != DBNull.Value ? (decimal)reader["PESO"] : -1;
                    pInformacionCaja.Tarima           = (reader["TARIMA"] != DBNull.Value) ? (int)reader["TARIMA"] : -1;
                    pInformacionCaja.Id_Salida        = (reader["ID_SALIDA"] != DBNull.Value) ? (int)reader["ID_SALIDA"] : -1;
                    pInformacionCaja.Bascula          = (reader["BASCULA"] != DBNull.Value) ? (int)reader["BASCULA"] : -1;
                    pResult                           = pInformacionCaja;
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
