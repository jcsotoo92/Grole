using grole.src.Entidades;
using grole.src.Persistencia;
using System.Collections.Generic;
using System;
using Aspose.Cells;

namespace grole.src.Logica
{
	public class CostosMaquilaLogica
	{
		public CostosMaquilaPersistencia _CostosMaquilaPersistencia {get; set;}
		
		public CostosMaquilaLogica(CostosMaquilaPersistencia ACostosMaquilaPersistencia)
		{
			this._CostosMaquilaPersistencia = ACostosMaquilaPersistencia;
		}
		//Retorna Lista de Costos Maquila
		public List<CostoMaquilaM> ListaCostosMaquila()
		{
			return _CostosMaquilaPersistencia.ListaCostosMaquila();
		}
		//Inserta Una nueva MAquila de tipo M
		public int CostosMaquilaInsertarM(CostoMaquilaM ACostoMaquilaM)
		{
			return _CostosMaquilaPersistencia.insertarCostoMaquilaM(ACostoMaquilaM);
		}
		//Inserta productos de Maquila M en una nueva tabla llamada Maquila D que contiene los Productos
		public bool CostosMaquilaInsertarD(CostoMaquilaD ACostoMaquilaD)
		{
			return _CostosMaquilaPersistencia.insertarCostoMaquilaD(ACostoMaquilaD);
		}
		
		//Obtiene los Productos de la Maquila
		public List<CostoMaquilaD> ObtenerProductos(int Id_Costo)
		{
			return _CostosMaquilaPersistencia.ObtenerProductos(Id_Costo);
		}
		//Elimina la Maquila
		public bool EliminarMaquila(int Id)
		{
			return _CostosMaquilaPersistencia.eliminarMaquila(Id);
		}
		//Elimina Los productos de la Maquila
		public bool EliminarProductosMaquila(int Id)
		{
			return _CostosMaquilaPersistencia.eliminarProductosMaquila(Id);
		}
		//Obtiene Los Costos de la Maquila
		public CostoMaquilaM ObtenerCostosMaquila(int id)
		{
			return _CostosMaquilaPersistencia.ObtenerCostosMaquila(id);
		}
		
		//Modifica Maquila Detalles
		public bool ModificarCostoMaquilaM(CostoMaquilaM MaquilaM)
		{
			return _CostosMaquilaPersistencia.ModificarCostoMaquilaM(MaquilaM);
		}
		
		//Modifica Productos de Maquila
		public bool ModificarCostoMaquilaD(CostoMaquilaD MaquilaD)
		{
			return _CostosMaquilaPersistencia.ModificarCostoMaquilaD(MaquilaD);
		}

        public void exportarExcel()
        {
           
        }
	}
}