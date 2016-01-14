using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class Empresa
    {
        public int Clave { get; set; }
        public string RazonSocial { get; set; }
        public string Revision { get; set; }
        public DateTime FechaRev { get; set; }
        public string Tipo { get; set; }
        public int SerieKey { get; set; }
        public string TipoLicencia { get; set; }
        public string NomComercial { get; set; }
        public string Direccion { get; set; }
        public string Poblacion { get; set; }
        public string RFC { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string FormatoCtas { get; set; }
        public string ClaveSuper { get; set; }
        public byte[] Logo { get; set; }
        public string Color { get; set; }
        public string ClaveDia { get; set; }
        public string Alias { get; set; }
        public byte[] LogoDistrib { get; set; }
        public string Mantenimiento { get; set; }
        public string DbExtra { get; set; }
        public string CambiarOrdenCajasEmbarcadas { get; set; }
        public string RestringirFacturacion { get; set; }
    }
}
