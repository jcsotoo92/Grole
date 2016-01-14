using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System.Collections.Generic;
using System;

namespace grole.src.Persistencia
{
    public class SobrantesMPersistencia
    {
        private Conexiones _Conexiones { get; set; }
        public SobrantesMPersistencia(Conexiones AConexion)
        {
            this._Conexiones = AConexion;
        }

        public List<SobrantesM> obtener_lista()
        {
            List<SobrantesM> pResult = new List<SobrantesM>();
            string pSentencia = "SELECT T0.*, T1.DESCRIPCION FROM DRASSOBRANTESM T0 JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    SobrantesM pSobrantesM = new SobrantesM();
                    pSobrantesM.id = (reader["ID"] != DBNull.Value) ? (int)reader["ID"] : -1;
                    pSobrantesM.producto = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pSobrantesM.activo = (reader["ACTIVO"] != DBNull.Value) ? (string)reader["ACTIVO"] : "";
                    DateTime pFechaHoraSistema = (DateTime)reader["FECHAHORASISTEMA"];
                    pSobrantesM.fechahorasistema = pFechaHoraSistema.ToString("yyyy-MM-dd HH:mm:ss") + " -0700";
                    pSobrantesM.descripcion = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.Add(pSobrantesM);

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

        public SobrantesM obtener(int AId)
        {
            SobrantesM pResult = new SobrantesM();
            string pSentencia = "SELECT T0.*, T1.DESCRIPCION FROM DRASSOBRANTESM T0 JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO WHERE T0.ID = @ID";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value = AId;

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    SobrantesM pSobrantesM = new SobrantesM();
                    pSobrantesM.id = (reader["ID"] != DBNull.Value) ? (int)reader["ID"] : -1;
                    pSobrantesM.producto = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pSobrantesM.activo = (reader["ACTIVO"] != DBNull.Value) ? (string)reader["ACTIVO"] : "";
                    DateTime pFechaHoraSistema = (DateTime)reader["FECHAHORASISTEMA"];
                    pSobrantesM.fechahorasistema = pFechaHoraSistema.ToString("yyyy-MM-dd HH:mm:ss") + " -0700";
                    pSobrantesM.descripcion = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult = pSobrantesM;

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
        public List<SobrantesM> obtener_por_producto(int producto)
        {
            List<SobrantesM> pResult = null;
            string pSentencia = "SELECT T0.*, T1.DESCRIPCION FROM DRASSOBRANTESM T0 JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO WHERE T0.PRODUCTO = @PRODUCTO";

            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@PRODUCTO", FbDbType.Integer).Value = producto;

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    SobrantesM pSobrantesM = new SobrantesM();
                    pSobrantesM.id = (reader["ID"] != DBNull.Value) ? (int)reader["ID"] : -1;
                    pSobrantesM.producto = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pSobrantesM.activo = (reader["ACTIVO"] != DBNull.Value) ? (string)reader["ACTIVO"] : "";
                    DateTime pFechaHoraSistema = (DateTime)reader["FECHAHORASISTEMA"];
                    pSobrantesM.fechahorasistema = pFechaHoraSistema.ToString("yyyy-MM-dd HH:mm:ss") + " -0700";
                    pSobrantesM.descripcion = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult = new List<SobrantesM>();
                    pResult.Add(pSobrantesM);

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

        public bool eliminar(int Aid)
        {
            bool pResult = true;
            string pSentencia = "DELETE FROM DRASSOBRANTESM WHERE ID = @ID";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("ID", FbDbType.Integer).Value = Aid;

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
