using Aspose.Cells;
using grole.src.Entidades;
using grole.src.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grole.src.Logica
{
    public class EmpaquesLogica
    {
        private EmpaquesPersistencia _EmpaquesPersistencia;
        public EmpaquesLogica(EmpaquesPersistencia _EmpaquesPersistencia)
        {
            this._EmpaquesPersistencia = _EmpaquesPersistencia;
        }
        public List<Empaque> ObtenerEmpaques(string AOrderBy)
        {
            return _EmpaquesPersistencia.ObtenerEmpaques(AOrderBy);
        }

        public List<TipoEmpaque> ObtenerListaTiposEmpaques()
        {
            return _EmpaquesPersistencia.ObtenerListaTiposEmpaques();
        }

        public Empaque Insertar(Empaque AEmpaque)
        {
            if(!_EmpaquesPersistencia.ExisteEmpaque(AEmpaque))
                return _EmpaquesPersistencia.InsertarEmpaque(AEmpaque);
            return
                null;
        }

        public Empaque Modificar(Empaque AEmpaque)
        {
            if (!_EmpaquesPersistencia.ExisteEmpaque(AEmpaque))
                return _EmpaquesPersistencia.ModificarEmpaque(AEmpaque);
            return
                null;
        }

        public bool Eliminar(int AClave, out string AMensajeError)
        {
            return _EmpaquesPersistencia.EliminarEmpaque(AClave, out AMensajeError);
        }

        public List<EmpaqueProducto> ObtenerEmpaquesProducto(string AClave)
        {
            return _EmpaquesPersistencia.ObtenerEmpaquesProducto(AClave);
        }

        public bool InsertarEmpaquesProducto(string AIdProducto, int[] Achk, decimal[] Ainp)
        {
            return _EmpaquesPersistencia.InsertarEmpaquesProducto(AIdProducto, Achk, Ainp);
        }
        
        public TipoEmpaque AgregarTipoEmpaque(TipoEmpaque ATipoEmpaque){
            if(!_EmpaquesPersistencia.ExisteTipoEmpaque(ATipoEmpaque.Nombre)){
                return _EmpaquesPersistencia.AgregarTipoEmpaque(ATipoEmpaque);
            }else{
                return null;
            }
        }
        
        public TipoEmpaque ModificarTipoEmpaque(TipoEmpaque ATipoEmpaque){
            if(!_EmpaquesPersistencia.ExisteTipoEmpaque(ATipoEmpaque.Nombre)){
                return _EmpaquesPersistencia.ModificarTipoEmpaque(ATipoEmpaque);
            }else{
                return null;
            }
        }
        
        public bool EliminarTipoEmpaque(int AClave, out string AMensajeError)
        {
            return _EmpaquesPersistencia.EliminarTipoEmpaque(AClave, out AMensajeError);
        }

        public TablaProyeccionProduccion ObtenerTablaProyeccionProduccion(string AFechaIni, string AFechaFin, string ATipo)
        {
            TablaProyeccionProduccion pTabla       = new TablaProyeccionProduccion();
            List<ProyeccionProduccion> pLista      = null, pListaDetalle = null;
            
            if (ATipo.Equals("0"))
            {
                pLista        = _EmpaquesPersistencia.ObtenerProyeccionProduccion(AFechaIni, AFechaFin);
                pListaDetalle = _EmpaquesPersistencia.ObtenerProyeccionProduccionDetalle(AFechaIni, AFechaFin);
            }else if (ATipo.Equals("Nacional"))
            {
                pLista        = _EmpaquesPersistencia.ObtenerProyeccionProduccionNacional(AFechaIni, AFechaFin);
                pListaDetalle = _EmpaquesPersistencia.ObtenerProyeccionProduccionDetalleNacional(AFechaIni, AFechaFin);
            }else if (ATipo.Equals("Exportacion"))
            {
                pLista        = _EmpaquesPersistencia.ObtenerProyeccionProduccionExportacion(AFechaIni, AFechaFin);
                pListaDetalle = _EmpaquesPersistencia.ObtenerProyeccionProduccionDetalleExportacion(AFechaIni, AFechaFin);
            }
            List<FechasProyeccionEmpaques> pFechas = new List<FechasProyeccionEmpaques>();
            DateTime? pFechaTemp = null;
            
            foreach(var item in pListaDetalle)
            {
                if(pFechaTemp != item.Fecha_Embarque)
                {
                    FechasProyeccionEmpaques fecha = new FechasProyeccionEmpaques();
                    fecha.Fecha = item.Fecha_Embarque;
                    fecha.Count = 0;
                    fecha.PIds = null;

                    pFechas.Add(fecha);

                    pFechaTemp = item.Fecha_Embarque;
                }
            }

            foreach(var item in pFechas)
            {
                List<PIds> pIds = ObtenerConteoFecha(item.Fecha, pListaDetalle);
                item.Count = pIds.Count;
                item.PIds = pIds;
            }

            pTabla.Fechas = pFechas;
            pTabla.Lista  = pLista;
            pTabla.ListaDetalle = pListaDetalle;

            return pTabla;
        }

        public List<PIds> ObtenerConteoFecha(DateTime? AFecha, List<ProyeccionProduccion> ALista)
        {
            int pContador = 0;
            int pIdTmp    = 0;

            List<PIds> pResult = new List<PIds>();

            foreach(var item in ALista)
            {
                if(item.Fecha_Embarque == AFecha)
                {
                    if(pIdTmp != item.Id)
                    {
                        pContador++;

                        PIds pR        = new PIds();
                        pR.Id          = item.Id;
                        pR.Descripcion = item.Descripcion2;
                        pResult.Add(pR);

                        pIdTmp = item.Id;
                    }
                }
            }

            return pResult;
        }

        public bool ExportarCatalogoEmpaques(List<Empaque> ALista, string ARutaArchivo)
        {
            Workbook workbook = new Workbook();

            Worksheet worksheet = workbook.Worksheets[0];
            Cells cells = worksheet.Cells;

            Aspose.Cells.Style styleCabecera = workbook.Styles[workbook.Styles.Add()];
            styleCabecera.Font.Name = "Arial";
            styleCabecera.Font.Size = 14;
            styleCabecera.Font.IsBold = true;

            Aspose.Cells.Style styleSubtitulo = workbook.Styles[workbook.Styles.Add()];
            styleSubtitulo.Font.Name = "Arial";
            styleSubtitulo.Font.Size = 12;
            styleSubtitulo.Font.IsBold = true;
            
            cells["A1"].PutValue("NOMBRE");
            cells["B1"].PutValue("TIPO");
            cells["C1"].PutValue("CODIGO SAP");

            cells.SetColumnWidth(0, 25);
            cells.SetColumnWidth(1, 25);
            cells.SetColumnWidth(2, 25);

            Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()];
            style.ForegroundColor = System.Drawing.Color.NavajoWhite;
            style.Pattern = BackgroundType.Solid;
            style.HorizontalAlignment = TextAlignmentType.Right;
            style.Font.IsBold = true;

            Aspose.Cells.Style styleLeft = workbook.Styles[workbook.Styles.Add()];
            styleLeft.ForegroundColor = System.Drawing.Color.NavajoWhite;
            styleLeft.Pattern = BackgroundType.Solid;
            styleLeft.HorizontalAlignment = TextAlignmentType.Left;
            styleLeft.Font.IsBold = true;

            StyleFlag styleFlag = new StyleFlag();
            styleFlag.All = true;

            for (int col = 0; col <= 2; col++)
            {
                cells[0, col].SetStyle(styleLeft);
            }

            int i = 1;
            foreach (var item in ALista)
            {
                cells[i, 0].PutValue(item.Nombre);
                cells[i, 1].PutValue(_EmpaquesPersistencia.ObtenerTipoEmpaque(item.IdTipoEmpaque).Nombre);
                cells[i, 2].PutValue(item.CodigoSAP);

                i++;
            }

            style = workbook.Styles[workbook.Styles.Add()];
            style.ForegroundColor = System.Drawing.Color.WhiteSmoke;
            style.Pattern = BackgroundType.Solid;
            style.Font.IsBold = true;

            workbook.Save(ARutaArchivo, SaveFormat.Xlsx);
            return true;
        }
    }
}
