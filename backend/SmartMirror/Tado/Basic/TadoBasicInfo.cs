namespace SmartMirror.Tado.Basic
{
	public class TadoBasicInfo
	{
		public string name { get; set; }
		public string email { get; set; }
		public string username { get; set; }
		public string id { get; set; }
		public Home[] homes { get; set; }
		public string locale { get; set; }
		public MobileDevice[] mobileDevices { get; set; }
	}

	public class Home
	{
		public int id { get; set; }
		public string name { get; set; }
	}

	public class MobileDevice
	{
		public string name { get; set; }
		public int id { get; set; }
		public Settings settings { get; set; }
		public DeviceMetaData deviceMetadata { get; set; }
	}

	public class Settings
	{
		public bool geoTrackingEnabled { get; set; }
		public bool onDemandLogRetrievalEnabled { get; set; }
		public PushNotifications pushNotifications { get; set; }
	}

	public class PushNotifications
	{
		public bool lowBatteryReminder { get; set; }
		public bool awayModeReminder { get; set; }
		public bool homeModeReminder { get; set; }
		public bool openWindowReminder { get; set; }
		public bool energySavingsReportReminder { get; set; }
		public bool incidentDetection { get; set; }
	}

	public class DeviceMetaData
	{
		public string platform { get; set; }
		public string osVersion { get; set; }
		public string model { get; set; }
		public string locale { get; set; }
	}
}
