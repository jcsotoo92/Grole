using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using grole.src.Entidades;

namespace grole.src.Persistencia
{
	
	public class CamarasPersistencia
	{
		private Conexiones _Conexion;
		public CamarasPersistencia(Conexiones _Conexion){
			this._Conexion=_Conexion;
		}

        public bool ExisteCamaraMismoNombre(Camara ACamara)
        {
            string pSentencia = "SELECT ID FROM DRASCAM WHERE UPPER(TRIM(DESCRIPCION)) = @DESCRIPCION";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@DESCRIPCION", FbDbType.VarChar).Value = ACamara.Descripcion.ToUpper().Trim();

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    if ((int)reader["ID"] == ACamara.Clave)
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

        public bool ExisteCamaraMismoId(Camara ACamara)
        {
            string pSentencia = "SELECT ID FROM DRASCAM WHERE ID = @ID";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@ID", FbDbType.Integer).Value = ACamara.Clave;


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

        public bool ExisteProductoEnValidacion(int ACamara, string AProducto, string AFechaMin, string AFechaMax){
			
			string pSentencia = "SELECT * FROM DRASVALIDAPTOSCAMARA WHERE ID_CAMARA = @CAMARA AND PRODUCTO = @PRODUCTO AND FECHA_MIN_PRODUCCION = @FECHAMIN AND FECHA_MAX_PRODUCCION = @FECHAMAX";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@CAMARA", FbDbType.Integer).Value   = ACamara;
			com.Parameters.Add("@PRODUCTO", FbDbType.VarChar).Value = AProducto;
			com.Parameters.Add("@FECHAMIN", FbDbType.VarChar).Value = AFechaMin;
			com.Parameters.Add("@FECHAMAX", FbDbType.VarChar).Value = AFechaMax;
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				if(reader.Read()){
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
		
		private ValidacionCamara ObtenerValidacionCamara(int AClave){
			ValidacionCamara pResult = null;
			
			string pSentencia = "SELECT a.ID, a.ID_CAMARA, a.PRODUCTO, a.FECHA_MIN_PRODUCCION, a.FECHA_MAX_PRODUCCION, b.DESCRIPCION, a.CANTIDAD_MAXIM, a.KILOS_MAXIM"
                      +" FROM DRASVALIDAPTOSCAMARA a JOIN DRASPROD b ON b.CLAVE = a.PRODUCTO WHERE ID=@CLAVE";
			
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@CLAVE", FbDbType.Integer).Value = AClave;
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				if(reader.Read()){
					pResult                    = new ValidacionCamara();
					pResult.Id                 = (int)reader["ID"];
					pResult.IdCamara           = (int)reader["ID_CAMARA"];
					pResult.Producto           = (reader["PRODUCTO"]!=DBNull.Value) ? (string)reader["PRODUCTO"] : "";
					pResult.Descripcion        = (reader["DESCRIPCION"]!=DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
					pResult.CantidadMax        = (int)reader["CANTIDAD_MAXIM"];
					pResult.KilosMax           = (decimal)reader["KILOS_MAXIM"];
					pResult.FechaMaxProduccion = Utilerias.dateTimeToString((DateTime)reader["FECHA_MIN_PRODUCCION"]);
					pResult.FechaMinProduccion = Utilerias.dateTimeToString((DateTime)reader["FECHA_MAX_PRODUCCION"]);
					
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
		
		public ValidacionCamara InsertarValidacionCamara(int ACamara, string AProducto, int ACantidad, decimal AKilos, string AFechaMin, string AFechaMax){

			string pSentencia = "INSERT INTO DRASVALIDAPTOSCAMARA(ID_CAMARA, PRODUCTO, CANTIDAD_MAXIM, KILOS_MAXIM, FECHA_MIN_PRODUCCION, FECHA_MAX_PRODUCCION) "+
			"VALUES(@CAMARA, @PRODUCTO, @CANTIDAD, @KILOS, @FECHAMIN, @FECHAMAX) RETURNING ID";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@CAMARA", FbDbType.Integer).Value   = ACamara;
			com.Parameters.Add("@PRODUCTO", FbDbType.VarChar).Value = AProducto;
			com.Parameters.Add("@CANTIDAD", FbDbType.Integer).Value = ACantidad;
			com.Parameters.Add("@KILOS", FbDbType.Numeric).Value    = AKilos;
			com.Parameters.Add("@FECHAMIN", FbDbType.VarChar).Value = AFechaMin;
			com.Parameters.Add("@FECHAMAX", FbDbType.VarChar).Value = AFechaMax;
			
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
			return ObtenerValidacionCamara((int)pOutParameter.Value);
			}
			
			public List<ValidacionCamara> ObtenerValidacionesCamaras(string AClave)
			{
				List<ValidacionCamara> plistaValidaciones=new List<ValidacionCamara>();
				ValidacionCamara pResult = null;
				
				string pSentencia = "SELECT a.ID, a.ID_CAMARA, a.PRODUCTO, a.FECHA_MIN_PRODUCCION, a.FECHA_MAX_PRODUCCION, b.DESCRIPCION, a.CANTIDAD_MAXIM, a.KILOS_MAXIM"
                      +" FROM DRASVALIDAPTOSCAMARA a JOIN DRASPROD b ON b.CLAVE = a.PRODUCTO WHERE a.ID_CAMARA = @CLAVE";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@CLAVE", FbDbType.Integer).Value = AClave;
			
			try
			{
				con.Open();
				FbDataReader reader = com.ExecuteReader();
				while(reader.Read())
				{
					pResult                    = new ValidacionCamara();
					pResult.Id                 = (int)reader["ID"];
					pResult.IdCamara           = (int)reader["ID_CAMARA"];	
					pResult.Producto 		   = (reader["PRODUCTO"]!=DBNull.Value) ? (string)reader["PRODUCTO"] : pResult.Producto = "";
					pResult.Descripcion        = (reader["DESCRIPCION"]!=DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
					pResult.CantidadMax        = (int)reader["CANTIDAD_MAXIM"];
					pResult.KilosMax           = (decimal)reader["KILOS_MAXIM"];
					pResult.FechaMaxProduccion = Utilerias.dateTimeToString((DateTime)reader["FECHA_MIN_PRODUCCION"]);
					pResult.FechaMinProduccion = Utilerias.dateTimeToString((DateTime)reader["FECHA_MAX_PRODUCCION"]);
					
					plistaValidaciones.Add(pResult);
				}
			}
			finally
			{
				if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
			}
			
			return plistaValidaciones;
		}
		
		public List<Camara> ObtenerCamaras()
		{
			List<Camara> listaCamaras=new List<Camara>();
			Camara pResult = null;
			
			string pSentencia = "SELECT * FROM DRASCAM ORDER BY ID";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				while(reader.Read())
				{
					pResult                 = new Camara();
					pResult.Clave           = (int)reader["ID"];
					pResult.Descripcion     = (string)reader["DESCRIPCION"];
					pResult.Columnas        = (reader["COLUMNAS"]!=DBNull.Value) ? (int)reader["COLUMNAS"] : 0;
					pResult.Filas           = (reader["FILAS"]!=DBNull.Value) ? (int)reader["FILAS"] : 0;
					pResult.Profundidad     = (reader["PROFUNDIDAD"]!=DBNull.Value) ? (int)reader["PROFUNDIDAD"] : 0;
					pResult.PermiteSalida   = (reader["PERMITE_SALIDA"]!=DBNull.Value) ? (string)reader["PERMITE_SALIDA"] : "";
					pResult.ValidaPosicion  = (reader["VALIDA_POSICION"]!=DBNull.Value) ? (string)reader["VALIDA_POSICION"] : "" ;
					pResult.ValidaProductos = (reader["VALIDA_PRODUCTOS"]!=DBNull.Value) ? (string)reader["VALIDA_PRODUCTOS"] : "" ;
					pResult.Activo          = (reader["ACTIVO"]!=DBNull.Value) ? pResult.Activo = (string)reader["ACTIVO"] : "";
					pResult.Embarque        = (reader["EMBARQUE"]!=DBNull.Value) ? (string)reader["EMBARQUE"] : "";
					pResult.FechaEmbarque   = Utilerias.dateTimeToString((DateTime)reader["FECHA_EMBARQUE"]);
					listaCamaras.Add(pResult);
				}
			}
			finally
			{
				if (con.State == System.Data.ConnectionState.Open){
                    con.Close();
                }
			}
			
			return listaCamaras;
		}

		public Camara ObtenerCamara(int AClave){
			Camara pResult = null;
			
			string pSentencia = "SELECT * FROM DRASCAM WHERE ID=@CLAVE";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@CLAVE", FbDbType.Integer).Value = AClave;
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				if(reader.Read()){
					pResult                 = new Camara();
					pResult.Clave  	        = (int)reader["ID"];
					pResult.Descripcion     = (string)reader["DESCRIPCION"];
					pResult.Columnas        = (reader["COLUMNAS"]!=DBNull.Value) ? (int)reader["COLUMNAS"] : 0;
					pResult.Filas           = (reader["FILAS"]!=DBNull.Value) ? (int)reader["FILAS"] : 0;
					pResult.Profundidad     = (reader["PROFUNDIDAD"]!=DBNull.Value) ? (int)reader["PROFUNDIDAD"] : 0;
					pResult.PermiteSalida   = (reader["PERMITE_SALIDA"]!=DBNull.Value) ? (string)reader["PERMITE_SALIDA"] : "";
					pResult.ValidaPosicion  = (reader["VALIDA_POSICION"]!=DBNull.Value) ? (string)reader["VALIDA_POSICION"] : "" ;
					pResult.ValidaProductos = (reader["VALIDA_PRODUCTOS"]!=DBNull.Value) ? (string)reader["VALIDA_PRODUCTOS"] : "" ;
					pResult.Activo          = (reader["ACTIVO"]!=DBNull.Value) ? pResult.Activo = (string)reader["ACTIVO"] : "";
					pResult.Embarque        = (reader["EMBARQUE"]!=DBNull.Value) ? (string)reader["EMBARQUE"] : "";
					pResult.FechaEmbarque   = Utilerias.dateTimeToString( (DateTime)reader["FECHA_EMBARQUE"]);
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
		
		
		public Camara CamaraInsertar(Camara Cam){
			
			string pSentencia = "INSERT INTO DRASCAM VALUES(@CLAVE,@DESCRIPCION,@COLUMNAS,@FILAS,@PROFUNDIDAD,@PERMITE_SALIDA,@VALIDA_POSICION,@VALIDA_PRODUCTOS,@ACTIVO,@EMBARQUE,@FECHA_EMBARQUE) RETURNING ID";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@CLAVE", FbDbType.Integer).Value            = Cam.Clave;
			com.Parameters.Add("@DESCRIPCION", FbDbType.VarChar).Value      = Cam.Descripcion;
			com.Parameters.Add("@COLUMNAS", FbDbType.Integer).Value         = Cam.Columnas;
			com.Parameters.Add("@FILAS", FbDbType.Integer).Value            = Cam.Filas;
			com.Parameters.Add("@PROFUNDIDAD", FbDbType.Integer).Value      = Cam.Profundidad;
			com.Parameters.Add("@PERMITE_SALIDA", FbDbType.VarChar).Value   = Cam.PermiteSalida;
			com.Parameters.Add("@VALIDA_POSICION", FbDbType.VarChar).Value  = Cam.ValidaPosicion;
			com.Parameters.Add("@VALIDA_PRODUCTOS", FbDbType.VarChar).Value = Cam.ValidaProductos;
			com.Parameters.Add("@ACTIVO", FbDbType.VarChar).Value           = Cam.Activo;
			com.Parameters.Add("@EMBARQUE", FbDbType.VarChar).Value         = Cam.Embarque;
			com.Parameters.Add("@FECHA_EMBARQUE", FbDbType.TimeStamp).Value = Cam.FechaEmbarque;
			
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
			return ObtenerCamara((int)pOutParameter.Value);
		}
		
		public bool CamaraEliminar(int AClave, out string AMensajeError){
			bool pResult = true;
			AMensajeError = "";
			
			string pSentencia = "DELETE FROM DRASCAM WHERE ID = @CLAVE";
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
		
		public bool CamaraEliminarValidacion(int AClave, out string AMensajeError){
			bool pResult = true;
			AMensajeError = "";
			
			string pSentencia = "DELETE FROM DRASVALIDAPTOSCAMARA WHERE ID = @CLAVE";
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
				if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
			}
			
			return pResult;
		}

		public Camara CamaraModificar(Camara Cam){
			string pSentencia = "UPDATE DRASCAM SET DESCRIPCION=@DESCRIPCION, COLUMNAS=@COLUMNAS, FILAS=@FILAS,"+
			"PROFUNDIDAD=@PROFUNDIDAD,PERMITE_SALIDA=@PERMITE_SALIDA,VALIDA_POSICION=@VALIDA_POSICION,"+
			"VALIDA_PRODUCTOS=@VALIDA_PRODUCTOS,ACTIVO=@ACTIVO, EMBARQUE=@EMBARQUE, FECHA_EMBARQUE=@FECHA_EMBARQUE WHERE ID=@CLAVE";
			FbConnection con  = _Conexion.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@CLAVE", FbDbType.Integer).Value            = Cam.Clave;
			com.Parameters.Add("@DESCRIPCION", FbDbType.VarChar).Value      = Cam.Descripcion;
			com.Parameters.Add("@COLUMNAS", FbDbType.Integer).Value         = Cam.Columnas;
			com.Parameters.Add("@FILAS", FbDbType.Integer).Value            = Cam.Filas;
			com.Parameters.Add("@PROFUNDIDAD", FbDbType.Integer).Value      = Cam.Profundidad;
			com.Parameters.Add("@PERMITE_SALIDA", FbDbType.VarChar).Value   = Cam.PermiteSalida;
			com.Parameters.Add("@VALIDA_POSICION", FbDbType.VarChar).Value  = Cam.ValidaPosicion;
			com.Parameters.Add("@VALIDA_PRODUCTOS", FbDbType.VarChar).Value = Cam.ValidaProductos;
			com.Parameters.Add("@ACTIVO", FbDbType.VarChar).Value           = Cam.Activo;
			com.Parameters.Add("@EMBARQUE", FbDbType.VarChar).Value         = Cam.Embarque;
			com.Parameters.Add("@FECHA_EMBARQUE", FbDbType.TimeStamp).Value = Cam.FechaEmbarque;
			
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
			return ObtenerCamara(Cam.Clave);
		}

        public List<Camara> ObtenerCamarasActivas()
        {
            List<Camara> listaCamaras = new List<Camara>();
            Camara pResult = null;

            string pSentencia = "SELECT ID, DESCRIPCION FROM DRASCAM WHERE ACTIVO = 'Si'";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pResult = new Camara();
                    pResult.Clave = (int)reader["ID"];
                    pResult.Descripcion = (string)reader["DESCRIPCION"];
                    listaCamaras.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return listaCamaras;
        }

        public List<SaldoCamara> obtener_lista_camaras_catalogo_activas()
        {
            List<SaldoCamara> pResult = new List<SaldoCamara>();
            SaldoCamara pSaldoCamara = null;
            DateTime thisDay = DateTime.Today;
            string pSentencia = "SELECT ID, DESCRIPCION FROM DRASCAM WHERE EMBARQUE = 'Si' AND FECHA_EMBARQUE >='" + thisDay.ToString("dd'.'MM'.'yyyy") + ", 00:00:00.000'";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pSaldoCamara = new SaldoCamara();
                    pSaldoCamara.Id = (reader["ID"] != DBNull.Value) ? (int)reader["ID"] : -1;
                    pSaldoCamara.Descripcion = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pResult.Add(pSaldoCamara);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            var cam = 123;
            this.obtener_validaciones_camara(cam);
            return pResult;
        }

        public List<SaldoCamara> obtener_validaciones_camara(int ACamara)
        {
            List<SaldoCamara> pResult = new List<SaldoCamara>();
            SaldoCamara pSaldoCamara = null;
            string pSentencia = "SELECT a.ID, a.ID_CAMARA, a.PRODUCTO, a.FECHA_MIN_PRODUCCION, a.FECHA_MAX_PRODUCCION, b.DESCRIPCION, a.CANTIDAD_MAXIM, a.KILOS_MAXIM FROM DRASVALIDAPTOSCAMARA a JOIN DRASPROD b ON b.CLAVE = a.PRODUCTO WHERE a.ID_CAMARA =" + ACamara;
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                Console.WriteLine("Validaciones Camara");
                while (reader.Read())
                {
                    pSaldoCamara = new SaldoCamara();
                    pSaldoCamara.Id = (reader["ID"] != DBNull.Value) ? (int)reader["ID"] : -1;
                    pSaldoCamara.Id_Camara = (reader["ID_CAMARA"] != DBNull.Value) ? (int)reader["ID_CAMARA"] : -1;
                    pSaldoCamara.Producto = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pSaldoCamara.Fecha_Min_Produccion = reader["FECHA_MIN_PRODUCCION"] != DBNull.Value ? Utilerias.dateTimeToString((DateTime)reader["FECHA_MIN_PRODUCCION"]) : "";
                    pSaldoCamara.Fecha_Max_Produccion = reader["FECHA_MAX_PRODUCCION"] != DBNull.Value ? Utilerias.dateTimeToString((DateTime)reader["FECHA_MAX_PRODUCCION"]) : "";
                    pSaldoCamara.Descripcion = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pSaldoCamara.Cantidad_Maxim = (reader["CANTIDAD_MAXIM"] != DBNull.Value) ? (int)reader["CANTIDAD_MAXIM"] : -1;
                    pSaldoCamara.Kilos_Maxim = reader["KILOS_MAXIM"] != DBNull.Value ? (decimal)reader["KILOS_MAXIM"] : -1;
                    pResult.Add(pSaldoCamara);

                    Console.WriteLine("ID: " + pSaldoCamara.Id + " ID_CAMARA: " + pSaldoCamara.Id_Camara + " PRODUCTO: " + pSaldoCamara.Producto + " FECHA_MIN_PRODUCCION: " + pSaldoCamara.Fecha_Min_Produccion + " FECHA_MAX_PRODUCCION: " + pSaldoCamara.Fecha_Max_Produccion + " DESCRIPCION: " + pSaldoCamara.Descripcion + " CANTIDAD_MAXIM: " + pSaldoCamara.Cantidad_Maxim + " KILOS_MAXIM: " + pSaldoCamara.Kilos_Maxim);
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

        public SaldoCamara obtener_saldo_camara_producto(int AProducto, int ACamara)
        {
            SaldoCamara pResult = new SaldoCamara();
            string pSentencia = "SELECT COUNT(*) AS CAJAS, COALESCE(SUM(PESO), 0) AS KILOS, MIN(FECHA) AS FECHA_MIN, MAX(FECHA) AS FECHA_MAX FROM DRASCORT WHERE PRODUCTO = @PRODUCTO AND CAMARA = @CAMARA";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@PRODUCTO", FbDbType.VarChar).Value = AProducto;
            com.Parameters.Add("@CAMARA", FbDbType.VarChar).Value = ACamara;
            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                Console.WriteLine("Saldo Camara");
                if (reader.Read())
                {
                    pResult = new SaldoCamara();
                    pResult.Cajas = (reader["CAJAS"] != DBNull.Value) ? (int)reader["CAJAS"] : -1;
                    pResult.Kilos = (reader["KILOS"] != DBNull.Value) ? (decimal)reader["KILOS"] : -1;
                    pResult.Fecha_Min = reader["FECHA_MIN"] != DBNull.Value ? Utilerias.dateTimeToString((DateTime)reader["FECHA_MIN"]) : "";
                    pResult.Fecha_Max = reader["FECHA_MAX"] != DBNull.Value ? Utilerias.dateTimeToString((DateTime)reader["FECHA_MAX"]) : "";
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