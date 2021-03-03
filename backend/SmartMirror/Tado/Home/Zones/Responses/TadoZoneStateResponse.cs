namespace SmartMirror.Tado.Home.Zones.Responses
{
	using System;

	public class TadoZoneStateResponse
	{
		public string tadoMode { get; set; }
		public bool geolocationOverride { get; set; }
		public object geolocationOverrideDisableTime { get; set; }
		public object preparation { get; set; }
		public Setting setting { get; set; }
		public object overlayType { get; set; }
		public object overlay { get; set; }
		public object openWindow { get; set; }
		public NextScheduleChange nextScheduleChange { get; set; }
		public NextTimeBlock nextTimeBlock { get; set; }
		public Link link { get; set; }
		public ActivityDataPoints activityDataPoints { get; set; }
		public SensorDataPoints sensorDataPoints { get; set; }
	}

	public class Setting
	{
		public string type { get; set; }
		public string power { get; set; }
		public Temperature temperature { get; set; }
	}

	public class Temperature
	{
		public float celsius { get; set; }
		public float fahrenheit { get; set; }
	}

	public class NextScheduleChange
	{
		public DateTime start { get; set; }
		public Setting setting { get; set; }
	}

	public class NextTimeBlock
	{
		public DateTime start { get; set; }
	}

	public class Link
	{
		public string state { get; set; }
	}

	public class ActivityDataPoints
	{
		public HeatingPower heatingPower { get; set; }
	}

	public class HeatingPower
	{
		public string type { get; set; }
		public float percentage { get; set; }
		public DateTime timestamp { get; set; }
	}

	public class SensorDataPoints
	{
		public InsideTemperature insideTemperature { get; set; }
		public Humidity humidity { get; set; }
	}

	public class InsideTemperature
	{
		public float celsius { get; set; }
		public float fahrenheit { get; set; }
		public DateTime timestamp { get; set; }
		public string type { get; set; }
		public Precision precision { get; set; }
	}

	public class Precision
	{
		public float celsius { get; set; }
		public float fahrenheit { get; set; }
	}

	public class Humidity
	{
		public string type { get; set; }
		public float percentage { get; set; }
		public DateTime timestamp { get; set; }
	}
}
