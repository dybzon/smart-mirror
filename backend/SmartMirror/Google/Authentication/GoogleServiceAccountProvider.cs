using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SmartMirror.Google.Authentication
{
	public class GoogleServiceAccountProvider : IGoogleServiceAccountProvider
	{
		private Dictionary<string, GoogleServiceAccount> serviceAccounts = new Dictionary<string, GoogleServiceAccount>();

		public async Task<GoogleServiceAccount> GetServiceAccount(AccountConfiguration accountConfiguration)
		{
			if(serviceAccounts.TryGetValue(accountConfiguration.ServiceAccountEmail, out var serviceAccount))
			{
				return serviceAccount;
			}

			using StreamReader r = new StreamReader(accountConfiguration.CredentialsFileName);
			string json = await r.ReadToEndAsync();
			var newServiceAccount = JsonConvert.DeserializeObject<GoogleServiceAccount>(json);
			this.serviceAccounts.Add(accountConfiguration.ServiceAccountEmail, newServiceAccount);
			return newServiceAccount;
		}
	}
}
