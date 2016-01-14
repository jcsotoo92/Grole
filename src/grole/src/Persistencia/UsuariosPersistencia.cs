using FirebirdSql.Data.FirebirdClient;
using System.Collections.Generic;
using System;
using System.Data;
using grole.src.Entidades;

namespace grole.src.Persistencia
{
	public class UsuariosPersistencia
	{
		
		private Conexiones _Conexion;
		
		public UsuariosPersistencia(Conexiones _Conexion)
		{
			this._Conexion = _Conexion;
		}
		
		public List<Usuario> ObtenerListaUsuarios(){
			List<Usuario> pResult = new List<Usuario>();
			Usuario usuario;
			string pSentencia = "SELECT * FROM USUARIOSADMIN ORDER BY NOMBRE";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				while (reader.Read()){
					usuario                 	= new Usuario();
					usuario.Clave  	        	= (int)reader["ID"];
					usuario.Nombre     			= (reader["NOMBRE"]!=DBNull.Value) ? (string)reader["NOMBRE"] : "";
					usuario.Correo        		= (reader["CORREO"]!=DBNull.Value) ? (string)reader["CORREO"] : "";
					usuario.User           		= (reader["USUARIO"]!=DBNull.Value) ? (string)reader["USUARIO"] : "";
					usuario.Pass     			= (reader["PASS"]!=DBNull.Value) ? (string)reader["PASS"] : "";
					usuario.PuedeProgramar   	= (reader["PUEDE_PROGRAMAR"]!=DBNull.Value) ? (string)reader["PUEDE_PROGRAMAR"] : "";
					usuario.PuedeEditarOrdenes  = (reader["PUEDE_CAMBIAR_ESTADO"]!=DBNull.Value) ? (string)reader["PUEDE_CAMBIAR_ESTADO"] : "" ;
					usuario.PuedeCambiarEstado 	= (reader["PUEDE_EDITAR_ORDENES"]!=DBNull.Value) ? (string)reader["PUEDE_EDITAR_ORDENES"] : "" ;
					pResult.Add(usuario);
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

        public bool ExisteUsuario(Usuario aUsuario)
        {
            string pSentencia = "SELECT ID FROM USUARIOSADMIN WHERE USUARIO=@USER";
			FbConnection con = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia,con);
			com.Parameters.Add("@USER", FbDbType.VarChar).Value = aUsuario.User;
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				if (reader.Read()){
					return true;
				}
			}
			finally
			{
				if (con.State == System.Data.ConnectionState.Open){
                    con.Close();
                }
			}
			return false;
        }
		
		public bool ExisteUsuarioSinSerYo(Usuario aUsuario)
        {
            string pSentencia = "SELECT ID FROM USUARIOSADMIN WHERE USUARIO=@USER";
			FbConnection con = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia,con);
			com.Parameters.Add("@USER", FbDbType.VarChar).Value = aUsuario.User;
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				if (reader.Read()){
					if((int) reader["ID"] != aUsuario.Clave)
						return true;
				}
			}
			finally
			{
				if (con.State == System.Data.ConnectionState.Open){
                    con.Close();
                }
			}
			return false;
        }

        private DerechoUsuario ObtenerDerecho(int ADerecho){
			DerechoUsuario pResult=null;
			string pSentencia = "SELECT FIRST 1 * FROM  DERECHOS_USUARIO WHERE ID_DERECHO = @IDDERECHO";
			FbConnection con = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia,con);
			com.Parameters.Add("@IDDERECHO", FbDbType.Integer).Value = ADerecho;
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				while (reader.Read()){
					pResult                 	= new DerechoUsuario();
					pResult.IdDerecho 			= (int) reader["ID_DERECHO"]; 
					pResult.Controlador 		= (reader["CONTROLADOR"] != DBNull.Value) ? (string) reader["CONTROLADOR"] : ""; 
					pResult.Menu 				= (reader["MENU"] != DBNull.Value) ? (string) reader["MENU"] : ""; 
					pResult.Clasificacion 		= (reader["CLASIFICACION"] != DBNull.Value) ? (string) reader["CLASIFICACION"] : ""; 
					
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
		
        public bool InsertarDerechosUsuario(int AUsuario, int[] AChk)
        {
			string puedeModificarOrden = UsuarioTieneDerechoModificarOrden(AUsuario, 40);
			string puedeEliminarOrden  = UsuarioTieneDerechoEliminarOrden(AUsuario,40);
			
			EliminarDerechosUsuario(AUsuario);

			foreach(int item in AChk){
				DerechoUsuario pDerecho=ObtenerDerecho(item);
				if(pDerecho!=null){
					InsertarDerechoUsuario(pDerecho,AUsuario,puedeModificarOrden,puedeEliminarOrden);
				}
			}
			
			return true;
        }
		
		private void InsertarDerechoUsuario(DerechoUsuario ADerecho, int AUsuario, string APuedeModificarOrden, string APuedeEliminarOrden){
			string pSentencia="INSERT INTO DERECHOS_USUARIO(ID_USUARIO, ID_DERECHO, CONTROLADOR,"+
			" MENU, CLASIFICACION, PUEDE_ELIMINAR_ORDEN, PUEDE_MODIFICAR_ORDEN) VALUES(@IDUSUARIO,"+
			" @IDDERECHO, @CONTROLADOR, @MENU, @CLASIFICACION, @PUEDEELIMINARORDEN, @PUEDEMODIFICARORDEN)";
			FbConnection con = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia,con);
			com.Parameters.Add("@IDUSUARIO", FbDbType.Integer).Value           = AUsuario;
			com.Parameters.Add("@IDDERECHO", FbDbType.Integer).Value           = ADerecho.IdDerecho;
			com.Parameters.Add("@CONTROLADOR", FbDbType.VarChar).Value         = ADerecho.Controlador;
			com.Parameters.Add("@MENU", FbDbType.VarChar).Value                = ADerecho.Menu;
			com.Parameters.Add("@CLASIFICACION", FbDbType.VarChar).Value       = ADerecho.Clasificacion;
			com.Parameters.Add("@PUEDEELIMINARORDEN", FbDbType.VarChar).Value  = APuedeEliminarOrden;
			com.Parameters.Add("@PUEDEMODIFICARORDEN", FbDbType.VarChar).Value = APuedeModificarOrden;
			
			FbParameter pOutParameter = new FbParameter("@ID", FbDbType.Integer);
			pOutParameter.Direction = ParameterDirection.Output;
			com.Parameters.Add(pOutParameter);
			
			try{
				con.Open();
				com.ExecuteNonQuery();
			}
			finally{
				if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
			}
		}
		
		private bool EliminarDerechosUsuario(int AUsuario){
			bool pResult = true;
			
			string pSentencia = "DELETE FROM DERECHOS_USUARIO WHERE ID_USUARIO = @CLAVE";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@CLAVE", FbDbType.Integer).Value = AUsuario;			
			
			try
			{
				con.Open();
				
				try
				{
					com.ExecuteNonQuery();
				}
				catch(Exception ex)
				{
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
		
		private string UsuarioTieneDerechoModificarOrden(int AUsuario, int AIdDerecho){
			string pSentencia = "SELECT COUNT(*) FROM DERECHOS_USUARIO WHERE ID_USUARIO = @IDUSUARIO AND ID_DERECHO = @IDDERECHO AND PUEDE_MODIFICAR_ORDEN = 'Si'";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@IDUSUARIO", FbDbType.Integer).Value = AUsuario;
			com.Parameters.Add("@IDDERECHO", FbDbType.Integer).Value = AIdDerecho;
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				if(reader.Read()){
					if((int)reader["COUNT"]>0)
						return "Si";
					
				}
			}
			finally
			{
				if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
			}
			return "No";
		}
		
		private string UsuarioTieneDerechoEliminarOrden(int AUsuario, int AIdDerecho){
			string pSentencia = "SELECT COUNT(*) FROM DERECHOS_USUARIO WHERE ID_USUARIO = @IDUSUARIO AND ID_DERECHO = @IDDERECHO AND PUEDE_ELIMINAR_ORDEN = 'Si'";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@IDUSUARIO", FbDbType.Integer).Value = AUsuario;
			com.Parameters.Add("@IDDERECHO", FbDbType.Integer).Value = AIdDerecho;
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				if(reader.Read()){
					if((int)reader["COUNT"]>0)
						return "Si";
					
				}
			}
			finally
			{
				if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
			}
			return "No";
		}
		
        public Usuario InsertarUsuario(Usuario AUsuario)
        {
			
            string pSentencia="INSERT INTO USUARIOSADMIN(NOMBRE,CORREO,USUARIO,PASS) VALUES(@NOMBRE,@CORREO,@USUARIO,@PASS) RETURNING ID";
			FbConnection con = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia,con);
			com.Parameters.Add("@NOMBRE", FbDbType.VarChar).Value  = AUsuario.Nombre;
			com.Parameters.Add("@CORREO", FbDbType.VarChar).Value  = AUsuario.Correo;
			com.Parameters.Add("@USUARIO", FbDbType.VarChar).Value = AUsuario.User;
			com.Parameters.Add("@PASS", FbDbType.VarChar).Value    = AUsuario.Pass;
			
			FbParameter pOutParameter = new FbParameter("@ID", FbDbType.Integer);
			pOutParameter.Direction = ParameterDirection.Output;
			com.Parameters.Add(pOutParameter);
			
			try{
				con.Open();
				com.ExecuteNonQuery();
			}
			finally{
				if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
			}
			return ObtenerUsuario((int)pOutParameter.Value);
        }

        private Usuario ObtenerUsuario(int AClave)
        {
            Usuario pResult = null;
			
			string pSentencia = "SELECT * FROM USUARIOSADMIN WHERE ID=@CLAVE";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@CLAVE", FbDbType.Integer).Value = AClave;
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				if(reader.Read()){
					pResult                 	= new Usuario();
					pResult.Clave  	        	= (int)reader["ID"];
					pResult.Nombre     			= (reader["NOMBRE"]!=DBNull.Value) ? (string)reader["NOMBRE"] : "";
					pResult.Correo        		= (reader["CORREO"]!=DBNull.Value) ? (string)reader["CORREO"] : "";
					pResult.User           		= (reader["USUARIO"]!=DBNull.Value) ? (string)reader["USUARIO"] : "";
					pResult.Pass     			= (reader["PASS"]!=DBNull.Value) ? (string)reader["PASS"] : "";
					pResult.PuedeProgramar   	= (reader["PUEDE_PROGRAMAR"]!=DBNull.Value) ? (string)reader["PUEDE_PROGRAMAR"] : "";
					pResult.PuedeEditarOrdenes  = (reader["PUEDE_CAMBIAR_ESTADO"]!=DBNull.Value) ? (string)reader["PUEDE_CAMBIAR_ESTADO"] : "" ;
					pResult.PuedeCambiarEstado 	= (reader["PUEDE_EDITAR_ORDENES"]!=DBNull.Value) ? (string)reader["PUEDE_EDITAR_ORDENES"] : "" ;
				}
			}
			finally
			{
				if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
			}
			return  pResult;
        }
		
		public Usuario ModificarUsuario(Usuario AUser){
			string pSentencia = "UPDATE USUARIOSADMIN SET NOMBRE=@NOMBRE, CORREO=@CORREO, USUARIO=@USUARIO,"+
			"PASS=@PASS WHERE ID=@CLAVE";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@CLAVE", FbDbType.VarChar).Value   = AUser.Clave;
			com.Parameters.Add("@NOMBRE", FbDbType.VarChar).Value  = AUser.Nombre;
			com.Parameters.Add("@CORREO", FbDbType.VarChar).Value  = AUser.Correo;
			com.Parameters.Add("@USUARIO", FbDbType.VarChar).Value = AUser.User;
			com.Parameters.Add("@PASS", FbDbType.VarChar).Value    = AUser.Pass;
			
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
			return ObtenerUsuario(AUser.Clave);
		}
		
		public bool EliminarUsuario(int AClave, out string AMensajeError){
			bool pResult = true;
			AMensajeError = "";
			
			string pSentencia = "DELETE FROM USUARIOSADMIN WHERE ID = @CLAVE";
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
		
		public List<DerechoUsuario> ObtenerDerechos(){
			List<DerechoUsuario> pResult=new List<DerechoUsuario>();
			DerechoUsuario derecho=null;
			string pSentencia = "SELECT DISTINCT(T0.CONTROLADOR), T1.MENU, T1.ID_DERECHO, T1.CLASIFICACION FROM DERECHOS_USUARIO T0 "+
			          "INNER JOIN DERECHOS_USUARIO T1 ON T1.CONTROLADOR = T0.CONTROLADOR ORDER BY T1.CLASIFICACION, T1.MENU";
					  
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				while(reader.Read()){
					derecho 					= new DerechoUsuario();
					derecho.IdDerecho 			= (int) reader["ID_DERECHO"]; 
					derecho.Controlador 		= (reader["CONTROLADOR"] != DBNull.Value) ? (string) reader["CONTROLADOR"] : ""; 
					derecho.Menu 				= (reader["MENU"] != DBNull.Value) ? (string) reader["MENU"] : ""; 
					derecho.Clasificacion 		= (reader["CLASIFICACION"] != DBNull.Value) ? (string) reader["CLASIFICACION"] : ""; 
					pResult.Add(derecho);
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
		
		
		
		public List<int> ObtenerDerechosUsuario(int AClave){
			List<int> pResult=new List<int>();
			string pSentencia = "SELECT DISTINCT(ID_DERECHO) FROM DERECHOS_USUARIO WHERE ID_USUARIO = @CLAVE";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@CLAVE", FbDbType.Integer).Value = AClave;	

			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				while(reader.Read()){
					pResult.Add((int) reader["ID_DERECHO"]);
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
		
		public List<DerechoUsuario> ObtenerDerechosUsuarioTodosLosCampos(int AClave){
			List<DerechoUsuario> pResult=new List<DerechoUsuario>();
			DerechoUsuario derecho;
			string pSentencia = "SELECT DISTINCT ID_DERECHO,CONTROLADOR2,MENU,CLASIFICACION FROM DERECHOS_USUARIO WHERE ID_USUARIO = @CLAVE ORDER BY CLASIFICACION ";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@CLAVE", FbDbType.Integer).Value = AClave;	

			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				while(reader.Read()){
					derecho 					= new DerechoUsuario();
					derecho.IdDerecho 			= (int) reader["ID_DERECHO"]; 
					derecho.Controlador 		= (reader["CONTROLADOR2"] != DBNull.Value) ? (string) reader["CONTROLADOR2"] : ""; 
					derecho.Menu 				= (reader["MENU"] != DBNull.Value) ? (string) reader["MENU"] : ""; 
					derecho.Clasificacion 		= (reader["CLASIFICACION"] != DBNull.Value) ? (string) reader["CLASIFICACION"] : ""; 
					pResult.Add(derecho);
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

        //USUARIOS PISTOLA

        public List<UsuarioPistola> ObtenerListaUsuariosPistolas()
        {
            List<UsuarioPistola> pResult = new List<UsuarioPistola>();
            UsuarioPistola usuario;
            string pSentencia = "SELECT * FROM DRASUSUAP WHERE ACTIVO='Si' ORDER BY ID";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    usuario = new UsuarioPistola();
                    usuario.Clave       = (int)reader["ID"];
                    usuario.Nombre      = (reader["NOMBRE"] != DBNull.Value) ? (string)reader["NOMBRE"] : "";
                    usuario.Contrasena  = (reader["CONTRASENA"] != DBNull.Value) ? (string)reader["CONTRASENA"] : "";
                    usuario.Titular     = (reader["TITULAR"] != DBNull.Value) ? (string)reader["TITULAR"] : "";
                    usuario.Activo      = (reader["ACTIVO"] != DBNull.Value) ? (string)reader["ACTIVO"] : "";
                    pResult.Add(usuario);
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

        public UsuarioPistola InsertarUsuarioPistola(UsuarioPistola AUsuario)
        {
			
            string pSentencia="INSERT INTO DRASUSUAP(ID,NOMBRE,CONTRASENA,TITULAR,ACTIVO) VALUES(@ID,@NOMBRE,@CONTRASENA,@TITULAR,@ACTIVO) RETURNING ID";
			FbConnection con = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia,con);

            com.Parameters.Add("@ID", FbDbType.Integer).Value           = MaxUsuarioPistola()+1;
            com.Parameters.Add("@NOMBRE", FbDbType.VarChar).Value       = AUsuario.Nombre;
			com.Parameters.Add("@CONTRASENA", FbDbType.VarChar).Value   = AUsuario.Contrasena;
            com.Parameters.Add("@TITULAR", FbDbType.VarChar).Value      = AUsuario.Titular;
			com.Parameters.Add("@ACTIVO", FbDbType.VarChar).Value       = AUsuario.Activo;
			
			FbParameter pOutParameter = new FbParameter("@ID", FbDbType.Integer);
			pOutParameter.Direction = ParameterDirection.Output;
			com.Parameters.Add(pOutParameter);
			
			try{
				con.Open();
				com.ExecuteNonQuery();
			}
			finally{
				if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
			}
			return ObtenerUsuarioPistola((int)pOutParameter.Value);
        }
		
		public UsuarioPistola ModificarUsuarioPistola(UsuarioPistola AUsuario){

			string pSentencia = "UPDATE DRASUSUAP SET NOMBRE=@NOMBRE, CONTRASENA=@CONTRASENA, TITULAR=@TITULAR," +
			"ACTIVO=@ACTIVO WHERE ID=@CLAVE";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@CLAVE", FbDbType.VarChar).Value      = AUsuario.Clave;
            com.Parameters.Add("@NOMBRE", FbDbType.VarChar).Value     = AUsuario.Nombre;
            com.Parameters.Add("@CONTRASENA", FbDbType.VarChar).Value = AUsuario.Contrasena;
            com.Parameters.Add("@TITULAR", FbDbType.VarChar).Value    = AUsuario.Titular;
            com.Parameters.Add("@ACTIVO", FbDbType.VarChar).Value     = AUsuario.Activo;

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
			return ObtenerUsuarioPistola(AUsuario.Clave);
		}
		
		public bool EliminarUsuarioPistola(int AClave, out string AMensajeError){
			bool pResult = true;
			AMensajeError = "";
			
			string pSentencia = "UPDATE DRASUSUAP SET ACTIVO='No' WHERE ID=@CLAVE";
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
		
		private UsuarioPistola ObtenerUsuarioPistola(int AClave)
        {
            UsuarioPistola pResult = null;
			
			string pSentencia = "SELECT * FROM DRASUSUAP WHERE ID=@CLAVE";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@CLAVE", FbDbType.Integer).Value = AClave;
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				if(reader.Read()){
					pResult                 	= new UsuarioPistola();
					pResult.Clave  	        	= (int)reader["ID"];
					pResult.Nombre     			= (reader["NOMBRE"]!=DBNull.Value) ? (string)reader["NOMBRE"] : "";
					pResult.Contrasena        	= (reader["CONTRASENA"]!=DBNull.Value) ? (string)reader["CONTRASENA"] : "";
					pResult.Titular             = (reader["TITULAR"]!=DBNull.Value) ? (string)reader["TITULAR"] : "";
					pResult.Activo    			= (reader["ACTIVO"]!=DBNull.Value) ? (string)reader["ACTIVO"] : "";
				}
			}
			finally
			{
				if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
			}
			return  pResult;
        }

        
        private int MaxUsuarioPistola()
        {

            string pSentencia = "SELECT max(ID) as ID FROM DRASUSUAP";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    return (int)reader["ID"];
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

        public List<string> ObtenerClasificacionesDerechos()
        {
            List<string> pResult = new List<string>();
            string pSentencia = "SELECT DISTINCT CLASIFICACION FROM DERECHOS_USUARIO ";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pResult.Add((string)reader["CLASIFICACION"]);
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