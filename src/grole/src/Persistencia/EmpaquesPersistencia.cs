using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using grole.src.Entidades;
using FirebirdSql.Data.FirebirdClient;
using System.Data;

namespace grole.src.Persistencia
{
    public class EmpaquesPersistencia
    {
        private Conexiones _Conexion;

        public EmpaquesPersistencia(Conexiones _Conexion)
        {
            this._Conexion = _Conexion;
        }

        public bool ExisteEmpaque(Empaque AEmpaque)
        {
            string pSentencia = "SELECT ID FROM DRASEMPAQUE WHERE UPPER(TRIM(NOMBRE)) = @NOMBRE";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@NOMBRE", FbDbType.VarChar).Value = AEmpaque.Nombre.ToUpper().Trim();

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    if ((int)reader["ID"] == AEmpaque.Clave)
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

        public List<Empaque> ObtenerEmpaques(string AOrderBy)
        {
            List<Empaque> pResult = new List<Empaque>();
            Empaque pEmpaque = null;

            string pSentencia = "SELECT * FROM DRASEMPAQUE ORDER BY "+AOrderBy;
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia,con);
            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read()){
                    pEmpaque                = new Empaque();
                    pEmpaque.Clave          = (int)reader["ID"];
                    pEmpaque.IdTipoEmpaque  = (int)reader["ID_TIPOEMPAQUE"];
                    pEmpaque.Nombre         = (reader["NOMBRE"] != DBNull.Value) ? (string)reader["NOMBRE"] : "";
                    pEmpaque.CodigoSAP      = (reader["CODIGOSAP"] != DBNull.Value) ? (string)reader["CODIGOSAP"] : "";
                    pEmpaque.Costo          = (reader["COSTO"] != DBNull.Value) ? (decimal)reader["COSTO"] : 0;
                    pResult.Add(pEmpaque);
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

        public Empaque InsertarEmpaque(Empaque AEmpaque)
        {
            string pSentencia = "INSERT INTO DRASEMPAQUE(ID_TIPOEMPAQUE, NOMBRE, CODIGOSAP, COSTO) VALUES(@IDTIPOEMPAQUE,@NOMBRE,@CODIGOSAP,@COSTO) RETURNING ID";
            FbConnection con = _Conexion.ObtenerConexion();


            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@IDTIPOEMPAQUE", FbDbType.Integer).Value = AEmpaque.IdTipoEmpaque;
            com.Parameters.Add("@NOMBRE", FbDbType.VarChar).Value        = AEmpaque.Nombre;
            com.Parameters.Add("@CODIGOSAP", FbDbType.Integer).Value     = AEmpaque.CodigoSAP;
            com.Parameters.Add("@COSTO", FbDbType.Integer).Value         = AEmpaque.Costo;

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
            return ObtenerEmpaque((int)pOutParameter.Value);
        }

        public Empaque ModificarEmpaque(Empaque AEmpaque)
        {
            string pSentencia = "UPDATE DRASEMPAQUE SET ID_TIPOEMPAQUE=@IDTIPOEMPAQUE, NOMBRE=@NOMBRE, CODIGOSAP=@CODIGOSAP, COSTO=@COSTO WHERE ID=@CLAVE RETURNING ID";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CLAVE", FbDbType.Integer).Value         = AEmpaque.Clave;
            com.Parameters.Add("@IDTIPOEMPAQUE", FbDbType.Integer).Value = AEmpaque.IdTipoEmpaque;
            com.Parameters.Add("@NOMBRE", FbDbType.VarChar).Value        = AEmpaque.Nombre;
            com.Parameters.Add("@CODIGOSAP", FbDbType.VarChar).Value     = AEmpaque.CodigoSAP;
            com.Parameters.Add("@COSTO", FbDbType.Numeric).Value         = AEmpaque.Costo;

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
            return ObtenerEmpaque((int)pOutParameter.Value);
        }

        public bool EliminarEmpaque(int AClave, out string AMensajeError)
        {
            bool pResult = true;
            AMensajeError = "";

            string pSentencia = "DELETE FROM DRASEMPAQUE WHERE ID = @CLAVE";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CLAVE", FbDbType.Integer).Value = AClave;

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

        private Empaque ObtenerEmpaque(int AClave)
        {
            Empaque pEmpaque = null;

            string pSentencia = "SELECT * FROM DRASEMPAQUE WHERE ID=@ID";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value = AClave;

            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    pEmpaque                = new Empaque();
                    pEmpaque.Clave          = (int)reader["ID"];
                    pEmpaque.IdTipoEmpaque  = (int)reader["ID_TIPOEMPAQUE"];
                    pEmpaque.Nombre         = (reader["NOMBRE"] != DBNull.Value) ? (string)reader["NOMBRE"] : "";
                    pEmpaque.CodigoSAP      = (reader["CODIGOSAP"] != DBNull.Value) ? (string)reader["CODIGOSAP"] : "";
                    pEmpaque.Costo          = (reader["COSTO"] != DBNull.Value) ? (decimal)reader["COSTO"] : 0;
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return pEmpaque;
        }

        public List<TipoEmpaque> ObtenerListaTiposEmpaques()
        {
            List<TipoEmpaque> pResult = new List<TipoEmpaque>();
            TipoEmpaque pTipoEmpaque = null;

            string pSentencia = "SELECT ID, NOMBRE FROM DRASTIPOSEMPAQUE ORDER BY NOMBRE";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pTipoEmpaque        = new TipoEmpaque();
                    pTipoEmpaque.Id     = (int)reader["ID"];
                    pTipoEmpaque.Nombre = (reader["NOMBRE"] != DBNull.Value) ? (string)reader["NOMBRE"] : "";
                    pResult.Add(pTipoEmpaque);
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

        public List<EmpaqueProducto> ObtenerEmpaquesProducto(string AClave)
        {
            List<EmpaqueProducto> pResult = new List<EmpaqueProducto>();
            EmpaqueProducto pEmpaqueProducto = null;

            string pSentencia = "SELECT * FROM DRASEMPAQUE_PRODUCTO WHERE CLAVE_PRODUCTO=@CLAVE";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CLAVE", FbDbType.VarChar).Value = AClave;
            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pEmpaqueProducto               = new EmpaqueProducto();
                    pEmpaqueProducto.Id            = (int)reader["ID"];
                    pEmpaqueProducto.ClaveProducto = (reader["CLAVE_PRODUCTO"] != DBNull.Value) ? (string)reader["CLAVE_PRODUCTO"] : "";
                    pEmpaqueProducto.IdEmpaque     = (int)reader["ID_EMPAQUE"];
                    pEmpaqueProducto.Cantidad      = (decimal)reader["CANTIDAD"];
                    pResult.Add(pEmpaqueProducto);
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

        private TipoEmpaque ObtenerTipoEmpaque(int AClave)
        {
            TipoEmpaque pTipoEmpaque = null;

            string pSentencia = "SELECT * FROM DRASTIPOSEMPAQUE WHERE ID=@ID";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value = AClave;

            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    pTipoEmpaque                = new TipoEmpaque();
                    pTipoEmpaque.Id             = (int)reader["ID"];
                    pTipoEmpaque.Nombre         = (reader["NOMBRE"] != DBNull.Value) ? (string)reader["NOMBRE"] : "";
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return pTipoEmpaque;
        }

        private bool EliminarEmpaquesProducto(string AIdProducto)
        {
            bool pResult = true;
            string AMensajeError = "";

            string pSentencia = "DELETE FROM DRASEMPAQUE_PRODUCTO WHERE CLAVE_PRODUCTO = @CLAVE";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CLAVE", FbDbType.Integer).Value =AIdProducto;

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

        public bool InsertarEmpaquesProducto(string AIdProducto, int[] Achk, decimal[] Ainp)
        {
            EliminarEmpaquesProducto(AIdProducto);
            for(int i = 0; i < Achk.Length; i++)
            {
                string pSentencia = "INSERT INTO DRASEMPAQUE_PRODUCTO(CLAVE_PRODUCTO, ID_EMPAQUE, CANTIDAD) VALUES(@CLAVEPRODUCTO,@IDEMPAQUE,@CANTIDAD)";
                FbConnection con = _Conexion.ObtenerConexion();


                FbCommand com = new FbCommand(pSentencia, con);
                com.Parameters.Add("@CLAVEPRODUCTO", FbDbType.VarChar).Value = AIdProducto;
                com.Parameters.Add("@IDEMPAQUE", FbDbType.Integer).Value     = Achk[i];
                com.Parameters.Add("@CANTIDAD", FbDbType.Numeric).Value      = Ainp[i];
                
                try
                {
                    con.Open();
                    com.ExecuteNonQuery();
                }catch(Exception e)
                {
                    return false;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            
            return true;
        }
        
        
        
        public TipoEmpaque AgregarTipoEmpaque(TipoEmpaque ATipoEmpaque){
            string pSentencia = "INSERT INTO DRASTIPOSEMPAQUE(NOMBRE) VALUES(@NOMBRE) RETURNING ID";
            FbConnection con  = _Conexion.ObtenerConexion();


            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@NOMBRE", FbDbType.VarChar).Value = ATipoEmpaque.Nombre;

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
            return ObtenerTipoEmpaque((int)pOutParameter.Value);
        }
        
        public TipoEmpaque ModificarTipoEmpaque(TipoEmpaque ATipoEmpaque)
        {
            string pSentencia = "UPDATE DRASTIPOSEMPAQUE SET NOMBRE=@NOMBRE WHERE ID=@CLAVE RETURNING ID";
            FbConnection con = _Conexion.ObtenerConexion();

            
            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CLAVE", FbDbType.Integer).Value  = ATipoEmpaque.Id;
            com.Parameters.Add("@NOMBRE", FbDbType.VarChar).Value = ATipoEmpaque.Nombre;

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
            return ObtenerTipoEmpaque((int)pOutParameter.Value);
        }
        
        public bool EliminarTipoEmpaque(int AClave, out string AMensajeError)
        {
            bool pResult = true;
            AMensajeError = "";

            string pSentencia = "DELETE FROM DRASTIPOSEMPAQUE WHERE ID = @CLAVE";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CLAVE", FbDbType.Integer).Value = AClave;

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
        
        public bool ExisteTipoEmpaque(string ANombre){
            
            string pSentencia = "SELECT * FROM DRASTIPOSEMPAQUE WHERE NOMBRE=@NOMBRE";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@NOMBRE", FbDbType.VarChar).Value = ANombre;

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

        public List<ProyeccionProduccion> ObtenerProyeccionProduccion(string AFechaIni, string AFechaFin)
        {
            List<ProyeccionProduccion> pProyeccionProduccion = new List<ProyeccionProduccion>();
            ProyeccionProduccion pResult = null;

            string pSentencia = "SELECT MAX(PRODUCTO) AS PRODUCTO, MAX(CODIGOSAP) AS CODIGOSAP, MAX(DESCRIPCION) AS DESCRIPCION, MAX(INVENTARIO) AS INVENTARIO FROM "+
                               "(SELECT T0.FECHA_EMBARQUE, T0.ID, T1.PRODUCTO, T2.CODIGOSAP, T2.DESCRIPCION, T0.DESCRIPCION AS DESCRIPCION2, T1.CAJAS, "+
                               "(SELECT COUNT(*) FROM DRASCORT WHERE PRODUCTO = T1.PRODUCTO AND EMBARCADO = 'No' AND CAMARA NOT IN(SELECT ID FROM DRASCAM WHERE ACTIVO = 'No')) AS INVENTARIO "+
                               "FROM DRASORDENSM T0 "+
                               "JOIN DRASORDENSD T1 ON T1.ID_ORDEN = T0.ID "+
                               "JOIN DRASPROD T2 ON T2.CLAVE = T1.PRODUCTO "+
                               "WHERE T0.FECHA_EMBARQUE >= @FECHAINI AND T0.FECHA_EMBARQUE <= @FECHAFIN AND T0.ESTADO = 'Nuevo' "+
                               "ORDER BY T0.FECHA_EMBARQUE, T0.ID) A "+
                               "GROUP BY A.PRODUCTO, A.DESCRIPCION, A.INVENTARIO";

            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHAINI", FbDbType.TimeStamp).Value = AFechaIni;
            com.Parameters.Add("@FECHAFIN", FbDbType.TimeStamp).Value = AFechaFin;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult             = new ProyeccionProduccion();
                    pResult.Producto    = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pResult.CodigoSAP   = (reader["CODIGOSAP"] != DBNull.Value) ? (string)reader["CODIGOSAP"] : "";
                    pResult.Descripcion = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.Inventario  = (reader["INVENTARIO"] != DBNull.Value) ? (int)reader["INVENTARIO"] : 0;
                    pProyeccionProduccion.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return pProyeccionProduccion;
        }

        public List<ProyeccionProduccion> ObtenerProyeccionProduccionDetalle(string AFechaIni, string AFechaFin)
        {
            List<ProyeccionProduccion> pProyeccionProduccion = new List<ProyeccionProduccion>();
            ProyeccionProduccion pResult = null;

            string pSentencia = "SELECT T0.FECHA_EMBARQUE, T0.ID, T1.PRODUCTO, T2.CODIGOSAP, T2.DESCRIPCION, T0.DESCRIPCION AS DESCRIPCION2, T1.CAJAS, "+
                               "(SELECT COUNT(*) FROM DRASCORT WHERE PRODUCTO = T1.PRODUCTO AND EMBARCADO = 'No' AND CAMARA NOT IN(SELECT ID FROM DRASCAM WHERE ACTIVO = 'No')) AS INVENTARIO "+
                               "FROM DRASORDENSM T0 "+
                               "JOIN DRASORDENSD T1 ON T1.ID_ORDEN = T0.ID "+
                               "JOIN DRASPROD T2 ON T2.CLAVE = T1.PRODUCTO "+
                               "WHERE T0.FECHA_EMBARQUE >= @FECHAINI AND T0.FECHA_EMBARQUE <= @FECHAFIN AND T0.ESTADO = 'Nuevo' ORDER BY T0.FECHA_EMBARQUE, T0.ID";

            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHAINI", FbDbType.TimeStamp).Value = AFechaIni;
            com.Parameters.Add("@FECHAFIN", FbDbType.TimeStamp).Value = AFechaFin;
            
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult                = new ProyeccionProduccion();
                    pResult.Id             = (reader["ID"] != DBNull.Value) ? (int)reader["ID"] : -1;
                    pResult.Fecha_Embarque = (DateTime)reader["FECHA_EMBARQUE"];
                    pResult.Producto       = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pResult.CodigoSAP      = (reader["CODIGOSAP"] != DBNull.Value) ? (string)reader["CODIGOSAP"] : "";
                    pResult.Descripcion    = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.Descripcion2   = (reader["DESCRIPCION2"] != DBNull.Value) ? (string)reader["DESCRIPCION2"] : "";
                    pResult.Inventario     = (reader["INVENTARIO"] != DBNull.Value) ? (int)reader["INVENTARIO"] : 0;
                    pResult.Cajas          = (reader["CAJAS"] != DBNull.Value) ? (int)reader["CAJAS"] : 0;
                    pProyeccionProduccion.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return pProyeccionProduccion;
        }

        public List<ProyeccionProduccion> ObtenerProyeccionProduccionNacional(string AFechaIni, string AFechaFin)
        {
            List<ProyeccionProduccion> pProyeccionProduccion = new List<ProyeccionProduccion>();
            ProyeccionProduccion pResult = null;

            string pSentencia = "SELECT MAX(PRODUCTO) AS PRODUCTO, MAX(CODIGOSAP) AS CODIGOSAP, MAX(DESCRIPCION) AS DESCRIPCION, MAX(INVENTARIO) AS INVENTARIO FROM "+
                               "(SELECT T0.FECHA_EMBARQUE, T0.ID, T1.PRODUCTO, T2.CODIGOSAP, T2.DESCRIPCION, T0.DESCRIPCION AS DESCRIPCION2, T1.CAJAS, "+
                               "(SELECT COUNT(*) FROM DRASCORT WHERE PRODUCTO = T1.PRODUCTO AND EMBARCADO = 'No' AND CAMARA NOT IN(SELECT ID FROM DRASCAM WHERE ACTIVO = 'No')) AS INVENTARIO "+
                               "FROM DRASORDENSM T0 "+
                               "JOIN DRASORDENSD T1 ON T1.ID_ORDEN = T0.ID "+
                               "JOIN DRASPROD T2 ON T2.CLAVE = T1.PRODUCTO "+
                               "WHERE T0.FECHA_EMBARQUE >= @FECHAINI AND T0.FECHA_EMBARQUE <= @FECHAFIN AND T0.ESTADO = 'Nuevo' AND T0.Tipo = 'Nacional' "+
                               "ORDER BY T0.FECHA_EMBARQUE, T0.ID) A "+
                               "GROUP BY A.PRODUCTO, A.DESCRIPCION, A.INVENTARIO";

            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHAINI", FbDbType.TimeStamp).Value = AFechaIni;
            com.Parameters.Add("@FECHAFIN", FbDbType.TimeStamp).Value = AFechaFin;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult = new ProyeccionProduccion();
                    pResult.Producto    = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pResult.CodigoSAP   = (reader["CODIGOSAP"] != DBNull.Value) ? (string)reader["CODIGOSAP"] : "";
                    pResult.Descripcion = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.Inventario  = (reader["INVENTARIO"] != DBNull.Value) ? (int)reader["INVENTARIO"] : 0;
                    pProyeccionProduccion.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return pProyeccionProduccion;
        }

        public List<ProyeccionProduccion> ObtenerProyeccionProduccionDetalleNacional(string AFechaIni, string AFechaFin)
        {
            List<ProyeccionProduccion> pProyeccionProduccion = new List<ProyeccionProduccion>();
            ProyeccionProduccion pResult = null;

            string pSentencia = "SELECT T0.FECHA_EMBARQUE, T0.ID, T1.PRODUCTO, T2.CODIGOSAP, T2.DESCRIPCION, T0.DESCRIPCION AS DESCRIPCION2, T1.CAJAS, "+
                               "(SELECT COUNT(*) FROM DRASCORT WHERE PRODUCTO = T1.PRODUCTO AND EMBARCADO = 'No' AND CAMARA NOT IN(SELECT ID FROM DRASCAM WHERE ACTIVO = 'No')) AS INVENTARIO "+
                               "FROM DRASORDENSM T0 "+
                               "JOIN DRASORDENSD T1 ON T1.ID_ORDEN = T0.ID "+
                               "JOIN DRASPROD T2 ON T2.CLAVE = T1.PRODUCTO "+
                               "WHERE T0.FECHA_EMBARQUE >= @FECHAINI AND T0.FECHA_EMBARQUE <= @FECHAFIN AND T0.ESTADO = 'Nuevo'  AND T0.Tipo = 'Nacional' ORDER BY T0.FECHA_EMBARQUE, T0.ID";

            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHAINI", FbDbType.TimeStamp).Value = AFechaIni;
            com.Parameters.Add("@FECHAFIN", FbDbType.TimeStamp).Value = AFechaFin;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult                = new ProyeccionProduccion();
                    pResult.Id             = (reader["ID"] != DBNull.Value) ? (int)reader["ID"] : -1;
                    pResult.Fecha_Embarque = (DateTime)reader["FECHA_EMBARQUE"];
                    pResult.Producto       = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pResult.CodigoSAP      = (reader["CODIGOSAP"] != DBNull.Value) ? (string)reader["CODIGOSAP"] : "";
                    pResult.Descripcion    = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.Descripcion2   = (reader["DESCRIPCION2"] != DBNull.Value) ? (string)reader["DESCRIPCION2"] : "";
                    pResult.Inventario     = (reader["INVENTARIO"] != DBNull.Value) ? (int)reader["INVENTARIO"] : 0;
                    pResult.Cajas          = (reader["CAJAS"] != DBNull.Value) ? (int)reader["CAJAS"] : 0;
                    pProyeccionProduccion.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return pProyeccionProduccion;
        }

        public List<ProyeccionProduccion> ObtenerProyeccionProduccionExportacion(string AFechaIni, string AFechaFin)
        {
            List<ProyeccionProduccion> pProyeccionProduccion = new List<ProyeccionProduccion>();
            ProyeccionProduccion pResult = null;

            string pSentencia = "SELECT MAX(PRODUCTO) AS PRODUCTO, MAX(CODIGOSAP) AS CODIGOSAP, MAX(DESCRIPCION) AS DESCRIPCION, MAX(INVENTARIO) AS INVENTARIO FROM " +
                               "(SELECT T0.FECHA_EMBARQUE, T0.ID, T1.PRODUCTO, T2.CODIGOSAP, T2.DESCRIPCION, T0.DESCRIPCION AS DESCRIPCION2, T1.CAJAS, " +
                               "(SELECT COUNT(*) FROM DRASCORT WHERE PRODUCTO = T1.PRODUCTO AND EMBARCADO = 'No' AND CAMARA NOT IN(SELECT ID FROM DRASCAM WHERE ACTIVO = 'No')) AS INVENTARIO " +
                               "FROM DRASORDENSM T0 " +
                               "JOIN DRASORDENSD T1 ON T1.ID_ORDEN = T0.ID " +
                               "JOIN DRASPROD T2 ON T2.CLAVE = T1.PRODUCTO " +
                               "WHERE T0.FECHA_EMBARQUE >= @FECHAINI AND T0.FECHA_EMBARQUE <= @FECHAFIN AND T0.ESTADO = 'Nuevo' AND T0.Tipo = 'Exportacion' " +
                               "ORDER BY T0.FECHA_EMBARQUE, T0.ID) A " +
                               "GROUP BY A.PRODUCTO, A.DESCRIPCION, A.INVENTARIO";

            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHAINI", FbDbType.TimeStamp).Value = AFechaIni;
            com.Parameters.Add("@FECHAFIN", FbDbType.TimeStamp).Value = AFechaFin;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult             = new ProyeccionProduccion();
                    pResult.Producto    = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pResult.CodigoSAP   = (reader["CODIGOSAP"] != DBNull.Value) ? (string)reader["CODIGOSAP"] : "";
                    pResult.Descripcion = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.Inventario  = (reader["INVENTARIO"] != DBNull.Value) ? (int)reader["INVENTARIO"] : 0;
                    pProyeccionProduccion.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return pProyeccionProduccion;
        }

        public List<ProyeccionProduccion> ObtenerProyeccionProduccionDetalleExportacion(string AFechaIni, string AFechaFin)
        {
            List<ProyeccionProduccion> pProyeccionProduccion = new List<ProyeccionProduccion>();
            ProyeccionProduccion pResult = null;
            
            string pSentencia = "SELECT T0.FECHA_EMBARQUE, T0.ID, T1.PRODUCTO, T2.CODIGOSAP, T2.DESCRIPCION, T0.DESCRIPCION AS DESCRIPCION2, T1.CAJAS, " +
                               "(SELECT COUNT(*) FROM DRASCORT WHERE PRODUCTO = T1.PRODUCTO AND EMBARCADO = 'No' AND CAMARA NOT IN(SELECT ID FROM DRASCAM WHERE ACTIVO = 'No')) AS INVENTARIO " +
                               "FROM DRASORDENSM T0 " +
                               "JOIN DRASORDENSD T1 ON T1.ID_ORDEN = T0.ID " +
                               "JOIN DRASPROD T2 ON T2.CLAVE = T1.PRODUCTO " +
                               "WHERE T0.FECHA_EMBARQUE >= @FECHAINI AND T0.FECHA_EMBARQUE <= @FECHAFIN AND T0.ESTADO = 'Nuevo'  AND T0.Tipo = 'Exportacion' ORDER BY T0.FECHA_EMBARQUE, T0.ID";
            
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHAINI", FbDbType.TimeStamp).Value = AFechaIni;
            com.Parameters.Add("@FECHAFIN", FbDbType.TimeStamp).Value = AFechaFin;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult                = new ProyeccionProduccion();
                    pResult.Id             = (reader["ID"] != DBNull.Value) ? (int)reader["ID"] : -1;
                    pResult.Fecha_Embarque = (DateTime)reader["FECHA_EMBARQUE"];
                    pResult.Producto       = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pResult.CodigoSAP      = (reader["CODIGOSAP"] != DBNull.Value) ? (string)reader["CODIGOSAP"] : "";
                    pResult.Descripcion    = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.Descripcion2   = (reader["DESCRIPCION2"] != DBNull.Value) ? (string)reader["DESCRIPCION2"] : "";
                    pResult.Inventario     = (reader["INVENTARIO"] != DBNull.Value) ? (int)reader["INVENTARIO"] : 0;
                    pResult.Cajas          = (reader["CAJAS"] != DBNull.Value) ? (int)reader["CAJAS"] : 0;
                    pProyeccionProduccion.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return pProyeccionProduccion;
        }
    }
}
