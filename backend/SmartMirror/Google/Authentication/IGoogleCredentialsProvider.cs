using Google.Apis.Auth.OAuth2;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartMirror.Google.Authentication
{
	public interface IGoogleCredentialsProvider
	{
		Task<IEnumerable<ServiceAccountCredential>> GetCredentials(IEnumerable<AccountConfiguration> accounts);

		Task<ServiceAccountCredential> GetCredential(AccountConfiguration account);
	}
}
