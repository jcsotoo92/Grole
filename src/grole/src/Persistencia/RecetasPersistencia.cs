using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System.Collections.Generic;
using System;
using System.Data;

namespace grole.src.Persistencia
{
    public class RecetasPersistencia
    {
        private Conexiones _Conexiones { get; set; }

        public RecetasPersistencia(Conexiones AConexion)
        {
            this._Conexiones = AConexion;
        }

        //Genera Clave
        public int GeneraClave(string campo, string tabla)
        {
            string pSentencia = "SELECT " + campo + " FROM " + tabla + " ORDER BY " + campo;
            FbConnection con = _Conexiones.ObtenerConexion();
            FbCommand com = new FbCommand(pSentencia, con);
            int Clave = 0;

            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Clave = reader.GetInt32(0);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return (Clave + 1);
        }

        //Guarda RecetaM
        public RecetaM guarda_receta_productoM(RecetaM AReceta)
        {
            AReceta.Id = GeneraClave("ID", "DRASRECETAM");
            
            string pSentencia = "INSERT INTO DRASRECETAM (ID, PRODUCTO, DESCRIPCION) VALUES (@ID, @PRODUCTO, @DESCRIPCION)";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value          = AReceta.Id;
            com.Parameters.Add("@PRODUCTO", FbDbType.VarChar).Value    = AReceta.Producto;
            com.Parameters.Add("@DESCRIPCION", FbDbType.VarChar).Value = AReceta.Descripcion;
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
            return AReceta;
        }

        //Guarda RecetaD
        public RecetaD guarda_receta_productoD(RecetaD AReceta)
        {
            Console.WriteLine("ID:"+GeneraClave("ID", "DRASRECETAD"));
            AReceta.Id = GeneraClave("ID", "DRASRECETAD");
            string pSentencia = "INSERT INTO DRASRECETAD (ID, ID_RECETA, PRODUCTO, RENDIMIENTO) VALUES (@ID, @ID_RECETA, @PRODUCTO, @RENDIMIENTO)";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value          = AReceta.Id;
            com.Parameters.Add("@ID_RECETA", FbDbType.Integer).Value   = AReceta.Id_Receta;
            com.Parameters.Add("@PRODUCTO", FbDbType.VarChar).Value    = AReceta.Producto;
            com.Parameters.Add("@RENDIMIENTO", FbDbType.VarChar).Value = AReceta.Rendimiento;
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
            return AReceta;
        }
        public List<RecetaM> ObtenerLista_receta_productoM(int Producto)
        {
            List<RecetaM> pResult = new List<RecetaM>();
            string pSentencia = "SELECT * FROM DRASRECETAM WHERE PRODUCTO = @PRODUCTO";
            FbConnection con = _Conexiones.ObtenerConexion();
            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@PRODUCTO", FbDbType.Integer).Value = Producto;
            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    RecetaM pReceta     = new RecetaM();
                    pReceta.Id          = reader.GetInt32(0);
                    pReceta.Producto    = reader.GetString(1);
                    pReceta.Descripcion = reader.GetString(2);
                    pResult.Add(pReceta);
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

        //Regresa Lista de Productos de una Receta (Productos en receta)
        public List<RecetaD> ObtenerLista_receta_productoD(int AId_Receta)
        {
            List<RecetaD> pResult = new List<RecetaD>();
            string pSentencia = "SELECT T0.ID, T0.ID_RECETA, T0.PRODUCTO, T0.RENDIMIENTO, T1.DESCRIPCION FROM DRASRECETAD T0 JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO WHERE T0.ID_RECETA = " + AId_Receta;
            FbConnection con = _Conexiones.ObtenerConexion();
            FbCommand com = new FbCommand(pSentencia, con);

            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    RecetaD pReceta             = new RecetaD();
                    pReceta.Id                  = reader.GetInt32(0);
                    pReceta.Id_Receta           = reader.GetInt32(1);
                    pReceta.Producto            = reader.GetString(2);
                    pReceta.Rendimiento         = reader.GetInt32(3);
                    pReceta.DescripcionProducto = reader.GetString(4);
                    pResult.Add(pReceta);
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

        //Elimina La Receta
        public bool eliminarReceta(int Id_Receta)
        {
            //Elimina Tabla D
            string pSentencia = "DELETE FROM DRASRECETAD WHERE ID_RECETA = @ID_RECETA";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID_RECETA", FbDbType.Integer).Value = Id_Receta;
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

            //Elimina Tabla M
            string pSentencia2 = "DELETE FROM DRASRECETAM WHERE ID = @ID";
            FbConnection con2 = _Conexiones.ObtenerConexion();

            FbCommand com2 = new FbCommand(pSentencia2, con2);
            com2.Parameters.Add("@ID", FbDbType.Integer).Value = Id_Receta;
            try
            {
                con2.Open();
                com2.ExecuteNonQuery();

            }
            finally
            {
                if (con2.State == System.Data.ConnectionState.Open)
                {
                    con2.Close();
                }
            }

            return true;
        }

        public bool actualizarRecetaD(RecetaD AReceta)
        {
            String pSentencia = "UPDATE DRASRECETAD SET PRODUCTO=@PRODUCTO, RENDIMIENTO=@RENDIMIENTO WHERE ID=@ID";
            FbConnection con = _Conexiones.ObtenerConexion();
            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("PRODUCTO", FbDbType.Integer).Value    = AReceta.Producto;
            com.Parameters.Add("RENDIMIENTO", FbDbType.Integer).Value = AReceta.Rendimiento;
            com.Parameters.Add("ID", FbDbType.Integer).Value          = AReceta.Id;

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
            return true;
        }

        public bool actualizarRecetaM(RecetaM AReceta)
        {
            string pSentencia = "UPDATE DRASRECETAM SET PRODUCTO=@PRODUCTO, DESCRIPCION=@DESCRIPCION WHERE ID=@ID";
            FbConnection con = _Conexiones.ObtenerConexion();
            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("PRODUCTO", FbDbType.Integer).Value    = AReceta.Producto;
            com.Parameters.Add("DESCRIPCION", FbDbType.Integer).Value = AReceta.Descripcion;
            com.Parameters.Add("ID", FbDbType.Integer).Value          = AReceta.Id;

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

            return true;
        }

        public bool eliminarProductoReceta(int AId)
        {
            string pSentencia = "DELETE FROM DRASRECETAD WHERE ID = @ID";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value = AId;
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
            return true;
        }



        
    }
}
