namespace SmartMirror.Google
{
	using System.Collections.Generic;

	public class GoogleConfiguration
	{
		public const string SectionKey = "Google";

		public IEnumerable<AccountConfiguration> Accounts { get; set; }
	}
}

