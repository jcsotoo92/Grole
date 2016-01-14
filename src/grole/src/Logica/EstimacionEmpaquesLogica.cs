using grole.src.Entidades;
using grole.src.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Logica
{
    public class EstimacionEmpaquesLogica
    {
        private EstimacionEmpaquesDPersistencia _EstimacionEmpaquesDPersistencia;
        private EstimacionEmpaquesMPersistencia _EstimacionEmpaquesMPersistencia;

        public EstimacionEmpaquesLogica(EstimacionEmpaquesDPersistencia _EstimacionEmpaquesDPersistencia, EstimacionEmpaquesMPersistencia _EstimacionEmpaquesMPersistencia)
        {
            this._EstimacionEmpaquesDPersistencia = _EstimacionEmpaquesDPersistencia;
            this._EstimacionEmpaquesMPersistencia = _EstimacionEmpaquesMPersistencia;
        }

        public List<EstimacionEmpaqueM> ObtenerLista()
        {
            return _EstimacionEmpaquesMPersistencia.ObtenerLista();
        }
    }
}
