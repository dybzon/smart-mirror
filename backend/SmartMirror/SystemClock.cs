namespace SmartMirror
{
	using System;

	public class SystemClock : IClock
	{
		public DateTime Now()
		{
			return DateTime.Now;
		}
	}
}
