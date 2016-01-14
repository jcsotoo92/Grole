using grole.Models;
using grole.src.Entidades;
using grole.src.Persistencia;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace grole.src.Logica
{
    public class ProductosLogica
    {
        public ProductosPersistencia _ProductosPersistencia { get; set; }
        private CambiosTaraPersistencia _CambiosTaraPersistencia { get; set; }
        private Mail _Mails;
        public ProductosLogica(ProductosPersistencia AProductosPersistencia, CambiosTaraPersistencia _CambiosTaraPersistencia, Mail _Mails)
        {
            this._ProductosPersistencia = AProductosPersistencia;
            this._CambiosTaraPersistencia = _CambiosTaraPersistencia;
            this._Mails = _Mails;
        }

        //Retorna La Lista de Productos
        public List<Producto> ListaProductos()
        {
            return _ProductosPersistencia.ListaProductos();
        }

        //Retorna Producto Especifico
        public Producto ObtenerProducto(string AClave)
        {
            return _ProductosPersistencia.ObtenerProducto(AClave);
        }
        //Retorna bool para Validar Clave, False=No Existe True=Existe 
        public bool ValidarClave(int AClave)
        {
            return _ProductosPersistencia.ValidarClave(AClave);
        }

        //Producto insertar
        public void ProductoInsertar(Producto AProducto)
        {
            _ProductosPersistencia.ProductoInsertar(AProducto);
        }

        //Producto ACTUALIZAR
        public void ProductoActualizar(Producto AProducto)
        {
            _ProductosPersistencia.ProductoActualizar(AProducto);
        }

        //JUAN CARLOS
        public List<Producto> ObtenerProductosValidaciones()
        {
            return _ProductosPersistencia.ObtenerProductosValidaciones();
        }

        public List<Producto> BuscarProductos(string AQuery)
        {
            return _ProductosPersistencia.BuscarProductos(AQuery);
        }

        public Producto ObtenerFechaDeSacrificioDeProducto(string AClave)
        {
            return _ProductosPersistencia.ObtenerFechaDeSacrificioDeProducto(AClave);
        }

        public DateTime CambiarFechaSacrificio(Producto AProducto)
        {
            return _ProductosPersistencia.CambiarFechaSacrificio(AProducto);
        }

        public List<CambioTara> ObtenerListaCambiosTara(string AProducto, string AFechaIni, string AFechaFin)
        {
            return _CambiosTaraPersistencia.ObtenerListaCambiosTara(AProducto, AFechaIni, AFechaFin).OrderBy(x => x.Fecha_Cambio).ToList();
        }

        public List<Producto> ObtenerListaProductosNoInventariables()
        {
            return _ProductosPersistencia.ObtenerListaProductosNoInventariables();
        }

        public List<UsoEmpaque> ObtenerUsoEmpaque(string AFechaIni, string AFechaFin)
        {
            return _ProductosPersistencia.ObtenerUsoEmpaque(AFechaIni, AFechaFin);
        }

        public List<DetalleUsoEmpaque> ObtenerDetalleUsoEmpaque(int AEmpaque, string AFechaIni, string AFechaFin)
        {
            return _ProductosPersistencia.ObtenerDetalleUsoEmpaque(AEmpaque, AFechaIni, AFechaFin);
        }

        public string DameDescripcionProducto(string AProducto)
        {
            return _ProductosPersistencia.DameDescripcionProducto(AProducto);
        }

        public List<Producto> DameListaProductos()
        {
            return _ProductosPersistencia.DameListaProductos();
        }

        public int CambiarTaraProducto(string AProducto, float ATara, string AUsuario)
        {
            Producto pProductoTmp = ObtenerProducto(AProducto);
            int pAffected = _ProductosPersistencia.CambiarTaraProducto(pProductoTmp, ATara, AUsuario);
            _CambiosTaraPersistencia.InsertarCambiosTara(pProductoTmp.Clave, DateTime.Today, pProductoTmp.Pesotara, ATara, AUsuario);

            CorreoCambioTara correo = new CorreoCambioTara();
            correo.Folio = pProductoTmp.Clave;
            correo.Descripcion = pProductoTmp.Descripcion;
            correo.Data = "";
            correo.Accion = "";
            correo.Fecha = DateTime.Today;
            correo.Usuario = AUsuario;

            correo.Accion += "Modificó tara";
            correo.Data += "< div >< span style = \"font-weight: bold;\" > Tara nueva: </ span > ' "+ ATara+" ' </ div >< div >< span style = \"font-weight: bold;\" > Tara anterior: </ span > ' + ' % .2f' %"+ pProductoTmp.Pesotara +" ' </ div >";

            //_Mails.EnviarCorreoCambioProducto(correo);
            
            return pAffected;
        }
    }	
}