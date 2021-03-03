namespace SmartMirror.Tado.Home.Zones
{
	using Flurl;
	using Flurl.Http;
	using SmartMirror.Tado.Authentication;
	using SmartMirror.Tado.Home.Zones.Responses;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public class TadoZoneFetcher : ITadoZoneFetcher
	{
		private readonly ITadoAccessTokenProvider accessTokenProvider;

		private const string TadoHomesBaseUrl = "https://my.tado.com/api/v2/homes";

		public TadoZoneFetcher(ITadoAccessTokenProvider accessTokenProvider)
		{
			this.accessTokenProvider = accessTokenProvider;
		}

		public async Task<List<TadoZoneResponse>> GetZones(int homeId)
		{
			var zonesRequest = await this.GetZonesRequest(homeId);
			return await zonesRequest.GetJsonAsync<List<TadoZoneResponse>>();
		}

		public async Task<TadoZoneStateResponse> GetZoneState(int homeId, int zoneId)
		{
			var zonesRequest = await this.GetZonesRequest(homeId);
			return await zonesRequest.AppendPathSegment(zoneId).AppendPathSegment("state").GetJsonAsync<TadoZoneStateResponse>();
		}

		private async Task<IFlurlRequest> GetZonesRequest(int homeId)
		{
			var accessToken = await this.accessTokenProvider.GetAccessToken();
			return TadoHomesBaseUrl.AppendPathSegment(homeId).AppendPathSegment("zones").WithHeader("Authorization", $"Bearer {accessToken}");
		}
	}
}
