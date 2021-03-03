namespace SmartMirror.Tado.Home.Zones
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface ITadoZoneProvider
	{
		Task<IEnumerable<TadoZone>> GetZones(int homeId);
	}
}
