using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SmartMirror.Extensions;
using System.Threading.Tasks;

namespace SmartMirror.Sonos.Authentication
{
	public class SonosAccessTokenProvider : ISonosAccessTokenProvider
	{
		private const string AuthTokenUrl = "https://api.sonos.com/login/v3/oauth/access";
		private readonly IClock clock;
		private readonly ILogger logger;
		private readonly SonosConfiguration configuration;

		private AccessTokenContainer TokenContainer { get; set; }

		public SonosAccessTokenProvider(IClock clock, ILogger<SonosAccessTokenProvider> logger, IConfiguration configuration)
		{
			this.clock = clock;
			this.logger = logger;
			this.configuration = configuration.GetSection(SonosConfiguration.SectionKey).Get<SonosConfiguration>();
		}

		public async Task<string> GetAccessToken()
		{
			if (this.TokenContainer is null || this.TokenContainer.ExpiresAt < this.clock.Now())
			{
				await this.GetToken();
			}

			return this.TokenContainer.AccessToken;
		}

		private async Task GetToken()
		{
			if (this.TokenContainer is null || string.IsNullOrWhiteSpace(this.TokenContainer.AccessToken))
			{
				await this.GetInitialAccessToken();
				return;
			}

			if (this.TokenContainer.ExpiresAt > this.clock.Now())
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
						grant_type = "authorization_code",
						code = this.configuration.AuthorizationCode,
						redirect_uri = this.configuration.RedirectUri,
					})
					.WithHeader("Authorization", $"Basic {this.configuration.ClientId.Base64Encode()}:{this.configuration.ClientSecret.Base64Encode()}")
					.WithHeader("Content-Type", "application/x-www-form-urlencoded")
					.PostAsync().ReceiveJson<AccessTokenResponse>();
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
						refresh_token = this.TokenContainer.RefreshToken,
						redirect_uri = Url.Encode(this.configuration.RedirectUri),
					})
					.WithHeader("Authorization", this.GetBasicAuthHeader())
					.WithHeader("Content-Type", "application/x-www-form-urlencoded")
					.PostAsync().ReceiveJson<AccessTokenResponse>();
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

		private string GetBasicAuthHeader()
		{
			var clientIdAndSecret = $"{this.configuration.ClientId}:{this.configuration.ClientSecret}";
			return $"Basic {clientIdAndSecret.Base64Encode()}";
		}
	}
}
