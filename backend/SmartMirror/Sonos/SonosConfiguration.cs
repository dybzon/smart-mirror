namespace SmartMirror.Sonos
{
	public class SonosConfiguration
	{
		public const string SectionKey = "Sonos";

		public string ClientId { get; set; }

		public string ClientSecret { get; set; }

		public string AuthorizationCode { get; set; }

		public string RedirectUri { get; set; }
	}
}
