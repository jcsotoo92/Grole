using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Persistencia
{
    public class CanalesPersistencia
    {
        private Conexiones _Conexion;

        public CanalesPersistencia(Conexiones _Conexion)
        {
            this._Conexion = _Conexion;
        }

        public List<CanalProgramado> ObtenerListaCanalesProgramados(string AFecha)
        {
            List<CanalProgramado> pResult = new List<CanalProgramado>();
            CanalProgramado pCanalProgramado = null;

            string pSentencia = "SELECT A.FECHA, A.GRANJA AS CLAVE_GRANJA, B.NOMBRE AS GRANJA, A.LOTE, A.CANALES, A.ACUMULADOS FROM DRASCCALL A JOIN DRASGRAN B ON B.CLAVE = A.GRANJA WHERE FECHA = @FECHA";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value = AFecha;

            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pCanalProgramado              = new CanalProgramado();
                    pCanalProgramado.Fecha        = reader["FECHA"] != DBNull.Value ? (DateTime?)reader["FECHA"] : null;
                    pCanalProgramado.Clave_Granja = reader["CLAVE_GRANJA"] != DBNull.Value ? (int)reader["CLAVE_GRANJA"] : -1;
                    pCanalProgramado.Granja       = reader["GRANJA"] != DBNull.Value ? (string)reader["GRANJA"] : "";
                    pCanalProgramado.Lote         = reader["LOTE"] != DBNull.Value ? (int)reader["LOTE"] : -1;
                    pCanalProgramado.Canales      = reader["CANALES"] != DBNull.Value ? (Int16)reader["CANALES"] : (Int16)0;
                    pCanalProgramado.Acumulados   = reader["ACUMULADOS"] != DBNull.Value ? (Int16)reader["ACUMULADOS"] : (Int16)0;
                    pResult.Add(pCanalProgramado);
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

        public CanalProgramado InsertarLoteSacrificio(CCall ACanal)
        {

            string pSentencia = "INSERT INTO DRASCCALL (GRANJA, LOTE, FECHA, CANALES) VALUES (@GRANJA, @LOTE, @FECHA, @CANALES) RETURNING CLAVE";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@GRANJA", FbDbType.Integer).Value  = ACanal.Granja;
            com.Parameters.Add("@LOTE", FbDbType.VarChar).Value    = ACanal.Lote;
            com.Parameters.Add("@FECHA", FbDbType.Integer).Value   = ACanal.Fecha;
            com.Parameters.Add("@CANALES", FbDbType.Integer).Value = ACanal.Canales;

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
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return ObtenerCanalProgramado((int)pOutParameter.Value);
        }

        public CanalProgramado ObtenerCanalProgramado(int AClave)
        {
            CanalProgramado pCanalProgramado = null;
            string pSentencia = "SELECT A.FECHA, A.GRANJA AS CLAVE_GRANJA, B.NOMBRE AS GRANJA, A.LOTE, A.CANALES, A.ACUMULADOS FROM DRASCCALL A JOIN DRASGRAN B ON B.CLAVE = A.GRANJA WHERE A.CLAVE = @CLAVE";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CLAVE", FbDbType.Integer).Value = AClave;

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    pCanalProgramado = new CanalProgramado();
                    pCanalProgramado.Fecha        = reader["FECHA"] != DBNull.Value ? (DateTime?)reader["FECHA"] : null;
                    pCanalProgramado.Clave_Granja = reader["CLAVE_GRANJA"] != DBNull.Value ? (int)reader["CLAVE_GRANJA"] : -1;
                    pCanalProgramado.Granja       = reader["GRANJA"] != DBNull.Value ? (string)reader["GRANJA"] : "";
                    pCanalProgramado.Lote         = reader["LOTE"] != DBNull.Value ? (int)reader["LOTE"] : -1;
                    pCanalProgramado.Canales      = reader["CANALES"] != DBNull.Value ? (Int16)reader["CANALES"] : (Int16)0;
                    pCanalProgramado.Acumulados   = reader["ACUMULADOS"] != DBNull.Value ? (Int16)reader["ACUMULADOS"] : (Int16)0;
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return pCanalProgramado;
        }

        public Boolean CanalExiste(int AGranja, DateTime AFecha, int ALote, int ACanal)
        {
            string Fecha = AFecha.ToString("dd.MM.yyyy") + "";
            Boolean Existe = false;

            string pSentencia = "SELECT CANAL FROM DRASCCALD WHERE GRANJA = @GRANJA AND FECHA = @FECHA AND LOTE = @LOTE AND CANAL = @CANAL";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@GRANJA", FbDbType.Integer).Value  = AGranja;
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value = Fecha;
            com.Parameters.Add("@LOTE", FbDbType.Integer).Value    = ALote;
            com.Parameters.Add("@CANAL", FbDbType.Integer).Value   = ACanal;

            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    Existe = true;
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return Existe;
        }

        public Boolean DescortarCanal(int AGranja, DateTime AFecha, int ALote, int ACanal)
        {
            string Fecha = AFecha.ToString("dd.MM.yyyy") + "";
            string pSentencia = "UPDATE DRASCCALD SET PESOFRIO = NULL, FECHAFRIO = NULL WHERE GRANJA = @GRANJA AND FECHA = @FECHA AND LOTE = @LOTE AND CANAL = @CANAL";
            FbConnection con = _Conexion.ObtenerConexion();
            Boolean resultado = false;
            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@GRANJA", FbDbType.Integer).Value  = AGranja;
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value = Fecha;
            com.Parameters.Add("@LOTE", FbDbType.Integer).Value    = ALote;
            com.Parameters.Add("@CANAL", FbDbType.Integer).Value   = ACanal;

            try
            {
                con.Open();
                com.ExecuteNonQuery();
                resultado = true;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return resultado;
        }

        public List<LotesPie> ObtenerListaLotesEnPie(string AFechaIni, string AFechaFin, int AGranja)
        {

            string pSentencia = "SELECT * FROM DRASLPIE WHERE GRANJA = @GRANJA AND FECHA >= @FECHAINI AND FECHA <= @FECHAFIN";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@GRANJA", FbDbType.Integer).Value     = AGranja;
            com.Parameters.Add("@FECHAINI", FbDbType.TimeStamp).Value = AFechaIni;
            com.Parameters.Add("@FECHAFIN", FbDbType.TimeStamp).Value = AFechaFin;
            List<LotesPie> pResult = new List<LotesPie>();

            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    LotesPie pLotesPie  = new LotesPie();
                    pLotesPie.Fecha     = reader["FECHA"] != DBNull.Value ? Utilerias.dateTimeToString((DateTime)reader["FECHA"]) : "";
                    pLotesPie.Lote      = (reader["LOTE"] != DBNull.Value) ? (int)reader["LOTE"] : -1;
                    pLotesPie.Cantidad  = (reader["CANTIDAD"] != DBNull.Value) ? (int)reader["CANTIDAD"] : -1;
                    pLotesPie.Peso      = (reader["PESO"] != DBNull.Value) ? (decimal)reader["PESO"] : -1;
                    pLotesPie.Estatus   = (reader["ESTATUS"] != DBNull.Value) ? (string)reader["ESTATUS"] : "";
                    pLotesPie.Bajas     = (reader["BAJAS"] != DBNull.Value) ? (short)reader["BAJAS"] : -1;
                    pLotesPie.Peso      = (reader["PESO"] != DBNull.Value) ? (decimal)reader["PESO"] : -1;
                    pLotesPie.PesoBajas = (reader["PESOBAJA"] != DBNull.Value) ? (float)reader["PESOBAJA"] : -1;
                    pLotesPie.Indice    = (reader["INDICE"] != DBNull.Value) ? (int)reader["INDICE"] : -1;
                    pLotesPie.Id        = (reader["ID"] != DBNull.Value) ? (int)reader["ID"] : -1;
                    pResult.Add(pLotesPie);

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

        public LotesPie ObtenerLoteEnPie(string AFechaIni, string AFechaFin, int AGranja, int ALote)
        {
            string pSentencia = "SELECT * FROM DRASLPIE WHERE GRANJA = @GRANJA AND @LOTE=LOTE AND FECHA >= @FECHAINI AND FECHA <= @FECHAFIN";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@GRANJA", FbDbType.Integer).Value     = AGranja;
            com.Parameters.Add("@FECHAINI", FbDbType.TimeStamp).Value = AFechaIni;
            com.Parameters.Add("@FECHAFIN", FbDbType.TimeStamp).Value = AFechaFin;
            com.Parameters.Add("@LOTE", FbDbType.Integer).Value       = ALote;
            LotesPie pResult = new LotesPie();

            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    LotesPie pLotesPie = new LotesPie();
                    pLotesPie.Fecha      = reader["FECHA"] != DBNull.Value ? Utilerias.dateTimeToString((DateTime)reader["FECHA"]) : "";
                    pLotesPie.Lote       = (reader["LOTE"] != DBNull.Value) ? (int)reader["LOTE"] : -1;
                    pLotesPie.Cantidad   = (reader["CANTIDAD"] != DBNull.Value) ? (int)reader["CANTIDAD"] : -1;
                    pLotesPie.Peso       = (reader["PESO"] != DBNull.Value) ? (decimal)reader["PESO"] : -1;
                    pLotesPie.Estatus    = (reader["ESTATUS"] != DBNull.Value) ? (string)reader["ESTATUS"] : "";
                    pLotesPie.Bajas      = (reader["BAJAS"] != DBNull.Value) ? (short)reader["BAJAS"] : -1;
                    pLotesPie.Peso       = (reader["PESO"] != DBNull.Value) ? (decimal)reader["PESO"] : -1;
                    pLotesPie.PesoBajas  = (reader["PESOBAJA"] != DBNull.Value) ? (float)reader["PESOBAJA"] : -1;
                    pLotesPie.Indice     = (reader["INDICE"] != DBNull.Value) ? (int)reader["INDICE"] : -1;
                    pLotesPie.Id         = (reader["ID"] != DBNull.Value) ? (int)reader["ID"] : -1;
                    pLotesPie.MotivoBaja = (reader["MOTIVOBAJA"] != DBNull.Value) ? (string)reader["MOTIVOBAJA"] : "";
                    pResult              = pLotesPie;
                    Console.WriteLine("PesoBaja" + pLotesPie.PesoBajas);

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

        public LotesPie ObtenerLoteEnPieMod(string AFechaIni, string AFechaFin, int AGranja, int ALote)
        {

            string pSentencia = "SELECT * FROM DRASLPIE WHERE GRANJA = @GRANJA AND @LOTE=LOTE AND FECHA >= @FECHAINI AND FECHA <= @FECHAFIN";
            FbConnection con = _Conexion.ObtenerConexion();
            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@GRANJA", FbDbType.Integer).Value     = AGranja;
            com.Parameters.Add("@FECHAINI", FbDbType.TimeStamp).Value = AFechaIni;
            com.Parameters.Add("@FECHAFIN", FbDbType.TimeStamp).Value = AFechaFin;
            com.Parameters.Add("@LOTE", FbDbType.Integer).Value       = ALote;
            LotesPie pResult = new LotesPie();

            try
            {
                con.Open();
                FbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    LotesPie pLotesPie                = new LotesPie();
                    pLotesPie.Fecha                   = reader["FECHA"] != DBNull.Value ? Utilerias.dateTimeToString((DateTime)reader["FECHA"]) : "";
                    pLotesPie.Lote                    = (reader["LOTE"] != DBNull.Value) ? (int)reader["LOTE"] : -1;
                    pLotesPie.Cantidad                = (reader["CANTIDAD"] != DBNull.Value) ? (int)reader["CANTIDAD"] : -1;
                    pLotesPie.Peso                    = (reader["PESO"] != DBNull.Value) ? (decimal)reader["PESO"] : -1;
                    pLotesPie.Tipo                    = (reader["TIPO"] != DBNull.Value) ? (string)reader["TIPO"] : "";
                    pLotesPie.Estatus                 = (reader["ESTATUS"] != DBNull.Value) ? (string)reader["ESTATUS"] : "";
                    pLotesPie.Jaula                   = (reader["JAULA"] != DBNull.Value) ? (string)reader["JAULA"] : "";
                    pLotesPie.CerdosObservaciones     = (reader["CERDOS_OBSERVACION"] != DBNull.Value) ? (int)reader["CERDOS_OBSERVACION"] : -1;
                    pLotesPie.CerdosFaltantes         = (reader["CERDOS_FALTANTES"] != DBNull.Value) ? (int)reader["CERDOS_FALTANTES"] : -1;
                    pLotesPie.Vehiculo                = (reader["VEHICULO"] != DBNull.Value) ? (string)reader["VEHICULO"] : "";
                    pLotesPie.MuertosEnCorral         = (reader["MUERTOS_EN_CORRAL"] != DBNull.Value) ? (int)reader["MUERTOS_EN_CORRAL"] : -1;
                    pLotesPie.MuertosEnTrayecto       = (reader["MUERTOS_EN_TRAYECTO"] != DBNull.Value) ? (int)reader["MUERTOS_EN_TRAYECTO"] : -1;
                    pLotesPie.CostoBajas              = reader["COSTO_BAJAS"] != DBNull.Value ? (decimal)reader["COSTO_BAJAS"] : -1;
                    pLotesPie.Observaciones           = (reader["OBSERVACIONES"] != DBNull.Value) ? (string)reader["OBSERVACIONES"] : "";
                    pLotesPie.HoraRecepcion           = (reader["HORA_RECEPCION"] != DBNull.Value) ? (string)reader["HORA_RECEPCION"] : "";
                    pLotesPie.HoraLlegada             = (reader["HORA_LLEGADA"] != DBNull.Value) ? (string)reader["HORA_LLEGADA"] : "";
                    pLotesPie.InicioDescarga          = (reader["INICIO_DESCARGA"] != DBNull.Value) ? (string)reader["INICIO_DESCARGA"] : "";
                    pLotesPie.FinDescarga             = (reader["FIN_DESCARGA"] != DBNull.Value) ? (string)reader["FIN_DESCARGA"] : "";
                    pLotesPie.TiempoEstancia          = (reader["TIEMPO_ESTANCIA"] != DBNull.Value) ? (int)reader["TIEMPO_ESTANCIA"] : -1;
                    pLotesPie.HoraSalida              = (reader["HORA_SALIDA"] != DBNull.Value) ? (string)reader["HORA_SALIDA"] : "";
                    pLotesPie.TiempoRealDescarga      = (reader["TIEMPO_REAL_DESCARGA"] != DBNull.Value) ? (int)reader["TIEMPO_REAL_DESCARGA"] : -1;
                    pLotesPie.CanalesRetenidos        = (reader["CANALES_RETENIDOS"] != DBNull.Value) ? (int)reader["CANALES_RETENIDOS"] : -1;
                    pLotesPie.NumeroCorrales          = (reader["NUMERO_CORRALES"] != DBNull.Value) ? (string)reader["NUMERO_CORRALES"] : "";
                    pLotesPie.PesoPromedio            = reader["PESO_PROMEDIO"] != DBNull.Value ? (decimal)reader["PESO_PROMEDIO"] : -1;
                    pLotesPie.ObservacionesSacrificio = (reader["OBSERVACIONES_SACRIFICIO"] != DBNull.Value) ? (string)reader["OBSERVACIONES_SACRIFICIO"] : "";
                    pResult = pLotesPie;
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

        public Boolean EliminarLotePie(string AFecha, int AGranja, int ALote)
        {
            string pSentencia = "DELETE FROM DRASLPIE WHERE GRANJA = @GRANJA AND FECHA = @FECHA AND LOTE = @LOTE";
            FbConnection con = _Conexion.ObtenerConexion();
            Boolean resultado = false;
            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@GRANJA", FbDbType.Integer).Value  = AGranja;
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value = AFecha;
            com.Parameters.Add("@LOTE", FbDbType.Integer).Value    = ALote;

            try
            {
                con.Open();
                com.ExecuteNonQuery();
                resultado = true;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return resultado;
        }

        public bool InsertarLotePie(int AGranja, string AFecha, int ALote, int ACantidad, decimal AKilos, string ATipo, string AJaula, int ACerdosObservacion, int ACerdosFaltantes, string AVehiculo, int AMuertosEnCorral, int AMuertosEnTrayecto, decimal ACostoBajas, string AObservaciones, string AHoraRecepcion, string AHoraLlegada, string AInicioDescarga, string AFinDescarga, int ATiempoEstancia, string AHoraSalida, int ATiempoRealDescarga, int ACanalesRetenidos, string ANumeroCorrales, decimal APesoPromedio, string AObservacionesSacrificio)
        {
            string pSentencia = "INSERT INTO DRASLPIE (GRANJA, FECHA, LOTE, CANTIDAD, PESO, TIPO, ESTATUS, JAULA, CERDOS_OBSERVACION, CERDOS_FALTANTES, VEHICULO, MUERTOS_EN_CORRAL, MUERTOS_EN_TRAYECTO, COSTO_BAJAS, OBSERVACIONES, HORA_RECEPCION, HORA_LLEGADA, INICIO_DESCARGA, FIN_DESCARGA, TIEMPO_ESTANCIA, HORA_SALIDA, TIEMPO_REAL_DESCARGA, CANALES_RETENIDOS, NUMERO_CORRALES, PESO_PROMEDIO, OBSERVACIONES_SACRIFICIO) VALUES (@GRANJA, @FECHA, @LOTE, @CANTIDAD, @PESO, @TIPO, 'Pie', @JAULA, @CERDOS_OBSERVACION, @CERDOS_FALTANTES, @VEHICULO, @MUERTOS_EN_CORRAL, @MUERTOS_EN_TRAYECTO, @COSTO_BAJAS, @OBSERVACIONES, @HORA_RECEPCION, @HORA_LLEGADA, @INICIO_DESCARGA, @FIN_DESCARGA, @TIEMPO_ESTANCIA, @HORA_SALIDA, @TIEMPO_REAL_DESCARGA, @CANALES_RETENIDOS, @NUMERO_CORRALES, @PESO_PROMEDIO, @OBSERVACIONES_SACRIFICIO)";
            FbConnection con = _Conexion.ObtenerConexion();
            Boolean resultado = false;
            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@GRANJA", FbDbType.Integer).Value                   = AGranja;
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value                  = AFecha;
            com.Parameters.Add("@LOTE", FbDbType.Integer).Value                     = ALote;
            com.Parameters.Add("@CANTIDAD", FbDbType.Integer).Value                 = ACantidad;
            com.Parameters.Add("@PESO", FbDbType.Numeric).Value                     = AKilos;
            com.Parameters.Add("@TIPO", FbDbType.VarChar).Value                     = ATipo;
            com.Parameters.Add("@JAULA", FbDbType.VarChar).Value                    = AJaula;
            com.Parameters.Add("@CERDOS_OBSERVACION", FbDbType.Integer).Value       = ACerdosObservacion;
            com.Parameters.Add("@CERDOS_FALTANTES", FbDbType.Integer).Value         = ACerdosFaltantes;
            com.Parameters.Add("@VEHICULO", FbDbType.VarChar).Value                 = AVehiculo;
            com.Parameters.Add("@MUERTOS_EN_CORRAL", FbDbType.Integer).Value        = AMuertosEnCorral;
            com.Parameters.Add("@MUERTOS_EN_TRAYECTO", FbDbType.Integer).Value      = AMuertosEnTrayecto;
            com.Parameters.Add("@COSTO_BAJAS", FbDbType.Numeric).Value              = ACostoBajas;
            com.Parameters.Add("@OBSERVACIONES", FbDbType.VarChar).Value            = AObservaciones;
            com.Parameters.Add("@HORA_RECEPCION", FbDbType.VarChar).Value           = AHoraRecepcion;
            com.Parameters.Add("@HORA_LLEGADA", FbDbType.VarChar).Value             = AHoraLlegada;
            com.Parameters.Add("@INICIO_DESCARGA", FbDbType.VarChar).Value          = AInicioDescarga;
            com.Parameters.Add("@FIN_DESCARGA", FbDbType.VarChar).Value             = AFinDescarga;
            com.Parameters.Add("@TIEMPO_ESTANCIA", FbDbType.Integer).Value          = ATiempoEstancia;
            com.Parameters.Add("@HORA_SALIDA", FbDbType.VarChar).Value              = AHoraSalida;
            com.Parameters.Add("@TIEMPO_REAL_DESCARGA", FbDbType.Integer).Value     = ATiempoRealDescarga;
            com.Parameters.Add("@CANALES_RETENIDOS", FbDbType.Integer).Value        = ACanalesRetenidos;
            com.Parameters.Add("@NUMERO_CORRALES", FbDbType.VarChar).Value          = ANumeroCorrales;
            com.Parameters.Add("@PESO_PROMEDIO", FbDbType.VarChar).Value            = APesoPromedio;
            com.Parameters.Add("@OBSERVACIONES_SACRIFICIO", FbDbType.VarChar).Value = AObservacionesSacrificio;
            try
            {
                con.Open();
                com.ExecuteNonQuery();
                resultado = true;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return resultado;
        }

        public bool ModificarLotePie(int AGranja, string AFecha, int ALote, int ACantidad, decimal AKilos, string ATipo, string AJaula, int ACerdosObservacion, int ACerdosFaltantes, string AVehiculo, int AMuertosEnCorral, int AMuertosEnTrayecto, decimal ACostoBajas, string AObservaciones, string AHoraRecepcion, string AHoraLlegada, string AInicioDescarga, string AFinDescarga, int ATiempoEstancia, string AHoraSalida, int ATiempoRealDescarga, int ACanalesRetenidos, string ANumeroCorrales, decimal APesoPromedio, string AObservacionesSacrificio)
        {
            string pSentencia = "UPDATE DRASLPIE SET CANTIDAD = @CANTIDAD, PESO = @PESO, TIPO = @TIPO, JAULA = @JAULA, CERDOS_OBSERVACION = @CERDOS_OBSERVACION, CERDOS_FALTANTES = @CERDOS_FALTANTES, VEHICULO = @VEHICULO, MUERTOS_EN_CORRAL = @MUERTOS_EN_CORRAL, MUERTOS_EN_TRAYECTO = @MUERTOS_EN_TRAYECTO, COSTO_BAJAS = @COSTO_BAJAS, OBSERVACIONES = @OBSERVACIONES, HORA_RECEPCION = @HORA_RECEPCION, HORA_LLEGADA = @HORA_LLEGADA, INICIO_DESCARGA = @INICIO_DESCARGA, FIN_DESCARGA = @FIN_DESCARGA, TIEMPO_ESTANCIA = @TIEMPO_ESTANCIA, HORA_SALIDA = @HORA_SALIDA, TIEMPO_REAL_DESCARGA = @TIEMPO_REAL_DESCARGA, CANALES_RETENIDOS = @CANALES_RETENIDOS, NUMERO_CORRALES = @NUMERO_CORRALES, PESO_PROMEDIO = @PESO_PROMEDIO, OBSERVACIONES_SACRIFICIO = @OBSERVACIONES_SACRIFICIO WHERE GRANJA = @GRANJA AND FECHA = @FECHA AND LOTE = @LOTE";
            FbConnection con = _Conexion.ObtenerConexion();
            Boolean resultado = false;
            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@GRANJA", FbDbType.Integer).Value                   = AGranja;
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value                  = AFecha;
            com.Parameters.Add("@LOTE", FbDbType.Integer).Value                     = ALote;
            com.Parameters.Add("@CANTIDAD", FbDbType.Integer).Value                 = ACantidad;
            com.Parameters.Add("@PESO", FbDbType.Numeric).Value                     = AKilos;
            com.Parameters.Add("@TIPO", FbDbType.VarChar).Value                     = ATipo;
            com.Parameters.Add("@JAULA", FbDbType.VarChar).Value                    = AJaula;
            com.Parameters.Add("@CERDOS_OBSERVACION", FbDbType.Integer).Value       = ACerdosObservacion;
            com.Parameters.Add("@CERDOS_FALTANTES", FbDbType.Integer).Value         = ACerdosFaltantes;
            com.Parameters.Add("@VEHICULO", FbDbType.VarChar).Value                 = AVehiculo;
            com.Parameters.Add("@MUERTOS_EN_CORRAL", FbDbType.Integer).Value        = AMuertosEnCorral;
            com.Parameters.Add("@MUERTOS_EN_TRAYECTO", FbDbType.Integer).Value      = AMuertosEnTrayecto;
            com.Parameters.Add("@COSTO_BAJAS", FbDbType.Numeric).Value              = ACostoBajas;
            com.Parameters.Add("@OBSERVACIONES", FbDbType.VarChar).Value            = AObservaciones;
            com.Parameters.Add("@HORA_RECEPCION", FbDbType.VarChar).Value           = AHoraRecepcion;
            com.Parameters.Add("@HORA_LLEGADA", FbDbType.VarChar).Value             = AHoraLlegada;
            com.Parameters.Add("@INICIO_DESCARGA", FbDbType.VarChar).Value          = AInicioDescarga;
            com.Parameters.Add("@FIN_DESCARGA", FbDbType.VarChar).Value             = AFinDescarga;
            com.Parameters.Add("@TIEMPO_ESTANCIA", FbDbType.Integer).Value          = ATiempoEstancia;
            com.Parameters.Add("@HORA_SALIDA", FbDbType.VarChar).Value              = AHoraSalida;
            com.Parameters.Add("@TIEMPO_REAL_DESCARGA", FbDbType.Integer).Value     = ATiempoRealDescarga;
            com.Parameters.Add("@CANALES_RETENIDOS", FbDbType.Integer).Value        = ACanalesRetenidos;
            com.Parameters.Add("@NUMERO_CORRALES", FbDbType.VarChar).Value          = ANumeroCorrales;
            com.Parameters.Add("@PESO_PROMEDIO", FbDbType.VarChar).Value            = APesoPromedio;
            com.Parameters.Add("@OBSERVACIONES_SACRIFICIO", FbDbType.VarChar).Value = AObservacionesSacrificio;
            try
            {
                con.Open();
                com.ExecuteNonQuery();
                resultado = true;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return resultado;
        }

        public bool AgregaBajaLotePie(int AGranja, string AFecha, int ALote, short ABaja, float APesoBaja, string AMotivoBaja)
        {
            string pSentencia = "UPDATE DRASLPIE SET BAJAS = @BAJAS, PESOBAJA = @PESOBAJA, MOTIVOBAJA = @MOTIVOBAJA WHERE GRANJA = @GRANJA AND FECHA = @FECHA AND LOTE = @LOTE";
            FbConnection con = _Conexion.ObtenerConexion();
            Boolean resultado = false;
            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@BAJAS", FbDbType.SmallInt).Value    = ABaja;
            com.Parameters.Add("@PESOBAJA", FbDbType.Float).Value    = APesoBaja;
            com.Parameters.Add("MOTIVOBAJA", FbDbType.VarChar).Value = AMotivoBaja;
            com.Parameters.Add("@GRANJA", FbDbType.Integer).Value    = AGranja;
            com.Parameters.Add("@FECHA", FbDbType.TimeStamp).Value   = AFecha;
            com.Parameters.Add("@LOTE", FbDbType.Integer).Value      = ALote;
            try
            {
                con.Open();
                com.ExecuteNonQuery();
                resultado = true;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return resultado;
        }
    }
}
