using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System.Collections.Generic;
using System.Data;
using System;

namespace grole.src.Persistencia
{
    public class ClaseOrdenProduccionPersistencia
    {
        private Conexiones _Conexiones { get; set; }

        public ClaseOrdenProduccionPersistencia(Conexiones AConexion)
        {
            this._Conexiones = AConexion;
        }

        private ClaseOrdenProduccion ReaderToEntidad(FbDataReader AReader)
        {
            ClaseOrdenProduccion pResult = new ClaseOrdenProduccion();
            pResult.Id = (int)AReader["ID"];
            pResult.Clase   = AReader["CLASE"] != DBNull.Value ? (string)AReader["CLASE"] : "";
            pResult.Activo  = AReader["ACTIVO"] != DBNull.Value ? (string)AReader["ACTIVO"] : "";
            pResult.OcrCode = AReader["OCRCODE"] != DBNull.Value ? (string)AReader["OCRCODE"] : "";
            return pResult;
        }

        public bool ExisteClaseOrdenProduccion(ClaseOrdenProduccion AClaseOrdenProduccion)
        {
            string pSentencia = "SELECT ID FROM DRASCLASE_ORDENP WHERE UPPER(TRIM(CLASE)) = @CLASE";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CLASE", FbDbType.VarChar).Value = AClaseOrdenProduccion.Clase.ToUpper().Trim();

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    if ((int)reader["ID"] == AClaseOrdenProduccion.Id)
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

        public ClaseOrdenProduccion ClaseOrdenProduccionObtener(int AClave)
        {
            ClaseOrdenProduccion pResult = null;

            string pSentencia = "SELECT ID, CLASE, ACTIVO, OCRCODE FROM DRASCLASE_ORDENP WHERE ID = @ID";
            FbConnection con = _Conexiones.ObtenerConexion();

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

        public List<ClaseOrdenProduccion> ObtenerLista()
        {
            List<ClaseOrdenProduccion> pResult = new List<ClaseOrdenProduccion>();

            string pSentencia = "SELECT ID, CLASE, ACTIVO, OCRCODE FROM DRASCLASE_ORDENP";
            FbConnection con = _Conexiones.ObtenerConexion();

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

        public ClaseOrdenProduccion ClaseOrdenProduccionInsertar(ClaseOrdenProduccion AClaseOrdenProduccion)
        {
            string pSentencia = "INSERT INTO DRASCLASE_ORDENP (CLASE,ACTIVO,OCRCODE) VALUES (@CLASE,@ACTIVO,@OCRCODE) RETURNING ID";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CLASE", FbDbType.VarChar).Value   = AClaseOrdenProduccion.Clase;
            com.Parameters.Add("@ACTIVO", FbDbType.VarChar).Value  = AClaseOrdenProduccion.Activo;
            com.Parameters.Add("@OCRCODE", FbDbType.VarChar).Value = AClaseOrdenProduccion.OcrCode;
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

            return ClaseOrdenProduccionObtener((int)pOutParameter.Value);
        }

        public ClaseOrdenProduccion ClaseOrdenProduccionModificar(ClaseOrdenProduccion AClaseOrdenProduccion)
        {

            string pSentencia = "UPDATE DRASCLASE_ORDENP SET CLASE=@CLASE, ACTIVO=@ACTIVO, OCRCODE=@OCRCODE WHERE ID=@IDD RETURNING ID";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@IDD", FbDbType.Integer).Value     = AClaseOrdenProduccion.Id;
            com.Parameters.Add("@CLASE", FbDbType.VarChar).Value   = AClaseOrdenProduccion.Clase;
            com.Parameters.Add("@ACTIVO", FbDbType.VarChar).Value  = AClaseOrdenProduccion.Activo;
            com.Parameters.Add("@OCRCODE", FbDbType.VarChar).Value = AClaseOrdenProduccion.OcrCode;

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

            return ClaseOrdenProduccionObtener((int)pOutParameter.Value);
        }

        public bool ClaseOrdenProduccionEliminar(int AID, out string AMensajeError)
        {
            bool pResult = true;
            AMensajeError = "";

            string pSentencia = "DELETE FROM DRASCLASE_ORDENP WHERE ID = @ID";
            FbConnection con = _Conexiones.ObtenerConexion();

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
