using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Configuration;

namespace grole.src.Persistencia
{
	
	public class Conexiones
	{
		
		IConfiguration _Configuration;
		
		public Conexiones(IConfiguration AConfiguracion){
			this._Configuration = AConfiguracion;
		}
		
		public FbConnection ObtenerConexion(){
			var AppSettings       = _Configuration.GetSection("AppSettings");
			var ConnectionString  = AppSettings["Rastro"];
			return new FbConnection(ConnectionString.ToString());
		}
	
	}
	
}