using System;

namespace SmartMirror.Tado.Home.Zones
{
	public class TadoZone
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public Humidity Humidity { get; set; }

		public Temperature Temperature { get; set; }
	}

	public class Humidity
	{
		public float Percentage { get; set; }
		public DateTime Timestamp { get; set; }
	}

	public class Temperature
	{
		public float Celsius { get; set; }
		public float Fahrenheit { get; set; }
		public DateTime Timestamp { get; set; }
	}
}
