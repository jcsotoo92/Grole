using System;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Collections.Generic;
using grole.src.Entidades;

namespace grole.src.Persistencia
{
	public class DestinosPersistencia
	{
		
		private Conexiones _Conexion;
		
		public DestinosPersistencia(Conexiones _Conexion){
			this._Conexion = _Conexion;
		}
		
		private Destino ReaderToEntidad(FbDataReader AReader){
			Destino pResult = new Destino();
			pResult.Clave  = (int)AReader["CLAVE"];
			pResult._Destino = (string)AReader["DESTINO"];
			
			return pResult;
		}

        public bool ExisteDestino(Destino ADestino)
        {
            string pSentencia = "SELECT CLAVE FROM DRASDEST WHERE UPPER(TRIM(DESTINO)) = @DESTINO";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@DESTINO", FbDbType.VarChar).Value = ADestino._Destino.ToUpper().Trim();

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    int clave = (int)reader["CLAVE"];
                    if (clave == ADestino.Clave)
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

        public Destino DestinoObtener(int AClave){
			Destino pResult = null;
			
			string pSentencia = "SELECT CLAVE, DESTINO FROM DRASDEST WHERE CLAVE = @CLAVE";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@CLAVE", FbDbType.Integer).Value = AClave;
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				if (reader.Read()){
					pResult = ReaderToEntidad(reader);
				}
			}
			finally
			{
				if (con.State == System.Data.ConnectionState.Open){
                    con.Close();
                }
			}
			
			return pResult;
		}
		
		public List<Destino> ObtenerLista(){
			List<Destino> pResult = new List<Destino>();
			
			string pSentencia = "SELECT CLAVE, DESTINO FROM DRASDEST";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				while (reader.Read()){
					pResult.Add(ReaderToEntidad(reader));
				}
			}
			finally
			{
				if (con.State == System.Data.ConnectionState.Open){
                    con.Close();
                }
			}
			
			return pResult;
		}
		
		public Destino DestinoInsertar(Destino ADestino){
			string pSentencia = "INSERT INTO DRASDEST (DESTINO) VALUES (@DESTINO) RETURNING CLAVE";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@DESTINO", FbDbType.VarChar).Value = ADestino._Destino;			
			FbParameter pOutParameter = new FbParameter("@CLAVE", FbDbType.Integer);
			pOutParameter.Direction = ParameterDirection.Output;
			com.Parameters.Add(pOutParameter);
			
			try
			{
				con.Open();
				com.ExecuteNonQuery();
			}
			finally
			{
				if (con.State == System.Data.ConnectionState.Open){
                    con.Close();
                }
			}
			
			return DestinoObtener((int)pOutParameter.Value);
		}
		
		public Destino DestinoModificar(Destino ADestino){
			string pSentencia = "UPDATE DRASDEST SET DESTINO=@DESTINO WHERE CLAVE=@CLAVEE RETURNING CLAVE";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@DESTINO", FbDbType.VarChar).Value = ADestino._Destino;			
			com.Parameters.Add("@CLAVEE", FbDbType.Integer).Value = ADestino.Clave;	
			FbParameter pOutParameter = new FbParameter("@CLAVE", FbDbType.Integer);
			pOutParameter.Direction = ParameterDirection.Output;
			com.Parameters.Add(pOutParameter);
			
			try
			{
				con.Open();
				com.ExecuteNonQuery();
			}
			finally
			{
				if (con.State == System.Data.ConnectionState.Open){
                    con.Close();
                }
			}
			
			return DestinoObtener((int)pOutParameter.Value);
		}
		
		public bool DestinoEliminar(int AClave, out string AMensajeError){
			bool pResult = true;
			AMensajeError = "";
			
			string pSentencia = "DELETE FROM DRASDEST WHERE CLAVE = @CLAVE";
			FbConnection con  = _Conexion.ObtenerConexion();
			
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
				if (con.State == System.Data.ConnectionState.Open) {
                    con.Close();
                }
			}
			
			return pResult;
		}
		
	}
}