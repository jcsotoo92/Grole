using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System.Collections.Generic;
using System;

namespace grole.src.Persistencia
{
    public class SobrantesDPersistencia
    {
        private Conexiones _Conexiones { get; set; }
        public SobrantesDPersistencia(Conexiones AConexiones)
        {
            this._Conexiones = AConexiones;
        }
        //SELECT T0.*, T1.DESCRIPCION FROM DRASSOBRANTESD T0 JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO WHERE ID_SOBRANTE = ?

        public List<SobrantesD> obtener_lista(int Aid)
        {
            List<SobrantesD> pResult = new List<SobrantesD>();
            string pSentencia = "SELECT T0.*, T1.DESCRIPCION FROM DRASSOBRANTESD T0 JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO WHERE ID_SOBRANTE = @ID_SOBRANTE";
            FbConnection con = _Conexiones.ObtenerConexion();


            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID_SOBRANTE", FbDbType.Integer).Value = Aid;
            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    SobrantesD pSobrandesD = new SobrantesD();
                    pSobrandesD.id = (reader["ID"] != DBNull.Value) ? (int)reader["ID"] : -1;
                    pSobrandesD.id_sobrante = (reader["ID_SOBRANTE"] != DBNull.Value) ? (int)reader["ID_SOBRANTE"] : -1;
                    pSobrandesD.producto = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pSobrandesD.descripcion = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.Add(pSobrandesD);
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

        public bool eliminar_por_sobrante(int AidSobrante)
        {
            bool pResult = true;
            string pSentencia = "DELETE FROM DRASSOBRANTESD WHERE ID_SOBRANTE = @ID_SOBRANTE";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("ID_SOBRANTE", FbDbType.Integer).Value = AidSobrante;

            try
            {
                con.Open();
                com.ExecuteNonQuery();
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
