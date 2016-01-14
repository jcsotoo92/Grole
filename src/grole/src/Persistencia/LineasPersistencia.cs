using System;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Collections.Generic;
using grole.src.Entidades;

namespace grole.src.Persistencia
{
	public class LineasPersistencia
	{
		
		private Conexiones _Conexion;
		
		public LineasPersistencia(Conexiones AConexion){
			this._Conexion = AConexion;
		}
		
		private Linea ReaderToEntidad(FbDataReader AReader)
		{
			Linea pResult  		= new Linea();
			pResult.Clave  		= (int)AReader["CLAVE"];
			pResult.Descripcion = (string)AReader["DESCRIPCION"];
			return pResult; 
		}

        public bool ExisteLineaMismoNombre(Linea ALinea)
        {
            string pSentencia = "SELECT CLAVE FROM DRASLINEAS WHERE UPPER(TRIM(DESCRIPCION)) = @DESCRIPCION";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@DESCRIPCION", FbDbType.VarChar).Value = ALinea.Descripcion.ToUpper().Trim();

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    if ((int)reader["CLAVE"] == ALinea.Clave)
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

        public bool ExisteLineaMismoId(Linea ALinea)
        {
            string pSentencia = "SELECT CLAVE FROM DRASLINEAS WHERE CLAVE = @CLAVE";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CLAVE", FbDbType.Integer).Value = ALinea.Clave;


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

        public Linea LineaObtener(int AClave)
		{
			Linea pResult = null;
			
			string pSentencia = "SELECT CLAVE, DESCRIPCION FROM DRASLINEAS WHERE CLAVE = @CLAVE";
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
		
		public List<Linea> ObtenerLista()
		{
			List<Linea> pResult = new List<Linea>();
			
			string pSentencia = "SELECT CLAVE, DESCRIPCION FROM DRASLINEAS";
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
		
		public Linea LineaInsertar(Linea ALinea){
			string pSentencia = "INSERT INTO DRASLINEAS(CLAVE,DESCRIPCION) VALUES (@CLAVEE,@DESCRIPCION) RETURNING CLAVE";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@CLAVEE", FbDbType.VarChar).Value = ALinea.Clave;	
			com.Parameters.Add("@DESCRIPCION", FbDbType.VarChar).Value = ALinea.Descripcion;
						
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
				if (con.State == System.Data.ConnectionState.Open) {
                    con.Close();
                }
			}
			
			return LineaObtener((int)pOutParameter.Value);
		}
		
		public Linea LineaModificar(Linea ALinea,string AClave){
			string pSentencia = "UPDATE DRASLINEAS SET CLAVE=@CLAVEE, DESCRIPCION=@DESCRIPCION WHERE CLAVE=@CLAVEMOD RETURNING CLAVE";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@CLAVEMOD", FbDbType.VarChar).Value = AClave;	
			com.Parameters.Add("@CLAVEE", FbDbType.VarChar).Value = ALinea.Clave;	
			com.Parameters.Add("@DESCRIPCION", FbDbType.VarChar).Value = ALinea.Descripcion;
			
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
			return LineaObtener((int)pOutParameter.Value);
		}
		
		public bool LineaEliminar(int AClave, out string AMensajeError){
			bool pResult = true;
			AMensajeError = "";
			
			string pSentencia = "DELETE FROM DRASLINEAS WHERE CLAVE = @CLAVE";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@CLAVE", FbDbType.Integer).Value = AClave;			
			
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
				if (con.State == System.Data.ConnectionState.Open){
                    con.Close();
                }
			}
			
			return pResult;
		}
		
	}
}