namespace SmartMirror.Tado
{
	public class TadoConfiguration
	{
		public const string SectionKey = "Tado";

		public string Password { get; set; }

		public string UserName { get; set; }

		public string ClientId { get; set; }

		public string ClientSecret { get; set; }
	}
}
