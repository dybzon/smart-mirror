namespace SmartMirror.Tado.Home
{
	using System;

	public class TadoHome
	{
		public int id { get; set; }
		public string name { get; set; }
		public string dateTimeZone { get; set; }
		public DateTime dateCreated { get; set; }
		public string temperatureUnit { get; set; }
		public string partner { get; set; }
		public bool simpleSmartScheduleEnabled { get; set; }
		public float awayRadiusInMeters { get; set; }
		public bool installationCompleted { get; set; }
		public Incidentdetection incidentDetection { get; set; }
		public bool autoAssistFreeTrialEnabled { get; set; }
		public bool usePreSkillsApps { get; set; }
		public object[] skills { get; set; }
		public bool christmasModeEnabled { get; set; }
		public bool showAutoAssistReminders { get; set; }
		public ContactDetails contactDetails { get; set; }
		public Address address { get; set; }
		public GeoLocation geolocation { get; set; }
		public bool consentGrantSkippable { get; set; }
	}

	public class Incidentdetection
	{
		public bool supported { get; set; }
		public bool enabled { get; set; }
	}


	public class ContactDetails
	{
		public string name { get; set; }
		public string email { get; set; }
		public string phone { get; set; }
	}

	public class Address
	{
		public string addressLine1 { get; set; }
		public object addressLine2 { get; set; }
		public string zipCode { get; set; }
		public string city { get; set; }
		public object state { get; set; }
		public string country { get; set; }
	}

	public class GeoLocation
	{
		public float latitude { get; set; }
		public float longitude { get; set; }
	}
}
