namespace SmartMirror.Tado.Home.Zones.Responses
{
	using System;

	public class TadoZoneResponse
	{
		public int id { get; set; }
		public string name { get; set; }
		public string type { get; set; }
		public DateTime dateCreated { get; set; }
		public string[] deviceTypes { get; set; }
		public Device[] devices { get; set; }
		public bool reportAvailable { get; set; }
		public bool supportsDazzle { get; set; }
		public bool dazzleEnabled { get; set; }
		public DazzleMode dazzleMode { get; set; }
		public OpenWindowDetection openWindowDetection { get; set; }
	}

	public class DazzleMode
	{
		public bool supported { get; set; }
		public bool enabled { get; set; }
	}

	public class OpenWindowDetection
	{
		public bool supported { get; set; }
		public bool enabled { get; set; }
		public int timeoutInSeconds { get; set; }
	}

	public class Device
	{
		public string deviceType { get; set; }
		public string serialNo { get; set; }
		public string shortSerialNo { get; set; }
		public string currentFwVersion { get; set; }
		public Connectionstate connectionState { get; set; }
		public Characteristics characteristics { get; set; }
		public string batteryState { get; set; }
		public string[] duties { get; set; }
		public bool isDriverConfigured { get; set; }
	}

	public class Connectionstate
	{
		public bool value { get; set; }
		public DateTime timestamp { get; set; }
	}

	public class Characteristics
	{
		public string[] capabilities { get; set; }
	}
}
