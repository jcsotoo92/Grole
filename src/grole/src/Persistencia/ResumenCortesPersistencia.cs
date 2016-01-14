using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Persistencia
{
    public class ResumenCortesPersistencia
    {
        private Conexiones _Conexiones { get; set; }

        public ResumenCortesPersistencia(Conexiones AConexion)
        {
            this._Conexiones = AConexion;
        }

        //Areas Usuario
        public List<AreaCortes> ObtenerAreasUsuario(int AId)
        {
            string pSentencia = "SELECT ID, ID_AREA, (SELECT DESCRIPCION FROM CLASIFICACIONCORTES WHERE ID = ID_AREA) FROM AREA_CORTES WHERE ID_USUARIO = @ID_USUARIO";
            FbConnection con = _Conexiones.ObtenerConexion();
            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID_USUARIO", FbDbType.Integer).Value = AId;
            List<AreaCortes> pResult = new List<AreaCortes>();
            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    AreaCortes pAreaCortes  = new AreaCortes();
                    pAreaCortes.Id          = (reader["ID"] != DBNull.Value) ? (int)reader["ID"] : -1;
                    pAreaCortes.Id_Area     = (reader["ID_AREA"] != DBNull.Value) ? (int)reader["ID_AREA"] : -1;
                    pAreaCortes.Descripcion = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.Add(pAreaCortes);
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

        //Lotes
        public string ObtenerLotesArea(int AArea)
        {
            string pSentencia = "SELECT LOTES FROM CLASIFICACIONCORTES WHERE ID = @ID";
            FbConnection con = _Conexiones.ObtenerConexion();
            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value = AArea;
            string lotes ="";

            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    lotes = reader.GetString(0);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return lotes;
        }

        public List<AreaCortes> ObtenerResumenCortesArea(DateTime AFecha, int AArea)
        {
            string pLotes = ObtenerLotesArea(AArea);
            string[] words=pLotes.Split(',');
            string pLote = "";
            for (int i = 0; i < words.Length; i++)
            {
                if(i==0)
                    pLote = pLote + "'" + words[i] + "'";
                else
                    pLote = pLote + ",'" + words[i] + "'";
            }

           
            string Fecha = AFecha.ToString("MM.dd.yyyy") + ", 00:00:00.000"; 
            string pSentencia = "SELECT FECHA, PRODUCTO, (SELECT DESCRIPCION FROM DRASPROD WHERE CLAVE = PRODUCTO) AS DESCRIPCION, LOTE, SUM(PESO) AS PESO, SUM(PESONETO) AS PESONETO, SUM(CANTIDAD) AS CANTIDAD FROM DRASRESUMENCORTES WHERE FECHA = '"+Fecha+"' AND LOTE IN ("+pLote+") GROUP BY FECHA, LOTE, PRODUCTO";
            Console.WriteLine(pSentencia);
            FbConnection con = _Conexiones.ObtenerConexion();
            FbCommand com = new FbCommand(pSentencia, con);
            List<AreaCortes> pResult = new List<AreaCortes>();

            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                { /* Fecha, Producto, Descripcion, Lote, Peso, PesoNeto, Cantidad */
                    AreaCortes pAreaCortes  = new AreaCortes();
                    pAreaCortes.Fecha       = reader["FECHA"] != DBNull.Value ? (DateTime?)reader["FECHA"] : null;
                    pAreaCortes.Producto    = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pAreaCortes.Descripcion = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pAreaCortes.Lote        = (reader["LOTE"] != DBNull.Value) ? (int)reader["LOTE"] : -1;
                    pAreaCortes.Peso        = reader["PESO"] != DBNull.Value ? (Decimal)reader["PESO"] : 0;
                    pAreaCortes.PesoNeto    = reader["PESONETO"] != DBNull.Value ? (Decimal)reader["PESONETO"] : 0;
                    pAreaCortes.Cantidad    = (reader["CANTIDAD"] != DBNull.Value) ? (int)reader["CANTIDAD"] : -1;
                    pResult.Add(pAreaCortes);
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

        public string CambiarResumenVerificado(DateTime AFecha, int AArea, int AUsuario)
        {
            string pLotes = ObtenerLotesArea(AArea);
            string[] words = pLotes.Split(',');
            string pLote = "";
            for (int i = 0; i < words.Length; i++)
            {
                if (i == 0)
                    pLote = pLote + "'" + words[i] + "'";
                else
                    pLote = pLote + ",'" + words[i] + "'";
            }


            string Fecha = AFecha.ToString("MM.dd.yyyy") + ", 00:00:00.000";
            string pSentencia = "UPDATE DRASRESUMENCORTES SET VERIFICADO = 'Si', USUARIO_ADMIN = @USUARIO_ADMIN WHERE FECHA ='" + Fecha + "' AND LOTE IN (" + pLote + ")";
            Console.WriteLine(pSentencia);
            FbConnection con = _Conexiones.ObtenerConexion();
            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@USUARIO_ADMIN", FbDbType.VarChar).Value = AUsuario;

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
            InsertarVerificada(AFecha, AArea);
            return "La producción se marco como buena";
        }

        public Boolean ProduccionVerificada(DateTime AFecha, int AArea)
        {
            string Fecha = AFecha.ToString("MM.dd.yyyy") + ", 00:00:00.000";
            string pSentencia = "SELECT COUNT(ID) AS CANTIDAD FROM PRODUCCION_VERIFICADA WHERE FECHA = '" + Fecha + "' AND AREA = " + AArea;
            FbConnection con = _Conexiones.ObtenerConexion();
            FbCommand com = new FbCommand(pSentencia, con);
            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    int cantidad=reader.GetInt32(0);
                    Console.WriteLine("Cantidad: " + cantidad);
                    if (cantidad > 0)
                        return true;
                    else
                        return false;
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

        public int InsertarVerificada(DateTime AFecha, int AArea)
        {
            
            string Fecha = AFecha.ToString("MM.dd.yyyy") + ", 00:00:00.000";
            string pSentencia = "INSERT INTO PRODUCCION_VERIFICADA(FECHA, AREA) VALUES('" + Fecha + "', '" + AArea + "')";
            FbConnection con = _Conexiones.ObtenerConexion();
            FbCommand com = new FbCommand(pSentencia, con);
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
            return 1;
        }

        public List<ClasificacionCorte> ObtenerListaClasificaciones()
        {
            string pSentencia = "SELECT ID, DESCRIPCION, LOTES FROM CLASIFICACIONCORTES";
            FbConnection con = _Conexiones.ObtenerConexion();
            FbCommand com = new FbCommand(pSentencia, con);
            List<ClasificacionCorte> pResult = new List<ClasificacionCorte>();
            try
            {
                
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    ClasificacionCorte pClasificacionCorte = new ClasificacionCorte();
                    pClasificacionCorte.Id                 = (reader["ID"] != DBNull.Value) ? (int)reader["ID"] : -1;
                    pClasificacionCorte.Descripcion        = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pClasificacionCorte.Lotes              = (reader["LOTES"] != DBNull.Value) ? (string)reader["LOTES"] : "";
                    pResult.Add(pClasificacionCorte);
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
        public List<AreaCortes> ObtenerResumenCortesAdmin(DateTime AFecha, string ALotes)
        {
            string pLotes = ALotes;
            string[] words = pLotes.Split(',');
            string pLote = "";
            for (int i = 0; i < words.Length; i++)
            {
                if (i == 0)
                    pLote = pLote + "'" + words[i] + "'";
                else
                    pLote = pLote + ",'" + words[i] + "'";
            }


            string Fecha = AFecha.ToString("MM.dd.yyyy") + ", 00:00:00.000";
            
            string pSentencia = "SELECT ID, FECHA, PRODUCTO, (SELECT DESCRIPCION FROM DRASPROD WHERE CLAVE = PRODUCTO) AS DESCRIPCION, LOTE, SUM(PESO)AS PESO, SUM(PESONETO) AS PESONETO, SUM(CANTIDAD) AS CANTIDAD FROM DRASRESUMENCORTES WHERE FECHA = '" + Fecha + "' AND LOTE IN(" + pLote + ") GROUP BY ID, FECHA, LOTE, PRODUCTO";
            FbConnection con = _Conexiones.ObtenerConexion();
            FbCommand com = new FbCommand(pSentencia, con);
            List<AreaCortes> pResult = new List<AreaCortes>();

            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                { /* Fecha, Producto, Descripcion, Lote, Peso, PesoNeto, Cantidad */
                    AreaCortes pAreaCortes  = new AreaCortes();
                    pAreaCortes.Id          = (reader["ID"] != DBNull.Value) ? (int)reader["ID"] : -1;
                    pAreaCortes.Fecha       = reader["FECHA"] != DBNull.Value ? (DateTime?)reader["FECHA"] : null;
                    pAreaCortes.Producto    = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pAreaCortes.Descripcion = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pAreaCortes.Lote        = (reader["LOTE"] != DBNull.Value) ? (int)reader["LOTE"] : -1;
                    pAreaCortes.Peso        = reader["PESO"] != DBNull.Value ? (Decimal)reader["PESO"] : 0;
                    pAreaCortes.PesoNeto    = reader["PESONETO"] != DBNull.Value ? (Decimal)reader["PESONETO"] : 0;
                    pAreaCortes.Cantidad    = (reader["CANTIDAD"] != DBNull.Value) ? (int)reader["CANTIDAD"] : -1;
                    pResult.Add(pAreaCortes);
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

        public AreaCortes ObtenerResumenCortesPorId(int AId)
        {
            string pSentencia = "SELECT ID, FECHA, PRODUCTO, (SELECT DESCRIPCION FROM DRASPROD WHERE CLAVE = PRODUCTO) AS DESCRIPCION, LOTE, SUM(PESO)AS PESO, SUM(PESONETO) AS PESONETO, SUM(CANTIDAD) AS CANTIDAD FROM DRASRESUMENCORTES WHERE ID = @ID GROUP BY ID, FECHA, LOTE, PRODUCTO";
            FbConnection con = _Conexiones.ObtenerConexion();
            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value = AId;
            AreaCortes pResult = new AreaCortes();

            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                { /* Fecha, Producto, Descripcion, Lote, Peso, PesoNeto, Cantidad */
                    AreaCortes pAreaCortes  = new AreaCortes();
                    pAreaCortes.Id          = (reader["ID"] != DBNull.Value) ? (int)reader["ID"] : -1;
                    pAreaCortes.Fecha       = reader["FECHA"] != DBNull.Value ? (DateTime?)reader["FECHA"] : null;
                    pAreaCortes.Producto    = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pAreaCortes.Descripcion = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pAreaCortes.Lote        = (reader["LOTE"] != DBNull.Value) ? (int)reader["LOTE"] : -1;
                    pAreaCortes.Peso        = reader["PESO"] != DBNull.Value ? (Decimal)reader["PESO"] : 0;
                    pAreaCortes.PesoNeto    = reader["PESONETO"] != DBNull.Value ? (Decimal)reader["PESONETO"] : 0;
                    pAreaCortes.Cantidad    = (reader["CANTIDAD"] != DBNull.Value) ? (int)reader["CANTIDAD"] : -1;
                    pResult                 = pAreaCortes;
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

        public string CambiaFechaProduccion(DateTime fecha, int id, DateTime fecha_ant)
        {
            string Fecha = fecha.ToString("MM.dd.yyyy") + ", 00:00:00.000";
            string Fecha_ant = fecha_ant.ToString("MM.dd.yyyy") + ", 00:00:00.000";
            string pSentencia = "UPDATE DRASRESUMENCORTES SET FECHA = '" + Fecha + "' WHERE ID = @ID";
            FbConnection con = _Conexiones.ObtenerConexion();
            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.VarChar).Value = id;

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
            return "La fecha se actualizo!";
        }
    }
}
