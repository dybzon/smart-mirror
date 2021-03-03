using Microsoft.AspNetCore.Mvc;
using SmartMirror.Google.Calendar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartMirror.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GoogleCalendarController : ControllerBase
	{
		private readonly ICalendarEventProvider eventProvider;
		private readonly IClock clock;

		public GoogleCalendarController(ICalendarEventProvider eventProvider, IClock clock)
		{
			this.eventProvider = eventProvider;
			this.clock = clock;
		}

		[HttpGet("events")]
		public async Task<IEnumerable<CalendarEvent>> GetEvents()
		{
			var now = this.clock.Now().Date;
			var maxDate = now.AddDays(2).AddHours(23).AddMinutes(59).AddSeconds(59);
			return await this.eventProvider.GetEvents(now, maxDate);
		}

	}
}
