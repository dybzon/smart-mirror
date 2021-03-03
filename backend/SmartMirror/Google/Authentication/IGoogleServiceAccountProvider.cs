using System.Threading.Tasks;

namespace SmartMirror.Google.Authentication
{
	public interface IGoogleServiceAccountProvider
	{
		public Task<GoogleServiceAccount> GetServiceAccount(AccountConfiguration accountConfiguration);
	}
}
