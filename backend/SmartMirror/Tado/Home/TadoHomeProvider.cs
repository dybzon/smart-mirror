namespace SmartMirror.Tado.Home
{
	using Flurl;
	using Flurl.Http;
	using System.Threading.Tasks;
	using SmartMirror.Tado.Authentication;

	public class TadoHomeProvider : ITadoHomeProvider
	{
		private const string TadoHomesBaseUrl = "https://my.tado.com/api/v2/homes";
		private readonly ITadoAccessTokenProvider accessTokenProvider;

		public TadoHomeProvider(ITadoAccessTokenProvider accessTokenProvider)
		{
			this.accessTokenProvider = accessTokenProvider;
		}

		public async Task<TadoHome> GetHomeDetails(int homeId)
		{
			var accessToken = await this.accessTokenProvider.GetAccessToken();
			return await TadoHomesBaseUrl.AppendPathSegment(homeId).WithHeader("Authorization", $"Bearer {accessToken}").GetJsonAsync<TadoHome>();
		}
	}
}
