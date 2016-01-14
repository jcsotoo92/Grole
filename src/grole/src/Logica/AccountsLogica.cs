using grole.src.Persistencia;

namespace grole.src.Logica
{
	public class AccountsLogica{
	private	AccountsPersistencia _AccountPersistencia;
		public AccountsLogica (AccountsPersistencia _AccountPersistencia){
			this._AccountPersistencia=_AccountPersistencia;
		}
		
		public int ChecarUsuario(string AUsuario, string APassword){
			return _AccountPersistencia.ChecarUsuario(AUsuario,APassword);
		}
		public int ObtenerIdUsuario(string AUsuario){
			return _AccountPersistencia.ObtenerIdUsuario(AUsuario);
		}
	}	
}