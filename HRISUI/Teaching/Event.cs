using System;

namespace HRIS.Teaching {
	public class Event {
		
		public DayOfWeek Day { get; set; }

		public TimeSpan Start { get; set; }

		public TimeSpan End { get; set; }
	}
}