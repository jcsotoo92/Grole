using grole.src.Entidades;
using grole.src.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Logica
{
    public class ConfiguracionesLogica
    {
        private EmpresasPersistencia _EmpresasPersistencia;

        public ConfiguracionesLogica(EmpresasPersistencia _EmpresasPersistencia)
        {
            this._EmpresasPersistencia = _EmpresasPersistencia;
        }

        public Empresa ObtenerConfiguraciones()
        {
            return _EmpresasPersistencia.ObtenerConfiguraciones();
        }

        public string CambiarConfiguraciones(Empresa AEmpresa)
        {
            return _EmpresasPersistencia.CambiarConfiguraciones(AEmpresa);
        }

    }
}
