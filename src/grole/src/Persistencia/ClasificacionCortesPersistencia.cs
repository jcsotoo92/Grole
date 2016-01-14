using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Persistencia
{
    public class ClasificacionCortesPersistencia
    {
        private Conexiones _Conexion;

        public ClasificacionCortesPersistencia(Conexiones _Conexion)
        {
            this._Conexion = _Conexion;
        }

        private ClasificacionCorte ReaderToEntidad(FbDataReader AReader)
        {
            ClasificacionCorte pResult = new ClasificacionCorte();
            pResult.Id          = (int)AReader["ID"];
            pResult.Descripcion = AReader["DESCRIPCION"] != DBNull.Value ? (string)AReader["DESCRIPCION"] : "";
            pResult.Lotes       = AReader["LOTES"] != DBNull.Value? (string)AReader["LOTES"] : "";
            return pResult;
        }

        public bool ExisteClasificacionCorte(ClasificacionCorte AClasificacionCorte)
        {
            string pSentencia = "SELECT ID FROM CLASIFICACIONCORTES WHERE UPPER(TRIM(DESCRIPCION)) = @DESCRIPCION";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@DESCRIPCION", FbDbType.VarChar).Value = AClasificacionCorte.Descripcion.ToUpper().Trim();

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    if ((int)reader["ID"] == AClasificacionCorte.Id)
                        return false;
                    else return true;
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

        public ClasificacionCorte ClasificacionCorteObtener(int AClave)
        {
            ClasificacionCorte pResult = null;

            string pSentencia = "SELECT ID, DESCRIPCION, LOTES FROM CLASIFICACIONCORTES WHERE ID = @ID";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value = AClave;

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    pResult = ReaderToEntidad(reader);
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

        public List<ClasificacionCorte> ObtenerLista()
        {
            List<ClasificacionCorte> pResult = new List<ClasificacionCorte>();

            string pSentencia = "SELECT ID, DESCRIPCION, LOTES FROM CLASIFICACIONCORTES";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult.Add(ReaderToEntidad(reader));
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

        public ClasificacionCorte ClasificacionCorteInsertar(ClasificacionCorte AClasificacionCorte)
        {
            string pSentencia = "INSERT INTO CLASIFICACIONCORTES (DESCRIPCION,LOTES) VALUES (@DESCRIPCION,@LOTES) RETURNING ID";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@DESCRIPCION", FbDbType.VarChar).Value = AClasificacionCorte.Descripcion;
            com.Parameters.Add("@LOTES", FbDbType.VarChar).Value       = AClasificacionCorte.Lotes;

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

            return ClasificacionCorteObtener((int)pOutParameter.Value);
        }

        public ClasificacionCorte ClasificacionCorteModificar(ClasificacionCorte AClasificacionCorte)
        {

            string pSentencia = "UPDATE CLASIFICACIONCORTES SET DESCRIPCION=@DESCRIPCION, LOTES=@LOTES WHERE ID=@IDD RETURNING ID";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@IDD", FbDbType.Integer).Value         = AClasificacionCorte.Id;
            com.Parameters.Add("@DESCRIPCION", FbDbType.VarChar).Value = AClasificacionCorte.Descripcion;
            com.Parameters.Add("@LOTES", FbDbType.VarChar).Value       = AClasificacionCorte.Lotes;

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

            return ClasificacionCorteObtener((int)pOutParameter.Value);
        }

        public bool ClasificacionCorteEliminar(int AID, out string AMensajeError)
        {
            bool pResult = true;
            AMensajeError = "";

            string pSentencia = "DELETE FROM CLASIFICACIONCORTES WHERE ID = @ID";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("ID", FbDbType.Integer).Value = AID;

            try
            {
                con.Open();

                try
                {
                    com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    AMensajeError = ex.Message;
                    pResult = false;
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
