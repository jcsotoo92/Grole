using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;
using System;
using grole.src.Entidades;

namespace grole.src.Persistencia
{
	
	public class CajasPersistencia
	{
		private Conexiones _Conexion;
		
		public CajasPersistencia(Conexiones _Conexion){
			this._Conexion=_Conexion;
		}
		
		public InventarioProducto ObtenerInventarioProducto(string AFechaIni, string AFechaFin, string AProducto){
			
			InventarioProducto pResult = null;
			
			string pSentencia = "SELECT COUNT(*) AS CAJAS, COALESCE(SUM(PESO), 0) AS KILOS FROM DRASCORT "+
					   "WHERE CAST((SUBSTRING(CODIGOBARRAS FROM 11 FOR 2) || '/' || SUBSTRING(CODIGOBARRAS FROM 13 FOR 2) || '/' || SUBSTRING(CODIGOBARRAS FROM 7 FOR 4)) AS TIMESTAMP) >= @FECHAINI AND "+
					   "CAST((SUBSTRING(CODIGOBARRAS FROM 11 FOR 2) || '/' || SUBSTRING(CODIGOBARRAS FROM 13 FOR 2) || '/' || SUBSTRING(CODIGOBARRAS FROM 7 FOR 4)) AS TIMESTAMP) <= @FECHAFIN  "+
					   "AND PRODUCTO = @PRODUCTO AND EMBARCADO = 'No'";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@PRODUCTO", FbDbType.VarChar).Value = AProducto;
			com.Parameters.Add("@FECHAINI", FbDbType.VarChar).Value = AFechaIni;
			com.Parameters.Add("@FECHAFIN", FbDbType.VarChar).Value = AFechaFin;
			try
			{
				con.Open();
				FbDataReader reader = com.ExecuteReader();
				if(reader.Read())
				{
					pResult        = new InventarioProducto();
					pResult.Cajas  = (int)reader["CAJAS"];
					pResult.Kilos  = (decimal)reader["KILOS"];
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
		
		public List<InventarioProductoDesglosado> ObtenerInventarioProductoDesglosado(string AFechaIni, string AFechaFin, string AProducto)
		{
			List<InventarioProductoDesglosado> pInventarioProdDes=new List<InventarioProductoDesglosado>();
			InventarioProductoDesglosado pResult = null;
			string pSentencia = "SELECT T0.FOLIO, T0.FECHA, (SUBSTRING(CODIGOBARRAS FROM 11 FOR 2) || '/' || SUBSTRING(CODIGOBARRAS FROM 13 FOR 2) || '/' || SUBSTRING(CODIGOBARRAS FROM 7 FOR 4))"+
			                    " AS FECHA_CODIGO_BARRAS, T0.PRODUCTO, T1.DESCRIPCION, T0.CODIGOBARRAS, T0.PESO, T0.CAMARA, T0.TARIMA FROM DRASCORT T0 JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO WHERE "+ 
			                    "CAST((SUBSTRING(CODIGOBARRAS FROM 11 FOR 2) || '/' || SUBSTRING(CODIGOBARRAS FROM 13 FOR 2) || '/' || SUBSTRING(CODIGOBARRAS FROM 7 FOR 4)) AS TIMESTAMP) >= @FECHAINI AND "+
			                    "CAST((SUBSTRING(CODIGOBARRAS FROM 11 FOR 2) || '/' || SUBSTRING(CODIGOBARRAS FROM 13 FOR 2) || '/' || SUBSTRING(CODIGOBARRAS FROM 7 FOR 4)) AS TIMESTAMP) <= @FECHAFIN "+ 
			                    "AND PRODUCTO = @PRODUCTO ORDER BY TARIMA";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@PRODUCTO", FbDbType.VarChar).Value = AProducto;
			com.Parameters.Add("@FECHAINI", FbDbType.VarChar).Value = AFechaIni;
			com.Parameters.Add("@FECHAFIN", FbDbType.VarChar).Value = AFechaFin;
			try
			{
				
				con.Open();
				FbDataReader reader = com.ExecuteReader();
				
				while(reader.Read())
				{
					pResult                   = new InventarioProductoDesglosado();
					pResult.Folio             = (int)reader["FOLIO"];
					pResult.Fecha  		      = Utilerias.dateTimeToString((DateTime)reader["FECHA"]);
					pResult.FechaCodigoBarras = (reader["FECHA_CODIGO_BARRAS"]!=DBNull.Value) ? (string)reader["FECHA_CODIGO_BARRAS"] : "";
					pResult.Producto          = (reader["PRODUCTO"]!=DBNull.Value) ? (string)reader["PRODUCTO"] : "";
					pResult.Descripcion       = (reader["DESCRIPCION"]!=DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
					pResult.CodigoBarras      = (reader["CODIGOBARRAS"]!=DBNull.Value) ? (string)reader["CODIGOBARRAS"] : "";
					pResult.Peso              = (decimal)reader["PESO"];
					pResult.Camara  		  = (int)reader["CAMARA"];
					pResult.Tarima            = (int)reader["TARIMA"];
					pInventarioProdDes.Add(pResult);
				}
			}
			finally
			{
				if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
			}
			
			return pInventarioProdDes;
		}

        public List<Corte> ObtenerCajasPorFolio(int AFolio)
        {
            List<Corte> pCajasPorFolio = new List<Corte>();
            Corte pResult = null;
            string pSentencia = "SELECT FECHA, FOLIO, PRODUCTO, LOTE, CODIGOBARRAS, PESO, EMBARCADO, COALESCE(TARIMA, 0) AS TARIMA, EMB_FECHA FROM DRASCORT WHERE FOLIO = @FOLIO ORDER BY FECHA";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FOLIO", FbDbType.Integer).Value = AFolio;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult = new Corte();
                    if(reader["FECHA"] != DBNull.Value)
                        pResult.Fecha    = (DateTime)reader["FECHA"];
                    pResult.Folio        = reader["FOLIO"] != DBNull.Value ? (int)reader["FOLIO"] : -1;
                    pResult.Producto     = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pResult.Lote         = reader["LOTE"] != DBNull.Value ? (int)reader["LOTE"] : -1;
                    pResult.CodigoBarras = (reader["CODIGOBARRAS"] != DBNull.Value) ? (string)reader["CODIGOBARRAS"] : "";
                    pResult.Peso         = reader["PESO"] != DBNull.Value ? (decimal)reader["PESO"] : -1;
                    pResult.Embarcado    = (reader["EMBARCADO"] != DBNull.Value) ? (string)reader["EMBARCADO"] : "";
                    pResult.Tarima       = (int)reader["TARIMA"];
                    if (reader["EMB_FECHA"] != DBNull.Value)
                        pResult.Emb_Fecha    = (DateTime)reader["EMB_FECHA"];

                    pCajasPorFolio.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return pCajasPorFolio;
        }

        public List<SaldoPedimento> ObtenerSaldosPedimento(int APedimento)
        {
            List<SaldoPedimento> pSaldosPedimento = new List<SaldoPedimento>();
            SaldoPedimento pResult = null;
            string pSentencia = "SELECT T0.PRODUCTO, T1.DESCRIPCION, SUM(T0.PESO) AS KILOS, "+
                               "COALESCE((SELECT SUM(R0.PESO) FROM DRASCORT R0 "+
                               "JOIN DRASRESUMENCORTES R1 ON R1.ID = R0.ID_ACUM "+
                               "WHERE R1.PEDIMENTO = @PEDIMENTO AND R0.EMBARCADO = 'Si' AND R0.PRODUCTO = T0.PRODUCTO "+
                               "GROUP BY R0.PRODUCTO "+
                               "), 0) AS SALIDAS "+
                               "FROM DRASRESUMENCORTES T0 "+
                               "JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO "+
                               "WHERE T0.PEDIMENTO = @PEDIMENTO "+
                               "GROUP BY T0.PRODUCTO, T1.DESCRIPCION";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@PEDIMENTO", FbDbType.Integer).Value = APedimento;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult             = new SaldoPedimento();
                    pResult.Producto    = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pResult.Descripcion = reader["DESCRIPCION"] != DBNull.Value ? (string)reader["DESCRIPCION"] : "";
                    pResult.Kilos       = reader["KILOS"] != DBNull.Value ? (decimal)reader["KILOS"] : -1;
                    pResult.Salidas     = reader["SALIDAS"] != DBNull.Value ? (decimal)reader["SALIDAS"] : -1;

                    pSaldosPedimento.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return pSaldosPedimento;
        }

        public List<CorteFecha> ObtenerCorteFecha(string AFecha)
        {
            List<CorteFecha> pCorteFecha = new List<CorteFecha>();
            CorteFecha pResult = null;
            string pSentencia = "SELECT T0.CAMARA, T2.DESCRIPCION AS NOMBRE_CAMARA, T0.EMBARCADO, COUNT(*) AS CAJAS, SUM(T0.PESO) AS PESO, 1 AS APLICADAS, 1 AS TRANSITORIAS, 1 AS DIFERENCIA FROM DRASCORT T0 "+
                       "JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO "+
                       "JOIN DRASCAM T2 ON T2.ID = T0.CAMARA "+
                       "WHERE FECHA = @FECHA "+
                       "GROUP BY T0.CAMARA, T0.EMBARCADO, T2.DESCRIPCION "+
                       "ORDER BY T0.CAMARA, T0.EMBARCADO";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value = AFecha;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult              = new CorteFecha();
                    pResult.Camara       = (reader["CAMARA"] != DBNull.Value) ? (int)reader["CAMARA"] : -1;
                    pResult.NombreCamara = (reader["NOMBRE_CAMARA"] != DBNull.Value) ? (string)reader["NOMBRE_CAMARA"] : "";
                    pResult.Embarcado    = (reader["EMBARCADO"] != DBNull.Value) ? (string)reader["EMBARCADO"] : "";
                    pResult.Cajas        = (reader["CAJAS"] != DBNull.Value) ? (int)reader["CAJAS"] : 0;
                    pResult.Peso         = (reader["PESO"] != DBNull.Value) ? (decimal)reader["PESO"] : 0;
                    pResult.Aplicadas    = ObtenerCajasAplicadasCorte(AFecha, (int)reader["CAMARA"], (string)reader["EMBARCADO"]);
                    pResult.Transitorias = (reader["TRANSITORIAS"] != DBNull.Value) ? (int)reader["TRANSITORIAS"] : 0;
                    pResult.Diferencia   = (reader["DIFERENCIA"] != DBNull.Value) ? (int)reader["DIFERENCIA"] : 0;

                    pCorteFecha.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return pCorteFecha;
        }

        public int ObtenerPendientesCorte(string AFecha)
        {
            int pResult = 0;
            string pSentencia = "SELECT COUNT(*) FROM DRASCORTTRAN T0 WHERE FECHA = @FECHA AND ESCANEADO = 'No' AND ESTATUS = 'E' AND PRODUCTO NOT IN (SELECT CLAVE FROM DRASPROD WHERE INVENTARIABLE = 'No')";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value = AFecha;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    pResult = (int)reader["COUNT"];
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

        public List<DetallePendienteCorte> ObtenerDetallePendientesCorte(string AFecha)
        {
            List<DetallePendienteCorte> pDetallePendienteCorte = new List<DetallePendienteCorte>();
            DetallePendienteCorte pResult = null;
            string pSentencia = "SELECT T0.FOLIO, T0.FECHA, T0.PRODUCTO, T1.DESCRIPCION, T0.CODIGOBARRAS, T0.LOTE, T0.PESO FROM DRASCORTTRAN T0 "+
                                "JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO "+
                                "WHERE T0.FECHA = @FECHA AND T0.ESCANEADO = 'No' AND T0.ESTATUS = 'E' AND PRODUCTO NOT IN (SELECT CLAVE FROM DRASPROD WHERE INVENTARIABLE = 'No')";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value = AFecha;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult = new DetallePendienteCorte();
                    pResult.Folio        = (reader["FOLIO"] != DBNull.Value) ? (int)reader["FOLIO"] : -1;
                    pResult.Fecha        = (reader["FECHA"] != DBNull.Value) ? (DateTime?)reader["FECHA"] : null;
                    pResult.Producto     = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pResult.Descripcion  = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.CodigoBarras = (reader["CODIGOBARRAS"] != DBNull.Value) ? (string)reader["CODIGOBARRAS"] : "";
                    pResult.Lote         = (reader["LOTE"] != DBNull.Value) ? (int)reader["LOTE"] : 0;
                    pResult.Peso         = (reader["PESO"] != DBNull.Value) ? (decimal)reader["PESO"] : 0;

                    pDetallePendienteCorte.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return pDetallePendienteCorte;
        }

        public int ObtenerCajasAplicadasCorte(string AFecha, int ACamara, string AEmbarcado)
        {
            string pSentencia = "SELECT COUNT(*) FROM DRASCORT WHERE "+ 
                               "FECHA = @FECHA "+
                               "AND CAMARA = @CAMARA "+
                               "AND EMBARCADO = @EMBARCADO "+
                               "AND ENTRADA_APLICADA = 'Si'";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value   = AFecha;
            com.Parameters.Add("@CAMARA", FbDbType.Integer).Value    = ACamara;
            com.Parameters.Add("@EMBARCADO", FbDbType.VarChar).Value = AEmbarcado;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    return (int)reader["COUNT"];
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

        public List<DetalleCorteFecha> ObtenerDetalleCorteFecha(string AFecha, int ACamara, string AEmbarcado)
        {
            List<DetalleCorteFecha> pDetalleCorteFecha = new List<DetalleCorteFecha>();
            DetalleCorteFecha pResult = null;
            string pSentencia = "SELECT T0.FOLIO, T0.LOTE, T0.PRODUCTO, T1.DESCRIPCION, T0.CODIGOBARRAS, T0.EMBARCADO, T0.PESO, T0.TARIMA, T0.UBICACION, "+
                               "COALESCE(T0.ENTRADA_APLICADA, 'No') AS ENTRADA_APLICADA, ID_ACUM FROM DRASCORT T0 "+
                               "JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO "+
                               "WHERE FECHA = @FECHA AND EMBARCADO = @EMBARCADO AND CAMARA = @CAMARA ORDER BY T0.PRODUCTO";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value   = AFecha;
            com.Parameters.Add("@CAMARA", FbDbType.Integer).Value    = ACamara;
            com.Parameters.Add("@EMBARCADO", FbDbType.VarChar).Value = AEmbarcado;

            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult                  = new DetalleCorteFecha();
                    pResult.Folio            = (reader["FOLIO"] != DBNull.Value) ? (int)reader["FOLIO"] : -1;
                    pResult.Lote             = (reader["LOTE"] != DBNull.Value) ? (int)reader["LOTE"] : -1;
                    pResult.Producto         = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pResult.Descripcion      = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.CodigoBarras     = (reader["CODIGOBARRAS"] != DBNull.Value) ? (string)reader["CODIGOBARRAS"] : "";
                    pResult.Embarcado        = (reader["EMBARCADO"] != DBNull.Value) ? (string)reader["EMBARCADO"] : "";
                    pResult.Peso             = (reader["PESO"] != DBNull.Value) ? (decimal)reader["PESO"] : 0;
                    pResult.Tarima           = (reader["TARIMA"] != DBNull.Value) ? (int)reader["TARIMA"] : -1;
                    pResult.Ubicacion        = (reader["UBICACION"] != DBNull.Value) ? (string)reader["UBICACION"] : "";
                    pResult.Entrada_Aplicada = (reader["ENTRADA_APLICADA"] != DBNull.Value) ? (string)reader["ENTRADA_APLICADA"] : "";
                    pResult.Id_Acum          = (reader["ID_ACUM"] != DBNull.Value) ? (int)reader["ID_ACUM"] : 0;
                    pDetalleCorteFecha.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return pDetalleCorteFecha;
        }

        public List<ProduccionNoInventariada> ObtenerProduccionNoInventariadas(string AProducto, string AFechaIni, string AFechaFin)
        {
            List<ProduccionNoInventariada> pProduccionNoInventariada = new List<ProduccionNoInventariada>();
            ProduccionNoInventariada pResult = null;
            string pSentencia = "SELECT T0.FECHA, T0.LOTE, T0.PRODUCTO, T1.DESCRIPCION, COUNT(*) AS CANTIDAD, SUM(PESO) AS PESO FROM DRASCORTTRAN T0 " +
                                "JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO " +
                                "WHERE T0.FECHA >= @FECHAINI AND T0.FECHA <= @FECHAFIN ";

            if (AProducto != "0")
                pSentencia = pSentencia + "AND T0.PRODUCTO = '" + AProducto + "' AND LOTE IN (SELECT LOTE FROM LOTES_NO_INVENTARIABLES) ";

            pSentencia = pSentencia + "GROUP BY T0.FECHA, T0.LOTE, T0.PRODUCTO, T1.DESCRIPCION " +
                                    "ORDER BY FECHA, PRODUCTO, LOTE ";

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
                    pResult             = new ProduccionNoInventariada();
                    pResult.Fecha       = (reader["FECHA"] != DBNull.Value) ? (DateTime?)reader["FECHA"] : null;
                    pResult.Lote        = (reader["LOTE"] != DBNull.Value) ? (int)reader["LOTE"] : -1;
                    pResult.Producto    = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pResult.Descripcion = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.Cantidad    = (reader["CANTIDAD"] != DBNull.Value) ? (int)reader["CANTIDAD"] : 0;
                    pResult.Peso        = (reader["PESO"] != DBNull.Value) ? (decimal)reader["PESO"] : 0;

                    pProduccionNoInventariada.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return pProduccionNoInventariada;
        }

        public bool EliminarProduccionNoInventariable(string AFecha, int ALote, string AProducto, out string AMensajeError)
        {
            bool pResult = true;
            AMensajeError = "";

            string pSentencia = "DELETE FROM DRASCORTTRAN WHERE FECHA = @FECHA AND LOTE = @LOTE AND PRODUCTO = @PRODUCTO";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value  = AFecha;
            com.Parameters.Add("@LOTE", FbDbType.Integer).Value     = ALote;
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

        public List<ProduccionPorBascula> ObtenerProduccionPorBascula(string AFecha, int ABascula)
        {
            List<ProduccionPorBascula> listaTarimas = new List<ProduccionPorBascula>();
            ProduccionPorBascula pResult = null;

            string pSentencia = "SELECT T0.FOLIO, T0.FECHA, T0.HORA, T0.LOTE, T0.PRODUCTO, T1.DESCRIPCION, T0.CODIGOBARRAS, T0.PESO, COALESCE(T0.CONSECUTIVO_BASCULA, 0) " +
                                "AS CONSECUTIVO_BASCULA, T0.EMBARCADO, T0.ORDEN_PRODUCCION, T0.HORA FROM DRASCORTTRAN T0 " +
                                "JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO " +
                                "WHERE T0.FECHA = @FECHA AND T0.BASCULA = @BASCULA ORDER BY T0.CONSECUTIVO_BASCULA";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value = AFecha;
            com.Parameters.Add("@BASCULA", FbDbType.Integer).Value = ABascula;
            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pResult = new ProduccionPorBascula();
                    pResult.Folio               = reader["FOLIO"] != DBNull.Value ? (int)reader["FOLIO"] : -1;
                    pResult.Fecha               = reader["FECHA"] != DBNull.Value ? (DateTime?)reader["FECHA"] : null;
                    pResult.Hora                = reader["HORA"] != DBNull.Value ? (string)reader["HORA"] : "";
                    pResult.Lote                = reader["LOTE"] != DBNull.Value ? (int)reader["LOTE"] : -1;
                    pResult.Producto            = reader["PRODUCTO"] != DBNull.Value ? (string)reader["PRODUCTO"] : "";
                    pResult.Descripcion         = reader["DESCRIPCION"] != DBNull.Value ? (string)reader["DESCRIPCION"] : "";
                    pResult.CodigoBarras        = reader["CODIGOBARRAS"] != DBNull.Value ? (string)reader["CODIGOBARRAS"] : "";
                    pResult.Peso                = reader["PESO"] != DBNull.Value ? (decimal)reader["PESO"] : 0;
                    pResult.Consecutivo_Bascula = reader["CONSECUTIVO_BASCULA"] != DBNull.Value ? (int)reader["CONSECUTIVO_BASCULA"] : -1;
                    pResult.Embarcado           = reader["EMBARCADO"] != DBNull.Value ? (string)reader["EMBARCADO"] : "";
                    pResult.OrdenProduccion     = reader["ORDEN_PRODUCCION"] != DBNull.Value ? (int)reader["ORDEN_PRODUCCION"] : -1;
                    listaTarimas.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return listaTarimas;
        }

        public List<ProduccionNoInventariada> ObtenerProduccionNoInventariadasPorProducto(string AProducto, string AFechaIni, string AFechaFin)
        {
            List<ProduccionNoInventariada> pProduccionNoInventariada = new List<ProduccionNoInventariada>();
            ProduccionNoInventariada pResult = null;
            string pSentencia = "SELECT T0.FECHA, T0.LOTE, T0.PRODUCTO, T1.DESCRIPCION, COUNT(*) AS CANTIDAD, SUM(T0.PESO) AS PESO, SUM(T0.PESO) AS PESO FROM DRASCORTTRAN T0 "+
                                "JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO "+
                                "WHERE T0.FECHA >= ? AND T0.FECHA <= ? "+
                                "AND T0.PRODUCTO IN(SELECT CLAVE FROM DRASPROD WHERE CLAVE NOT CONTAINING('-') AND INVENTARIABLE = 'No')";

            if (AProducto != "0")
                pSentencia = pSentencia + "AND T0.PRODUCTO = '" + AProducto + "' ";

            pSentencia = pSentencia + "GROUP BY T0.FECHA, T0.LOTE, T0.PRODUCTO, T1.DESCRIPCION ORDER BY FECHA, PRODUCTO, LOTE ";

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
                    pResult             = new ProduccionNoInventariada();
                    pResult.Fecha       = (reader["FECHA"] != DBNull.Value) ? (DateTime?)reader["FECHA"] : null;
                    pResult.Lote        = (reader["LOTE"] != DBNull.Value) ? (int)reader["LOTE"] : -1;
                    pResult.Producto    = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pResult.Descripcion = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.Cantidad    = (reader["CANTIDAD"] != DBNull.Value) ? (int)reader["CANTIDAD"] : 0;
                    pResult.Peso        = (reader["PESO"] != DBNull.Value) ? (decimal)reader["PESO"] : 0;

                    pProduccionNoInventariada.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return pProduccionNoInventariada;
        }

        public List<DesgloseProductoCamara> ObtenerDesgloseProductoPorCamara(string AProducto)
        {
            List<DesgloseProductoCamara> pDesgloseProductoCamara = new List<DesgloseProductoCamara>();
            DesgloseProductoCamara pResult = null;
            string pSentencia = "SELECT T0.PRODUCTO, T1.DESCRIPCION, T0.CAMARA, T2.DESCRIPCION AS NOMBRE_CAMARA, COUNT(*) AS CAJAS FROM DRASCORT T0 "+
                               "JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO "+
                               "JOIN DRASCAM T2 ON T2.ID = T0.CAMARA "+
                               "WHERE T0.PRODUCTO = @PRODUCTO AND T0.EMBARCADO = 'No' "+
                               "AND T2.ACTIVO = 'Si' "+
                               "GROUP BY T0.PRODUCTO, T1.DESCRIPCION, T0.CAMARA, T2.DESCRIPCION";

            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);

            com.Parameters.Add("@PRODUCTO", FbDbType.VarChar).Value = AProducto;

            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult = new DesgloseProductoCamara();
                    
                    pResult.Producto      = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pResult.Descripcion   = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.Camara        = (reader["CAMARA"] != DBNull.Value) ? (int)reader["CAMARA"] : -1;
                    pResult.Nombre_Camara = (reader["NOMBRE_CAMARA"] != DBNull.Value) ? (string)reader["NOMBRE_CAMARA"] : "";
                    pResult.Cajas         = (reader["CAJAS"] != DBNull.Value) ? (int)reader["CAJAS"] : 0;
                    pDesgloseProductoCamara.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return pDesgloseProductoCamara;
        }

        public List<ResumenProductoPorCamara> ObtenerResumenPtosCamara(int ACamara)
        {
            List<ResumenProductoPorCamara> ResumenPtosCamara = new List<ResumenProductoPorCamara>();
            ResumenProductoPorCamara pResult = null;
            string pSentencia = "SELECT T0.PRODUCTO, T1.DESCRIPCION, T1.CODIGOSAP, COUNT(*) AS CAJAS, SUM(T0.PESO) AS KILOS FROM DRASCORT T0 "+
                               "JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO "+
                               "WHERE T0.CAMARA IN (@CAMARA) AND T0.EMBARCADO = 'No' "+
                               "GROUP BY T0.PRODUCTO, T1.DESCRIPCION, T1.CODIGOSAP";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CAMARA", FbDbType.Integer).Value = ACamara;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult             = new ResumenProductoPorCamara();
                    pResult.Producto    = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pResult.Descripcion = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.CodigoSAP   = (reader["CODIGOSAP"] != DBNull.Value) ? (string)reader["CODIGOSAP"] : "";
                    pResult.Cajas       = (reader["CAJAS"] != DBNull.Value) ? (int)reader["CAJAS"] : 0;
                    pResult.Kilos       = (reader["KILOS"] != DBNull.Value) ? (decimal)reader["KILOS"] : 0;
                    pResult.Camara      = ACamara;
                    ResumenPtosCamara.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return ResumenPtosCamara;
        }

        public List<Corte> ObtenerDetalleProductosCamara(int ACamara, string AProducto)
        {
            List<Corte> DetallePtosCamara = new List<Corte>();
            Corte pResult = null;
            string pSentencia = "SELECT TARIMA, FECHA, FOLIO, CODIGOBARRAS, PESO, CAMARA, UBICACION FROM DRASCORT T0 "+
                                "WHERE T0.CAMARA IN (@CAMARA) AND T0.EMBARCADO = 'No' AND PRODUCTO = @PRODUCTO ";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CAMARA", FbDbType.Integer).Value = ACamara;
            com.Parameters.Add("@PRODUCTO", FbDbType.Integer).Value = AProducto;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult              = new Corte();
                    pResult.Tarima       = (reader["TARIMA"] != DBNull.Value) ? (int)reader["TARIMA"] : -1;
                    pResult.Fecha        = (DateTime)reader["FECHA"];
                    pResult.Folio        = (reader["FOLIO"] != DBNull.Value) ? (int)reader["FOLIO"] : -1;
                    pResult.CodigoBarras = (reader["CODIGOBARRAS"] != DBNull.Value) ? (string)reader["CODIGOBARRAS"] : "";
                    pResult.Peso         = (reader["PESO"] != DBNull.Value) ? (decimal)reader["PESO"] : 0;
                    pResult.Camara       = (reader["CAMARA"] != DBNull.Value) ? (int)reader["CAMARA"] : -1;
                    pResult.Ubicacion    = (reader["UBICACION"] != DBNull.Value) ? (string)reader["UBICACION"] : "";
                    DetallePtosCamara.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return DetallePtosCamara;
        }

        public List<CajaPendienteRecepcionEmbarque> ObtenerCajasPendientesRecepcionEmbarques(string AFecha)
        {
            List<CajaPendienteRecepcionEmbarque> CajasPendientesRecepcionEmbarques = new List<CajaPendienteRecepcionEmbarque>();
            CajaPendienteRecepcionEmbarque pResult = null;
            string pSentencia = "SELECT T0.PRODUCTO, T1.DESCRIPCION, COUNT(*) AS CAJAS FROM DRASCORT T0 "+
                               "JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO "+
                               "WHERE FECHA = @FECHA AND COALESCE(ESCANEADO, 'No') = 'No' AND COALESCE(EMBARCADO, 'No') = 'No' "+
                               "GROUP BY T0.PRODUCTO, T1.DESCRIPCION";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value = AFecha;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult             = new CajaPendienteRecepcionEmbarque();
                    pResult.Producto    = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pResult.Descripcion = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.Cajas       = (reader["CAJAS"] != DBNull.Value) ? (int)reader["CAJAS"] : 0;

                    CajasPendientesRecepcionEmbarques.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return CajasPendientesRecepcionEmbarques;
        }

        public List<Corte> ObtenerDetalleCajasPendientesRecepcionEmbarques(string AFecha, string AProducto)
        {
            List<Corte> DetalleCajasPendientesRecepcionEmbarques = new List<Corte>();
            Corte pResult = null;
            string pSentencia = "SELECT FOLIO, CODIGOBARRAS, PESO FROM DRASCORT WHERE FECHA = @FECHA AND PRODUCTO = @PRODUCTO  AND COALESCE(ESCANEADO, 'No') = 'No' AND COALESCE(EMBARCADO, 'No') = 'No' ORDER BY FOLIO";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value  = AFecha;
            com.Parameters.Add("@PRODUCTO", FbDbType.VarChar).Value = AProducto;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult              = new Corte();
                    pResult.Folio        = (reader["FOLIO"] != DBNull.Value) ? (int)reader["FOLIO"] : -1;
                    pResult.CodigoBarras = (reader["CODIGOBARRAS"] != DBNull.Value) ? (string)reader["CODIGOBARRAS"] : "";
                    pResult.Peso         = (reader["PESO"] != DBNull.Value) ? (decimal)reader["PESO"] : 0;

                    DetalleCajasPendientesRecepcionEmbarques.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return DetalleCajasPendientesRecepcionEmbarques;
        }

        public List<CajaLote> ObtenerCajasLote(string AFecha, int ALote)
        {
            List<CajaLote> pCajasLote = new List<CajaLote>();
            CajaLote pResult = null;
            string pSentencia = "SELECT T0.FOLIO, T0.PRODUCTO, T1.DESCRIPCION, T0.CODIGOBARRAS, T0.PESO FROM DRASCORT T0 " +
                      "JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO " +
                      "WHERE T0.FECHA = @FECHA AND T0.LOTE = @LOTE AND (T0.ENTRADA_APLICADA IS NULL OR T0.ENTRADA_APLICADA = 'No')";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value = AFecha;
            com.Parameters.Add("@LOTE", FbDbType.Integer).Value = ALote;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult              = new CajaLote();
                    pResult.Folio        = reader["FOLIO"] != DBNull.Value ? (int)reader["FOLIO"] : -1;
                    pResult.Producto     = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pResult.Descripcion  = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.CodigoBarras = (reader["CODIGOBARRAS"] != DBNull.Value) ? (string)reader["CODIGOBARRAS"] : "";
                    pResult.Peso         = reader["PESO"] != DBNull.Value ? (decimal)reader["PESO"] : -1;

                    pCajasLote.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return pCajasLote;
        }

        public int CambiarCajaDeLote(int ALoteNuevo, string ACodigoBarras)
        {
            int pAffected;
            string pSentencia = "UPDATE DRASCORT SET LOTE = @LOTENUEVO WHERE CODIGOBARRAS = @CODIGOBARRAS";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@LOTENUEVO", FbDbType.Integer).Value = ALoteNuevo;
            com.Parameters.Add("@CODIGOBARRAS", FbDbType.VarChar).Value = ACodigoBarras;
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

        public Corte ObtenerCaja(string ACodigoBarras)
        {
            Corte pResult = null;
            string pSentencia = "SELECT * FROM DRASCORT WHERE CODIGOBARRAS = @CODIGOBARRAS";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CODIGOBARRAS", FbDbType.VarChar).Value = ACodigoBarras;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    pResult                  = new Corte();
                    pResult.Fecha            = (reader["FECHA"]!= DBNull.Value) ? (DateTime?)reader["FECHA"] : null;
                    pResult.Folio            = (reader["FOLIO"]!= DBNull.Value) ? (int)reader["FOLIO"] : -1;
                    pResult.Lote             = (reader["LOTE"]!= DBNull.Value) ? (int)reader["LOTE"] : -1;
                    pResult.Granja           = (reader["GRANJA"]!= DBNull.Value) ? (int)reader["GRANJA"] : -1;
                    pResult.Producto         = (reader["PRODUCTO"]!= DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pResult.Bascula          = (reader["BASCULA"]!= DBNull.Value) ? (int)reader["BASCULA"] : -1;
                    pResult.Peso             = (reader["PESO"]!= DBNull.Value) ? (decimal)reader["PESO"] : 0;
                    pResult.Tara             = (reader["TARA"]!= DBNull.Value) ? (decimal)reader["TARA"] : 0;
                    pResult.PesoNeto         = (reader["PESONETO"]!= DBNull.Value) ? (decimal)reader["PESONETO"] : 0;
                    pResult.Embarcado        = (reader["EMBARCADO"]!= DBNull.Value) ? (string)reader["EMBARCADO"] : "";
                    pResult.CodigoBarras     = (reader["CODIGOBARRAS"]!= DBNull.Value) ? (string)reader["CODIGOBARRAS"] : "";
                    pResult.Tarima           = (reader["TARIMA"]!= DBNull.Value) ? (int)reader["TARIMA"] : -1;
                    pResult.Almacenado       = (reader["ALMACENADO"]!= DBNull.Value) ? (string)reader["ALMACENADO"] : "";
                    pResult.Estatus          = (reader["ESTATUS"]!= DBNull.Value) ? (string)reader["ESTATUS"] : "";
                    pResult.Entrada_Aplicada = (reader["ENTRADA_APLICADA"]!= DBNull.Value) ? (string)reader["ENTRADA_APLICADA"] : "";
                    pResult.Salida_Aplicada  = (reader["SALIDA_APLICADA"]!= DBNull.Value) ? (string)reader["SALIDA_APLICADA"] : "";
                    pResult.Id_Acum          = (reader["ID_ACUM"]!= DBNull.Value) ? (int)reader["ID_ACUM"] : -1;
                    pResult.Fecha_Sacrificio = (reader["FECHA_SACRIFICIO"]!= DBNull.Value) ? (DateTime?)reader["FECHA_SACRIFICIO"] : null;
                    pResult.Id_Salida        = (reader["ID_SALIDA"]!= DBNull.Value) ? (int)reader["ID_SALIDA"] : -1;
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

        public List<ProgramacionCorteD> ObtenerProgramacionLCFecha(string AFecha)
        {
            List<ProgramacionCorteD> pProgramacionLC = new List<ProgramacionCorteD>();
            ProgramacionCorteD pResult = null;
            string pSentencia = "SELECT T0.* FROM DRASPROGCORTED T0 JOIN DRASPROGCORTEM T1 ON T1.ID = T0.ID_FECHAPROG WHERE T1.FECHA = @FECHA";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value = AFecha;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult                       = new ProgramacionCorteD();
                    pResult.Id                    = reader["ID"] != DBNull.Value ? (int)reader["ID"] : -1;
                    pResult.Id_FechaProg          = reader["ID_FECHAPROG"] != DBNull.Value ? (int)reader["ID_FECHAPROG"] : -1;
                    pResult.Lote                  = reader["LOTE"] != DBNull.Value ? (int)reader["LOTE"] : -1;
                    pResult.Productos             = (reader["PRODUCTOS"] != DBNull.Value) ? (string)reader["PRODUCTOS"] : "";
                    pResult.FechaHoraSistema      = (reader["FECHAHORASISTEMA"] != DBNull.Value) ? (DateTime?)reader["FECHAHORASISTEMA"] : null;
                    pResult.FechaHoraModificacion = (reader["FECHAHORAMODIFICACION"] != DBNull.Value) ? (DateTime?)reader["FECHAHORAMODIFICACION"] : null;

                    pProgramacionLC.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return pProgramacionLC;
        }

        public ProgramacionCorteD ObtenerProgramacionLCIndividual(int AId)
        {
            ProgramacionCorteD pResult = null;
            string pSentencia = "SELECT T0.* FROM DRASPROGCORTED T0 JOIN DRASPROGCORTEM T1 ON T1.ID = T0.ID_FECHAPROG WHERE T0.ID = @ID";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value = AId;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult                       = new ProgramacionCorteD();
                    pResult.Id                    = reader["ID"] != DBNull.Value ? (int)reader["ID"] : -1;
                    pResult.Id_FechaProg          = reader["ID_FECHAPROG"] != DBNull.Value ? (int)reader["ID_FECHAPROG"] : -1;
                    pResult.Lote                  = reader["LOTE"] != DBNull.Value ? (int)reader["LOTE"] : -1;
                    pResult.Productos             = (reader["PRODUCTOS"] != DBNull.Value) ? (string)reader["PRODUCTOS"] : "";
                    pResult.FechaHoraSistema      = (reader["FECHAHORASISTEMA"] != DBNull.Value) ? (DateTime?)reader["FECHAHORASISTEMA"] : null;
                    pResult.FechaHoraModificacion = (reader["FECHAHORAMODIFICACION"] != DBNull.Value) ? (DateTime?)reader["FECHAHORAMODIFICACION"] : null;
                    
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

        public int ObtenerIdFechaProgramada(string AFecha)
        {
            string pSentencia = "SELECT ID FROM DRASPROGCORTEM WHERE FECHA = @FECHA";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value = AFecha;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    return reader["ID"] != DBNull.Value ? (int)reader["ID"] : -1;
                }
                else
                {
                    return InsertarFechaProgramada(AFecha);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public int InsertarFechaProgramada(string AFechaProg)
        {
            string pSentencia = "INSERT INTO DRASPROGCORTEM(FECHA) VALUES(@FECHA)";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value = AFechaProg;
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

            return ObtenerIdFechaProgramada(AFechaProg);
        }

        public int LoteProgramado(string AFecha, int ALote)
        {
            string pSentencia = "SELECT T0.* FROM DRASPROGCORTED T0 JOIN DRASPROGCORTEM T1 ON T1.ID = T0.ID_FECHAPROG WHERE T1.FECHA = @FECHA AND T0.LOTE = @LOTE";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value = AFecha;
            com.Parameters.Add("@LOTE", FbDbType.Integer).Value = ALote;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    return reader["ID_FECHAPROG"] != DBNull.Value ? (int)reader["ID_FECHAPROG"] : 0;
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

        public int InsertarLoteProgramado(int AIdFechaProg, int ALote, string AProductos)
        {
            int pAffected = 0;
            string pSentencia = "INSERT INTO DRASPROGCORTED(ID_FECHAPROG, LOTE, PRODUCTOS) VALUES(@IDFECHAPROG, @LOTE, @PRODUCTOS)";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@IDFECHAPROG", FbDbType.Integer).Value = AIdFechaProg;
            com.Parameters.Add("@LOTE", FbDbType.Integer).Value        = ALote;
            com.Parameters.Add("@PRODUCTOS", FbDbType.VarChar).Value   = AProductos;
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

        public ProgramacionCorteD ActualizarLoteProgramado(int AId, string AProductos)
        {
            string pSentencia = "UPDATE DRASPROGCORTED SET PRODUCTOS = @PRODUCTOS WHERE ID = @ID";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);

            com.Parameters.Add("@ID", FbDbType.Integer).Value = AId;
            com.Parameters.Add("@PRODUCTOS", FbDbType.VarChar).Value = AProductos;

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
            return ObtenerProgramacionLCIndividual(AId);
        }

        public bool EliminarLp(int AId, out string AMensajeError)
        {
            bool pResult = true;
            AMensajeError = "";

            string pSentencia = "DELETE FROM DRASPROGCORTED WHERE ID = @ID";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value = AId;

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

        public List<ResumenCajaSalida> ObtenerResumenCajasSalida(int AIdSalida)
        {
            List<ResumenCajaSalida> pResult = new List<ResumenCajaSalida>();
            ResumenCajaSalida pResumenCajaSalida = null;
            string pSentencia = "SELECT T0.ID_SALIDA, T0.PRODUCTO, T1.DESCRIPCION, COUNT(*) CAJAS, SUM(T0.PESO) AS KILOS  FROM DRASCORT T0 " +
                               "JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO " +
                               "WHERE ID_SALIDA = @ID " +
                               "GROUP BY T0.ID_SALIDA, T0.PRODUCTO, T1.DESCRIPCION";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value = AIdSalida;
            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResumenCajaSalida             = new ResumenCajaSalida();
                    pResumenCajaSalida.Id_Salida   = reader["ID_SALIDA"] != DBNull.Value ? (int)reader["ID_SALIDA"] : -1;
                    pResumenCajaSalida.Producto    = reader["PRODUCTO"] != DBNull.Value ? (string)reader["PRODUCTO"] : "";
                    pResumenCajaSalida.Descripcion = reader["DESCRIPCION"] != DBNull.Value ? (string)reader["DESCRIPCION"] : "";
                    pResumenCajaSalida.Cajas       = (reader["CAJAS"] != DBNull.Value) ? (int)reader["CAJAS"] : 0;
                    pResumenCajaSalida.Kilos       = (reader["KILOS"] != DBNull.Value) ? (decimal)reader["KILOS"] : 0;
                    pResult.Add(pResumenCajaSalida);
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

        public List<ProgramacionFechaLote> ObtenerProgramacionFechaLote(string AFecha, int ALote)
        {
            List<ProgramacionFechaLote> pResult = new List<ProgramacionFechaLote>();
            ProgramacionFechaLote pProgramacionFechaLote = null;
            string pSentencia = "SELECT T0.*, T1.FECHA FROM DRASPROGCORTED T0 JOIN DRASPROGCORTEM T1 ON T1.ID = T0.ID_FECHAPROG WHERE T1.FECHA = @FECHA";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value = AFecha;
            com.Parameters.Add("@LOTE", FbDbType.Integer).Value = ALote;
            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pProgramacionFechaLote                       = new ProgramacionFechaLote();
                    pProgramacionFechaLote.Id                    = reader["ID"] != DBNull.Value ? (int)reader["ID"] : -1;
                    pProgramacionFechaLote.Id_FechaProg          = reader["ID_FECHAPROG"] != DBNull.Value ? (int)reader["ID_FECHAPROG"] : -1;
                    pProgramacionFechaLote.Lote                  = reader["LOTE"] != DBNull.Value ? (int)reader["LOTE"] : -1;
                    pProgramacionFechaLote.Productos             = reader["PRODUCTOS"] != DBNull.Value ? (string)reader["PRODUCTOS"] : "";
                    pProgramacionFechaLote.FechaHoraSistema      = reader["FECHAHORASISTEMA"] != DBNull.Value ? (DateTime?)reader["FECHAHORASISTEMA"] : null;
                    pProgramacionFechaLote.FechaHoraModificacion = (reader["FECHAHORAMODIFICACION"] != DBNull.Value) ? (DateTime?)reader["FECHAHORAMODIFICACION"] : null;
                    pProgramacionFechaLote.Fecha                 = (reader["FECHA"] != DBNull.Value) ? (DateTime?)reader["FECHA"] : null;
                    pResult.Add(pProgramacionFechaLote);
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

        //JORGE
        public Corte ObtenerDatosCaja(int AFolio, string AFecha)
        {
            Corte pCajas = null;
            Corte pResult = null;
            string pSentencia = "SELECT FECHA, PESO, BASCULA, TARIMA, ID_SALIDA, PRODUCTO, CODIGOBARRAS, EMBARCADO, ENTRADA_APLICADA, FECHA_SACRIFICIO FROM DRASCORT WHERE FOLIO = @FOLIO AND FECHA = @FECHA";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FOLIO", FbDbType.Integer).Value = AFolio;
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value = AFecha;
            try
            {

                con.Open();
                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    pResult = new Corte();
                    if (reader["FECHA"] != DBNull.Value)
                        pResult.Fecha = (DateTime)reader["FECHA"];
                    pResult.Peso = reader["PESO"] != DBNull.Value ? (decimal)reader["PESO"] : -1;
                    pResult.Bascula = reader["BASCULA"] != DBNull.Value ? (int)reader["BASCULA"] : -1;
                    pResult.Tarima = reader["TARIMA"] != DBNull.Value ? (int)reader["TARIMA"] : -1;
                    pResult.Id_Salida = (reader["ID_SALIDA"] != DBNull.Value) ? (int)reader["ID_SALIDA"] : -1;
                    pResult.Producto = reader["PRODUCTO"] != DBNull.Value ? (string)reader["PRODUCTO"] : "";
                    pResult.CodigoBarras = (reader["CODIGOBARRAS"] != DBNull.Value) ? (string)reader["CODIGOBARRAS"] : "";
                    pResult.Embarcado = (reader["EMBARCADO"] != DBNull.Value) ? (string)reader["EMBARCADO"] : "";
                    pResult.Entrada_Aplicada = (reader["ENTRADA_APLICADA"] != DBNull.Value) ? (string)reader["ENTRADA_APLICADA"] : "";
                    if (reader["FECHA_SACRIFICIO"] != DBNull.Value)
                        pResult.Fecha = (DateTime)reader["FECHA_SACRIFICIO"];
                    pCajas = pResult;
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return pCajas;
        }
        
        public Boolean BorrarEtiqueta(string ACodigoBarras, string AMotivo, string ACodigoAlta, int AUsuario, out string AMensajeError)
        {

            AMensajeError = "";
            string pSentencia = "DELETE FROM DRASCORT WHERE CODIGOBARRAS = @CODIGOBARRAS";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CODIGOBARRAS", FbDbType.Integer).Value = ACodigoBarras;

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
                }
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

        public Boolean EliminaCajaDeTarima(string ACodigoBarras, out string AMensajeError)
        {
            AMensajeError = "";

            string pSentencia = "DELETE FROM DRASTARD WHERE CODIGO = @CODIGO";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CODIGO", FbDbType.Integer).Value = ACodigoBarras;

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
                }
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

        public bool RegresaCaja(string AIp, int AUsuario, string ACodigoBbarras)
        {
            Corte pCaja = ObtenerCaja(ACodigoBbarras);
            Console.WriteLine(":" + pCaja.Id_Salida + ":");
            if (pCaja.Id_Salida == null || pCaja.Id_Salida == 0)
            {
                InsertaLog(AIp, "Regresar", "DRASCORT", DateTime.Now.ToString("yyyy/MM/ddTHH:mm:sszzz"), ACodigoBbarras + " : La caja no tiene salida : " + AUsuario);
                return false; // La caja no tiene salida
            }

            string pSentencia = "UPDATE DRASSALIDAS SET CAJAS = CAJAS - 1, KILOS = KILOS - " + pCaja.Peso + " WHERE ID_SALIDA = " + pCaja.Id_Salida + " AND ID_TARIMA = " + pCaja.Tarima;
            EjecutarQuery(pSentencia);
            pSentencia = "UPDATE SALIDASD SET CAJAS = CAJAS - 1, KILOS = KILOS - " + pCaja.Peso + " WHERE ID_SALIDA = " + pCaja.Id_Salida + " AND ID_TARIMA = " + pCaja.Tarima;
            EjecutarQuery(pSentencia);
            pSentencia = "UPDATE SALIDASM SET CAJAS = CAJAS - 1, KILOS = KILOS - " + pCaja.Peso + " WHERE ID = " + pCaja.Id_Salida;
            EjecutarQuery(pSentencia);
            pSentencia = "UPDATE DRASCORT SET EMBARCADO = 'No', ID_SALIDA = NULL, SALIDA_APLICADA = 'No', TARIMA = 0 WHERE CODIGOBARRAS = '" + ACodigoBbarras + "'";
            EjecutarQuery(pSentencia);
            pSentencia = "UPDATE DRASTARD SET ESTATUS = 'E' WHERE CODIGO = '" + ACodigoBbarras + "'";
            EjecutarQuery(pSentencia);
            pSentencia = "DELETE FROM DRASTARD WHERE CODIGO = '" + ACodigoBbarras + "'";
            EjecutarQuery(pSentencia);
            InsertaLog(AIp, "Regresar", "DRASCORT", DateTime.Now.ToString("yyyy/MM/ddTHH:mm:sszzz"), ACodigoBbarras + " : Caja regresada : " + AUsuario);
            return true; //La caja se regreso exitosamente
        }

        void InsertaLog(string AIp, string AOperacion, string ATabla, string AFecha, string ADatosBorrados)
        {

            string pSentencia = "INSERT INTO DRASLOG (IP, OPERACION, TABLA, FECHA, DATOS_BORRADOS) VALUES(@IP, @OPERACION, @TABLA, @FECHA, @DATOS_BORRADOS)";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@IP", FbDbType.VarChar).Value = AIp;
            com.Parameters.Add("@OPERACION", FbDbType.VarChar).Value = AOperacion;
            com.Parameters.Add("@TABLA", FbDbType.VarChar).Value = ATabla;
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value = AFecha;
            com.Parameters.Add("@DATOS_BORRADOS", FbDbType.VarChar).Value = ADatosBorrados;
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

        }

        void EjecutarQuery(string pSentencia)
        {
            Console.WriteLine("Ejecutando: " + pSentencia);
            FbConnection con = _Conexion.ObtenerConexion();
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
        }
        //TERMINA JORGE
    }
}