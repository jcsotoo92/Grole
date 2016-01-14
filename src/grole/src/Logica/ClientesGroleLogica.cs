using grole.src.Entidades;
using grole.src.Persistencia;
using System.Collections.Generic;
using System;

namespace grole.src.Logica
{
	public class ClientesGroleLogica
	{
		public ClientesGrolePersistencia _ClientesGrolePersistencia { get; set; }
		
		public ClientesGroleLogica(ClientesGrolePersistencia AClientesGrolePersistencia)
		{
			this._ClientesGrolePersistencia = AClientesGrolePersistencia;
		}
		
		//Retorna La Lista de Clientes Grole
		public List<ClienteGrole> ListaClientesGrole()
		{
			return _ClientesGrolePersistencia.ListaClientesGrole();
		}
		//Ingresa Cliente Grole a la Base de Datos
		public ClienteGrole ClientesGroleInsertar(ClienteGrole AClientesGrole)
		{
			return _ClientesGrolePersistencia.ClientesGroleInsertar(AClientesGrole);
		}
		//Modifica Cliente Grole
		public ClienteGrole ClientesGroleModificar(ClienteGrole AClientesGrole)
		{
			return _ClientesGrolePersistencia.ClientesGroleModificar(AClientesGrole);
		}
		//Eliminar Clientes Grole
		public bool ClientesGroleEliminar(int AId, out string AMensajeError)
		{
			return _ClientesGrolePersistencia.ClientesGroleEliminar(AId, out AMensajeError);
		}
		//Filtro Busqueda
		public List<ClienteGrole> busquedaClienteGrole(string AFiltrado)
		{
			return _ClientesGrolePersistencia.busquedaClienteGrole(AFiltrado);
		}
	}	
}