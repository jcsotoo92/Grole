using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Persistencia
{
    public class EmpresasPersistencia
    {
        private Conexiones _Conexion;

        public EmpresasPersistencia(Conexiones _Conexion)
        {
            this._Conexion = _Conexion;
        }

        public Empresa ObtenerConfiguraciones()
        {
            Empresa pResult = null;
            string pSentencia = "SELECT CAMBIAR_ORDEN_CAJAS_EMBARCADAS, RESTRINGIR_FACTURACION FROM DGENEMPR";
            FbConnection con = _Conexion.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);

            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    pResult = new Empresa();
                    pResult.CambiarOrdenCajasEmbarcadas = (reader["CAMBIAR_ORDEN_CAJAS_EMBARCADAS"]) != DBNull.Value ? (string)reader["CAMBIAR_ORDEN_CAJAS_EMBARCADAS"] : "";
                    pResult.RestringirFacturacion = (reader["RESTRINGIR_FACTURACION"]) != DBNull.Value ? (string)reader["RESTRINGIR_FACTURACION"] : "";
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

        public string CambiarConfiguraciones(Empresa AEmpresa)
        {
            string pSentencia = "UPDATE DGENEMPR SET CAMBIAR_ORDEN_CAJAS_EMBARCADAS = @CAMBIARORDENCAJASEMBARCADAS, RESTRINGIR_FACTURACION = @RESTRINGIRFACTURACION";
            FbConnection con = _Conexion.ObtenerConexion();


            FbCommand com = new FbCommand(pSentencia, con);
            com.Parameters.Add("@CAMBIARORDENCAJASEMBARCADAS", FbDbType.VarChar).Value = AEmpresa.CambiarOrdenCajasEmbarcadas;
            com.Parameters.Add("@RESTRINGIRFACTURACION", FbDbType.VarChar).Value = AEmpresa.RestringirFacturacion;

            try
            {
                con.Open();
                com.ExecuteNonQuery();
            }catch(Exception e)
            {
                return e.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return "";
        }
    }
}
