namespace SmartMirror.Tado.Home.Zones
{
	using SmartMirror.Tado.Home.Zones.Responses;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface ITadoZoneFetcher
	{
		Task<List<TadoZoneResponse>> GetZones(int homeId);

		Task<TadoZoneStateResponse> GetZoneState(int homeId, int zoneId);
	}
}
