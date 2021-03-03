using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMirror.Google.Authentication
{
	public class GoogleCredentialsProvider : IGoogleCredentialsProvider
	{
		private readonly IGoogleServiceAccountProvider serviceAccountProvider;
		private readonly ILogger<GoogleCredentialsProvider> logger;

		public GoogleCredentialsProvider(IGoogleServiceAccountProvider serviceAccountProvider,
            ILogger<GoogleCredentialsProvider> logger)
		{
			this.serviceAccountProvider = serviceAccountProvider;
			this.logger = logger;
		}

		public async Task<IEnumerable<ServiceAccountCredential>> GetCredentials(IEnumerable<AccountConfiguration> accounts)
		{
            return await Task.WhenAll(accounts.Select(this.GetCredential));
		}

        public async Task<ServiceAccountCredential> GetCredential(AccountConfiguration account)
		{
            var serviceAccount = await this.serviceAccountProvider.GetServiceAccount(account);
            return this.GetCredential(account.ServiceAccountEmail, serviceAccount.private_key);
        }

        private ServiceAccountCredential GetCredential(string serviceAccountEmail, string privateKey)
		{
            try
            {
                // Check that the required parameter values are supplied
                if (string.IsNullOrEmpty(privateKey))
                    throw new Exception($"Missing private key for {serviceAccountEmail}.");
                if (string.IsNullOrEmpty(serviceAccountEmail))
                    throw new Exception("ServiceAccountEmail is required.");

                // We only need Read permission to Google Calendar. We will not be creating or altering events.
                string[] scopes = new string[] { CalendarService.Scope.CalendarReadonly };

                // Create new credentials with the given serviceAccountEmail and private key
                var credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(serviceAccountEmail)
                {
                    Scopes = scopes
                }.FromPrivateKey(privateKey));

                return credential;
            }
            catch (Exception ex)
            {
                this.logger.LogError("Creating google service account failed" + ex.Message);
                throw;
            }
        }
    }
}
