using System;

namespace grole.src.Persistencia
{
	public static class Utilerias
	{
		
		public static string dateTimeToString(DateTime fecha){
			return fecha.ToString("dd/MM/yyyy");
		}
		public static string dateTimeToTimeStamp(DateTime fecha){
			return fecha.ToString("dd.MM.yyyy")+", 00:00:00.000";
		}
	}
}