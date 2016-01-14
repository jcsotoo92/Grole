using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System.Collections.Generic;
using System;

namespace grole.src.Persistencia
{
	public class ClientesGrolePersistencia
	{
		
		private Conexiones _Conexiones { get; set; }
		
		public ClientesGrolePersistencia(Conexiones AConexion)
		{
			this._Conexiones = AConexion;
		}
		
		//Retorna la lista de Clientes Grole
		public List<ClienteGrole> ListaClientesGrole()
		{
			List<ClienteGrole> pResult = new List<ClienteGrole>();
			string pSentencia = "SELECT ID, NOMBRE FROM DRASCLIENTES";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
						
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				while (reader.Read())
				{
					ClienteGrole pClienteGrole = new ClienteGrole();
					pClienteGrole.Id           = reader.GetInt32(0);
					pClienteGrole.Nombre       = reader.GetString(1);
					pResult.Add(pClienteGrole);
					
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
		
		//Ingresa Clientes Grole a la Base de Datos
		public ClienteGrole ClientesGroleInsertar(ClienteGrole AClienteGrole)
		{
			
			string pSentencia = "INSERT INTO DRASCLIENTES (NOMBRE) VALUES (@NOMBRE)";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@NOMBRE", FbDbType.VarChar).Value = AClienteGrole.Nombre;	
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
			
			
			return ClienteGroleObtener(AClienteGrole.Nombre);
		}
		
		public ClienteGrole ClienteGroleObtener(string ANombre)
		{
			ClienteGrole pResult = null;
			
			string pSentencia = "SELECT ID, NOMBRE FROM DRASCLIENTES WHERE NOMBRE=@NOMBRE";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@NOMBRE", FbDbType.Integer).Value = ANombre;
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				if (reader.Read())
				{
					pResult        = new ClienteGrole();
					pResult.Id     = reader.GetInt32(0);
					pResult.Nombre = reader.GetString(1);
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
		public ClienteGrole ClientesGroleModificar(ClienteGrole AClienteGrole)
		{ 
			string pSentencia = "UPDATE DRASCLIENTES SET NOMBRE=@NOMBRE WHERE ID=@ID";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("ID", FbDbType.Integer).Value     = AClienteGrole.Id;
			com.Parameters.Add("NOMBRE", FbDbType.VarChar).Value = AClienteGrole.Nombre;
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
			
			return ClienteGroleObtener(AClienteGrole.Nombre);
		}
		
		//Elimina Cliente Grole
		public bool ClientesGroleEliminar(int AId, out string AMensajeError)
		{
			bool pResult = true;
			AMensajeError = "";
			
			string pSentencia = "DELETE FROM DRASCLIENTES WHERE ID = @ID";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("ID", FbDbType.Integer).Value = AId;			
			
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
		
		//Filtro Busqueda
		public List<ClienteGrole> busquedaClienteGrole(string AFiltrado)
		{
			List<ClienteGrole> pResult = new List<ClienteGrole>();
			string pSentencia = "SELECT * FROM DRASCLIENTES WHERE NOMBRE LIKE '%"+AFiltrado+"%' ORDER BY NOMBRE DESC";
			FbConnection con  = _Conexiones.ObtenerConexion();
			FbCommand com = new FbCommand(pSentencia, con);
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				while (reader.Read())
				{
					
					ClienteGrole pClienteGrole = new ClienteGrole();
					pClienteGrole.Id           = reader.GetInt32(0);/*(string)reader["Id"]*/
					pClienteGrole.Nombre       = reader.GetString(1);/*(string)reader["Nombre"];*/
					pResult.Add(pClienteGrole);
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