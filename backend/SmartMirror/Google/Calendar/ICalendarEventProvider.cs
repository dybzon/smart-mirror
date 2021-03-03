namespace SmartMirror.Google.Calendar
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface ICalendarEventProvider
	{
		public Task<IEnumerable<CalendarEvent>> GetEvents(DateTime from, DateTime to);
	}
}
