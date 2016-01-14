using System;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System.Collections.Generic;

namespace grole.src.Persistencia
{
	public class UsuarioBasculaPersistencia
	{
		
		private Conexiones _Conexiones { get; set; }
		
		public UsuarioBasculaPersistencia(Conexiones AConexion)
		{
			this._Conexiones = AConexion;
		}
		
		private UsuarioBascula ReaderToEntidad(FbDataReader AReader)
		{
			UsuarioBascula pResult = new UsuarioBascula();
			pResult.CLAVE          = AReader.GetInt32(0);
			pResult.NOMBRE         = AReader.GetString(1);
			pResult.CONTRASENA     = AReader.GetString(2);
			pResult.TITULAR        = AReader.GetString(3);
			pResult.LOTES          = AReader.GetString(4);
			return pResult;
		}
		
		//Retorna la lista de Usuarios en Bascula ---- Se utiliza para mostrar la lista en el INDEX
		public List<UsuarioBascula> ListaUsuarioBascula()
		{
			List<UsuarioBascula> pResult = new List<UsuarioBascula>();
			
			string pSentencia = "SELECT CLAVE, NOMBRE, CONTRASENA, TITULAR, LOTES FROM DRASUSUA ORDER BY CLAVE";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				while (reader.Read())
				{
					UsuarioBascula pUsuarioBascula = new UsuarioBascula();
					pUsuarioBascula.CLAVE          = reader.GetInt32(0);/*(string)reader["CLAVE"]*/;
					pUsuarioBascula.NOMBRE         = reader.GetString(1);/*(string)reader["NOMBRE"];*/
					pUsuarioBascula.CONTRASENA     = reader.GetString(2);/*(string)reader["CONTRASENA"];*/
					pUsuarioBascula.TITULAR        = reader.GetString(3);/*(string)reader["TITULAR"];*/
					pUsuarioBascula.LOTES          = reader.GetString(4);/*(string)reader["LOTES"];*/
					pResult.Add(pUsuarioBascula);
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
		
		//Registra un nuevo usuario en la BD, retorna el usuario registrado...
		public UsuarioBascula UsuarioBasculaInsertar(UsuarioBascula AUsuarioBascula)
		{
			
			string pSentencia = "INSERT INTO DRASUSUA (NOMBRE, CONTRASENA, TITULAR, LOTES) VALUES (@NOMBRE, @CONTRASENA, @TITULAR, @LOTES)";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@NOMBRE", FbDbType.VarChar).Value     = AUsuarioBascula.NOMBRE;			
			com.Parameters.Add("@CONTRASENA", FbDbType.VarChar).Value = AUsuarioBascula.CONTRASENA;	
			com.Parameters.Add("@TITULAR", FbDbType.VarChar).Value    = AUsuarioBascula.TITULAR;
			com.Parameters.Add("@LOTES", FbDbType.VarChar).Value      = AUsuarioBascula.LOTES;		
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
			
			
			return UsuarioBasculaObtener(AUsuarioBascula.NOMBRE);
		}
		
		//Retorna un usuario especifico, recive la clave del usuario a buscar
		public UsuarioBascula UsuarioBasculaObtener(string ANombre)
		{
			UsuarioBascula pResult = null;
			
			string pSentencia = "SELECT CLAVE, NOMBRE, CONTRASENA, TITULAR, LOTES FROM DRASUSUA WHERE NOMBRE=@NOMBRE";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@NOMBRE", FbDbType.Integer).Value = ANombre;
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				if (reader.Read())
				{
					pResult            = new UsuarioBascula();
					pResult.CLAVE      = reader.GetInt32(0);
					pResult.NOMBRE     = reader.GetString(1);
					pResult.CONTRASENA = reader.GetString(2);
					pResult.TITULAR    = reader.GetString(3);
					pResult.LOTES      = reader.GetString(4);
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
		
		//Elimina Usuario de Basculas
		public bool UsuarioBasculaEliminar(int AClave, out string AMensajeError)
		{
			bool pResult = true;
			AMensajeError = "";
			
			string pSentencia = "DELETE FROM DRASUSUA WHERE CLAVE = @CLAVE";
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
		
		public UsuarioBascula UsuarioBasculaModificar(UsuarioBascula AUsuarioBascula)
		{ 
			string pSentencia = "UPDATE DRASUSUA SET CONTRASENA=@CONTRASENA, TITULAR=@TITULAR, LOTES=@LOTES WHERE CLAVE=@CLAVE";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("CLAVE", FbDbType.Integer).Value      = AUsuarioBascula.CLAVE;
			com.Parameters.Add("CONTRASENA", FbDbType.VarChar).Value = AUsuarioBascula.CONTRASENA;
			com.Parameters.Add("TITULAR", FbDbType.VarChar).Value    = AUsuarioBascula.TITULAR;
			com.Parameters.Add("LOTES", FbDbType.VarChar).Value      = AUsuarioBascula.LOTES;
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
			
			return UsuarioBasculaObtener(AUsuarioBascula.NOMBRE);
		}
		
		public List<UsuarioBascula> busquedaUsuarioBascula(string AFiltrado)
		{
			List<UsuarioBascula> pResult = new List<UsuarioBascula>();
			string pSentencia = "SELECT * FROM DRASUSUA WHERE NOMBRE LIKE '%"+AFiltrado+"%' or TITULAR LIKE '%"+AFiltrado+"%' ORDER BY NOMBRE DESC";
			FbConnection con  = _Conexiones.ObtenerConexion();
			FbCommand com = new FbCommand(pSentencia, con);
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				while (reader.Read())
				{
					
					UsuarioBascula pUsuarioBascula = new UsuarioBascula();
					pUsuarioBascula.CLAVE          = reader.GetInt32(0);/*(string)reader["CLAVE"]*/
					pUsuarioBascula.NOMBRE         = reader.GetString(1);/*(string)reader["NOMBRE"];*/
					pUsuarioBascula.CONTRASENA     = reader.GetString(2);/*(string)reader["CONTRASENA"];*/
					pUsuarioBascula.TITULAR        = reader.GetString(3);/*(string)reader["TITULAR"];*/
					pUsuarioBascula.LOTES          = reader.GetString(4);/*(string)reader["LOTES"];*/
					pResult.Add(pUsuarioBascula);
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