using FirebirdSql.Data.FirebirdClient;

namespace grole.src.Persistencia
{
	
	public class AccountsPersistencia
	{
		private Conexiones conexion;
		public AccountsPersistencia(Conexiones conexion){
			this.conexion=conexion;
		}

        public int ChecarUsuario(string AUsuario, string APassword){
            string pSentencia="SELECT USUARIO,PASS FROM USUARIOSADMIN";
			
			FbConnection con  = conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			
			try
			{
				con.Open();
				FbDataReader reader = com.ExecuteReader();
				
				while(reader.Read())
				{
					if(((string) reader["USUARIO"]).Equals(AUsuario)){//Encontro usuario
						if(((string) reader["PASS"]).Equals(APassword)){
							return 0;
						}else{
							return 2;
						}
					}
				}
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
		
		public int ObtenerIdUsuario(string AUsuario){
			string pSentencia = "SELECT ID FROM USUARIOSADMIN WHERE USUARIO = @USUARIO";
			FbConnection con  = conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@USUARIO",FbDbType.VarChar).Value = AUsuario;
			try
			{
				con.Open();
				FbDataReader reader = com.ExecuteReader();
				
				if(reader.Read())
				{
					return (int) reader["ID"];
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