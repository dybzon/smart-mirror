namespace SmartMirror.Tado.Home.Zones
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	public class TadoZoneProvider : ITadoZoneProvider
	{
		private readonly ITadoZoneFetcher fetcher;

		public TadoZoneProvider(ITadoZoneFetcher fetcher)
		{
			this.fetcher = fetcher;
		}

		public async Task<IEnumerable<TadoZone>> GetZones(int homeId)
		{
			var zones = await this.fetcher.GetZones(homeId);
			return await Task.WhenAll(zones.Select(async z => {
				var state = await this.fetcher.GetZoneState(homeId, z.id);
				var temperature = state.sensorDataPoints.insideTemperature;
				var humidity = state.sensorDataPoints.humidity;
				return new TadoZone { Id = z.id, Name = z.name, Humidity = new Humidity {Percentage = humidity.percentage, Timestamp = humidity.timestamp },Temperature = new Temperature { Celsius = temperature.celsius, Fahrenheit = temperature.fahrenheit, Timestamp = temperature.timestamp }  };
			}));
		}
	}
}
