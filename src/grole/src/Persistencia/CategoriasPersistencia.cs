using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System.Collections.Generic;
using System.Data;
using System;

namespace grole.src.Persistencia
{
    public class CategoriasPersistencia
    {
        private Conexiones _Conexiones { get; set; }

        public CategoriasPersistencia(Conexiones AConexion)
        {
            this._Conexiones = AConexion;
        }

        //Retorna la lista de Costos Maquila
        public List<Categoria> ListaCategorias()
        {
            List<Categoria> pResult = new List<Categoria>();
            string pSentencia = "SELECT * FROM DRASCATEGORIAS";
            FbConnection con = _Conexiones.ObtenerConexion();
            FbCommand com = new FbCommand(pSentencia, con);
            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Categoria pCategoria = new Categoria();
                    pCategoria.Id = reader.GetInt32(0);
                    pCategoria.Cat = reader.GetString(1);
                    pCategoria.Activo = reader.GetString(2);
                    pResult.Add(pCategoria);
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

        public bool ExisteCategoria(Categoria ACategoria)
        {
            string pSentencia = "SELECT ID FROM DRASCATEGORIAS WHERE UPPER(TRIM(CATEGORIA)) = @CATEGORIA";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CATEGORIA", FbDbType.VarChar).Value = ACategoria.Cat.ToUpper().Trim();

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    if ((int)reader["ID"] == ACategoria.Id)
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

        private Categoria ReaderToEntidad(FbDataReader AReader)
        {
            Categoria pResult = new Categoria();
            pResult.Id = (int)AReader["ID"];
            pResult.Cat = AReader["CATEGORIA"] != DBNull.Value ? (string)AReader["CATEGORIA"] : "";
            pResult.Activo = AReader["ACTIVO"] != DBNull.Value ? (string)AReader["ACTIVO"] : "";
            return pResult;
        }

        public Categoria CategoriaObtener(int AClave)
        {
            Categoria pResult = null;

            string pSentencia = "SELECT ID, CATEGORIA, ACTIVO FROM DRASCATEGORIAS WHERE ID = @ID";
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

        public List<Categoria> ObtenerLista()
        {
            List<Categoria> pResult = new List<Categoria>();

            string pSentencia = "SELECT ID, CATEGORIA, ACTIVO FROM DRASCATEGORIAS";
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

        public Categoria CategoriaInsertar(Categoria ACategoria)
        {
            string pSentencia = "INSERT INTO DRASCATEGORIAS (CATEGORIA,ACTIVO) VALUES (@CATEGORIA,@ACTIVO) RETURNING ID";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CATEGORIA", FbDbType.VarChar).Value = ACategoria.Cat;
            com.Parameters.Add("@ACTIVO", FbDbType.VarChar).Value    = ACategoria.Activo;
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

            return CategoriaObtener((int)pOutParameter.Value);
        }

        public Categoria CategoriaModificar(Categoria ACategoria)
        {

            string pSentencia = "UPDATE DRASCATEGORIAS SET CATEGORIA=@CATEGORIA, ACTIVO=@ACTIVO WHERE ID=@IDD RETURNING ID";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@IDD", FbDbType.Integer).Value       = ACategoria.Id;
            com.Parameters.Add("@CATEGORIA", FbDbType.VarChar).Value = ACategoria.Cat;
            com.Parameters.Add("@ACTIVO", FbDbType.VarChar).Value    = ACategoria.Activo;

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

            return CategoriaObtener((int)pOutParameter.Value);
        }

        public bool CategoriaEliminar(int AID, out string AMensajeError)
        {
            bool pResult = true;
            AMensajeError = "";

            string pSentencia = "DELETE FROM DRASCATEGORIAS WHERE ID = @ID";
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
