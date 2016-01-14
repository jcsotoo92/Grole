using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Persistencia
{
    public class BasculasPersistencia
    {
        private Conexiones _Conexiones;

        public BasculasPersistencia(Conexiones _Conexiones)
        {
            this._Conexiones = _Conexiones;
        }

        public List<Bascula> ObtenerBasculasActivas()
        {
            List<Bascula> pResult = new List<Bascula>();
            string pSentencia = "SELECT * FROM DRASBASC WHERE ACTIVO = 1";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    Bascula pBascula      = new Bascula();
                    pBascula.Clave        = reader["CLAVE"] != DBNull.Value ? (int)reader["CLAVE"] : -2;
                    pBascula.Puerto       = reader["PUERTO"] !=DBNull.Value ? (int)reader["PUERTO"] : -1 ;
                    pBascula.Baud         = reader["BAUD"] !=DBNull.Value ? (int)reader["BAUD"] : -1 ;
                    pBascula.DataBits     = reader["DATABITS"] !=DBNull.Value ? (int)reader["DATABITS"] : -1 ;
                    pBascula.Paridad      = reader["PARIDAD"] !=DBNull.Value ? (int)reader["PARIDAD"] : -1 ;
                    pBascula.StopBits     = reader["STOPBITS"] !=DBNull.Value ? (int)reader["STOPBITS"] : -1 ;
                    pBascula.Descripcion  = reader["DESCRIPCION"] !=DBNull.Value ? (string)reader["DESCRIPCION"] : "" ;
                    pBascula.Tipo         = reader["TIPO"] !=DBNull.Value ? (int)reader["TIPO"] : -1 ;
                    pBascula.TabSheet     = (Int16)reader["TABSHEET"];
                    pBascula.Grupo        = (Int16)reader["GRUPO"];
                    pBascula.Activo       = (Int16)reader["ACTIVO"];
                    pBascula.Script       = reader["SCRIPT"] !=DBNull.Value ? (string)reader["SCRIPT"] : null ;
                    pBascula.PesoTara     = (Int16)reader["PESOTARA"];
                    pBascula.FecCaducidad = reader["FECCADUCIDAD"] !=DBNull.Value ? (DateTime?)reader["FECCADUCIDAD"] : null ;
                    pBascula.TipoBascula  = reader["TIPOBASCULA"] !=DBNull.Value ? (string)reader["TIPOBASCULA"] : "" ;
                    pBascula.PesoMinimo   = reader["PESOMINIMO"] !=DBNull.Value ? (float)reader["PESOMINIMO"] : 0 ;
                    pBascula.PesoMaximo   = reader["PESOMAXIMO"] !=DBNull.Value ? (float)reader["PESOMAXIMO"] : 0 ;
                    pBascula.PesoFijo     = reader["PESOFIJO"] !=DBNull.Value ? (float)reader["PESOFIJO"] : 0 ;
                    pResult.Add(pBascula);

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
