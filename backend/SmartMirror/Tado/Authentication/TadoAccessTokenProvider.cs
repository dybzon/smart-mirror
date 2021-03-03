namespace SmartMirror.Tado.Authentication
{
	using System.Threading.Tasks;
	using Flurl;
	using Flurl.Http;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;

	public class TadoAccessTokenProvider : ITadoAccessTokenProvider
	{
		private const string AuthTokenUrl = "https://auth.tado.com/oauth/token";
		private readonly IClock clock;
		private readonly ILogger logger;
		private readonly TadoConfiguration configuration;

		private AccessTokenContainer TokenContainer { get; set; }

		public TadoAccessTokenProvider(IClock clock, ILogger<TadoAccessTokenProvider> logger, IConfiguration configuration)
		{
			this.clock = clock;
			this.logger = logger;
			this.configuration = configuration.GetSection(TadoConfiguration.SectionKey).Get<TadoConfiguration>();
		}

		public async Task<string> GetAccessToken()
		{
			if(this.TokenContainer is null || this.TokenContainer.ExpiresAt < this.clock.Now())
			{
				await this.GetToken();
			}

			return this.TokenContainer.AccessToken;
		}

		private async Task GetToken()
		{
			if(this.TokenContainer is null || string.IsNullOrWhiteSpace(this.TokenContainer.AccessToken))
			{
				await this.GetInitialAccessToken();
				return;
			}

			if(this.TokenContainer.ExpiresAt > this.clock.Now())
			{
				return;
			}

			await this.RefreshAccessToken();
		}

		private async Task GetInitialAccessToken()
		{
			try
			{
				var initialTokenResponse = await new Url(AuthTokenUrl).SetQueryParams(
					new
					{
						client_id = this.configuration.ClientId,
						grant_type = "password",
						scope = "home.user",
						username = this.configuration.UserName,
						password = this.configuration.Password,
						client_secret = this.configuration.ClientSecret,
					}).PostAsync().ReceiveJson<AccessTokenResponse>();
				this.UpdateTokenFromResponse(initialTokenResponse);
			}
			catch (FlurlHttpException ex)
			{
				this.logger.LogError(ex.Message);
			}
		}

		private async Task RefreshAccessToken()
		{
			try
			{
				var refreshTokenResponse = await new Url(AuthTokenUrl).SetQueryParams(
					new
					{
						grant_type = "refresh_token",
						client_id = this.configuration.ClientId,
						scope = "home.user",
						client_secret = this.configuration.ClientSecret,
						refresh_token = this.TokenContainer.RefreshToken
					}).PostAsync().ReceiveJson<AccessTokenResponse>();
				this.UpdateTokenFromResponse(refreshTokenResponse);
			}
			catch (FlurlHttpException ex)
			{
				this.logger.LogError(ex.Message);
			}
		}

		private void UpdateTokenFromResponse(AccessTokenResponse response)
		{
			var currentTime = this.clock.Now();
			var newToken = new AccessTokenContainer
			{
				AccessToken = response.access_token,
				RefreshToken = response.refresh_token,
				ExpiresAt = currentTime,
			};

			if (int.TryParse(response.expires_in, out var expiresInSeconds))
			{
				newToken.ExpiresAt = currentTime.AddSeconds(expiresInSeconds);
			}

			this.TokenContainer = newToken;
		}
	}
}
