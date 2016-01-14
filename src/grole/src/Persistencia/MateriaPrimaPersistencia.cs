using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Persistencia
{
    public class MateriaPrimaPersistencia
    {
        private Conexiones _Conexion;
        public MateriaPrimaPersistencia(Conexiones _Conexion)
        {
            this._Conexion = _Conexion;
        }
        public MateriaPrimaM ObtenerMateriaPrima(int AIdProg)
        {
            MateriaPrimaM pResult = null;
            string pSentencia = "SELECT * FROM DRASMATPRIMAM WHERE ID_PROG = @IDPROG";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@IDPROG", FbDbType.Integer).Value = AIdProg;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    pResult                       = new MateriaPrimaM();
                    pResult.Id                    = reader["ID"] != DBNull.Value ? (int)reader["ID"] : -1;
                    pResult.Id_Prog               = reader["ID_PROG"] != DBNull.Value ? (int)reader["ID_PROG"] : -1;
                    pResult.Producto              = reader["PRODUCTO"] != DBNull.Value ? (string)reader["PRODUCTO"] : "";
                    pResult.Descripcion           = reader["DESCRIPCION"] != DBNull.Value ? (string)reader["DESCRIPCION"] : "";
                    pResult.Decomiso_Kilos        = reader["DECOMISO_KILOS"] != DBNull.Value ? (decimal)reader["DECOMISO_KILOS"] : 0;
                    pResult.Merma                 = reader["MERMA"] != DBNull.Value ? (decimal)reader["MERMA"] : 0;
                    pResult.Rendimiento           = reader["RENDIMIENTO"] != DBNull.Value ? (decimal)reader["RENDIMIENTO"] : 0;
                    pResult.Decomiso_Kilos_Moldeo = reader["DECOMISO_KILOS_MOLDEO"] != DBNull.Value ? (decimal)reader["DECOMISO_KILOS_MOLDEO"] : 0;
                    pResult.Merma_Moldeo          = reader["MERMA_MOLDEO"] != DBNull.Value ? (decimal)reader["MERMA_MOLDEO"] : 0;
                    pResult.Decomiso_Moldeo       = reader["DECOMISO_MOLDEO"] != DBNull.Value ? (decimal)reader["DECOMISO_MOLDEO"] : 0;
                    pResult.Rendimiento_Moldeo    = reader["RENDIMIENTO_MOLDEO"] != DBNull.Value ? (decimal)reader["RENDIMIENTO_MOLDEO"] : 0;
                    pResult.Estado                = reader["ESTADO"] != DBNull.Value ? (string)reader["ESTADO"] : "";
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

        public MateriaPrimaM InsertarMateriaPrima(int AIdProg, string AProducto, string ADescripcion, decimal ADecomisoKilos, decimal AMerma, decimal ARendimiento, decimal ADecomisoKilosM, decimal AMermaMoldeo, decimal ARendimientoMoldeo)
        {
            string pSentencia = "INSERT INTO DRASMATPRIMAM (ID_PROG, PRODUCTO, DESCRIPCION, DECOMISO_KILOS,MERMA, RENDIMIENTO, DECOMISO_KILOS_MOLDEO, MERMA_MOLDEO, RENDIMIENTO_MOLDEO) " +
                           "VALUES(@IDPROG, @PRODUCTO, @DESCRIPCION, @DECOMISOKILOS, @MERMA, @RENDIMIENTO, @DECOMISOKILOSM, @MERMAMOLDEO, @RENDIMIENTOMOLDEO)";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@IDPROG", FbDbType.Integer).Value            = AIdProg;
            com.Parameters.Add("@PRODUCTO", FbDbType.VarChar).Value          = AProducto;
            com.Parameters.Add("@DESCRIPCION", FbDbType.VarChar).Value       = ADescripcion;
            com.Parameters.Add("@DECOMISOKILOS", FbDbType.Numeric).Value     = ADecomisoKilos;
            com.Parameters.Add("@MERMA", FbDbType.Numeric).Value             = AMerma;
            com.Parameters.Add("@RENDIMIENTO", FbDbType.Numeric).Value       = ARendimiento;
            com.Parameters.Add("@DECOMISOKILOSM", FbDbType.Numeric).Value    = ADecomisoKilosM;
            com.Parameters.Add("@MERMAMOLDEO", FbDbType.Numeric).Value       = AMermaMoldeo;
            com.Parameters.Add("@RENDIMIENTOMOLDEO", FbDbType.Numeric).Value = ARendimientoMoldeo;
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

            return ObtenerMateriaPrima(AIdProg);
        }

        public string ObtenerProductosMateriaPrima(int AId)
        {
            string pResult = "";
            string pSentencia = "SELECT PRODUCTOS FROM DRASPROGCORTED WHERE ID = @ID";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value = AId;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {

                    string pProductos = reader["PRODUCTOS"] != DBNull.Value ? (string)reader["PRODUCTOS"] : "";
                    string[] pArr = pProductos.Split(',');
                    string[] pArr2 = pArr.Select(i => "'" + i + "'").ToArray();
                    pResult = String.Join(",", pArr2);
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

        public int ObtenerLoteMateriaPrima(int AId)
        {
            string pSentencia = "SELECT LOTE FROM DRASPROGCORTED WHERE ID = @ID";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value = AId;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    return reader["LOTE"] != DBNull.Value ? (int)reader["LOTE"] : -1;
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return -1;
        }

        public List<ProduccionProducto> ObtenerProduccionProductos(string AFecha, string AProductos, int ALote)
        {
            List<ProduccionProducto> pResult = new List<ProduccionProducto>();
            ProduccionProducto pProducto = null;
            string pSentencia = "SELECT T0.FECHA, T1.DESCRIPCION, T0.PRODUCTO, COUNT(T0.FOLIO) AS CAJAS, SUM(T0.PESO) "+
                "AS KILOS FROM DRASCORT T0 JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO WHERE FECHA = @FECHA AND PRODUCTO "+
                "IN(" + AProductos + ") AND LOTE = @LOTE GROUP BY T0.FECHA, T1.DESCRIPCION, T0.PRODUCTO ORDER BY T0.PRODUCTO";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value   = AFecha;
            com.Parameters.Add("@PRODUCTOS", FbDbType.VarChar).Value = AProductos;
            com.Parameters.Add("@LOTE", FbDbType.Integer).Value      = ALote;
            
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pProducto             = new ProduccionProducto();
                    pProducto.Fecha       = reader["FECHA"] != DBNull.Value ? (DateTime?)reader["FECHA"] : null;
                    pProducto.Descripcion = reader["DESCRIPCION"] != DBNull.Value ? (string)reader["DESCRIPCION"] : "";
                    pProducto.Producto    = reader["PRODUCTO"] != DBNull.Value ? (string)reader["PRODUCTO"] : "";
                    pProducto.Cajas       = reader["CAJAS"] != DBNull.Value ? (int)reader["CAJAS"] : 0;
                    pProducto.Kilos       = reader["KILOS"] != DBNull.Value ? (decimal)reader["KILOS"] : 0;
                    pResult.Add(pProducto);
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

        public int InsertarDetalleMateriaPrima(int AIdMatPrima, string AProducto, decimal ACajas, decimal AKilos, string ATipo, int AIdSalida, int AIdDestino)
        {
            int pAffected = 0;
            string pSentencia = "INSERT INTO DRASMATPRIMAD (ID_MATPRIMA, PRODUCTO, CAJAS, KILOS, TIPO, ID_SALIDA, ID_DESTINO) " +
                                "VALUES(@IDMATPRIMA, @PRODUCTO, @CAJAS, @KILOS, @TIPO, @IDSALIDA, @IDDESTINO)";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@IDMATPRIMA", FbDbType.Integer).Value = AIdMatPrima;
            com.Parameters.Add("@PRODUCTO", FbDbType.VarChar).Value   = AProducto;
            com.Parameters.Add("@CAJAS", FbDbType.Numeric).Value      = ACajas;
            com.Parameters.Add("@KILOS", FbDbType.Numeric).Value      = AKilos;
            com.Parameters.Add("@TIPO", FbDbType.VarChar).Value       = ATipo;
            com.Parameters.Add("@IDSALIDA", FbDbType.Integer).Value   = AIdSalida;
            com.Parameters.Add("@IDDESTINO", FbDbType.Integer).Value  = AIdDestino;
            try
            {

                con.Open();

                pAffected = com.ExecuteNonQuery();
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return pAffected;
        }

        public List<DetalleMateriaPrima> ObtenerDetalleMateriaPrima(int AIdMatPrima, int ADestino)
        {
            List<DetalleMateriaPrima> pResult = new List<DetalleMateriaPrima>();
            DetalleMateriaPrima pDetalleMateriaPrima = null;
            string pSentencia = "SELECT T0.*, T1.DESCRIPCION FROM DRASMATPRIMAD T0 JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO WHERE ID_MATPRIMA = @IDMATPRIMA AND ID_DESTINO = @IDDESTINO";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@IDMATPRIMA", FbDbType.Integer).Value = AIdMatPrima;
            com.Parameters.Add("@IDDESTINO", FbDbType.Integer).Value  = ADestino;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pDetalleMateriaPrima              = new DetalleMateriaPrima();
                    pDetalleMateriaPrima.Id           = reader["ID"] != DBNull.Value ? (int)reader["ID"] : -1;
                    pDetalleMateriaPrima.Id_MatPrima  = reader["ID_MATPRIMA"] != DBNull.Value ? (int)reader["ID_MATPRIMA"] : -1;
                    pDetalleMateriaPrima.Producto     = reader["PRODUCTO"] != DBNull.Value ? (string)reader["PRODUCTO"] : "";
                    pDetalleMateriaPrima.Cajas        = reader["CAJAS"] != DBNull.Value ? (decimal)reader["CAJAS"] : 0;
                    pDetalleMateriaPrima.Kilos        = reader["KILOS"] != DBNull.Value ? (decimal)reader["KILOS"] : 0;
                    pDetalleMateriaPrima.Tipo         = reader["TIPO"] != DBNull.Value ? (string)reader["TIPO"] : "";
                    pDetalleMateriaPrima.Id_Salida    = reader["ID_SALIDA"] != DBNull.Value ? (int)reader["ID_SALIDA"] : -1;
                    pDetalleMateriaPrima.Id_Destino   = reader["ID_DESTINO"] != DBNull.Value ? (int)reader["ID_DESTINO"] : -1;
                    pDetalleMateriaPrima.Subtipo      = reader["SUBTIPO"] != DBNull.Value ? (string)reader["SUBTIPO"] : "";
                    pDetalleMateriaPrima.Descripcion  = reader["DESCRIPCION"] != DBNull.Value ? (string)reader["DESCRIPCION"] : "";
                    pResult.Add(pDetalleMateriaPrima);
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

        public decimal ObtenerTotalKilosMateriaPrima(int AIdMatPrima, int ADestino)
        {
            string pSentencia = "SELECT COALESCE(SUM(KILOS), 0) AS KILOS FROM DRASMATPRIMAD WHERE ID_MATPRIMA = @IDMATPRIMA AND ID_DESTINO = @DESTINO";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@IDMATPRIMA", FbDbType.Integer).Value = AIdMatPrima;
            com.Parameters.Add("@DESTINO", FbDbType.Integer).Value = ADestino;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    return reader["KILOS"] != DBNull.Value ? (decimal)reader["KILOS"] : 0;
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return 0;
        }

        public List<DetalleMateriaPrima> ObtenerDetalleMateriaPrimaTipo(int AIdMaestro, string ATipo)
        {
            List<DetalleMateriaPrima> pResult = new List<DetalleMateriaPrima>();
            DetalleMateriaPrima pDetalleMateriaPrima = null;
            string pSentencia = "SELECT T0.PRODUCTO, T1.DESCRIPCION, COUNT(*) AS CAJAS, SUM(T0.PESO) AS KILOS, MAX(T0.ID_SALIDA) AS ID_SALIDA FROM DRASMATPRIMAD_DETALLE T0 " +
                                "JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO " +
                                "WHERE T0.ID_MAESTRO = @IDMAESTRO AND T0.TIPO = @TIPO " +
                                "GROUP BY PRODUCTO, T1.DESCRIPCION ORDER BY T0.PRODUCTO";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@IDMAESTRO", FbDbType.Integer).Value = AIdMaestro;
            com.Parameters.Add("@TIPO", FbDbType.VarChar).Value = ATipo;

            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pDetalleMateriaPrima              = new DetalleMateriaPrima();
                    pDetalleMateriaPrima.Producto     = reader["PRODUCTO"] != DBNull.Value ? (string)reader["PRODUCTO"] : "";
                    pDetalleMateriaPrima.Descripcion  = reader["DESCRIPCION"] != DBNull.Value ? (string)reader["DESCRIPCION"] : "";
                    pDetalleMateriaPrima.Cajas        = reader["CAJAS"] != DBNull.Value ? (int)reader["CAJAS"] : 0;
                    pDetalleMateriaPrima.Kilos        = reader["KILOS"] != DBNull.Value ? (decimal)reader["KILOS"] : 0;
                    pDetalleMateriaPrima.Id_Salida    = reader["ID_SALIDA"] != DBNull.Value ? (int)reader["ID_SALIDA"] : -1;
                    
                    pResult.Add(pDetalleMateriaPrima);
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

        public string ObtenerEstadoMateriaPrima(int AId)
        {
            string pResult = null;
            string pSentencia = "SELECT ESTADO FROM DRASMATPRIMAM WHERE ID = @ID";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value = AId;

            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    return reader["ESTADO"] != DBNull.Value ? (string)reader["ESTADO"] : null;
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

        public string ObtenerSubtipoMateriaPrima(int AId)
        {
            string pResult = null;
            string pSentencia = "SELECT SUBTIPO FROM DRASMATPRIMAD WHERE ID = @ID";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value = AId;

            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    return reader["SUBTIPO"] != DBNull.Value ? (string)reader["SUBTIPO"] : null;
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

        public string ActualizarEstadoMateriaPrima(int AId, string AEstado)
        {
            int pAffected = 0;
            string pSentencia = "UPDATE DRASMATPRIMAM SET ESTADO = @ESTADO WHERE ID = @ID";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value      = AId;
            com.Parameters.Add("@ESTADO", FbDbType.VarChar).Value = AEstado;

            try
            {
                con.Open();
                pAffected = com.ExecuteNonQuery();

            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            if (pAffected > 0)
                return ObtenerEstadoMateriaPrima(AId);
            else
                return null;
        }

        public string ActualizarSubtipoMateriaPrima(int AId, string ASubtipo)
        {
            int pAffected = 0;
            string pSentencia = "UPDATE DRASMATPRIMAD SET SUBTIPO = @SUBTIPO WHERE ID = @ID";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value = AId;
            com.Parameters.Add("@SUBTIPO", FbDbType.VarChar).Value = ASubtipo;

            try
            {

                con.Open();
                pAffected = com.ExecuteNonQuery();

            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            if (pAffected > 0)
                return ObtenerSubtipoMateriaPrima(AId);
            else
                return null;
        }

        public List<DesgloseMateriaPrima> ObtenerDesgloseMateriaPrima(int AIdMatPrima, string AProducto)
        {
            List<DesgloseMateriaPrima> pResult = new List<DesgloseMateriaPrima>();
            DesgloseMateriaPrima pDesgloseMateriaPrima = null;
            string pSentencia = "SELECT ID_MAESTRO, PRODUCTO, ID_SALIDA, COUNT(*) AS CAJAS, SUM(PESO) AS KILOS FROM DRASMATPRIMAD_DETALLE " +
                               "WHERE ID_MAESTRO = @ID AND PRODUCTO = @PRODUCTO " +
                               "GROUP BY ID_MAESTRO, PRODUCTO, ID_SALIDA";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value = AIdMatPrima;
            com.Parameters.Add("@PRODUCTO", FbDbType.VarChar).Value = AProducto;

            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pDesgloseMateriaPrima            = new DesgloseMateriaPrima();
                    pDesgloseMateriaPrima.Id_Maestro = reader["ID_MAESTRO"] != DBNull.Value ? (int)reader["ID_MAESTRO"] :    -1;
                    pDesgloseMateriaPrima.Producto   = reader["PRODUCTO"] != DBNull.Value ? (string)reader["PRODUCTO"] : "";
                    pDesgloseMateriaPrima.Id_Salida  = reader["ID_SALIDA"] != DBNull.Value ? (int)reader["ID_SALIDA"] : -1;
                    pDesgloseMateriaPrima.Cajas      = reader["CAJAS"] != DBNull.Value ? (int)reader["CAJAS"] : 0;
                    pDesgloseMateriaPrima.Kilos      = reader["KILOS"] != DBNull.Value ? (decimal)reader["KILOS"] : 0;
                    pResult.Add(pDesgloseMateriaPrima);
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

        public bool EliminarProductoProduccion(int AId_Salida, string AProducto, out string AMensajeError)
        {
            bool pResult = true;
            AMensajeError = "";

            string pSentencia = "DELETE FROM DRASMATPRIMAD_DETALLE WHERE ID_SALIDA = @IDSALIDA AND PRODUCTO = @PRODUCTO";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@IDSALIDA", FbDbType.Integer).Value = AId_Salida;
            com.Parameters.Add("@PRODUCTO", FbDbType.VarChar).Value = AProducto;

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

        public DetalleMateriaPrimaD ObtenerCajaMateriaPrima(string ACodigoBarras)
        {
            DetalleMateriaPrimaD pDetalleMateriaPrimaD = null;
            string pSentencia = "SELECT * FROM DRASMATPRIMAD_DETALLE WHERE CODIGOBARRAS = @CODIGOBARRAS";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CODIGOBARRAS", FbDbType.VarChar).Value = ACodigoBarras;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pDetalleMateriaPrimaD              = new DetalleMateriaPrimaD();
                    pDetalleMateriaPrimaD.Id           = reader["ID"] != DBNull.Value ? (int)reader["ID"] : -1;
                    pDetalleMateriaPrimaD.Id_Maestro   = reader["ID_MAESTRO"] != DBNull.Value ? (int)reader["ID_MAESTRO"] : -1;
                    pDetalleMateriaPrimaD.CodigoBarras = reader["CODIGOBARRAS"] != DBNull.Value ? (string)reader["CODIGOBARRAS"] : "";
                    pDetalleMateriaPrimaD.Peso         = reader["PESO"] != DBNull.Value ? (decimal)reader["PESO"] : 0;
                    pDetalleMateriaPrimaD.Tipo         = reader["TIPO"] != DBNull.Value ? (string)reader["TIPO"] : "";
                    pDetalleMateriaPrimaD.Producto     = reader["PRODUCTO"] != DBNull.Value ? (string)reader["PRODUCTO"] : "";
                    pDetalleMateriaPrimaD.Id_Salida    = reader["ID_SALIDA"] != DBNull.Value ? (int)reader["ID_SALIDA"] : -1;
                    pDetalleMateriaPrimaD.Id_Tarima    = reader["ID_TARIMA"] != DBNull.Value ? (int)reader["ID_TARIMA"] : -1;
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return pDetalleMateriaPrimaD;
        }

        public int InsertarDetalleCajaMatPrima(int AIdMatPrima, string ACodigoBarras, decimal APeso, string AProducto, string ATipo, int AIdSalida, int AIdTarima)
        {
            int pAffected = 0;
            string pSentencia = "INSERT INTO DRASMATPRIMAD_DETALLE (ID_MAESTRO, CODIGOBARRAS, PESO, PRODUCTO, TIPO, ID_SALIDA, ID_TARIMA) VALUES(@IDMAESTRO, @CODIGOBARRAS, @PESO, @PRODUCTO, @TIPO, @ID_SALIDA, @ID_TARIMA)";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@IDMAESTRO", FbDbType.Integer).Value    = AIdMatPrima;
            com.Parameters.Add("@CODIGOBARRAS", FbDbType.VarChar).Value = ACodigoBarras;
            com.Parameters.Add("@PESO", FbDbType.Numeric).Value         = APeso;
            com.Parameters.Add("@PRODUCTO", FbDbType.VarChar).Value     = AProducto;
            com.Parameters.Add("@TIPO", FbDbType.VarChar).Value         = ATipo;
            com.Parameters.Add("@ID_SALIDA", FbDbType.Integer).Value    = AIdSalida;
            com.Parameters.Add("@ID_TARIMA", FbDbType.Integer).Value    = AIdTarima;
            try
            {

                con.Open();
                pAffected = com.ExecuteNonQuery();
                
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return pAffected;
        }

        public int SalidaEnDetalleMateriaPrima(int AIdSalida)
        {
            string pSentencia = "SELECT ID_MATPRIMA FROM DRASMATPRIMAD WHERE ID_SALIDA = @IDSALIDA";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@IDSALIDA", FbDbType.Integer).Value = AIdSalida;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    return reader["ID_MATPRIMA"] != DBNull.Value ? (int)reader["ID_MATPRIMA"] : -1;
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return -1;
        }
        

    }
}
