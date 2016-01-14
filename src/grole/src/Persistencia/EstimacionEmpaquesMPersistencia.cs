using FirebirdSql.Data.FirebirdClient;
using grole.src.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Persistencia
{
    public class EstimacionEmpaquesMPersistencia
    {
        private Conexiones _Conexiones;

        public EstimacionEmpaquesMPersistencia(Conexiones _Conexiones)
        {
            this._Conexiones = _Conexiones;
        }

        public List<EstimacionEmpaqueM> ObtenerLista()
        {
            List<EstimacionEmpaqueM> pResult = new List<EstimacionEmpaqueM>();
            EstimacionEmpaqueM estimacionEmpM;
            string pSentencia = "SELECT * FROM DRASESTIEMPAQUEM";
            FbConnection con = _Conexiones.ObtenerConexion();

            FbCommand com = new FbCommand(pSentencia, con);
            try
            {
                con.Open();

                FbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    estimacionEmpM                  = new EstimacionEmpaqueM();
                    estimacionEmpM.Id               = (int)reader["ID"];
                    estimacionEmpM.Descripcion      = reader["DESCRIPCION"] != DBNull.Value ? (string)reader["DESCRIPCION"] : "";
                    estimacionEmpM.FechaHoraSistema = reader["FECHAHORASISTEMA"] != DBNull.Value ? (DateTime?)reader["FECHAHORASISTEMA"] : null;
                    pResult.Add(estimacionEmpM);
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
