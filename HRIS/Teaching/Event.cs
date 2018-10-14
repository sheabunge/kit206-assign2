using System;

namespace HRIS.Teaching {
	/// <summary>
	/// Represents an event which takes place in a physical location at a specified time
	/// </summary>
	public class Event {
		/// <summary>
		/// Day when the event takes place
		/// </summary>
		public DayOfWeek Day { get; set; }

		/// <summary>
		/// Time of day when the event begins
		/// </summary>
		public TimeSpan Start { get; set; }

		/// <summary>
		/// Time of day when the event concludes
		/// </summary>
		public TimeSpan End { get; set; }

		/// <summary>
		/// The day during the current week when the event will take place
		/// </summary>
		private DateTime DayThisWeek => DateTime.Today.AddDays(-(int) DateTime.Today.DayOfWeek + (int) Day);

		/// <summary>
		/// The date and time during the current week when the event will begin
		/// </summary>
		public DateTime StartDate => DayThisWeek + Start;

		/// <summary>
		/// The date and time during the current week when the event will conclude
		/// </summary>
		public DateTime EndDate => DayThisWeek + End;
	}
}
