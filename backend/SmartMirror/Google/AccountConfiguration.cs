using System.Collections.Generic;

namespace SmartMirror.Google
{
	public class AccountConfiguration
	{
		public IEnumerable<string> CalendarIds { get; set; }

		public string ServiceAccountEmail { get; set; }

		public string CredentialsFileName { get; set; }
	}
}
