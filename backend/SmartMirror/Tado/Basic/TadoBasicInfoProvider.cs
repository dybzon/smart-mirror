namespace SmartMirror.Tado.Basic
{
	using System.Threading.Tasks;
	using Flurl.Http;
	using SmartMirror.Tado.Authentication;

	public class TadoBasicInfoProvider : ITadoBasicInfoProvider
	{
		private const string BasicInfoUrl = "https://my.tado.com/api/v2/me";
		private readonly ITadoAccessTokenProvider accessTokenProvider;

		public TadoBasicInfoProvider(ITadoAccessTokenProvider accessTokenProvider)
		{
			this.accessTokenProvider = accessTokenProvider;
		}

		public async Task<TadoBasicInfo> GetBasicInfo()
		{
			var accessToken = await this.accessTokenProvider.GetAccessToken();
			return await BasicInfoUrl.WithHeader("Authorization", $"Bearer {accessToken}").GetJsonAsync<TadoBasicInfo>();
		}
	}
}
