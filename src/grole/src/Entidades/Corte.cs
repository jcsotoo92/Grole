using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Entidades
{
    public class Corte
    {
        public DateTime? Fecha { get; set; }
        public int Folio { get; set; }
        public int Granja { get; set; }
        public int Lote { get; set; }
        public string Producto { get; set; }
        public int Almacen { get; set; }
        public string Mes { get; set; }
        public string Mercado { get; set; }
        public string Hora { get; set; }
        public int Bascula { get; set; }
        public decimal Peso { get; set; }
        public decimal Tara { get; set; }
        public decimal PesoNeto { get; set; }
        public DateTime FechaCanal { get; set; }
        public int LoteCanal { get; set; }
        public string Almacenado { get; set; }
        public string Embarcado { get; set; }
        public string Ent_Serie { get; set; }
        public int Ent_Folio { get; set; }
        public string Emb_Serie { get; set; }
        public int Emb_Folio { get; set; }
        public DateTime Emb_Fecha { get; set; }
        public string Tipo { get; set; }
        public string Estatus { get; set; }
        public string CodigoBarras { get; set; }
        public int Tarima { get; set; }
        public int LoteEmb { get; set; }
        public int Contenedor { get; set; }
        public DateTime FechaHora { get; set; }
        public string Usuario { get; set; }
        public int Camara { get; set; }
        public DateTime IngresoCamara { get; set; }
        public string Ubicacion { get; set; }
        public int Id_Salida_Parcial { get; set; }
        public int Id { get; set; }
        public int Id_Acum { get; set; }
        public int Id_Salida { get; set; }
        public string Entrada_Aplicada { get; set; }
        public string Salida_Aplicada { get; set; }
        public string Eliminada { get; set; }
        public string Recibida { get; set; }
        public DateTime Fecha_Recibida { get; set; }
        public int Folio_Recepcion { get; set; }
        public DateTime Fecha_Inventario { get; set; }
        public decimal Peso_Real { get; set; }
        public decimal PesoReal { get; set; }
        public DateTime? Fecha_Sacrificio { get; set; }
        public DateTime Fecha_Caducidad { get; set; }
        public int PreOrden { get; set; }
        public int Orden_Produccion { get; set; }
        public int DiasCad { get; set; }
        public int Tipo_Fecha_Sacrificio { get; set; }
        public int Tipo_Fecha_Caducidad { get; set; }
        public DateTime Fecha_SacrificioP { get; set; }
        public DateTime Fecha_CaducidadP { get; set; }
        public int Canastilla { get; set; }
        public string Escaneado { get; set; }
        public DateTime FechaHoraEscaneo { get; set; }
        public string UsuarioEscaneo { get; set; }
        public int Orden_Salida { get; set; }
        public string OCRCode { get; set; }
        public int Consecutivo_Bascula { get; set; }
        public int Semana { get; set; }
    }
}
