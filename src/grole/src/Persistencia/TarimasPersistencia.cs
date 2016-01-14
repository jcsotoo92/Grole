using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Persistencia
{
    public class TarimasPersistencia
    {
        Conexiones _Conexiones;
        public TarimasPersistencia(Conexiones _Conexiones)
        {
            this._Conexiones = _Conexiones;
        }

        public List<Tarima> ObtenerTarimasCamara(int ACamara)
        {
            List<Tarima> listaTarimas = new List<Tarima>();
            Tarima pResult = null;

            string pSentencia = "SELECT FOLIO, COALESCE(FECHAHORASISTEMA, FECHA) AS FECHA, COALESCE(CAJAS, 0) AS CAJAS, COALESCE(KILOS, 0) AS KILOS, COALESCE(LOTE, 0) AS LOTE, UBICACION, CONTENEDOR "+
                               "FROM DRASTARM WHERE CONTENEDOR IN(" + ACamara + ") AND ESTATUS = 'C' "+
                               "AND CAJAS > 0 "+
                               "UNION ALL "+
                               "SELECT 0 AS FOLIO, CURRENT_TIMESTAMP AS FECHA, COALESCE(COUNT(*), 0) AS CAJAS, COALESCE(SUM(PESO), 0) AS KILOS, "+
                               "0 AS LOTE, 'A0101' AS UBICACION, CAMARA  FROM DRASCORT WHERE CAMARA IN(" + ACamara + ") AND(TARIMA IS NULL OR TARIMA = 0) AND EMBARCADO = 'No' GROUP BY CAMARA";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pResult            = new Tarima();
                    pResult.Folio      = reader["FOLIO"] != DBNull.Value ? (int)reader["FOLIO"] : -1;
                    pResult.Fecha      = (DateTime)reader["FECHA"];
                    pResult.Cajas      = reader["CAJAS"] != DBNull.Value ? (int)reader["CAJAS"] : 0;
                    pResult.Kilos      = reader["KILOS"] != DBNull.Value ? (float)reader["KILOS"] : 0;
                    pResult.Lote       = reader["LOTE"] != DBNull.Value ? (Int16)reader["LOTE"] : 0;
                    pResult.Ubicacion  = reader["UBICACION"] != DBNull.Value ? (string)reader["UBICACION"] : "";
                    pResult.Contenedor = reader["CONTENEDOR"] != DBNull.Value ? (int)reader["CONTENEDOR"] : 0;
                    listaTarimas.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return listaTarimas;
        }

        public List<CajaTarima> ObtenerCajasDeTarima(int AFolioTarima)
        {
            List<CajaTarima> listaTarimas = new List<CajaTarima>();
            CajaTarima pResult = null;

            string pSentencia = "SELECT C.FECHA, C.FOLIO, C.CODIGOBARRAS, C.PRODUCTO, P.DESCRIPCION, C.PESO, C.TARA, C.UBICACION FROM DRASTARD D " +
                      "JOIN DRASCORT C  ON C.CODIGOBARRAS = D.CODIGO " +
                      "JOIN DRASPROD P ON P.CLAVE = C.PRODUCTO  " +
                      "WHERE D.FOLIO = @FOLIO";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FOLIO", FbDbType.Integer).Value = AFolioTarima;

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pResult              = new CajaTarima();
                    pResult.Fecha        = (DateTime)reader["FECHA"];
                    pResult.Folio        = reader["FOLIO"] != DBNull.Value ? (int)reader["FOLIO"] : -1;
                    pResult.CodigoBarras = reader["CODIGOBARRAS"] != DBNull.Value ? (string)reader["CODIGOBARRAS"] : "";
                    pResult.Producto     = reader["PRODUCTO"] != DBNull.Value ? (string)reader["PRODUCTO"] : "";
                    pResult.Descripcion  = reader["DESCRIPCION"] != DBNull.Value ? (string)reader["DESCRIPCION"] : "";
                    pResult.Peso         = reader["PESO"] != DBNull.Value ? (decimal)reader["PESO"] : 0;
                    pResult.Tara         = reader["TARA"] != DBNull.Value ? (decimal)reader["TARA"] : 0;
                    pResult.Ubicacion    = reader["UBICACION"] != DBNull.Value ? (string)reader["UBICACION"] : "";
                    
                    listaTarimas.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return listaTarimas;
        }

        public List<CajaTarima> ObtenerCajasSinTarima(int ACamara)
        {
            List<CajaTarima> listaTarimas = new List<CajaTarima>();
            CajaTarima pResult = null;

            string pSentencia = "SELECT C.FECHA, C.FOLIO, C.CODIGOBARRAS, C.PRODUCTO, P.DESCRIPCION, C.PESO, C.TARA, C.UBICACION FROM DRASCORT C "+
                               "JOIN DRASPROD P ON P.CLAVE = C.PRODUCTO "+
                               "WHERE CAMARA = @CAMARA AND(TARIMA IS NULL OR TARIMA = 0) AND EMBARCADO = 'No'";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CAMARA", FbDbType.Integer).Value = ACamara;

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pResult              = new CajaTarima();
                    pResult.Fecha        = (DateTime)reader["FECHA"];
                    pResult.Folio        = reader["FOLIO"] != DBNull.Value ? (int)reader["FOLIO"] : -1;
                    pResult.CodigoBarras = reader["CODIGOBARRAS"] != DBNull.Value ? (string)reader["CODIGOBARRAS"] : "";
                    pResult.Producto     = reader["PRODUCTO"] != DBNull.Value ? (string)reader["PRODUCTO"] : "";
                    pResult.Descripcion  = reader["DESCRIPCION"] != DBNull.Value ? (string)reader["DESCRIPCION"] : "";
                    pResult.Peso         = reader["PESO"] != DBNull.Value ? (decimal)reader["PESO"] : 0;
                    pResult.Tara         = reader["TARA"] != DBNull.Value ? (decimal)reader["TARA"] : 0;
                    pResult.Ubicacion    = reader["UBICACION"] != DBNull.Value ? (string)reader["UBICACION"] : "";

                    listaTarimas.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return listaTarimas;
        }

        public List<Tarima> ObtenerTarimasLote(string AFechaIni, string AFechaFin, int ALoteIni, int ALoteFin)
        {
            List<Tarima> listaTarimas = new List<Tarima>();
            Tarima pResult = null;

            string pSentencia = "SELECT FOLIO, FECHA, CAJAS, KILOS, COALESCE(LOTE, 0) AS LOTE, CONTENEDOR, UBICACION "+
                                "FROM DRASTARM WHERE FECHA >= @FECHAINI AND FECHA <= @FECHAFIN AND LOTE >= @LOTEINI AND LOTE <= @LOTEFIN AND ESTATUS = 'C'";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHAINI", FbDbType.TimeStamp).Value = AFechaIni;
            com.Parameters.Add("@FECHAFIN", FbDbType.TimeStamp).Value = AFechaFin;
            com.Parameters.Add("@LOTEINI", FbDbType.SmallInt).Value   = ALoteIni;
            com.Parameters.Add("@LOTEFIN", FbDbType.SmallInt).Value   = ALoteFin;

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pResult = new Tarima();
                    pResult.Folio      = reader["FOLIO"] != DBNull.Value ? (int)reader["FOLIO"] : -1;
                    pResult.Fecha      = (DateTime)reader["FECHA"];
                    pResult.Cajas      = reader["CAJAS"] != DBNull.Value ? (int)reader["CAJAS"] : 0;
                    pResult.Kilos      = reader["KILOS"] != DBNull.Value ? (float)reader["KILOS"] : 0;
                    pResult.Lote       = reader["LOTE"] != DBNull.Value ? (Int16)reader["LOTE"] : 0;
                    pResult.Ubicacion  = reader["UBICACION"] != DBNull.Value ? (string)reader["UBICACION"] : "";
                    pResult.Contenedor = reader["CONTENEDOR"] != DBNull.Value ? (int)reader["CONTENEDOR"] : 0;
                    listaTarimas.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return listaTarimas;
        }

        public Tarima ObtenerTarima(int AFolio)
        {
            Tarima pResult = null;

            string pSentencia = "SELECT T0.FOLIO, T0.FECHA, T0.CAJAS, T0.KILOS, T0.IP, T0.ESTATUS, T0.LOTE, T0.CONTENEDOR, T1.DESCRIPCION AS CAMARA, " +
                                "T0.UBICACION, T0.FECHAENTRADA, T0.USUARIO, T0.ID_SALIDA, T0.FECHAHORASISTEMA FROM DRASTARM T0 " +
                                "JOIN DRASCAM T1 ON T0.CONTENEDOR = T1.ID " +
                                "WHERE T0.FOLIO = @FOLIO";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FOLIO", FbDbType.Integer).Value = AFolio;

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pResult                       = new Tarima();
                    pResult.Folio                 = reader["FOLIO"] != DBNull.Value ? (int)reader["FOLIO"] : -1;
                    pResult.Fecha                 = (DateTime)reader["FECHA"];
                    pResult.Cajas                 = reader["CAJAS"] != DBNull.Value ? (int)reader["CAJAS"] : 0;
                    pResult.Kilos                 = reader["KILOS"] != DBNull.Value ? (float)reader["KILOS"] : 0;
                    pResult.Ip                    = reader["IP"] != DBNull.Value ? (string)reader["IP"] : "";
                    pResult.Estatus               = reader["ESTATUS"] != DBNull.Value ? (string)reader["ESTATUS"] : "";
                    pResult.Lote                  = reader["LOTE"] != DBNull.Value ? (Int16)reader["LOTE"] : 0;
                    pResult.Ubicacion             = reader["UBICACION"] != DBNull.Value ? (string)reader["UBICACION"] : "";
                    pResult.Contenedor            = reader["CONTENEDOR"] != DBNull.Value ? (int)reader["CONTENEDOR"] : 0;
                    pResult.ContenedorDescripcion = reader["CAMARA"] != DBNull.Value ? (string)reader["CAMARA"] : "";
                    pResult.FechaEntrada          = (DateTime)reader["FECHAENTRADA"];
                    pResult.Usuario               = reader["USUARIO"] != DBNull.Value ? (string)reader["USUARIO"] : "";
                    pResult.Id_Salida             = reader["ID_SALIDA"] != DBNull.Value ? (int)reader["ID_SALIDA"] : -1;
                    pResult.FechaHoraSistema      = (DateTime)reader["FECHAHORASISTEMA"];
                    
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

        public bool SetEntradaTarimaContenedor(Tarima AUbicacion, int ACamara, int AFolioTarima, string AUbicacionDestino, DateTime AFecha, string AUsuario, string AIp)
        {
            bool pResult = false;
            int pAffected = 0;
            string pSentencia = "UPDATE DRASTARM SET CONTENEDOR = @CONTENEDOR, UBICACION = @UBICACION, FECHAENTRADA = @FECHA, USUARIO = @USUARIO WHERE FOLIO = @FOLIO";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CONTENEDOR", FbDbType.Integer).Value = ACamara;
            com.Parameters.Add("@UBICACION", FbDbType.VarChar).Value  = AUbicacionDestino;
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value    = AFecha;
            com.Parameters.Add("@USUARIO", FbDbType.VarChar).Value    = AUsuario;
            com.Parameters.Add("@FOLIO", FbDbType.Integer).Value      = AFolioTarima;
            
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
                    pResult = pAffected > 0;
                    if (pResult)
                    {
                        InsertaRastroTraspasos(AFolioTarima, AUbicacion.Ubicacion, AUbicacionDestino, AIp, AUsuario, AUbicacion.Contenedor, ACamara);
                    }

                }
            }

            return pResult;
        }

        private bool InsertaRastroTraspasos(int AFolioTarima, string AUbicacionFuente, string AUbicacionDestino, string AIp, string AUsuario, int AContenedorOrigen, int AContenedorDestino)
        {
            int pAffected = 0;
            string pSentencia = "INSERT INTO DRASTRACINGTRAS (FOLIO_TARIMA, UBICACION_FUENTE, UBICACION_DESTINO, IP, USUARIO, CONTENEDOR_ORIGEN, CONTENEDOR_DESTINO) " +
                                "VALUES (@FOLIOTARIMA, @UBICACION_FUENTE, @UBICACION_DESTINO, @IP, @USUARIO, @CONTENEDOR_ORIGEN, @CONTENEDOR_DESTINO)";
            FbConnection con = _Conexiones.ObtenerConexion();


            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FOLIOTARIMA", FbDbType.Integer).Value        = AFolioTarima;
            com.Parameters.Add("@UBICACION_FUENTE", FbDbType.VarChar).Value   = AUbicacionFuente;
            com.Parameters.Add("@UBICACION_DESTINO", FbDbType.VarChar).Value  = AUbicacionDestino;
            com.Parameters.Add("@IP", FbDbType.VarChar).Value                 = AIp;
            com.Parameters.Add("@USUARIO", FbDbType.VarChar).Value            = AUsuario;
            com.Parameters.Add("@CONTENEDOR_ORIGEN", FbDbType.Integer).Value  = AContenedorOrigen;
            com.Parameters.Add("@CONTENEDOR_DESTINO", FbDbType.Integer).Value = AContenedorDestino;
            

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
            return pAffected > 0;
        }

        public List<Salida> ObtenerDatosSalidaTarima(int AFolio)
        {
            List<Salida> pListaSalidas = new List<Salida>();
            Salida pResult = null;

            string pSentencia = "SELECT * FROM DRASSALIDAS WHERE ID_TARIMA = @FOLIOTARIMA";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FOLIOTARIMA", FbDbType.Integer).Value = AFolio;

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    pResult                  = new Salida();
                    pResult.Id               = reader["ID"] != DBNull.Value ? (int)reader["ID"] : -1;
                    pResult.Fecha            = (DateTime)reader["FECHA"];
                    pResult.Producto         = reader["PRODUCTO"] != DBNull.Value ? (string)reader["PRODUCTO"] : "";
                    pResult.Cajas            = reader["CAJAS"] != DBNull.Value ? (int)reader["CAJAS"] : 0;
                    pResult.Kilos            = reader["KILOS"] != DBNull.Value ? (decimal)reader["KILOS"] : 0;
                    pResult.Concepto         = reader["CONCEPTO"] != DBNull.Value ? (string)reader["CONCEPTO"] : "";
                    pResult.Id_Salida        = reader["ID_SALIDA"] != DBNull.Value ? (int)reader["ID_SALIDA"] : -1;
                    pResult.FechaHoraSistema = (DateTime)reader["FECHAHORASISTEMA"];
                    pResult.Id_Salida        = reader["ID_TARIMA"] != DBNull.Value ? (int)reader["ID_TARIMA"] : -1;
                    pListaSalidas.Add(pResult);
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return pListaSalidas;
        }

        public void GuardarLogTarimaRegresada(int AFolioTarima, string AMotivo, string AUsuario)
        {
            Tarima pTarima = ObtenerTarima(AFolioTarima);

            string pSentencia = "INSERT INTO LOG_TARIMAS_REGRESADAS (ID_TARIMA, CAJAS_TARIMA, KILOS_TARIMA, USUARIO, MOTIVO) VALUES (@FOLIOTARIMA, @CAJAS, @KILOS, @USUARIO, @MOTIVO)";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FOLIOTARIMA", FbDbType.Integer).Value = AFolioTarima;
            com.Parameters.Add("@CAJAS", FbDbType.Integer).Value       = pTarima.Cajas;
            com.Parameters.Add("@KILOS", FbDbType.Numeric).Value       = pTarima.Kilos;
            com.Parameters.Add("@USUARIO", FbDbType.VarChar).Value     = AUsuario;
            com.Parameters.Add("@MOTIVO", FbDbType.VarChar).Value      = AMotivo;
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

        public int RegresarTarima(int AFolioTarima, string AMotivo, string AUsuario)
        {
            int pAffected = 0;
            string pSentencia1 = "DELETE FROM DRASSALIDAS WHERE ID_TARIMA = @FOLIOTARIMA";
            string pSentencia2 = "DELETE FROM SALIDASD WHERE ID_TARIMA = @FOLIOTARIMA";
            string pSentencia3 = "UPDATE DRASTARM SET ESTATUS = 'C' WHERE FOLIO = @FOLIOTARIMA";
            string pSentencia4 = "UPDATE DRASCORT SET ID_SALIDA = NULL, SALIDA_APLICADA = NULL WHERE TARIMA = @FOLIOTARIMA";
            string pSentencia5 = "UPDATE DRASCORT SET EMBARCADO = 'No' WHERE TARIMA = @FOLIOTARIMA";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com1 = new FbCommand(pSentencia1, con);
            FbCommand com2 = new FbCommand(pSentencia2, con);
            FbCommand com3 = new FbCommand(pSentencia3, con);
            FbCommand com4 = new FbCommand(pSentencia4, con);
            FbCommand com5 = new FbCommand(pSentencia5, con);

            com1.Parameters.Add("@FOLIOTARIMA", FbDbType.Integer).Value = AFolioTarima;
            com2.Parameters.Add("@FOLIOTARIMA", FbDbType.Integer).Value = AFolioTarima;
            com3.Parameters.Add("@FOLIOTARIMA", FbDbType.Integer).Value = AFolioTarima;
            com4.Parameters.Add("@FOLIOTARIMA", FbDbType.Integer).Value = AFolioTarima;
            com5.Parameters.Add("@FOLIOTARIMA", FbDbType.Integer).Value = AFolioTarima;
            try
            {
                con.Open();

                pAffected += com1.ExecuteNonQuery();
                pAffected += com2.ExecuteNonQuery();
                pAffected += com3.ExecuteNonQuery();
                pAffected += com4.ExecuteNonQuery();
                pAffected += com5.ExecuteNonQuery();

                if(pAffected > 0)
                {
                    GuardarLogTarimaRegresada(AFolioTarima, AMotivo, AUsuario);
                }
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

        public List<CajaDeSalida> ObtenerCajasDeSalida(int AIdSalida)
        {
            List<CajaDeSalida> pResult = new List<CajaDeSalida>();
            CajaDeSalida pCajaDeSalida = null;

            string pSentencia = "SELECT C.FECHA, C.FOLIO, C.CODIGOBARRAS, C.PRODUCTO, P.DESCRIPCION, C.PESO, C.TARA, C.UBICACION, C.ID_SALIDA  FROM DRASTARD D " +
                      "JOIN DRASCORT C  ON C.CODIGOBARRAS = D.CODIGO " +
                      "JOIN DRASPROD P ON P.CLAVE = C.PRODUCTO  " +
                      "WHERE C.ID_SALIDA = @IDSALIDA";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@IDSALIDA", FbDbType.Integer).Value = AIdSalida;

            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pCajaDeSalida              = new CajaDeSalida();
                    pCajaDeSalida.Fecha        = (reader["FECHA"] != DBNull.Value) ? (DateTime?)reader["FECHA"] : null;
                    pCajaDeSalida.Folio        = (reader["FOLIO"] != DBNull.Value) ? (int)reader["FOLIO"] : -1;
                    pCajaDeSalida.CodigoBarras = (reader["CODIGOBARRAS"] != DBNull.Value) ? (string)reader["CODIGOBARRAS"] : "";
                    pCajaDeSalida.Producto     = (reader["PRODUCTO"] != DBNull.Value) ? (string)reader["PRODUCTO"] : "";
                    pCajaDeSalida.Descripcion  = (reader["DESCRIPCION"] != DBNull.Value) ? (string)reader["DESCRIPCION"] : "";
                    pCajaDeSalida.Peso         = (reader["PESO"] != DBNull.Value) ? (decimal)reader["PESO"] : 0;
                    pCajaDeSalida.Tara         = (reader["TARA"] != DBNull.Value) ? (decimal)reader["TARA"] : 0;
                    pCajaDeSalida.Ubicacion    = (reader["UBICACION"] != DBNull.Value) ? (string)reader["UBICACION"] : "";
                    pCajaDeSalida.Id_Salida    = (reader["ID_SALIDA"] != DBNull.Value) ? (int)reader["ID_SALIDA"] : -1;
                    pResult.Add(pCajaDeSalida);
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
