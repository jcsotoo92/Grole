using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System;
using System.Collections.Generic;
using System.Data;

namespace grole.src.Persistencia
{
    public class LotesNoInventariablesPersistencia
    {
        private Conexiones _Conexion;

        public LotesNoInventariablesPersistencia(Conexiones _Conexion)
        {
            this._Conexion = _Conexion;
        }

        public List<LoteNoInventariable> ObtenerLotes()
        {
            List<LoteNoInventariable> pResult = new List<LoteNoInventariable>();
            LoteNoInventariable lote;
            string pSentencia = "SELECT * FROM LOTES_NO_INVENTARIABLES ORDER BY LOTE";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    lote = new LoteNoInventariable();
                    lote.Clave = (int)reader["ID"];
                    lote.Lote = (int)reader["LOTE"];
                    pResult.Add(lote);
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

        public bool ExisteLote(int ALote)
        {
            string pSentencia = "SELECT * FROM LOTES_NO_INVENTARIABLES WHERE LOTE=@LOTE";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@LOTE", FbDbType.Integer).Value = ALote;

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    return true;
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return false;

        }
        public LoteNoInventariable InsertarLoteNoInventariable(LoteNoInventariable ALote)
        {
            string pSentencia = "INSERT INTO LOTES_NO_INVENTARIABLES (LOTE) VALUES (@LOTE) RETURNING ID";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@LOTE", FbDbType.Integer).Value = ALote.Lote;
            FbParameter pOutParameter = new FbParameter("@ID", FbDbType.Integer);
            pOutParameter.Direction = ParameterDirection.Output;
            com.Parameters.Add(pOutParameter);

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

            return ObtenerLote((int)pOutParameter.Value);
        }

        public LoteNoInventariable ObtenerLote(int AClave)
        {
            LoteNoInventariable lote = null;
            string pSentencia = "SELECT * FROM LOTES_NO_INVENTARIABLES WHERE ID=@ID";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value = AClave;
            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    lote = new LoteNoInventariable();
                    lote.Clave = (int)reader["ID"];
                    lote.Lote = (int)reader["LOTE"];
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return lote;
        }
    }
}
