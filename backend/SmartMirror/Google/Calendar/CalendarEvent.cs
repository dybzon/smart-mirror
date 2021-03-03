namespace SmartMirror.Google.Calendar
{
	using System;
	public class CalendarEvent
	{
		public string Id { get; set; }

		public DateTime? StartTime { get; set; }

		public DateTime? EndTime { get; set; }

		public string StartDate { get; set; }

		public string EndDate { get; set; }

		public string Summary { get; set; }

		public string Description { get; set; }

		public string CalendarId { get; set; }
	}
}
