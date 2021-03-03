namespace SmartMirror.Tado.Authentication
{
	using System;

	public class AccessTokenContainer
	{
		public string AccessToken { get; set; }

		public string RefreshToken { get; set; }

		public DateTime ExpiresAt { get; set; }
	}
}
