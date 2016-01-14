using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Persistencia
{
    public class RastreoFoliosPersistencia
    {
        private Conexiones _Conexiones;

        public RastreoFoliosPersistencia(Conexiones _Conexiones)
        {
            this._Conexiones = _Conexiones;
        }

        public List<RastreoFolio> ObtenerRastreoFolios(int Folio, int Producto)
        {
            List<RastreoFolio> pResult = new List<RastreoFolio>();
            RastreoFolio pRastreoFolio = null;

            string pSentencia = "SELECT * FROM DRASCORT WHERE FOLIO = @FOLIO AND PRODUCTO = @PRODUCTO";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FOLIO", FbDbType.Integer).Value = Folio;
            com.Parameters.Add("@PRODUCTO", FbDbType.Integer).Value = Producto;
            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pRastreoFolio              = new RastreoFolio();
                    pRastreoFolio.Fecha        = reader["FECHA"] != DBNull.Value ? Utilerias.dateTimeToString((DateTime)reader["FECHA"]) : "";
                    pRastreoFolio.Folio        = (reader["FOLIO"] != DBNull.Value) ? (int)reader["FOLIO"] : -1;
                    pRastreoFolio.Producto     = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pRastreoFolio.Peso         = reader["PESO"] != DBNull.Value ? (decimal)reader["PESO"] : -1;
                    pRastreoFolio.CodigoBarras = (reader["CODIGOBARRAS"] != DBNull.Value) ? (string)reader["CODIGOBARRAS"] : "";
                    pRastreoFolio.Tarima       = (reader["TARIMA"] != DBNull.Value) ? (int)reader["TARIMA"] : -1;
                    pResult.Add(pRastreoFolio);
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
