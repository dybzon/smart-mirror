using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using SmartMirror.Google.Authentication;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMirror.Google.Calendar
{
	public class CalendarServiceProvider : ICalendarServiceProvider
	{
		private Dictionary<string, CalendarService> CalendarServices = new Dictionary<string, CalendarService>();
		private readonly IGoogleCredentialsProvider credentialsProvider;

		public CalendarServiceProvider(IGoogleCredentialsProvider credentialsProvider)
		{
			this.credentialsProvider = credentialsProvider;
		}

		public async Task<IEnumerable<CalendarService>> GetCalendarServices(IEnumerable<AccountConfiguration> accounts)
		{
			return await Task.WhenAll(accounts.Select(this.GetCalendarService));
		}

		public async Task<CalendarService> GetCalendarService(AccountConfiguration account)
		{
			if(this.CalendarServices.TryGetValue(account.ServiceAccountEmail, out var service))
			{
				return service;
			}

			var credential = await this.credentialsProvider.GetCredential(account);
			var calendarService = new CalendarService(new BaseClientService.Initializer()
			{
				HttpClientInitializer = credential,
				ApplicationName = "Calendar Authentication Sample",
			});

			this.CalendarServices.Add(account.ServiceAccountEmail, calendarService);
			return calendarService;
		}
	}
}
