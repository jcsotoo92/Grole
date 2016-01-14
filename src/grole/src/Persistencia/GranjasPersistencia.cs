using System;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Collections.Generic;
using grole.src.Entidades;

namespace grole.src.Persistencia
{
	public class GranjasPersistencia
	{
		
		private Conexiones _Conexion;
		
		public GranjasPersistencia(Conexiones _Conexion){
			this._Conexion = _Conexion;
		}
		
		private Granja ReaderToEntidad(FbDataReader AReader){
			Granja pResult = new Granja();
			pResult.Clave  = (int)AReader["CLAVE"];
			pResult.Nombre = (string)AReader["NOMBRE"];
			
			return pResult;
		}

        public bool ExisteGranja(Granja AGranja)
        {
            string pSentencia = "SELECT CLAVE FROM DRASGRAN WHERE UPPER(TRIM(NOMBRE)) = @NOMBRE";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@NOMBRE", FbDbType.VarChar).Value = AGranja.Nombre.ToUpper().Trim();

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    if ((int)reader["CLAVE"] == AGranja.Clave)
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

        public Granja GranjaObtener(int AClave){
			Granja pResult = null;
			
			string pSentencia = "SELECT CLAVE, NOMBRE FROM DRASGRAN WHERE CLAVE = @CLAVE";
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
		
		public List<Granja> ObtenerLista(){
			List<Granja> pResult = new List<Granja>();
			
			string pSentencia = "SELECT CLAVE, NOMBRE FROM DRASGRAN";
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
		
		public Granja GranjaInsertar(Granja AGranja){
			string pSentencia = "INSERT INTO DRASGRAN (NOMBRE) VALUES (@NOMBRE) RETURNING CLAVE";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@NOMBRE", FbDbType.VarChar).Value = AGranja.Nombre;			
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
			
			return GranjaObtener((int)pOutParameter.Value);
		}
		
		public Granja GranjaModificar(Granja AGranja){
			string pSentencia = "UPDATE DRASGRAN SET NOMBRE=@NOMBRE WHERE CLAVE=@CLAVEE RETURNING CLAVE";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@NOMBRE", FbDbType.VarChar).Value = AGranja.Nombre;			
			com.Parameters.Add("@CLAVEE", FbDbType.Integer).Value = AGranja.Clave;	
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
			
			return GranjaObtener((int)pOutParameter.Value);
		}
		
		public bool GranjaEliminar(int AClave, out string AMensajeError){
			bool pResult = true;
			AMensajeError = "";
			
			string pSentencia = "DELETE FROM DRASGRAN WHERE CLAVE = @CLAVE";
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

        public List<Granja> ObtenerGranjas()
        {
            List<Granja> pResult = new List<Granja>();

            string pSentencia = "SELECT * FROM DRASGRAN ORDER BY NOMBRE";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult.Add(ReaderToEntidad(reader));
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