using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System.Collections.Generic;
using System;

namespace grole.src.Persistencia
{
	public class ClasesPersistencia
	{
		
		private Conexiones _Conexiones { get; set; }
		
		public ClasesPersistencia(Conexiones AConexion)
		{
			this._Conexiones = AConexion;
		}
		
		//Retorna la lista de clases para dropdown menu
		public List<Clase> ListaClases()
		{
			List<Clase> pResult = new List<Clase>();
			string pSentencia = "SELECT CLAVE, DESCRIPCION, MANEJAEXTRA, TIPO, ACTIVO FROM DRASCLA";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
						
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				while (reader.Read())
				{
					Clase pClase       = new Clase();
					pClase.Clave       = reader.GetInt32(0);
					pClase.Descripcion = reader.GetString(1);
					pClase.Manejaextra = reader.GetString(2);
					pClase.Tipo        = reader.GetString(3);
					pClase.Activo      = reader.GetString(4);
					
					pResult.Add(pClase);
					
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
		
		//Ingresa Clase a la Base de Datos
		public Clase ClaseInsertar(Clase AClase)
		{
			
			string pSentencia = "INSERT INTO DRASCLA (CLAVE, DESCRIPCION, MANEJAEXTRA, TIPO, ACTIVO) VALUES (@CLAVE, @DESCRIPCION, @MANEJAEXTRA, @TIPO, @ACTIVO)";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CLAVE", FbDbType.Integer).Value       = AClase.Clave;
			com.Parameters.Add("@DESCRIPCION", FbDbType.VarChar).Value = AClase.Descripcion;			
			com.Parameters.Add("@MANEJAEXTRA", FbDbType.VarChar).Value = AClase.Manejaextra;	
			com.Parameters.Add("@TIPO", FbDbType.VarChar).Value        = AClase.Tipo;
			com.Parameters.Add("@ACTIVO", FbDbType.SmallInt).Value     = AClase.Activo;
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
			
			
			return AClase;
		}
		
		public Clase ClaseObtener(string ADescripcion)
		{
			Clase pResult = null;
			
			string pSentencia = "SELECT CLAVE, DESCRIPCION, MANEJAEXTRA, TIPO, ACTIVO FROM DRASCLA WHERE DESCRIPCION=@DESCRIPCION";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@DESCRIPCION", FbDbType.Integer).Value = ADescripcion;
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				if (reader.Read())
				{
					pResult             = new Clase();
					pResult.Clave       = reader.GetInt32(0);
					pResult.Descripcion = reader.GetString(1);
					pResult.Manejaextra = reader.GetString(2);
					pResult.Tipo        = reader.GetString(3);
					pResult.Activo      = reader.GetString(4);
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
		
		//Modifica Clase
		public Clase ClaseModificar(Clase AClase)
		{ 
			string pSentencia = "UPDATE DRASCLA SET DESCRIPCION=@DESCRIPCION, MANEJAEXTRA=@MANEJAEXTRA, TIPO=@TIPO, ACTIVO=@ACTIVO WHERE CLAVE=@CLAVE";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("CLAVE", FbDbType.Integer).Value       = AClase.Clave;
			com.Parameters.Add("DESCRIPCION", FbDbType.VarChar).Value = AClase.Descripcion;
			com.Parameters.Add("MANEJAEXTRA", FbDbType.VarChar).Value = AClase.Manejaextra;
			com.Parameters.Add("TIPO", FbDbType.VarChar).Value        = AClase.Tipo;
			com.Parameters.Add("ACTIVO", FbDbType.VarChar).Value      = AClase.Activo;
			
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
			
			return ClaseObtener(AClase.Descripcion);
		}
		
		//Elimina Clase
		public bool ClaseEliminar(int AClave, out string AMensajeError)
		{
			bool pResult = true;
			AMensajeError = "";
			
			string pSentencia = "DELETE FROM DRASCLA WHERE CLAVE = @CLAVE";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("CLAVE", FbDbType.Integer).Value = AClave;			
			
			try
			{
				con.Open();
				
				try
				{
					com.ExecuteNonQuery();
				}
				catch(Exception ex)
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