using System;

namespace HRIS.Teaching {
	public class Event {
		public DayOfWeek Day { get; set; }

		public TimeSpan Start { get; set; }

		public TimeSpan End { get; set; }

		// converts the DayOfWeek Day property into a DateTime object for the current week
		private DateTime DayThisWeek => DateTime.Today.AddDays(-(int) DateTime.Today.DayOfWeek + (int) Day);

		// converts the TimeSpan Start and End properties into a DateTime objects for the current week
		public DateTime StartDate => DayThisWeek + Start;

		public DateTime EndDate => DayThisWeek + End;
	}
}
