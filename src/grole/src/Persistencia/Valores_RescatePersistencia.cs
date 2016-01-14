using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System.Collections.Generic;
using System;

namespace grole.src.Persistencia
{
    public class Valores_RescatePersistencia
    {
        private Conexiones _Conexiones { get; set; }
        public Valores_RescatePersistencia(Conexiones AConexion)
        {
            this._Conexiones = AConexion;
        }

        public List<ValoresRescate> obtener_lista_desc()
        {
            List<ValoresRescate> pResult = new List<ValoresRescate>();
            string pSentencia = "SELECT * FROM DRASVALORRESCATEM ORDER BY ID DESC";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    ValoresRescate pValoresRescate = new ValoresRescate();
                    pValoresRescate.id               = (reader["ID"] != DBNull.Value) ? (int)reader["ID"] : -1;
                    DateTime pFecha = (DateTime)reader["FECHA"];
                    pValoresRescate.fecha            = pFecha.ToString("yyyy-MM-dd") + " 00:00:00 -0700";
                    DateTime pFechaFinal = (DateTime)reader["FECHAFINAL"];
                    pValoresRescate.fechafinal       = pFechaFinal.ToString("yyyy-MM-dd") + " 00:00:00 -0700";
                    pValoresRescate.descripcion      = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pValoresRescate.activo           = (reader["ACTIVO"] != DBNull.Value) ? (string)reader["ACTIVO"] : "";
                    DateTime pFechaHoraSistema = (DateTime)reader["FECHAHORASISTEMA"];
                    pValoresRescate.fechahorasistema = pFechaHoraSistema.ToString("yyyy-MM-dd HH:mm:ss") + " -0700";
                    pResult.Add(pValoresRescate);

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

        public List<ValoresRescateD> obtener_lista(int a_folio)
        {
            List<ValoresRescateD> pResult = new List<ValoresRescateD>();
            string pSentencia = "SELECT T0.*, T1.DESCRIPCION FROM DRASVALORRESCATED T0 JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO WHERE ID_VALOR = @ID_VALOR ORDER BY T0.PRODUCTO";
            FbConnection con = _Conexiones.ObtenerConexion();
            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID_VALOR", FbDbType.Integer).Value = a_folio;


            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    ValoresRescateD pValoresRescateD = new ValoresRescateD();
                    pValoresRescateD.id = (reader["ID"] != DBNull.Value) ? (int)reader["ID"] : -1;
                    pValoresRescateD.id_valor = (reader["ID_VALOR"] != DBNull.Value) ? (int)reader["ID_VALOR"] : -1;
                    pValoresRescateD.producto = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pValoresRescateD.costo = (reader["COSTO"] != DBNull.Value) ? (decimal)reader["COSTO"] : -1;
                    pValoresRescateD.descripcion = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.Add(pValoresRescateD);

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
        public ValoresRescate Obtener(int AId)
        {
            ValoresRescate pResult = new ValoresRescate();
            string pSentencia = "SELECT * FROM DRASVALORRESCATEM WHERE ID = @ID";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value = AId;

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    ValoresRescate pValoresRescate = new ValoresRescate();
                    pValoresRescate.id               = (reader["ID"] != DBNull.Value) ? (int)reader["ID"] : -1;
                    DateTime pFecha                  = (DateTime)reader["FECHA"];
                    pValoresRescate.fecha            = pFecha.ToString("yyyy-MM-dd") + " 00:00:00 -0700";
                    DateTime pFechaFinal = (DateTime)reader["FECHAFINAL"];
                    pValoresRescate.fechafinal       = pFechaFinal.ToString("yyyy-MM-dd") + " 00:00:00 -0700";
                    pValoresRescate.descripcion      = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pValoresRescate.activo           = (reader["ACTIVO"] != DBNull.Value) ? (string)reader["ACTIVO"] : "";
                    DateTime pFechaHoraSistema = (DateTime)reader["FECHAHORASISTEMA"];
                    pValoresRescate.fechahorasistema = pFechaHoraSistema.ToString("yyyy-MM-dd HH:mm:ss") + " -0700";
                    pResult = pValoresRescate;

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
