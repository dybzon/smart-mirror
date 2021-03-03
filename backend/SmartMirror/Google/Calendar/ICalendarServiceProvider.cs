using Google.Apis.Calendar.v3;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartMirror.Google.Calendar
{
	public interface ICalendarServiceProvider
	{
		Task<IEnumerable<CalendarService>> GetCalendarServices(IEnumerable<AccountConfiguration> accounts);

		Task<CalendarService> GetCalendarService(AccountConfiguration account);
	}
}
