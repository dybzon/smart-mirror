namespace SmartMirror.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using SmartMirror.Tado.Basic;
	using SmartMirror.Tado.Home.Zones;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// A simplified version of the Tado api, exposing only the information necessary to build a smart mirror
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class TadoController : ControllerBase
	{
		private readonly ITadoBasicInfoProvider basicInfoProvider;
		private readonly ITadoZoneProvider zoneProvider;

		public TadoController(ITadoBasicInfoProvider basicInfoProvider, ITadoZoneProvider zoneProvider)
		{
			this.basicInfoProvider = basicInfoProvider;
			this.zoneProvider = zoneProvider;
		}

		[HttpGet("basic")]
		public async Task<TadoBasicInfo> GetBasicInfo()
		{
			return await this.basicInfoProvider.GetBasicInfo();
		}

		[HttpGet("zones")]
		public async Task<IEnumerable<TadoZone>> GetZones()
		{
			var basicInfo = await this.basicInfoProvider.GetBasicInfo();
			var homeId = basicInfo.homes[0].id;
			return await this.zoneProvider.GetZones(homeId);
		}
	}
}
