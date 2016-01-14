using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System.Collections.Generic;
using System;
using System.Data;

namespace grole.src.Persistencia
{
	public class ProductosPersistencia
	{
		
		private Conexiones _Conexiones { get; set; }
		
		public ProductosPersistencia(Conexiones AConexion)
		{
			this._Conexiones = AConexion;
		}
		
		//Regresa la lista de productos para dropdown menu
		public List<Producto> ListaProductos()
		{
			List<Producto> pResult = new List<Producto>();
			string pSentencia = "SELECT CLAVE, DESCRIPCION FROM DRASPROD";
			FbConnection con  = _Conexiones.ObtenerConexion();
			FbCommand com 	  = new FbCommand(pSentencia, con);
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();
				
				while (reader.Read())
				{
					Producto pProducto    = new Producto();
					pProducto.Clave       = (string)reader["CLAVE"];
					pProducto.Descripcion = (string)reader["DESCRIPCION"];
					pResult.Add(pProducto);
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
		
		public void ProductoInsertar(Producto AProducto)
		{
            /*FECHA_SACRIFICIO,*/
            /*@FECHA_SACRIFICIO,*/
            string pSentencia = "INSERT INTO DRASPROD (CLAVE, DESCRIPCION, CODIGOSAP, CODIGOPROVEDOR, MERCADO, CLASE, LINEA, GRUPO, OPERADOR, RENDIMIENTO, MOLDEADO, INVENTARIABLE, CAPTURA_PEDIMENTO, CATEGORIA, PESOFIJO, PESOMINIMO, PESOMAXIMO, PESOTARA, DECIMALES, FORMATO_ETIQUETA, TEMPERATURA, CLAVE_ALM, KILOS_POR_CAJA, TIPO_FECHA_PRODUCCION, FECHA_PRODUCCION, TIPO_FECHA_SACRIFICIO,  TIPO_FECHA_CADUCIDAD, DIASCAD, CALCULA_DIAS_CAD, COPIAS, OTROS) VALUES (@CLAVE, @DESCRIPCION, @CODIGOSAP, @CODIGOPROVEDOR, @MERCADO, @CLASE, @LINEA, @GRUPO, @OPERADOR, @RENDIMIENTO, @MOLDEADO, @INVENTARIABLE, @CAPTURA_PEDIMENTO, @CATEGORIA, @PESOFIJO, @PESOMINIMO, @PESOMAXIMO, @PESOTARA, @DECIMALES, @FORMATO_ETIQUETA, @TEMPERATURA, @CLAVE_ALM, @KILOS_POR_CAJA, @TIPO_FECHA_PRODUCCION, @FECHA_PRODUCCION, @TIPO_FECHA_SACRIFICIO, @TIPO_FECHA_CADUCIDAD, @DIASCAD, @CALCULA_DIAS_CAD, @COPIAS, @OTROS)";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@CLAVE", FbDbType.VarChar).Value        		  = AProducto.Clave;
			com.Parameters.Add("@DESCRIPCION", FbDbType.VarChar).Value  		  = AProducto.Descripcion;
            com.Parameters.Add("@CODIGOSAP", FbDbType.VarChar).Value              = AProducto.CodigoSap;
            com.Parameters.Add("@CODIGOPROVEDOR", FbDbType.VarChar).Value         = AProducto.CodigoProveedor;
            com.Parameters.Add("@MERCADO", FbDbType.VarChar).Value                = AProducto.Mercado;
            com.Parameters.Add("@CLASE", FbDbType.VarChar).Value                  = AProducto.Clase;
            com.Parameters.Add("@LINEA", FbDbType.VarChar).Value                  = AProducto.Linea;
            com.Parameters.Add("@GRUPO", FbDbType.SmallInt).Value                 = AProducto.Grupo;
            com.Parameters.Add("@OPERADOR", FbDbType.SmallInt).Value              = AProducto.Operador;
            com.Parameters.Add("@RENDIMIENTO", FbDbType.Float).Value              = AProducto.Rendimiento;
            com.Parameters.Add("@MOLDEADO", FbDbType.VarChar).Value               = AProducto.Moldeado;
            com.Parameters.Add("@INVENTARIABLE", FbDbType.VarChar).Value          = AProducto.Inventariable;
            com.Parameters.Add("@CAPTURA_PEDIMENTO", FbDbType.VarChar).Value      = AProducto.Captura_Pedimento;
            com.Parameters.Add("@CATEGORIA", FbDbType.Integer).Value              = AProducto.Categoria;
            com.Parameters.Add("@PESOFIJO", FbDbType.Float).Value                 = AProducto.Pesofijo;
            com.Parameters.Add("@PESOMINIMO", FbDbType.Float).Value               = AProducto.Pesominimo;
            com.Parameters.Add("@PESOMAXIMO", FbDbType.Float).Value               = AProducto.Pesomaximo;
            com.Parameters.Add("@PESOTARA", FbDbType.Float).Value                 = AProducto.Pesotara;
            com.Parameters.Add("@DECIMALES", FbDbType.SmallInt).Value             = AProducto.Decimales;
            com.Parameters.Add("@FORMATO_ETIQUETA", FbDbType.Integer).Value       = AProducto.Formato_Etiqueta;
            com.Parameters.Add("@TEMPERATURA", FbDbType.VarChar).Value            = AProducto.Temperatura;
            com.Parameters.Add("@CLAVE_ALM", FbDbType.VarChar).Value    		  = AProducto.Clave_Alm;
            com.Parameters.Add("@KILOS_POR_CAJA", FbDbType.Numeric).Value         = AProducto.Kilos_Por_Caja;
            com.Parameters.Add("@TIPO_FECHA_PRODUCCION", FbDbType.SmallInt).Value = AProducto.Tipo_Fecha_Produccion;
            com.Parameters.Add("@FECHA_PRODUCCION", FbDbType.TimeStamp).Value     = AProducto.Fecha_Produccion;
            com.Parameters.Add("@TIPO_FECHA_SACRIFICIO", FbDbType.SmallInt).Value = AProducto.Tipo_Fecha_Sacrificio;
            //com.Parameters.Add("@FECHA_SACRIFICIO", FbDbType.TimeStamp).Value     = AProducto.Fecha_Sacrificio;
            com.Parameters.Add("@TIPO_FECHA_CADUCIDAD", FbDbType.SmallInt).Value  = AProducto.Tipo_Fecha_Caducidad;
            com.Parameters.Add("@DIASCAD", FbDbType.Float).Value                  = AProducto.Diascad;
            com.Parameters.Add("@CALCULA_DIAS_CAD", FbDbType.VarChar).Value       = AProducto.Calcula_Dias_Cad;
            com.Parameters.Add("@COPIAS", FbDbType.SmallInt).Value                = AProducto.Copias;
            com.Parameters.Add("@OTROS", FbDbType.Integer).Value                  = AProducto.Otros;
			
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
		}

        public void ProductoActualizar(Producto AProducto)
        {
            string pSentencia = "UPDATE DRASPROD SET DESCRIPCION=@DESCRIPCION, CODIGOSAP=@CODIGOSAP, CODIGOPROVEDOR=@CODIGOPROVEDOR, MERCADO=@MERCADO, CLASE=@CLASE, LINEA=@LINEA, GRUPO=@GRUPO, OPERADOR=@OPERADOR, RENDIMIENTO=@RENDIMIENTO, MOLDEADO=@MOLDEADO, INVENTARIABLE=@INVENTARIABLE, CAPTURA_PEDIMENTO=@CAPTURA_PEDIMENTO, CATEGORIA=@CATEGORIA, PESOFIJO=@PESOFIJO, PESOMINIMO=@PESOMINIMO, PESOMAXIMO=@PESOMAXIMO, PESOTARA=@PESOTARA, DECIMALES=@DECIMALES, FORMATO_ETIQUETA=@FORMATO_ETIQUETA, TEMPERATURA=@TEMPERATURA, CLAVE_ALM=@CLAVE_ALM, KILOS_POR_CAJA=@KILOS_POR_CAJA, TIPO_FECHA_PRODUCCION=@TIPO_FECHA_PRODUCCION, FECHA_PRODUCCION=@FECHA_PRODUCCION, TIPO_FECHA_SACRIFICIO=@TIPO_FECHA_SACRIFICIO,  TIPO_FECHA_CADUCIDAD=@TIPO_FECHA_CADUCIDAD, DIASCAD=@DIASCAD, CALCULA_DIAS_CAD=@CALCULA_DIAS_CAD, COPIAS=@COPIAS, OTROS=@OTROS WHERE CLAVE=@CLAVE";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CLAVE", FbDbType.VarChar).Value                  = AProducto.Clave;
            com.Parameters.Add("@DESCRIPCION", FbDbType.VarChar).Value            = AProducto.Descripcion;
            com.Parameters.Add("@CODIGOSAP", FbDbType.VarChar).Value              = AProducto.CodigoSap;
            com.Parameters.Add("@CODIGOPROVEDOR", FbDbType.VarChar).Value         = AProducto.CodigoProveedor;
            com.Parameters.Add("@MERCADO", FbDbType.VarChar).Value                = AProducto.Mercado;
            com.Parameters.Add("@CLASE", FbDbType.VarChar).Value                  = AProducto.Clase;
            com.Parameters.Add("@LINEA", FbDbType.VarChar).Value                  = AProducto.Linea;
            com.Parameters.Add("@GRUPO", FbDbType.SmallInt).Value                 = AProducto.Grupo;
            com.Parameters.Add("@OPERADOR", FbDbType.SmallInt).Value              = AProducto.Operador;
            com.Parameters.Add("@RENDIMIENTO", FbDbType.Float).Value              = AProducto.Rendimiento;
            com.Parameters.Add("@MOLDEADO", FbDbType.VarChar).Value               = AProducto.Moldeado;
            com.Parameters.Add("@INVENTARIABLE", FbDbType.VarChar).Value          = AProducto.Inventariable;
            com.Parameters.Add("@CAPTURA_PEDIMENTO", FbDbType.VarChar).Value      = AProducto.Captura_Pedimento;
            com.Parameters.Add("@CATEGORIA", FbDbType.Integer).Value              = AProducto.Categoria;
            com.Parameters.Add("@PESOFIJO", FbDbType.Float).Value                 = AProducto.Pesofijo;
            com.Parameters.Add("@PESOMINIMO", FbDbType.Float).Value               = AProducto.Pesominimo;
            com.Parameters.Add("@PESOMAXIMO", FbDbType.Float).Value               = AProducto.Pesomaximo;
            com.Parameters.Add("@PESOTARA", FbDbType.Float).Value                 = AProducto.Pesotara;
            com.Parameters.Add("@DECIMALES", FbDbType.SmallInt).Value             = AProducto.Decimales;
            com.Parameters.Add("@FORMATO_ETIQUETA", FbDbType.Integer).Value       = AProducto.Formato_Etiqueta;
            com.Parameters.Add("@TEMPERATURA", FbDbType.VarChar).Value            = AProducto.Temperatura;
            com.Parameters.Add("@CLAVE_ALM", FbDbType.VarChar).Value              = AProducto.Clave_Alm;
            com.Parameters.Add("@KILOS_POR_CAJA", FbDbType.Numeric).Value         = AProducto.Kilos_Por_Caja;
            com.Parameters.Add("@TIPO_FECHA_PRODUCCION", FbDbType.SmallInt).Value = AProducto.Tipo_Fecha_Produccion;
            com.Parameters.Add("@FECHA_PRODUCCION", FbDbType.TimeStamp).Value     = AProducto.Fecha_Produccion;
            com.Parameters.Add("@TIPO_FECHA_SACRIFICIO", FbDbType.SmallInt).Value = AProducto.Tipo_Fecha_Sacrificio;
            //com.Parameters.Add("@FECHA_SACRIFICIO", FbDbType.TimeStamp).Value     = AProducto.Fecha_Sacrificio;
            com.Parameters.Add("@TIPO_FECHA_CADUCIDAD", FbDbType.SmallInt).Value  = AProducto.Tipo_Fecha_Caducidad;
            com.Parameters.Add("@DIASCAD", FbDbType.Float).Value                  = AProducto.Diascad;
            com.Parameters.Add("@CALCULA_DIAS_CAD", FbDbType.VarChar).Value       = AProducto.Calcula_Dias_Cad;
            com.Parameters.Add("@COPIAS", FbDbType.SmallInt).Value                = AProducto.Copias;
            com.Parameters.Add("@OTROS", FbDbType.Integer).Value                  = AProducto.Otros;

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
        }
        //Regresa Producto Especifico
        public Producto ObtenerProducto(string AClave)
		{
			Producto pResult = null;
			string pSentencia = "SELECT * FROM DRASPROD WHERE CLAVE = @CLAVE";
			FbConnection con  = _Conexiones.ObtenerConexion();
			
			FbCommand com = new FbCommand(pSentencia, con);
			com.Parameters.Add("@CLAVE", FbDbType.VarChar).Value = AClave+"";
			
			try
			{
				con.Open();
				
				FbDataReader reader = com.ExecuteReader();

                if (reader.Read()) {
                    pResult                         = new Producto();
                    pResult.Clave                   = reader["CLAVE"] != DBNull.Value ? (string)reader["CLAVE"] : "";
                    pResult.Descripcion             = reader["DESCRIPCION"] != DBNull.Value ? (string)reader["DESCRIPCION"] : "";
                    pResult.Almacen                 = reader["ALMACEN"] != DBNull.Value ? (int)reader["ALMACEN"] : 0;
                    pResult.Clave_Alm               = reader["CLAVE_ALM"] != DBNull.Value ? (string)reader["CLAVE_ALM"] : "";
                    pResult.Mercado                 = reader["MERCADO"] != DBNull.Value ? (string)reader["MERCADO"] : "";
                    pResult.Codbarras               = reader["CODBARRAS"] != DBNull.Value ? (string)reader["CODBARRAS"] : "";
                    pResult.Pesofijo                = reader["PESOFIJO"] != DBNull.Value ? (float)reader["PESOFIJO"] : 0;
                    pResult.Clase                   = reader["CLASE"] != DBNull.Value ? (string)reader["CLASE"] : "";
                    pResult.Pesotara                = reader["PESOTARA"] != DBNull.Value ? (float)reader["PESOTARA"] : 0;
                    pResult.Diascad                 = reader["DIASCAD"] != DBNull.Value ? (float)reader["DIASCAD"] : 0;
                    pResult.Linea                   = reader["LINEA"] != DBNull.Value ? (string)reader["LINEA"] : "";
                    pResult.Pesomaximo              = reader["PESOMAXIMO"] != DBNull.Value ? (float)reader["PESOMAXIMO"] : 0;
                    pResult.Pesominimo              = reader["PESOMINIMO"] != DBNull.Value ? (float)reader["PESOMINIMO"] : 0;
                    pResult.Rendimiento             = reader["RENDIMIENTO"] != DBNull.Value ? (float)reader["RENDIMIENTO"] : 0;
                    pResult.Grupo                   = reader["GRUPO"] != DBNull.Value ? (Int16)reader["GRUPO"] : 0;
                    pResult.Operador                = reader["OPERADOR"] != DBNull.Value ? (Int16)reader["OPERADOR"] : 0;
                    pResult.Fondo                   = reader["FONDO"] != DBNull.Value ? (int)reader["FONDO"] : 0;
                    pResult.Tapa                    = reader["TAPA"] != DBNull.Value ? (int)reader["TAPA"] : 0;
                    pResult.Pliego                  = reader["PLIEGO"] != DBNull.Value ? (int)reader["PLIEGO"] : 0;
                    pResult.Panal                   = reader["PANAL"] != DBNull.Value ? (int)reader["PANAL"] : 0;
                    pResult.Separador               = reader["SEPARADOR"] != DBNull.Value ? (int)reader["SEPARADOR"] : 0;
                    pResult.Bolsa                   = reader["BOLSA"] != DBNull.Value ? (int)reader["BOLSA"] : 0;
                    pResult.Huleespuma              = reader["HULEESPUMA"] != DBNull.Value ? (int)reader["HULEESPUMA"] : 0;
                    pResult.Palillos                = reader["PALILLOS"] != DBNull.Value ? (int)reader["PALILLOS"] : 0;
                    pResult.Huleburbuja             = reader["HULEBURBUJA"] != DBNull.Value ? (int)reader["HULEBURBUJA"] : 0;
                    pResult.Otros                   = reader["OTROS"] != DBNull.Value ? (int)reader["OTROS"] : 0;
                    pResult.OtrosDos                = reader["OTROSDOS"] != DBNull.Value ? (int)reader["OTROSDOS"] : 0;
                    pResult.Fondo_D                 = reader["FONDO_D"] != DBNull.Value ? (string)reader["FONDO_D"] : "";
                    pResult.Tapa_D                  = reader["TAPA_D"] != DBNull.Value ? (string)reader["TAPA_D"] : "";
                    pResult.Pliego_D                = reader["PLIEGO_D"] != DBNull.Value ? (string)reader["PLIEGO_D"] : "";
                    pResult.Panal_D                 = reader["PANAL_D"] != DBNull.Value ? (string)reader["PANAL_D"] : "";
                    pResult.Separador_D             = reader["SEPARADOR_D"] != DBNull.Value ? (string)reader["SEPARADOR_D"] : "";
                    pResult.Bolsa_D                 = reader["BOLSA_D"] != DBNull.Value ? (string)reader["BOLSA_D"] : "";
                    pResult.Huleespuma_D            = reader["HULEESPUMA_D"] != DBNull.Value ? (string)reader["HULEESPUMA_D"] : "";
                    pResult.Palillos_D              = reader["PALILLOS_D"] != DBNull.Value ? (string)reader["PALILLOS_D"] : "";
                    pResult.Huleburbuja_D           = reader["HULEBURBUJA_D"] != DBNull.Value ? (string)reader["HULEBURBUJA_D"] : "";
                    pResult.Otros_D                 = reader["OTROS_D"] != DBNull.Value ? (string)reader["OTROS_D"] : "";
                    pResult.OtrosDos_D              = reader["OTROSDOS_D"] != DBNull.Value ? (string)reader["OTROSDOS_D"] : "";
                    pResult.Temperatura             = reader["TEMPERATURA"] != DBNull.Value ? (string)reader["TEMPERATURA"] : "";
                    pResult.Copias                  = reader["COPIAS"] != DBNull.Value ? (Int16)reader["COPIAS"] : 0;
                    pResult.AplicaTemperatura       = reader["APLICATEMPERATURA"] != DBNull.Value ? (string)reader["APLICATEMPERATURA"] : "";
                    pResult.CodigoProveedor         = reader["CODIGOPROVEDOR"] != DBNull.Value ? (string)reader["CODIGOPROVEDOR"] : "";
                    pResult.Camara_Default          = reader["CAMARA_DEFAULT"] != DBNull.Value ? (int)reader["CAMARA_DEFAULT"] : 0;
                    pResult.Posicion_Default        = reader["POSICION_DEFAULT"] != DBNull.Value ? (string)reader["POSICION_DEFAULT"] : "";
                    pResult.Usa_Camara_Default      = reader["USA_CAMARA_DEFAULT"] != DBNull.Value ? (string)reader["USA_CAMARA_DEFAULT"] : "";
                    pResult.Etiqueta                = reader["ETIQUETA"] != DBNull.Value ? (decimal)reader["ETIQUETA"] : 0;
                    pResult.Etiqueta_D              = reader["ETIQUETA_D"] != DBNull.Value ? (string)reader["ETIQUETA_D"] : "";
                    pResult.Inventariable           = reader["INVENTARIABLE"] != DBNull.Value ? (string)reader["INVENTARIABLE"] : "";
                    pResult.Formato_Etiqueta        = reader["FORMATO_ETIQUETA"] != DBNull.Value ? (int)reader["FORMATO_ETIQUETA"] : 0;
                    pResult.Calcula_Dias_Cad        = reader["CALCULA_DIAS_CAD"] != DBNull.Value ? (string)reader["CALCULA_DIAS_CAD"] : "";
                    pResult.Decimales               = reader["DECIMALES"] != DBNull.Value ? (Int16)reader["DECIMALES"] : 0;
                    pResult.CodigoSap               = reader["CODIGOSAP"] != DBNull.Value ? (string)reader["CODIGOSAP"] : "";
                    pResult.Tipo_Redondeo           = reader["TIPO_REDONDEO"] != DBNull.Value ? (int)reader["TIPO_REDONDEO"] : 0;
                    pResult.Dias_Sacrificio         = reader["DIAS_SACRIFICIO"] != DBNull.Value ? (int)reader["DIAS_SACRIFICIO"] : 0;
                    pResult.Moldeado                = reader["MOLDEADO"] != DBNull.Value ? (string)reader["MOLDEADO"] : "";
                    pResult.Fecha_Sacrificio        = reader["FECHA_SACRIFICIO"] != DBNull.Value ? Utilerias.dateTimeToString((DateTime)reader["FECHA_SACRIFICIO"]) : "";
                    pResult.Tipo_Fecha_Sacrificio   = reader["TIPO_FECHA_SACRIFICIO"] != DBNull.Value ? (Int16)reader["TIPO_FECHA_SACRIFICIO"] : 0;
                    pResult.Fecha_Produccion        = reader["FECHA_PRODUCCION"] != DBNull.Value ? (DateTime?)reader["FECHA_PRODUCCION"] : null;
                    pResult.Tipo_Fecha_Produccion   = reader["TIPO_FECHA_PRODUCCION"] != DBNull.Value ? (Int16)reader["TIPO_FECHA_PRODUCCION"] : 0;
                    pResult.Tipo_Fecha_Caducidad    = reader["TIPO_FECHA_CADUCIDAD"] != DBNull.Value ? (Int16)reader["TIPO_FECHA_CADUCIDAD"] : 0;
                    pResult.Fecha_Caducidad         = reader["FECHA_CADUCIDAD"] != DBNull.Value ? (DateTime?)reader["FECHA_CADUCIDAD"] : null;
                    pResult.Kilos_Por_Caja          = reader["KILOS_POR_CAJA"] != DBNull.Value ? (decimal)reader["KILOS_POR_CAJA"] : 0;
                    pResult.Captura_Pedimento       = reader["CAPTURA_PEDIMENTO"] != DBNull.Value ? (string)reader["CAPTURA_PEDIMENTO"] : "";
                    pResult.Categoria               = reader["CATEGORIA"] != DBNull.Value ? (int)reader["CATEGORIA"] : 0;
                    pResult.FechaHoraSistema        = reader["FECHAHORASISTEMA"] != DBNull.Value ? (DateTime?)reader["FECHAHORASISTEMA"] : null;
                    pResult.Usuario_Cambio          = reader["USUARIO_CAMBIO"] != DBNull.Value ? (string)reader["USUARIO_CAMBIO"] : "";
                    pResult.Fec_Cad_Manual          = reader["FEC_CAD_MANUAL"] != DBNull.Value ? (string)reader["FEC_CAD_MANUAL"] : "";
                    pResult.Decimales_Etiqueta      = reader["DECIMALES_ETIQUETA"] != DBNull.Value ? (int)reader["DECIMALES_ETIQUETA"] : 0;
                    pResult.Lectura_E1              = reader["LECTURA_E1"] != DBNull.Value ? (string)reader["LECTURA_E1"] : "";
                    pResult.Lectura_E2              = reader["LECTURA_E2"] != DBNull.Value ? (string)reader["LECTURA_E2"] : "";
                    pResult.Lectura_E3              = reader["LECTURA_E3"] != DBNull.Value ? (string)reader["LECTURA_E3"] : "";
                    pResult.Lectura_Sagarpa         = reader["LECTURA_SAGARPA"] != DBNull.Value ? (string)reader["LECTURA_SAGARPA"] : "";
                    
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


        //JUAN CARLOS
        public List<Producto> ObtenerProductosValidaciones()
        {
            List<Producto> pResult = new List<Producto>();
            Producto pProd = null;

            string pSentencia = "SELECT CLAVE, DESCRIPCION FROM DRASPROD WHERE CLAVE NOT LIKE '%-%' AND TRIM(CLAVE) <> '' ORDER BY CHAR_LENGTH(CLAVE), CLAVE";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pProd = new Producto();
                    pProd.Clave = (string)reader["CLAVE"];
                    pProd.Descripcion = (string)reader["DESCRIPCION"];

                    pResult.Add(pProd);
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
        public int obtenerInventarioProducto(DateTime AFechaInicio, DateTime AFechaFin, string AClave)
        {
            string pSentencia = "SELECT COUNT(*) AS CAJAS, COALESCE(SUM(PESO), 0) AS KILOS FROM DRASCORT" +
                       "WHERE CAST((SUBSTRING(CODIGOBARRAS FROM 11 FOR 2) || '/' || SUBSTRING(CODIGOBARRAS FROM 13 FOR 2)" +
                       " || '/' || SUBSTRING(CODIGOBARRAS FROM 7 FOR 4)) AS TIMESTAMP) >= @FECHAI AND" +
                       "CAST((SUBSTRING(CODIGOBARRAS FROM 11 FOR 2) || '/' || SUBSTRING(CODIGOBARRAS FROM 13 FOR 2)" +
                       " || '/' || SUBSTRING(CODIGOBARRAS FROM 7 FOR 4)) AS TIMESTAMP) <= @FECHAF AND PRODUCTO = @PRODUCTO AND EMBARCADO = 'No'";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHAI", FbDbType.TimeStamp).Value = AClave;
            com.Parameters.Add("@FECHAF", FbDbType.TimeStamp).Value = AClave;
            com.Parameters.Add("@PRODUCTO", FbDbType.VarChar).Value = AClave;

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

        public List<Producto> BuscarProductos(string AQuery)
        {
            List<Producto> pResult = new List<Producto>();

            string pSentencia = "SELECT CLAVE, DESCRIPCION FROM DRASPROD WHERE DESCRIPCION LIKE '%" + AQuery + "%'";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    Producto pProducto = new Producto();
                    pProducto.Clave = (string)reader["CLAVE"];
                    pProducto.Descripcion = (string)reader["DESCRIPCION"];

                    pResult.Add(pProducto);
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

        public Producto ObtenerFechaDeSacrificioDeProducto(string AClave)
        {
            Producto pResult = null;
            string pSentencia = "SELECT FECHA_SACRIFICIO FROM DRASPROD WHERE CLAVE = @CLAVE";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CLAVE", FbDbType.VarChar).Value = AClave;

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    pResult = new Producto();
                    
                    pResult.Fecha_Sacrificio = reader["FECHA_SACRIFICIO"] != DBNull.Value ? Utilerias.dateTimeToString((DateTime)reader["FECHA_SACRIFICIO"]) : "";

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

        public DateTime CambiarFechaSacrificio(Producto AProducto)
        {
            string pSentencia = "UPDATE DRASPROD SET FECHA_SACRIFICIO = @FECHASAC WHERE CLAVE = @CLAVE RETURNING FECHA_SACRIFICIO";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CLAVE", FbDbType.VarChar).Value = AProducto.Clave;
            com.Parameters.Add("@FECHASAC", FbDbType.TimeStamp).Value = AProducto.Fecha_Sacrificio;

            FbParameter pOutParameter = new FbParameter("@FECHA_SACRIFICIO", FbDbType.TimeStamp);
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
            return (DateTime)pOutParameter.Value;
        }

        public bool ValidarClave(int AClave)
        {
            string pSentencia = "SELECT CLAVE FROM DRASPROD WHERE CLAVE = @CLAVE";
            FbConnection con = _Conexiones.ObtenerConexion();
            FbCommand com = new FbCommand(pSentencia, con);
            Console.WriteLine("Clave="+AClave);
            com.Parameters.Add("@CLAVE", FbDbType.VarChar).Value = AClave;
            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine(reader.GetString(0));
                    return true;//Existe Producto
                }
                else
                {
                    return false;//No Existe Producto
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public List<Producto> ObtenerListaProductosNoInventariables()
        {
            List<Producto> listaProductos = new List<Producto>();
            Producto pResult = null;

            string pSentencia = "SELECT CLAVE, DESCRIPCION FROM DRASPROD WHERE CLAVE NOT CONTAINING('-') AND INVENTARIABLE = 'No' ORDER BY CLAVE";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pResult = new Producto();
                    pResult.Clave       = reader["CLAVE"] != DBNull.Value ? (string)reader["CLAVE"] : "";
                    pResult.Descripcion = reader["DESCRIPCION"] != DBNull.Value ? (string)reader["DESCRIPCION"] : "";
                    listaProductos.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return listaProductos;
        }

        public List<UsoEmpaque> ObtenerUsoEmpaque(string AFechaIni, string AFechaFin)
        {
            List<UsoEmpaque> pResult = new List<UsoEmpaque>();
            UsoEmpaque pUsoEmpaque = null;

            string pSentencia = "SELECT T0.REMPAQUE, T2.NOMBRE, SUM(T0.RCANTIDAD) AS CANTIDAD_PRODUCTO, SUM(T0.RCANTIDADEMPAQUE) AS CANTIDAD_EMPAQUE "+
                               "FROM EMPAQUE_CONSUMIDO_GENERAL(@FECHAINI, @FECHAFIN) T0 "+
                               "JOIN DRASEMPAQUE T2 ON T2.ID = T0.REMPAQUE "+
                               "GROUP BY T0.REMPAQUE, T2.NOMBRE";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHAINI", FbDbType.TimeStamp).Value = AFechaIni;
            com.Parameters.Add("@FECHAFIN", FbDbType.TimeStamp).Value = AFechaFin;

            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pUsoEmpaque                   = new UsoEmpaque();
                    pUsoEmpaque.Rempaque          = reader["REMPAQUE"] != DBNull.Value ? (int)reader["REMPAQUE"] : -1;
                    pUsoEmpaque.Nombre            = (reader["NOMBRE"] != DBNull.Value) ? (string)reader["NOMBRE"] : "";
                    pUsoEmpaque.Cantidad_Producto = (reader["CANTIDAD_PRODUCTO"] != DBNull.Value) ? (decimal)reader["CANTIDAD_PRODUCTO"] : 0;
                    pUsoEmpaque.Cantidad_Empaque  = (reader["CANTIDAD_EMPAQUE"] != DBNull.Value) ? (decimal)reader["CANTIDAD_EMPAQUE"] : 0;
                    pResult.Add(pUsoEmpaque);
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

        public List<DetalleUsoEmpaque> ObtenerDetalleUsoEmpaque(int ARempaque, string AFechaIni, string AFechaFin)
        {
            List<DetalleUsoEmpaque> pResult = new List<DetalleUsoEmpaque>();
            DetalleUsoEmpaque pUsoEmpaque = null;

            string pSentencia = "SELECT T0.RPRODUCTO, T1.DESCRIPCION, T0.REMPAQUE, T2.NOMBRE, T0.RCANTIDAD, T0.RCANTIDADEMPAQUE "+
                               "FROM EMPAQUE_CONSUMIDO_GENERAL(@FECHAINI, @FECHAFIN) T0 "+
                               "JOIN DRASPROD T1 ON T1.CLAVE = T0.RPRODUCTO "+
                               "JOIN DRASEMPAQUE T2 ON T2.ID = T0.REMPAQUE "+
                               "WHERE T0.REMPAQUE = @REMPAQUE ";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@REMPAQUE", FbDbType.Integer).Value = ARempaque;
            com.Parameters.Add("@FECHAINI", FbDbType.TimeStamp).Value = AFechaIni;
            com.Parameters.Add("@FECHAFIN", FbDbType.TimeStamp).Value = AFechaFin;

            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pUsoEmpaque                  = new DetalleUsoEmpaque();
                    pUsoEmpaque.RProducto        = reader["RPRODUCTO"] != DBNull.Value ? (string)reader["RPRODUCTO"] : "";
                    pUsoEmpaque.Descripcion      = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pUsoEmpaque.Rempaque         = reader["REMPAQUE"] != DBNull.Value ? (int)reader["REMPAQUE"] : -1;
                    pUsoEmpaque.Nombre           = (reader["NOMBRE"] != DBNull.Value) ? (string)reader["NOMBRE"] : "";
                    pUsoEmpaque.RCantidad        = (reader["RCANTIDAD"] != DBNull.Value) ? (decimal)reader["RCANTIDAD"] : 0;
                    pUsoEmpaque.RCantidadEmpaque = (reader["RCANTIDADEMPAQUE"] != DBNull.Value) ? (decimal)reader["RCANTIDADEMPAQUE"] : 0;
                    pResult.Add(pUsoEmpaque);
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

        public string DameDescripcionProducto(string AProducto)
        {
            string pResult = "";

            string pSentencia = "SELECT DESCRIPCION FROM DRASPROD WHERE CLAVE = @PRODUCTO";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@PRODUCTO", FbDbType.VarChar).Value = AProducto;

            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    pResult = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
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

        public List<Producto> DameListaProductos()
        {
            List<Producto> listaProductos = new List<Producto>();
            Producto pResult = null;

            string pSentencia = "SELECT CLAVE, DESCRIPCION FROM DRASPROD WHERE CLAVE NOT CONTAINING('-') ORDER BY CLAVE";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pResult             = new Producto();
                    pResult.Clave       = reader["CLAVE"] != DBNull.Value ? (string)reader["CLAVE"] : "";
                    pResult.Descripcion = reader["DESCRIPCION"] != DBNull.Value ? (string)reader["DESCRIPCION"] : "";
                    listaProductos.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return listaProductos;
        }

        public int CambiarTaraProducto(Producto AProducto, float ATara, string AUsuario)
        {
            int pAffected = 0;
            string pSentencia = "UPDATE DRASPROD SET PESOTARA = @PESOTARA WHERE CLAVE = @CLAVE";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@PESOTARA", FbDbType.Float).Value = ATara;
            com.Parameters.Add("@CLAVE", FbDbType.VarChar).Value  = AProducto.Clave;

            try
            {
                con.Open();
                pAffected = com.ExecuteNonQuery();
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return pAffected;
        }
    }
}