using Google.Apis.Calendar.v3;

namespace SmartMirror.Google.Calendar
{
	using Microsoft.Extensions.Configuration;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using GoogleEvents = global::Google.Apis.Calendar.v3.Data.Events;
	using System.Linq;
	using System;

	public class CalendarEventProvider : ICalendarEventProvider
	{
		private readonly GoogleConfiguration configuration;
		private readonly ICalendarServiceProvider calendarServiceProvider;
		private readonly IClock clock;

		public CalendarEventProvider(IConfiguration configuration, ICalendarServiceProvider calendarServiceProvider, IClock clock)
		{
			this.configuration = configuration.GetSection(GoogleConfiguration.SectionKey).Get<GoogleConfiguration>();
			this.calendarServiceProvider = calendarServiceProvider;
			this.clock = clock;
		}

        public async Task<IEnumerable<CalendarEvent>> GetEvents(DateTime from, DateTime to)
        {
            var allEvents = new List<CalendarEvent>();
            foreach(var account in this.configuration.Accounts)
			{
                allEvents.AddRange(await this.GetEventsForCalendar(from, to, account));
			}

            return allEvents.OrderBy(e => e.StartDate).ThenBy(e => e.StartTime);
        }

		private async Task<IEnumerable<CalendarEvent>> GetEventsForCalendar(DateTime from, DateTime to, AccountConfiguration account)
		{
            var calendarService = await this.calendarServiceProvider.GetCalendarService(account);
            var eventRequests = this.BuildEventRequests(calendarService, account, from, to);
            var eventResults = await Task.WhenAll(eventRequests.Select(async e =>
			{
                var events = await e.ExecuteAsync();
                return new { Events = events, e.CalendarId };

            }));
            return eventResults.SelectMany(e => this.ConvertEvents(e.Events, e.CalendarId));
        }

        private IEnumerable<EventsResource.ListRequest> BuildEventRequests(CalendarService calendarService, AccountConfiguration account, DateTime from, DateTime to)
		{
            return account.CalendarIds.Select(calendarId => this.BuildEventRequest(calendarService, calendarId, from, to));
        }

        private EventsResource.ListRequest BuildEventRequest(CalendarService calendarService, string calendarId, DateTime from, DateTime to)
        {
            var request = calendarService.Events.List(calendarId);
            request.TimeMin = this.clock.Now();
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.TimeMin = from;
            request.TimeMax = to;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            return request;
        }

        private IEnumerable<CalendarEvent> ConvertEvents(GoogleEvents events, string calendarId)
		{
            return events.Items != null 
                ? events.Items.Select(i =>
                       new CalendarEvent
                       {
                           Id = i.Id,
                           StartTime = i.Start?.DateTime,
                           EndTime = i.End?.DateTime,
                           Description = i.Description,
                           Summary = i.Summary,
                           StartDate = i.Start?.Date ?? i.Start?.DateTime?.ToString("yyyy-MM-dd"),
                           EndDate = i.End?.Date ?? i.End?.DateTime?.ToString("yyyy-MM-dd"),
                           CalendarId = calendarId,
                       }) 
                : Enumerable.Empty<CalendarEvent>();
        }
	}
}
