namespace SmartMirror.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using SmartMirror.Sonos.Authentication;
	using System.Threading.Tasks;

	/// <summary>
	/// A controller for sonos data.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class SonosController : ControllerBase
	{
		private readonly ISonosAccessTokenProvider accessTokenProvider;

		public SonosController(ISonosAccessTokenProvider accessTokenProvider)
		{
			this.accessTokenProvider = accessTokenProvider;
		}

		[HttpGet("accesstoken")]
		public async Task<string> GetAccessToken()
		{
			return await this.accessTokenProvider.GetAccessToken();
		}
	}
}
