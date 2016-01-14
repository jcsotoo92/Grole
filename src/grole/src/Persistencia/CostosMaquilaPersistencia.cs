using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System.Collections.Generic;
using System.Data;
using System;

namespace grole.src.Persistencia
{
	public class CostosMaquilaPersistencia
	{
		
		private Conexiones _Conexiones { get; set; }
		
		public CostosMaquilaPersistencia(Conexiones AConexion)
		{
			this._Conexiones = AConexion;
		}
		
		//Retorna la lista de Costos Maquila
		public List<CostoMaquilaM> ListaCostosMaquila()
		{
			List<CostoMaquilaM> pResult = new List<CostoMaquilaM>();
			string pSentencia = "SELECT * FROM DRASCOSTOSMAQUILAM";
			FbConnection con  = _Conexiones.ObtenerConexion();
			FbCommand com = new FbCommand(pSentencia, con);
			try
			{
				con.Open();
				FbDataReader reader = com.ExecuteReader();
				while (reader.Read())
				{
					CostoMaquilaM pCostoMaquila    = new CostoMaquilaM();
					pCostoMaquila.Id               = reader.GetInt32(0);
					pCostoMaquila.Fecha            = reader.GetString(1);
					pCostoMaquila.FechaFinal       = reader.GetString(2);
					pCostoMaquila.Descripcion      = reader.GetString(3);
					pCostoMaquila.Activo           = reader.GetString(4);
					pCostoMaquila.FechaHoraSistema = reader.GetString(5);
					pResult.Add(pCostoMaquila);				
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
		
		//Retorna Datos de Maquila especifica
		public CostoMaquilaM ObtenerCostosMaquila(int Id)
		{
			CostoMaquilaM pResult = new CostoMaquilaM();
			string pSentencia = "SELECT * FROM DRASCOSTOSMAQUILAM WHERE ID = @ID ";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@ID", FbDbType.TimeStamp).Value = Id;
			try
			{
				con.Open();
				FbDataReader reader = com.ExecuteReader();
				if (reader.Read())
				{
					
					pResult.Id               = reader.GetInt32(0);
					pResult.Fecha            = reader.GetString(1);
					pResult.FechaFinal       = reader.GetString(2);
					pResult.Descripcion      = reader.GetString(3);
					pResult.Activo           = reader.GetString(4);
					pResult.FechaHoraSistema = reader.GetString(5);	
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
		
		//Inserta Maquila en Tabla M
		public int insertarCostoMaquilaM(CostoMaquilaM MaquilaM)
		{
			string pSentencia = "INSERT INTO DRASCOSTOSMAQUILAM (FECHA, FECHAFINAL, DESCRIPCION, ACTIVO, FECHAHORASISTEMA) VALUES (@FECHA, @FECHAFINAL, @DESCRIPCION, @ACTIVO, @FECHAHORASISTEMA) RETURNING ID";
			FbConnection con  = _Conexiones.ObtenerConexion();
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value 			  = MaquilaM.Fecha;
			com.Parameters.Add("@FECHAFINAL", FbDbType.TimeStamp).Value 	  = MaquilaM.FechaFinal;
			com.Parameters.Add("@DESCRIPCION", FbDbType.VarChar).Value 		  = MaquilaM.Descripcion;
			com.Parameters.Add("@ACTIVO", FbDbType.VarChar).Value 			  = MaquilaM.Activo;
			com.Parameters.Add("@FECHAHORASISTEMA", FbDbType.TimeStamp).Value = MaquilaM.FechaHoraSistema;
			
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
			
			var parametro = (int)pOutParameter.Value;
			return parametro;
		}
		
		//Inserta Maquila en Tabla D
		public bool insertarCostoMaquilaD(CostoMaquilaD MaquilaD)
		{
			string pSentencia = "INSERT INTO DRASCOSTOSMAQUILAD (ID_COSTO, PRODUCTO, COSTO) VALUES (@ID_COSTO, @PRODUCTO, @COSTO)";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@ID_COSTO", FbDbType.Integer).Value = MaquilaD.Id_Costo;
			com.Parameters.Add("@PRODUCTO", FbDbType.VarChar).Value = MaquilaD.Producto;
			com.Parameters.Add("@COSTO", FbDbType.Numeric).Value 	= MaquilaD.Costo;
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
		
		//Modificar Maquila en Tabla M
		public bool ModificarCostoMaquilaM(CostoMaquilaM MaquilaM)
		{
			string pSentencia = "UPDATE DRASCOSTOSMAQUILAM SET FECHA=@FECHA, FECHAFINAL=@FECHAFINAL, DESCRIPCION=@DESCRIPCION, ACTIVO=@ACTIVO, FECHAHORASISTEMA=@FECHAHORASISTEMA WHERE ID=@ID";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@ID", FbDbType.Integer).Value				  = MaquilaM.Id;
			com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value 			  = MaquilaM.Fecha;
			com.Parameters.Add("@FECHAFINAL", FbDbType.TimeStamp).Value 	  = MaquilaM.FechaFinal;
			com.Parameters.Add("@DESCRIPCION", FbDbType.VarChar).Value 		  = MaquilaM.Descripcion;
			com.Parameters.Add("@ACTIVO", FbDbType.VarChar).Value 			  = MaquilaM.Activo;
			com.Parameters.Add("@FECHAHORASISTEMA", FbDbType.TimeStamp).Value = MaquilaM.FechaHoraSistema;
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
		
		//Modificar Maquila en Tabla D
		public bool ModificarCostoMaquilaD(CostoMaquilaD MaquilaD)
		{
			string pSentencia = "UPDATE DRASCOSTOSMAQUILAD SET PRODUCTO=@PRODUCTO, COSTO=@COSTO WHERE ID = @ID";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@ID", FbDbType.Integer).Value = MaquilaD.Id;
			com.Parameters.Add("@PRODUCTO", FbDbType.VarChar).Value = MaquilaD.Producto;
			com.Parameters.Add("@COSTO", FbDbType.Numeric).Value 	= MaquilaD.Costo;
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
		
		public bool eliminarProductosMaquila(int Id_Costo)
		{
			string pSentencia = "DELETE FROM DRASCOSTOSMAQUILAD WHERE ID_COSTO = @ID_COSTO";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@ID_COSTO", FbDbType.Integer).Value = Id_Costo;
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
		
		public bool eliminarMaquila(int Id)
		{
			string pSentencia = "DELETE FROM DRASCOSTOSMAQUILAM WHERE ID = @ID";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@ID", FbDbType.Integer).Value = Id;
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
		
		public List<CostoMaquilaD> ObtenerProductos(int Id_Costo)
		{
			List<CostoMaquilaD> pResult = new List<CostoMaquilaD>();
			string pSentencia = "SELECT T0.ID, T0.ID_COSTO, T0.PRODUCTO, T0.COSTO, T1.DESCRIPCION FROM DRASCOSTOSMAQUILAD T0 JOIN DRASPROD T1 ON T1.CLAVE = T0.PRODUCTO WHERE T0.ID_COSTO = "+Id_Costo;
			FbConnection con  = _Conexiones.ObtenerConexion();
			FbCommand com = new FbCommand(pSentencia, con);
			
			try
			{
				con.Open();
				FbDataReader reader = com.ExecuteReader();
				while (reader.Read())
				{
					CostoMaquilaD pCostoMaquila = new CostoMaquilaD();
					pCostoMaquila.Id            = (int)reader["ID"];
					pCostoMaquila.Id_Costo      = (int)reader["ID_COSTO"];
					pCostoMaquila.Producto      = (String)reader["PRODUCTO"];
					pCostoMaquila.Descripcion   = (String)reader["DESCRIPCION"];
					pCostoMaquila.Costo         = reader.GetString(3);
					pResult.Add(pCostoMaquila);			
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